using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using LuaInterface;

namespace FightNew
{
	public partial class FightMgr : DDOLSingleton<FightMgr>
	{
		/// <summary>
		/// 是否在战斗场景
		/// </summary>
		public bool IsInFightScene = false;

		/// <summary>
		/// 地图管理器
		/// </summary>
		private FieldMgr _filedMgr;

		private Dictionary<string,Slot> _dicSlotNow;

        //播放交换动画的标志位
        public bool PlayRankChange;

        public bool PlayRankUp;
        

        public int RankChangeNums;
        /// <summary>
        /// 事件计数器
        /// </summary>
        public int eventCount;
	

		public bool isPlaying = false;

		public bool IsFighting{set;get;}

		/// <summary>
		/// 正在消除的
		/// </summary>
		public int matching = 0;

		/// <summary>
		/// 正在下落的卡牌个数
		/// </summary>
		public int gravity = 0;

		/// <summary>
		/// 剩余步数
		/// </summary>
		public int movesCount;

		/// <summary>
		/// 剩余时间
		/// </summary>
		public float timeLeft;

		/// <summary>
		/// 正在播放的动画数量
		/// </summary>
		public int animate = 0; // Number of current animation

		/// <summary>
		/// 最后移动的卡牌Id
		/// </summary>
		public int lastMovementId;

		/// <summary>
		/// 每次成功交换后加1
		/// </summary>
		public int swapEvent;
		/// <summary>
		/// 连续消除次数
		/// </summary>
		public int _continueMatchCount;

		public List<Solution> ListSoution;

		/// <summary>
		/// 限制检测协程是否运行完毕
		/// </summary>
		public bool limitationRoutineIsOver = false;

		private ModuleFight _moduleFight;
		public ModuleFight ModuleFight
		{
			get
			{
				if(_moduleFight == null)
				{
					_moduleFight = ModuleMgr.Instance.Get<ModuleFight>();
				}

				return _moduleFight;
			}
		}

		/// <summary>
		/// 是否需要记录第一次伤害
		/// </summary>
		/// <value><c>true</c> if need save first damage; otherwise, <c>false</c>.</value>
		public bool NeedSaveFirstDamage{get;set;}
		/// <summary>
		/// 第一次伤害
		/// </summary>
		private int _firstDamage = 0;


		#region 游戏相关
		/// <summary>
		/// 新生成的特殊消除块
		/// </summary>
		public List<Card> ListNewPowerUp = new List<Card>();

		public Dictionary<int,GameObject> DicSkill;

		private E_MatchCountMsg _enumMatchCount = E_MatchCountMsg.MatchCount_Three;

//		private Dictionary<int,Dragon> _dicBoss;
		private Dictionary<int,PlayerBase> _dicMyBoss;
		private Dictionary<int,PlayerBase> _dicOppBoss;

		private Queue<UILabel> _queueHpLabel = new Queue<UILabel>();
		private Transform _transHpFly;
		public Transform TransHpFlyPos
		{
//			set
//			{
//				_transHpFly = value;
//				while(_queueHpLabel.Count > 0)
//				{
//					Destroy(_queueHpLabel.Dequeue().gameObject);
//				}
//				_queueHpLabel.Clear();
//			}
			get
			{
				
				if(_transHpFly == null)
				{
					_transHpFly = FieldMgr.SlotRoot.Find("Enemy/PiaoXuePanel");
				}

				return _transHpFly;
			}
		}

		private Transform _transMyBossHpFly;
		public Transform TransMyBossHpFlyPos
		{
			//			set
			//			{
			//				_transHpFly = value;
			//				while(_queueHpLabel.Count > 0)
			//				{
			//					Destroy(_queueHpLabel.Dequeue().gameObject);
			//				}
			//				_queueHpLabel.Clear();
			//			}
			get
			{

				if(_transMyBossHpFly == null)
				{
					_transMyBossHpFly = FieldMgr.SlotRoot.Find("BossPos/PiaoXuePanel");
				}

				return _transMyBossHpFly;
			}
		}
		#endregion


		#region 属性
		/// <summary>
		/// 长
		/// </summary>
		/// <value>The width.</value>
		public int Width
		{
			get
			{
				if(_filedMgr != null)
					return _filedMgr.Width;
				else
					return 0;
			}
		}

		/// <summary>
		/// 高
		/// </summary>
		/// <value>The height.</value>
		public int Height
		{
			get
			{
				if(_filedMgr != null)
					return _filedMgr.Height;
				else
					return 0;
			}
		}

		#endregion

		public FightMgr()
		{
			_dicSlotNow = new Dictionary<string, Slot>();
			_filedMgr = new FieldMgr();

			ListSoution = new List<Solution>();
			Combinations.InitCombinationData();
			PowerUps.InitPowerUpData();
			SkillTable.InitSkill();
		}

		// Conditions to start animation
		public bool CanIAnimate ()
		{
			return isPlaying && gravity == 0 && matching == 0;
		}

		// Conditions to start falling chips
		public bool CanIGravity ()
		{
			return isPlaying && ((animate == 0 && matching == 0) || gravity > 0) && IsFighting;
		}

		public bool CanIWait ()
		{
			return isPlaying && animate == 0 && matching == 0 && gravity == 0 && IsFighting;
		}

		public bool CanICheckShuffle()
		{
			return CanIWait() && ListSoution.Count <= 0 && !_isDoSkillToSlot && !IsIceBlcok;
		}

		public int GetMovementID ()
		{
			lastMovementId ++;
			return lastMovementId;
		}

		// Event counter
		public void EventCounter ()
		{
			eventCount++;
		}

		private void OnEnable()
		{
			EventDispatcher.AddListener<object>("SERVER_FIGHT_RESULT", OnFightResultDataCBK);
            EventDispatcher.AddListener<object>("SERVER_RANK_CHANGE", OnRankChangeDataCBK);
            EventDispatcher.AddListener<object>("SERVER_RANK_UP", OnRankUpDataCBK);
        }

		private void OnDisable()
		{
			EventDispatcher.RemoveListener<object>("SERVER_FIGHT_RESULT", OnFightResultDataCBK);
            EventDispatcher.RemoveListener<object>("SERVER_RANK_CHANGE", OnRankChangeDataCBK);
            EventDispatcher.RemoveListener<object>("SERVER_RANK_UP", OnRankUpDataCBK);
        }

		void Awake()
		{
//			_dicBoss = new Dictionary<int, Dragon>();
			_dicMyBoss = new Dictionary<int, PlayerBase>();
			_dicOppBoss = new Dictionary<int, PlayerBase>();
            PlayRankChange = false;
            PlayRankUp = false;
        }

		// Update is called once per frame
		void Update () 
		{
//			if(Input.GetKeyDown(KeyCode.A))
//			{
////				PlaySkillEffectTest();
//
//				_FlyBlockToMyCnt = 3;
//				DoFlyBlockToMyself();
//			}

			#if !FightTest
			if(Time.timeScale < 1f)
			{
				Time.timeScale += Time.deltaTime * 0.3f;
			}
			#endif
//			DebugUtil.Debug("<color=green>update begin </color>");
			if(!isPlaying || !IsFighting || _isDoSkillToSlot || IsIceBlcok) return;


//			if(Input.GetKeyDown(KeyCode.A))
//				TriggerSkill(1007);

			//bool hasSoultion = false;
//			string date = string.Empty;
			if(ListSoution.Count > 0)
			{
				_continueMatchCount += ListSoution.Count;
				//有可消除的
//				date = System.DateTime.Now.ToString("HH:mm:ss fffffff");
//				Debug.Log("match soultion begin:" + date);
				MatchSolutions();
//				date = System.DateTime.Now.ToString("HH:mm:ss fffffff");
//				Debug.Log("match soultion end:" + date);
				ListSoution.Clear();
				//hasSoultion = true;
			}

//			if(hasSoultion)
//			{
//				date = System.DateTime.Now.ToString("HH:mm:ss fffffff");
//				Debug.Log("other begin:" + date);
//			}
			bool hasNewGenerator = false;
			for(int i = 0; i < AllSlotGenerator.Count;i++)
			{
				if(AllSlotGenerator[i].OnUpdate())
				{
					hasNewGenerator = true;
				}
			}

			bool hasNewGravity = false;
			for(int i = 0;i < AllSlotGravity.Count;i++)
			{
				if(AllSlotGravity[i].GravityReaction())
					hasNewGravity = true;
			}

			bool hasTeleport = false;
			for(int i = 0; i < AllSlotTeleport.Count;i++)
			{
				if(AllSlotTeleport[i].OnUpdate())
					hasTeleport = true;
			}

			if(!hasNewGravity && !hasNewGenerator && !hasTeleport 
				&& _continueMatchCount > 0 && CanIWait())
			{
				bool hasNoCardIsDrop = true;
				Card card;
				for(int i = 0; i < _allSlot.Count;i++)
				{
					card = _allSlot[i].GetChip();
					if(card != null && card.IsDrop)
					{
						hasNoCardIsDrop = false;
						break;
					}
				}

				if(hasNoCardIsDrop)
				{
					//弹提示
					if(_continueMatchCount != 0)
					{
						DebugUtil.Info("连续消除：" + _continueMatchCount);

						if(_continueMatchCount >= 3)
						{
							switch(_continueMatchCount)
							{
								case 3:
								case 4:
									_enumMatchCount = E_MatchCountMsg.MatchCount_Three;
									break;
								case 5:
								case 6:
									_enumMatchCount = E_MatchCountMsg.MatchCount_Five;
									break;
								case 7:
								case 8:
									_enumMatchCount = E_MatchCountMsg.MatchCount_Seven;
									break;
								case 9:
								case 10:
									_enumMatchCount = E_MatchCountMsg.MatchCount_Nine;
									break;
								default:
									_enumMatchCount = E_MatchCountMsg.MatchCount_Eleven;
									break;
							}

							MatchCountMgr.Instance.ShowMsg(_enumMatchCount);
						}

						#if !FightTest
//						if(_continueMatchCount > 1)
						ModuleFight.NormalAttack(_continueMatchCount,_firstDamage);
						_firstDamage = 0;
						#endif
						_continueMatchCount = 0;
					}

					//杂草增长
					WeedGrow();

//					if(hasSoultion)
//					{
//						date = System.DateTime.Now.ToString("HH:mm:ss fffffff");
//						Debug.Log("other end:" + date);
//					}

					//是否出步数没有的遮罩
					if(movesCount <= 0)
					{
						(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).CheckToShowMoveMask();
					}


					//新生成的
					if(ListNewPowerUp.Count > 0)
					{
						EventDispatcher.TriggerEvent<Slot,E_CardType>(FightDefine.Event_HasNewBomb, ListNewPowerUp[0].Slot, ListNewPowerUp[0].CardType);
						ListNewPowerUp.Clear();
					}

					if(_FlyBlockToMyCnt > 0)
					{
						//有技能释放
						DoFlyBlockToMyself();
					}
				}
			}
			else if(_FlyBlockToMyCnt > 0 && !hasNewGravity && !hasNewGenerator && !hasTeleport && CanIWait())
			{
				bool hasNoCardIsDrop = true;
				Card card;
				for(int i = 0; i < _allSlot.Count;i++)
				{
					card = _allSlot[i].GetChip();
					if(card != null && card.IsDrop)
					{
						hasNoCardIsDrop = false;
						break;
					}
				}

				if(hasNoCardIsDrop)
				{
					DoFlyBlockToMyself();
				}
			}
		}

		#region 加载资源
		/// <summary>
		/// 加载资源
		/// </summary>
		private Dictionary<string,object> _dicResTemp = new Dictionary<string, object>();
		public GameObject LoadAndInstantiate(string assetName)
		{
			#if !FightTest
			return ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(assetName);
			#else
			if(!_dicResTemp.ContainsKey(assetName))
			{
				GameObject prefab = Resources.Load<GameObject>(assetName);
				_dicResTemp.Add(assetName,prefab);
			}

			return Instantiate<GameObject>(_dicResTemp[assetName] as GameObject);
			#endif
		}
		#endregion

		#region Slot相关
		/// <summary>
		/// Slot添加到字典
		/// </summary>
		/// <param name="slot">Slot.</param>
		public void AddSlot(Slot slot)
		{
			if(_dicSlotNow.ContainsKey(slot.key))
			{
				_dicSlotNow[slot.key] = slot;
			}
			else
			{
				_dicSlotNow.Add(slot.key,slot);
			}
		}

		/// <summary>
		/// 查找Slot
		/// </summary>
		/// <returns>The slot.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		public Slot FindSlot(int row,int col)
		{
			string key = string.Format(FightDefine.Format_SlotKey,row,col);
			return FindSlot(key);
		}

		/// <summary>
		/// 查找Slot
		/// </summary>
		/// <returns>The slot.</returns>
		/// <param name="key">Key.</param>
		public Slot FindSlot(string key)
		{
			if(_dicSlotNow != null && _dicSlotNow.ContainsKey(key))
				return _dicSlotNow[key];

			return null;
		}

		/// <summary>
		/// 查邻近的Slot
		/// </summary>
		/// <returns>The near slot.</returns>
		/// <param name="point">Point.</param>
		/// <param name="side">Side.</param>
		public Slot FindNearSlot(Point point,Side side) 
		{
			string key = string.Format(FightDefine.Format_SlotKey,(point.X + Utils.SideOffsetX(side)),(point.Y + Utils.SideOffsetY(side)));
			return FindSlot(key);
		}


		private List<Slot> _allSlot;
		/// <summary>
		/// 获取所有的Slot
		/// </summary>
		/// <returns>The slots list.</returns>
		public List<Slot> GetSlotsList()
		{
			if(_dicSlotNow == null)
				return new List<Slot>();
			else if(_allSlot == null)
				_allSlot = new List<Slot>(_dicSlotNow.Values);

			return _allSlot;
		}

		private List<SlotGenerator> _allSlotGenerator;
		public List<SlotGenerator> AllSlotGenerator
		{
			set
			{
				_allSlotGenerator = value;
			}
			get
			{
				if(_allSlotGenerator == null)
					_allSlotGenerator = new List<SlotGenerator>();

				return _allSlotGenerator;
			}
		}

		private List<SlotTeleport> _allSlotTeleport;
		public List<SlotTeleport> AllSlotTeleport
		{
			set
			{
				_allSlotTeleport = value;
			}
			get
			{
				if(_allSlotTeleport == null)
					_allSlotTeleport = new List<SlotTeleport>();

				return _allSlotTeleport;
			}
		}

		private List<SlotGravity> _allSlotGravity;
		public List<SlotGravity> AllSlotGravity
		{
			set
			{
				_allSlotGravity = value;
			}
			get
			{
				if(_allSlotGravity == null)
					_allSlotGravity = new List<SlotGravity>();

				return _allSlotGravity;
			}
		}


		#endregion

		#region 交换
		public bool Swap(Slot slot, Side side) 
		{
			if (slot == null || slot[side] == null) return false;

			Card cardFrom = slot.GetChip();
			Card cardTo = slot[side].GetChip();
			if(cardFrom == null || cardTo == null) return false;

			AnimationMgr.Instance.SwapTwoItem(cardFrom, cardTo, false);

			return true;
		}
		#endregion

		#region 消除卡牌逻辑
		/// <summary>
		/// 消除卡牌
		/// </summary>
		/// <param name="solutions">Solutions.</param>
		void MatchSolutions() 
		{
			AnimationMgr.Instance.Iteraction = false;

			if (!isPlaying || !FightMgr.Instance.IsFighting) return;

			ListSoution.Sort(delegate(Solution x, Solution y) 
				{
					if (x.potential == y.potential)
						return 0;
					else if (x.potential > y.potential)
						return -1;
					else
						return 1;
				});

			int width = Width;
			int height = Height;

			bool[,] mask = new bool[width,height];
			//string key;
			Slot slot;

			//把卡牌标记为true
			for (int x = 0; x < width; x++) 
			{
				for (int y = 0; y < height; y++) 
				{
					mask[x, y] = false;

					slot = FindSlot(x,y);
					if (slot != null && slot.GetChip()) 
					{
						mask[x, y] = true;
					}
				}
			}

			List<Solution> final_solutions = new List<Solution>();

			bool breaker;
			Solution s = null;
			Card c = null;
			for(int solutionIndex = 0;solutionIndex < ListSoution.Count;solutionIndex++)
			{
				s = ListSoution[solutionIndex];

				//筛选solution
				breaker = false;
				for(int cardIndex = 0;cardIndex < s.chips.Count;cardIndex++)
					//				foreach (Chip c in s.chips) 
				{
					c = s.chips[cardIndex];

					if (c == null || c.Slot == null || !mask[c.Slot.Point.X, c.Slot.Point.Y]) 
					{
						breaker = true;
						break;
					}
				}

				if (breaker)
					continue;

				//最终的解决方案
				final_solutions.Add(s);

				//锁定
				//foreach (Chip c in s.chips)
				for(int cardIndex = 0;cardIndex < s.chips.Count;cardIndex++)
				{
					c = s.chips[cardIndex];

					mask[c.Slot.Point.X, c.Slot.Point.Y] = false;
				}
			}

			Solution solution = null;
			Card chip;
			//foreach (Solution solution in final_solutions) 
			//{
			for(int solutionIndex = 0;solutionIndex < final_solutions.Count;solutionIndex++)
			{
				solution = final_solutions[solutionIndex];

				EventCounter();

				//				Jelly jelly;
				int puID = -1;

				//消除的个数
				int matchCardFinalCount = 0;
				for(int cardIndex = 0; cardIndex < solution.chips.Count;cardIndex++)
				{
					chip = solution.chips[cardIndex];

					if ((int)chip.CardType == solution.id)// || chip.id == 10) 
					{
						if (!chip.Slot)
							continue;

						slot = chip.Slot;

						if (!chip.IsMatcheble())
							break;

						matchCardFinalCount++;

						if (chip.movementID > puID)
							puID = chip.movementID;

						//						chip.SetScore(Mathf.Pow(2, solution.count - 3) / solution.count);
						if (!slot.Block)
							_filedMgr.BlockCrush(slot.Point.X, slot.Point.Y, true);

						chip.DestroyChip();
						//						jelly = slot.GetJelly();
						//						if (jelly)
						//							jelly.JellyCrush();
					}
				}

				solution.chips.Sort(delegate(Card a, Card b) 
					{
						return a.movementID > b.movementID ? -1 : a.movementID == b.movementID ? 0 : 1;
					});

				//第一关新手引导不出现合并
				if(Fight.LevelProfile.NewCurrentLevelProfile.Level != 1)
				{
					breaker = false;
					Combinations combination;
					for (int i = 0; i < Combinations.combinations.Count;i++) 
					{
						combination = Combinations.combinations[i];

						if (combination.square && !solution.q)
							continue;

						if (!combination.square) 
						{
							if (combination.horizontal && !solution.h)
							//	|| (!combination.horizontal && solution.h))
								continue;

							if (combination.vertical && !solution.v)
							//	|| (!combination.vertical && solution.v))
								continue;

							if(combination.horizontal && combination.vertical)
							{
								if(combination.minCount > solution.count)
									continue;
							}
							else
							{
								if (combination.horizontal && combination.minCount > solution.hCount)
									continue;

								if (combination.vertical && combination.minCount > solution.vCount)
									continue;
							}
						}

						//					// For additional logic
						//					switch (combination.tag) 
						//					{
						//						case "Tag1":
						//							Debug.Log("Tag 1 combination..."); break;
						//						case "Tag2":
						//							Debug.Log("Tag 2 combination..."); break;
						//						case "Tag3":
						//							Debug.Log("Tag 3 combination..."); break;
						//					}
						//
						Card ch;
						for(int cardIndex = 0;cardIndex < solution.chips.Count;cardIndex++)
						{
							ch = solution.chips[cardIndex];

							if (ch.CardType == E_CardType.Fight_SimpleCard1
								|| ch.CardType == E_CardType.Fight_SimpleCard2
								|| ch.CardType == E_CardType.Fight_SimpleCard3
								|| ch.CardType == E_CardType.Fight_SimpleCard4
								|| ch.CardType == E_CardType.Fight_SimpleCard5
								|| ch.CardType == E_CardType.Fight_SimpleCard6) 
							{
								_filedMgr.GetNewBomb(ch.LastSlot.Point.X, ch.LastSlot.Point.Y, combination.powerup, ch.LastSlot.transform.position);
								breaker = true;
								break;
							}
						}

						if (breaker)
							break;
					}
				}

				#if !FightTest
				if(NeedSaveFirstDamage)
				{
					_firstDamage = ModuleFight.GetFirstRandomAttack(matchCardFinalCount);
					NeedSaveFirstDamage = false;
				}
				else
				{
					ModuleFight.NormalRandomAttack(matchCardFinalCount,_firstDamage);
					_firstDamage = 0;
				}
				#endif
			}

			ListSoution.Clear();

			AnimationMgr.Instance.Iteraction = true;
		}
		#endregion

		#region 出生点生成新的卡牌
		// Creating a simple random color chips
		public Card GetNewSimpleChip (int x, int y,Vector3 offset)
		{
			Card card = _filedMgr.GetNewSimpleChip(x, y);
			card.transform.localPosition += offset;
			card.IsDrop = true;
			return card;
		}

		public Card GetNewStone (int x, int y,Vector3 offset) 
		{
			Card card = _filedMgr.GetNewStone(x,y);
			card.transform.localPosition += offset;
			card.IsDrop = true;
			return card;
		}
		#endregion

		#region 杂草增长
		private int _lastSwapEventForSweed;
		private void WeedGrow()
		{
			if(Weed.all.Count > 0 && swapEvent > _lastSwapEventForSweed) 
			{
				_lastSwapEventForSweed = swapEvent;
				if (Weed.lastWeedCrush < swapEvent) 
				{
					Weed.seed = 1;
					Weed.lastWeedCrush = swapEvent;
				}

				Weed.Grow();
			}
		}
		#endregion

		#region 自动洗牌
		// Coroutine of call mixing chips in the absence of moves
		private int shuffleOrder = -1;
		IEnumerator ShuffleRoutine () 
		{
			yield return 0;

			shuffleOrder = -1;
			float delay = 1;
			while (true) 
			{
				if(!isPlaying || _isDoSkillToSlot || IsIceBlcok)
				{
					yield return new WaitForSeconds(0.5f);
					continue;
				}

				yield return StartCoroutine(Utils.WaitFor(CanICheckShuffle, delay));

				if(eventCount > shuffleOrder)
				{
					List<Card> chips = new List<Card>(FindObjectsOfType<Card>());

					if (eventCount > shuffleOrder && chips.Find(x => (x.move || x.destroying || x.IsDrop)) == null) {
						shuffleOrder = eventCount;
						yield return StartCoroutine(Shuffle(false));
					}
				}
			}
		}
			
		public IEnumerator Shuffle (bool f) 
		{
			if(!CanICheckShuffle())
			{
				shuffleOrder--;
				yield break;
			}
			
			bool force = f;

			List<Move> moves = FindMoves();
			if (moves.Count > 0 && !force)
				yield break;
			if (!isPlaying || !IsFighting || _isDoSkillToSlot || IsIceBlcok)
				yield break;

			isPlaying = false;


			List<Slot> slots = GetSlotsList();

			Dictionary<Slot, Vector3> positions = new Dictionary<Slot, Vector3> ();
			for (int index = 0;index < slots.Count;index++)
			{
				positions.Add(slots[index], slots[index].transform.position);
			}

			float t = 0;
			while (t < 1) {
				t += Time.unscaledDeltaTime * 3;
				FieldMgr.SlotRoot.transform.localScale = Vector3.one * Mathf.Lerp(1, 0.6f, EasingFunctions.easeInOutQuad(t));
				FieldMgr.SlotRoot.transform.eulerAngles = Vector3.forward * Mathf.Lerp(0, Mathf.Sin(Time.unscaledTime * 40) * 3, EasingFunctions.easeInOutQuad(t));

				yield return 0;
			}


			if (f || moves.Count == 0) {
				f = false;

				#if FightTest
				Debug.Break();
				#endif

				RawShuffle(slots);
			}

			moves = FindMoves();
			List<Solution> solutions = FindSolutions ();

			int itrn = 0;
			int targetID;
			if(solutions.Count > 0 || moves.Count == 0)
			{
				while (solutions.Count > 0 || moves.Count == 0) 
				{
					if (itrn > 100) {
						//ShowLosePopup();
						FieldMgr.SlotRoot.transform.localScale = Vector3.one;
						FieldMgr.SlotRoot.transform.eulerAngles = Vector3.zero;
						FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoShuffle);
						yield break;
					}
					if (solutions.Count > 0) 
					{
						for (int s = 0; s < solutions.Count; s++) 
						{
							targetID = Random.Range(0, slots.Count - 1);
							if (slots[targetID].GetChip() && slots[targetID].GetChip().CardType.ToString() != "SugarChip" && (int)slots[targetID].GetChip().CardType != solutions[s].id)
								AnimationMgr.Instance.SwapTwoItemNow(solutions[s].chips[Random.Range(0, solutions[s].chips.Count - 1)], slots[targetID].GetChip());
						}
					} 
					else
					{
						RawShuffle(slots);
					}

					moves = FindMoves();
					solutions = FindSolutions();
					itrn++;
					FieldMgr.SlotRoot.eulerAngles = Vector3.forward * Mathf.Sin(Time.unscaledTime * 40) * 3;

					yield return 0;
				}
			}
			//			else
			//			{
			//				_goSlotCreate.transform.eulerAngles = Vector3.forward * Mathf.Sin(Time.unscaledTime * 40) * 3;
			//				yield return 0;
			//			}

			t = 0;
			//			AudioAssistant.Shot("Shuffle");
			while (t < 1) {
				t += Time.unscaledDeltaTime * 3;
				FieldMgr.SlotRoot.transform.localScale = Vector3.one * Mathf.Lerp(0.6f, 1, EasingFunctions.easeInOutQuad(t));
				FieldMgr.SlotRoot.transform.eulerAngles = Vector3.forward * Mathf.Lerp(Mathf.Sin(Time.unscaledTime * 40) * 3, 0, EasingFunctions.easeInOutQuad(t));
				yield return 0;
			}

			FieldMgr.SlotRoot.transform.localScale = Vector3.one;
			FieldMgr.SlotRoot.transform.eulerAngles = Vector3.zero;

			isPlaying = true;
		}

		void RawShuffle(List<Slot> slots) 
		{
			EventCounter();
			int targetID;
			for (int j = 0; j < slots.Count; j++) 
			{
				targetID = Random.Range(0, j - 1);
				if (!slots[j].GetChip() || !slots[targetID].GetChip())
					continue;
//				if (slots[j].GetChip().chipType == "SugarChip" || slots[targetID].GetChip().chipType == "SugarChip")
//					continue;
				AnimationMgr.Instance.SwapTwoItemNow(slots[j].GetChip(), slots[targetID].GetChip());
			}
		}

		public List<Move> FindMoves ()
		{
			List<Move> moves = new List<Move>();
			//if (!FieldAssistant.Instance.gameObject.activeSelf) return moves;
			if (Fight.LevelProfile.NewCurrentLevelProfile == null) return moves;

			int x;
			int y;	
			int width = FightMgr.Instance.Width;
			int height = FightMgr.Instance.Height;
			Move move;
			Solution solution;
			int potential;
			Slot slot;
			string chipTypeA = "";
			string chipTypeB = "";

//			#if FightTest
//			for(x = 0;x < width;x++)
//				for(y = 0;y < height;y++)
//					Debug.Log(string.Format("{0}_{1}:{2}",x,y,FieldMgr.field.chips[x,y]));
//			#endif
						
			// horizontal
			for (x = 0; x < width; x++)
				for (y = 0; y < height; y++) 
				{
					//特殊卡牌
					slot = FightMgr.Instance.FindSlot(x,y);
					if(slot != null && slot.Card != null && slot.Card.IsLineCard)
					{
						move = new Move();
						move.solution = new Solution();
						move.solution.potential = 10;
						move.solution.chips.Add(slot.Card);
						moves.Add(move);
						return moves;
					}
					else
					{
						if(x == width - 1) continue;
						if (!FieldMgr.field.slots[x,y]) continue;
						if (!FieldMgr.field.slots[x+1,y]) continue;
						if (FieldMgr.field.blocks[x,y] > 0) continue;
						if (FieldMgr.field.blocks[x+1,y] > 0) continue;
						if (FieldMgr.field.chips[x,y] == FieldMgr.field.chips[x+1,y]) continue;
						if (FieldMgr.field.chips[x,y] == -1 && FieldMgr.field.chips[x+1,y] == -1) continue;
						if (FieldMgr.field.wallsV[x,y]) continue;
						move = new Move();
						move.fromX = x;
						move.fromY = y;
						move.toX = x + 1;
						move.toY = y;
						AnalizSwap(move);

						Solution solutionA = null;
						Solution solutionB = null;

						slot = FightMgr.Instance.FindSlot(move.fromX, move.fromY);
						chipTypeA = slot.GetChip() == null ? "SimpleChip" : slot.GetChip().CardType.ToString();

						potential = 0;

						solution = slot.MatchAnaliz();
						if (solution != null) {
							solutionA = solution;
							potential = solution.potential;
						}

						//					solution = slot.MatchSquareAnaliz();
						//					if (solution != null && potential < solution.potential) {
						//						solutionA = solution;
						//						potential = solution.potential;
						//					}

						move.potencial += potential;

						slot = FightMgr.Instance.FindSlot(move.toX, move.toY);
						chipTypeB = slot.GetChip() == null ? "SimpleChip" : slot.GetChip().CardType.ToString();

						potential = 0;
						solution = slot.MatchAnaliz();
						if (solution != null) {
							solutionB = solution;
							potential = solution.potential;
						}

						//					solution = slot.MatchSquareAnaliz();
						//					if (solution != null && potential < solution.potential) {
						//						solutionB = solution;
						//						potential = solution.potential;
						//					}

						move.potencial += potential;

						if (solutionA != null && solutionB != null)
							move.solution = solutionA.potential >= solutionB.potential ? solutionA : solutionB;
						else
							move.solution = solutionA != null ? solutionA : solutionB;

						AnalizSwap(move);
	//					if (Mix.ContainsThisMix(chipTypeA, chipTypeB))
	//						move.potencial += 100;
						if (move.potencial > 0)// || (chipTypeA != "SimpleChip" &&  chipTypeB != "SimpleChip")) 
							moves.Add(move);	
					}
				}

			// vertical
			for (x = 0; x < width; x++)
				for (y = 0; y < height - 1; y++) {
					if (!FieldMgr.field.slots[x,y]) continue;
					if (!FieldMgr.field.slots[x,y+1]) continue;
					if (FieldMgr.field.blocks[x,y] > 0) continue;
					if (FieldMgr.field.blocks[x,y+1] > 0) continue;
					if (FieldMgr.field.chips[x,y] == FieldMgr.field.chips[x,y+1]) continue;
					if (FieldMgr.field.chips[x,y] == -1 && FieldMgr.field.chips[x,y+1] == -1) continue;
					if (FieldMgr.field.wallsH[x,y]) continue;
					move = new Move();
					move.fromX = x;
					move.fromY = y;
					move.toX = x;
					move.toY = y + 1;

					AnalizSwap(move);

					Solution solutionA = null;
					Solution solutionB = null;

					slot = FightMgr.Instance.FindSlot(move.fromX, move.fromY);
					chipTypeA = slot.GetChip() == null ? "SimpleChip" : slot.GetChip().CardType.ToString();

					potential = 0;

					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionA = solution;
						potential = solution.potential;
					}

					//					solution = slot.MatchSquareAnaliz();
					//					if (solution != null && potential < solution.potential) {
					//						solutionA = solution;
					//						potential = solution.potential;
					//					}

					move.potencial += potential;

					slot = FightMgr.Instance.FindSlot(move.toX, move.toY);
					chipTypeB = slot.GetChip() == null ? "SimpleChip" : slot.GetChip().CardType.ToString();

					potential = 0;
					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionB = solution;
						potential = solution.potential;
					}

					//					solution = slot.MatchSquareAnaliz();
					//					if (solution != null && potential < solution.potential) {
					//						solutionB = solution;
					//						potential = solution.potential;
					//					}

					move.potencial += potential;

					if (solutionA != null && solutionB != null)
						move.solution = solutionA.potential >= solutionB.potential ? solutionA : solutionB;
					else
						move.solution = solutionA != null ? solutionA : solutionB;

					AnalizSwap(move);
//					if (Mix.ContainsThisMix(chipTypeA, chipTypeB))
//						move.potencial += 100;
					if (move.potencial > 0)// || (chipTypeA != "SimpleChip" &&  chipTypeB != "SimpleChip")) 
						moves.Add(move);		
				}

			return moves;
		}

		// change places 2 chips in accordance with the move for the analysis of the potential of this move
		void  AnalizSwap (Move move)
		{
			Slot slot;
			Card fChip = GameObject.Find(string.Format(FightDefine.Format_SlotKey,move.fromX,move.fromY)).GetComponent<Slot>().GetChip();
			Card tChip = GameObject.Find(string.Format(FightDefine.Format_SlotKey,move.toX,move.toY)).GetComponent<Slot>().GetChip();
			if (!fChip || !tChip) return;
			slot = tChip.Slot;
			fChip.Slot.SetChip(tChip);
			slot.SetChip(fChip);
		}

		// Class with information of move
		public class Move 
		{
			//
			// A -> B
			//

			// position of start chip (A)
			public int fromX;
			public int fromY;
			// position of target chip (B)
			public int toX;
			public int toY;

			public Solution solution; // solution of this move
			public int potencial; // potential of this move
		}

		// Function of searching possible solutions
		List<Solution> FindSolutions() 
		{
			List<Solution> solutions = new List<Solution> ();
			Solution zsolution;
			for(int i = 0;i < _allSlot.Count;i++)
			{
				zsolution = _allSlot[i].MatchAnaliz();
				if (zsolution != null) solutions.Add(zsolution);
				//zsolution = slot.MatchSquareAnaliz();
				//if (zsolution != null) solutions.Add(zsolution);
			}

			return solutions;
		}
		#endregion

		#region 提示
		// Coroutine of showing hints
		IEnumerator ShowingHintRoutine () 
		{
			yield return 0;

			int hintOrder = -1;
			float delay = 5;

			yield return new WaitForSeconds(1f);

			while (!limitationRoutineIsOver) 
			{
				while (!isPlaying || !IsFighting || _isDoSkillToSlot || IsIceBlcok)
					yield return 0;

				yield return StartCoroutine(Utils.WaitFor(CanIWait, delay));
				if (eventCount > hintOrder) 
				{
					hintOrder = eventCount;
					ShowHint();
				}
			}
		}

		// Showing random hint
		void  ShowHint ()
		{
			if (!isPlaying || !IsFighting || _isDoSkillToSlot || IsIceBlcok) return;
			List<Move> moves = FindMoves();

			//			foreach (Move move in moves) 
			//			{
			//				Debug.DrawLine(Slot.GetSlot(move.fromX, move.fromY).transform.position, Slot.GetSlot(move.toX, move.toY).transform.position, Color.red, 10);
			//			}

			if (moves.Count == 0) return;

			//yangzj：找最优解
			//			Move bestMove = moves[ Random.Range(0, moves.Count) ];
			Move bestMove = null;
			int potential = 0;
			for(int i = 0; i < moves.Count;i++)
			{
				if(moves[i].solution != null && moves[i].solution.potential > potential)
					bestMove = moves[i];
			}

			if (bestMove.solution == null) return;

			Card chip;
			for(int i = 0;i < bestMove.solution.chips.Count;i++)
			{
				chip = bestMove.solution.chips[i];

				if(!chip.IsLineCard)
					chip.Flashing(eventCount);
			}
		}
		#endregion

		#region 消除line块
		public void RemoveLineSlot(List<Slot> listSlot)
		{
			if(listSlot == null)
				return;

			isPlaying = false;
			StartCoroutine(RemoveLineSlotDetail(listSlot));


		}

		private IEnumerator RemoveLineSlotDetail(List<Slot> listSlot)
		{
			Slot slot;
			Card card;
			BlockInterface block;
			int index = 0;
			int totalMathchCount = 0;

			while(listSlot.Count > 0)
			{
				for(int i = 0; i < listSlot.Count;i++)
				{
					slot = listSlot[i];
					if(slot == null) continue;

					if(slot.RemoveIndex != index) continue;

					listSlot.Remove(slot);
					i--;

					card = slot.GetChip();
					if(card != null && card.IsMatcheble(true) && !slot.Block)
					{
						_filedMgr.BlockCrush(slot.Point.X, slot.Point.Y, true);
						card.DestroyChip();

						totalMathchCount++;

						//jelly = slot.GetJelly();
						//if (jelly)
						//	jelly.JellyCrush();
					}
					else if(slot.Block != null)
					{
						block = _filedMgr.GetBlock(slot.Point.X, slot.Point.Y);
						if (block) 
						{
							block.BlockCrush(true,slot.transform,true,E_CardType.None);
							totalMathchCount++;
						}
					}
				}

//				yield return 0;
//				yield return 0;
				yield  return new WaitForSeconds(0.08f);

				index++;
			}

			#if !FightTest
			_firstDamage = ModuleFight.GetFirstRandomAttack(totalMathchCount);
			NeedSaveFirstDamage = false;
//			ModuleFight.NormalRandomAttack(totalMathchCount);
			#endif

			yield return new WaitForSeconds(0.2f);

			isPlaying = true;
		}

		#endregion

		#region 显示提示
		/// <summary>
		/// 显示提示
		/// </summary>
		/// <param name="msgType">Message type.</param>
		public void ShowMsg(FightDefine.E_NoteMsgType msgType,params object[] param)
		{
			switch(msgType)
			{
				case FightDefine.E_NoteMsgType.NoMoves:
					DebugUtil.Info("移动步数不足");
					#if !FightTest
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("移动步数不足");
					#endif
					break;
				case FightDefine.E_NoteMsgType.NoShuffle:
					DebugUtil.Info("没有消除的了，等吧");
					#if !FightTest
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("没有消除的了，等吧");
					#endif
					break;
				case FightDefine.E_NoteMsgType.NotEnoughEnergy:
					#if !FightTest
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("能量不足");
					#endif
					break;
				case FightDefine.E_NoteMsgType.OppUseAddMoves:
					#if !FightTest
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("对手使用了增加步数技能");
					#endif
					break;
				case FightDefine.E_NoteMsgType.ShowIceNotice:
					string notice = string.Format("冰封{0}秒",param[0]);
					#if !FightTest
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg(notice);
					#endif
					break;
				default:
					break;
			}
		}
		#endregion

		#region 加载场景
		public void Reset() 
		{
			IsFighting = false;

			animate = 0;
			gravity = 0;
			matching = 0;
			//			//
			//			//		main.stars = 0;
			//			//
			eventCount = 0;
			lastMovementId = 0;
			swapEvent = 0;
			_continueMatchCount = 0;
			//			//		main.score = 0;
			//			//		main.firstChipGeneration = true;
			//			//
			isPlaying = false;

			GameResult = -1;
			IsLastAttackTrigger = false;

			#if FightTest
			movesCount = 1000;//FightControl.Instance.ModuleFight.Moves;
			timeLeft = 1800;//FightControl.Instance.ModuleFight.TimeSec;//LevelProfile.CurrentLevelProfile.SecDuration;
			#else
			movesCount = ModuleFight.Moves;
			timeLeft = ModuleFight.TimeSec;
			#endif

			//
			//			//		main.timeLeft = LevelProfile.main.duration;
			//			//		main.countOfEachTargetCount = new int[] { 0, 0, 0, 0, 0, 0};
			//			//		main.creatingSugarTask = 0;
			//			//
			//			//
			//			//			reachedTheTarget = false;
			//			outOfLimit = false;
			//			//
			//
			//			//			targetRoutineIsOver = false;
			//			limitationRoutineIsOver = false;
			//			//

			Weed.lastWeedCrush = 0;
			Weed.seed = 0;
			_lastSwapEventForSweed = 0;
			AnimationMgr.Instance.Iteraction = true;

			limitationRoutineIsOver = false;

			WinOrLooseAnimationIsPlaying = false;

			ListNewPowerUp.Clear();
		}

		public bool WinOrLooseAnimationIsPlaying = false;

		public void StartLevel(int level)
		{
			StopAllCoroutines();

			Reset();

			StartCoroutine(_filedMgr.CreateMap(level));

			StartCoroutine(ShuffleRoutine());

			StartCoroutine(ShowingHintRoutine());
		}

		/// <summary>
		/// 加载战斗场景
		/// </summary>
		public void LoadFightScene()
		{
			ClearLevel();
			IsInFightScene = true;

			//预加载资源
			ResourceMgr.Instance.PreLoadMultiAsset(FightDefine.PreLoadPrefabNames,(isSucess)=>{LoadFightTexture();});
		}

		private void LoadFightTexture()
		{
			//加载破碎效果图
			ResourceMgr.Instance.PreLoadMultiAsset(FightDefine.PreLoadTextues,
				(isSucess)=>{UIMgr.Instance.ShowUI(E_UIType.Fight,typeof(UIFight),OnUIShowed);},typeof(Texture2D));
		}

		/// <summary>
		/// 加载uiFight后
		/// </summary>
		/// <param name="ui">User interface.</param>
		private void OnUIShowed(BaseUI ui)
		{
//			_dicBoss.Clear();
//
//			//test
//			Transform transBossParent = GameObject.Find("BossParent").transform;
//			_dicBoss.Add(1,transBossParent.Find("BossCamera1/micro_dragon_mobile").GetComponent<Dragon>());
//			_dicBoss.Add(2,transBossParent.Find("BossCamera2/micro_werewolf_mobile").GetComponent<Dragon>());
//			_dicBoss.Add(3,transBossParent.Find("BossCamera3/micro_orc_mobile").GetComponent<Dragon>());

//			_dicMyBoss[1].SwithchState(StateDef.Idle);
//			_dicOppBoss[1].SwithchState(StateDef.Idle);
			Fight.Level.LoadLevel();
		}

		/// <summary>
		/// 设置boss状态机
		/// </summary>
		/// <param name="player">Player.</param>
		/// <param name="id">Identifier.</param>
		/// <param name="isOpp">If set to <c>true</c> is opp.</param>
		public void SetPlayerFsm(PlayerBase player,int id,bool isOpp)
		{
			if(isOpp)
			{
				_dicOppBoss.Add(id,player);
				//_dicOppBoss[id].SwithchState(StateDef.Idle);
			}
			else
			{
				_dicMyBoss.Add(id,player);
//				_dicMyBoss[id].SwithchState(StateDef.Idle);
			}
		}

		/// <summary>
		/// 我方当前上阵的bossFsm
		/// </summary>
		/// <value>The current my boss.</value>
		public PlayerBase CurrentMyBoss
		{
			get
			{
				if(_dicMyBoss != null && _dicMyBoss.ContainsKey(ModuleFight.CurrentMyBossId))
				{
					return _dicMyBoss[ModuleFight.CurrentMyBossId];
				}

				return null;
			}
		}

		/// <summary>
		/// 对方当前上阵的bossFsm
		/// </summary>
		/// <value>The current opp boss.</value>
		public PlayerBase CurrentOppBoss
		{
			get
			{
				if(_dicOppBoss != null && _dicOppBoss.ContainsKey(ModuleFight.CurrentOppBossId))
				{
					return _dicOppBoss[ModuleFight.CurrentOppBossId];
				}

				return null;
			}
		}
		#endregion

		#region boss受击

		private EnergyLightMgr _energyMgr;
		/// <summary>
		/// 能量坛
		/// </summary>
		/// <value>The energy mgr.</value>
		private EnergyLightMgr energyMgr
		{
			get
			{
				if(_energyMgr == null)
				{
					_energyMgr = FieldMgr.SlotRoot.Find("Fight_BossPos").GetComponent<EnergyLightMgr>();
				}

				return _energyMgr;
			}
		}

		public void RefreshEnergy(float currentEnergy)
		{
//			DebugUtil.Error("RefreshEnergy: " + currentEnergy);

			energyMgr.RefreshEnergy(currentEnergy);

			EventDispatcher.TriggerEvent<int>(SkillBtn.RefreshEnergy,(int)currentEnergy);

		}

		/// <summary>
		/// 增加boss能量
		/// </summary>
		/// <param name="isNearBoss">If set to <c>true</c> is near boss.</param>
		public void AddEnergy(E_AddEnergyType type)
		{
			ModuleFight.AddEnergy(type);
		}

		private void PlayHpFlying(Transform parent,int damage)
		{
			if(damage >= 0) return;

			UILabel label = null;
			if(_queueHpLabel.Count > 0)
				label = _queueHpLabel.Dequeue();
			else
			{
				label = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.LabelHPFlying).GetComponent<UILabel>();
			}

			TweenAlpha tweenAlpha = label.GetComponent<TweenAlpha>();
			tweenAlpha.ResetToBeginning();

			TweenPosition tweenPos = label.GetComponent<TweenPosition>();
			tweenPos.ResetToBeginning();

			label.text = damage.ToString();
			label.transform.SetParent(parent);
			label.transform.localPosition = new Vector3(0,-0.3f,0);
			label.transform.localScale = Vector3.one * 0.005f;
			label.depth = 213;
			//			Color color = label.color;
			//			color.a = 1f;
			//			label.color = color;

			tweenPos.delay = 0;//Random.Range(0,500) / 1000f;
			tweenPos.enabled = true;
			tweenAlpha.enabled = true;
			tweenPos.PlayForward();
			tweenAlpha.PlayForward();

			tweenAlpha.SetOnFinished(
				()=>
				{
					label.transform.parent = null;
					if(_transHpFly != null)
					{
						_queueHpLabel.Enqueue(label);
					}
					else
					{
						Destroy(label.gameObject);
					}
				});
		}

		/// <summary>
		/// 我方受到伤害
		/// </summary>
		/// <param name="boosId">Boos identifier.</param>
		/// <param name="currentHp">Current hp.</param>
		public void TakeDamageFinished(int bossId,int currentHp,int damage)
		{
			if(ModuleFight.CurrentMyBoss.BossId == bossId)
			{
				if(damage != 0)
				{
					ModuleFight.CurrentMyBoss.CurrentHp = currentHp;
					EventDispatcher.TriggerEvent<int,int>(UIFight.MyBossTakeDamage, bossId, currentHp);
					//DebugUtil.Debug("TakeDamage");
					FightMgr.Instance.CurrentMyBoss.SwithchState(StateDef.Hurt);

					//飘血
					PlayHpFlying(TransMyBossHpFlyPos,-damage);
				}
			}
		}

		/// <summary>
		/// 我方普通攻击
		/// </summary>
		public void AttackFinished(int bossId,int currentHp,int damage)
		{
			if(ModuleFight.CurrentOppBoss.BossId == bossId)
			{
				//显示敌方扣血
				if(damage != 0)
				{
					EventDispatcher.TriggerEvent<int,int>("CLIENT_ATTACK", bossId,currentHp);

	                if(FightMgr.Instance.CurrentOppBoss != null)
						FightMgr.Instance.CurrentOppBoss.SwithchState(StateDef.Hurt);

					//飘血
					PlayHpFlying(TransHpFlyPos,-damage);
				}
			}
		}
		#endregion

		#region 结算成功失败
		public int GameResult = -1;
		public bool IsLastAttackTrigger = false;
		private void OnFightResultDataCBK(object data)
		{
			if (data != null)
			{
				LuaTable msg = data as LuaTable;
				int result =int.Parse(msg.GetStringField("result"));
				object[] dataVal = Util.CallMethod("FightModule", "GetAwardTrophy");
				//				DebugUtil.Info("too large:" + dataVal[0].ToString());
				int jiangBei = int.Parse(dataVal[0].ToString());
				dataVal = Util.CallMethod("FightModule", "GetAwardG_money");
				//				DebugUtil.Info("too large:" + dataVal[0].ToString());
				int money = int.Parse(dataVal[0].ToString());

				//Util.CallMethod("UIMainCtrl", "Recalculate_Gold_Trophy_After_Fight");


//				EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,2);

				isPlaying = false; 
                limitationRoutineIsOver = true;
				IsFighting = false;

				GameResult = result;
				StartCoroutine(ShowUI(result == (int)Result.FIGHT_WIN,jiangBei,money));
			}
		}

        /// <summary>
        /// 前XX名，排名交换的时候调用这个方法
        /// </summary>
        /// <param name="data"></param>
        private void OnRankChangeDataCBK(object data)
        {
            PlayRankChange = true;
        }

        private void OnRankUpDataCBK(object data)
        {
            PlayRankChange = true;

            PlayRankUp = true;
        }
        

        private IEnumerator ShowUI(bool isWin,int jiangBei,int money)
		{
//			yield return new WaitForSeconds(0.5f);

//			int count = 0;
			//要等攻击动画放完
			//while(EffectCount > 0 && count <= 50)
			while(!FightMgr.Instance.IsLastAttackTrigger)
			{
//				count++;
				yield return 0;
			}
//
			WinOrLooseAnimationIsPlaying = true;
			if(isWin)
			{
				CurrentOppBoss.SwithchState(StateDef.Dead);
				CurrentMyBoss.SwithchState(StateDef.Win);
			}
			else
			{
				CurrentMyBoss.SwithchState(StateDef.Dead);
				CurrentOppBoss.SwithchState(StateDef.Win);
			}

//			yield return new WaitForSeconds(1.2f);

//			count = 0;
//			while(EffectCount > 0 && count <= 50)
			while(WinOrLooseAnimationIsPlaying || EnergyMgr.Instance.PrizeAnimationCount > 0)
			{
//				count++;
				yield return 0;
			}

			if(_energyMgr != null)
				_energyMgr.CloseUI();
//			EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,0);

//			yield return new WaitForSeconds(1.8f);
//			yield return new WaitForSeconds(0.3f);

			UIMgr.Instance.ShowUI(E_UIType.UIWinOrLosePanel,
				typeof(UIWinOrLose),
				(ui)=>
				{
					UIWinOrLose uiWinOrLoose = ui as UIWinOrLose;
					if(uiWinOrLoose)
					{
                        EventDispatcher.TriggerEvent(FightDefine.Event_GameOver);
						uiWinOrLoose.ShowWinOrLoose(isWin,jiangBei,money);
//						ClearLevel();
					}
				});
		}

		/// <summary>
		/// 开始播放死亡动画
		/// </summary>
		/// <param name="from">From.</param>
		public void BeginPlayDead(PlayerBase fromPlayer)
		{
			EventDispatcher.TriggerEvent<PlayerBase>(UIFight.BossKillEvent,fromPlayer);
		}

		/// <summary>
		/// 死亡动画播放完毕
		/// </summary>
		/// <param name="from">From.</param>
		public void EndPlayDead(PlayerBase fromPlayer)
		{
			EventDispatcher.TriggerEvent<PlayerBase>(UIFight.BossDiedFlyCoinEvent,fromPlayer);
		}

		public void ClearLevel()
		{
			if(!IsInFightScene)
				return;
			
			isPlaying = false; 
			limitationRoutineIsOver = true;
			IsFighting = false;
			IsInFightScene = false;

			//EFightStatus = E_FightStatus.None;
			Fight.Level.AllLevels.Clear();

			//清除所有卡牌
			CardMgr.Instance.DestroyAllCard();


			_transHpFly = null;

			if(ListSoution != null)
				ListSoution.Clear();
			
			ParticleMgr.Instance.Clear();
			EnergyMgr.Instance.Clear();
			if(Weed.all != null)
				Weed.all.Clear();

			if(_queueHpLabel != null)
				_queueHpLabel.Clear();

			if(_dicOppBoss != null)
				_dicOppBoss.Clear();

			if(_dicMyBoss != null)
				_dicMyBoss.Clear();

			LineMgr.Instance.Clear();

			if(AllSlotGenerator != null)
				AllSlotGenerator.Clear();
			if(AllSlotGravity != null)
				AllSlotGravity.Clear();
			if(AllSlotTeleport != null)
				AllSlotTeleport.Clear();
			
			_allSlot = null;
			if(_dicSlotNow != null)
				_dicSlotNow.Clear();

			if(_dicResTemp != null)
				_dicResTemp.Clear();

			_energyMgr = null;

			MatchCountMgr.Instance.Clear();

			//清除技能变量
			ClearSkillVal();
		

			limitationRoutineIsOver = false;
		}

		#endregion
	}
}