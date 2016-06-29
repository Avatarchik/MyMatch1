using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class FieldMgr
	{
		public static MatchField field;

		private static int[] colorMask = new int[6]; // Mask of random colors: color number - colorID

		public static Transform SlotRoot;
		public static Transform EnemyRoot;
		public static Transform MyBossRoot;

		private Fight.LevelProfile _currentLevelProfile;

		public FieldMgr()
		{
			
		}

		#region 属性
		public int Width
		{
			get
			{
				if(field != null)
					return field.width;

				return 0;
			}
		}

		public int Height
		{
			get
			{
				if(field != null)
					return field.height;

				return 0;
			}
		}
		#endregion

		public IEnumerator CreateMap(int level)
		{
			//加载当前地图
			Fight.LevelProfile.NewCurrentLevelProfile = Fight.Level.AllLevels[level];

			//保存地图数据
			_currentLevelProfile = Fight.LevelProfile.NewCurrentLevelProfile;
			field = new MatchField (_currentLevelProfile.Width, _currentLevelProfile.Height);
			field.chipCount = _currentLevelProfile.CardCount;

			//生成底图
			yield return FightMgr.Instance.StartCoroutine(GenerateSlots());
			//生成障碍物
			yield return FightMgr.Instance.StartCoroutine(GenerateBlocks());

			InitializeSlot();

			yield return FightMgr.Instance.StartCoroutine(GenerateWalls());

			SlotGravity.Reshading();

			//yield return StartCoroutine(GenerateJelly());
			yield return FightMgr.Instance.StartCoroutine(GenerateChips());
			yield return FightMgr.Instance.StartCoroutine(GeneratePowerups());


			yield return FightMgr.Instance.StartCoroutine(GenerateBoss());

			#if FightTest
			//可以开始
			for(int i = 0;i < MyBossRoot.childCount;i++)
			{
				MyBossRoot.GetChild(i).gameObject.SetActive(true);
			}

			for(int i = 0;i < EnemyRoot.childCount;i++)
			{
				EnemyRoot.GetChild(i).gameObject.SetActive(true);
			}

			yield return new WaitForSeconds(1f);
			FightMgr.Instance.isPlaying = true;
			#else
			//UIMatching._instance.ShowEffect(0);
			yield return new WaitForSeconds(0.3f);
			EventDispatcher.TriggerEvent<int>(UIFight.BossCreateEvent,0);
//			yield return new WaitForSeconds(1f);

			//UIMgr.Instance.DestroyUI(E_UIType.UIMainPanel);

			EventDispatcher.TriggerEvent(UIFight.BeginStartTime);

			MusicManager.Instance.PlaySoundEff("Music/readyGo");

//			FightMgr.Instance.RefreshEnergy(0f);

			FightMgr.Instance.isPlaying = true;
			FightMgr.Instance.IsFighting = true;
#endif
            EventDispatcher.TriggerEvent(FightDefine.Event_LevelLoadOver);

			//新生成的
			if(FightMgr.Instance.ListNewPowerUp.Count > 0)
			{
				EventDispatcher.TriggerEvent<Slot,E_CardType>(FightDefine.Event_HasNewBomb, FightMgr.Instance.ListNewPowerUp[0].Slot, FightMgr.Instance.ListNewPowerUp[0].CardType);
				FightMgr.Instance.ListNewPowerUp.Clear();
			}
        }


		#region 生成Slot
		/// <summary>
		/// 生成底图
		/// </summary>
		private IEnumerator GenerateSlots()
		{
			DebugUtil.Info("生成底图");

			//生成节点
			SlotRoot = GameObject.Find("Slot").transform;

			Slot slotAdd = null;
			SlotGenerator sGenerator = null;
			SlotTeleport sTeleport = null;

			for(int x = 0;x < field.width;x++)
			{
				yield return 0;
				for(int y = 0;y < field.height;y++)
				{
					field.slots[x, y] = _currentLevelProfile.GetSlot(x, Height - y - 1);
					field.generator[x, y] = _currentLevelProfile.GetGenerator(x, Height - y - 1);
					field.teleport[x, y] = _currentLevelProfile.GetTeleport(x, Height - y - 1);

					if (field.slots[x, y]) 
					{
						
						slotAdd = CreateOneSlot(x,y);
						slotAdd.transform.SetParent(SlotRoot,false);

						if (field.generator[x, y])
						{
							sGenerator = slotAdd.GetOrAddComponent<SlotGenerator>();
							sGenerator.SetSlot(slotAdd);
							FightMgr.Instance.AllSlotGenerator.Add(sGenerator);
						}

						if (field.teleport[x, y] > 0) 
						{
							sTeleport = slotAdd.GetOrAddComponent<SlotTeleport>();
							sTeleport.TargetID = field.teleport[x, y];
						}

						int gravity = _currentLevelProfile.GetGravity(x, Height - y - 1);
						switch (gravity) 
						{
							case 0:
								slotAdd.slotGravity.gravityDirection = Side.Bottom;
								break;
							case 1:
								slotAdd.slotGravity.gravityDirection = Side.Left;
								break;
							case 2:
								slotAdd.slotGravity.gravityDirection = Side.Top;
								break;
							case 3:
								slotAdd.slotGravity.gravityDirection = Side.Right;
								break;
						}

						FightMgr.Instance.AllSlotGravity.Add(slotAdd.GetOrAddComponent<SlotGravity>());
					}
				}
			}
		}

		/// <summary>
		/// 生成一个slot
		/// </summary>
		/// <returns>The one slot.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private Slot CreateOneSlot(int x,int y)
		{
			//生成一个新的
			GameObject goSlot = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Slot_Empty);//ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.Prefab_Slot_Empty);
			Slot slot = goSlot.GetComponent<Slot>();
			slot.Point = new Point(x,y);

			FightMgr.Instance.AddSlot(slot);
		
			//监听触摸
			TouchMgr.Instance.AddTouchControl(slot);
			return slot;
		}

		/// <summary>
		/// 初始化slot的相邻，slot的wall，slot的传送
		/// </summary>
		public void InitializeSlot ()
		{
			//			foreach (Slot slot in GameObject.FindObjectsOfType<Slot>())
			//				if (!all.ContainsKey(slot.key))
			//					all.Add(slot.key, slot);

			List<Slot> listSlot = FightMgr.Instance.GetSlotsList();
			Slot slot = null;

			for(int index = 0; index < listSlot.Count;index++)
			{
				slot = listSlot[index];
				for (int i = 0;i < Utils.allSides.Length;i++)// Filling of the nearby slots dictionary 
				{
//					slot.nearSlot.Add(Utils.allSides[i], FightMgr.Instance.FindNearSlot(slot.Point, Utils.allSides[i]));
					slot.nearSlot[(int)Utils.allSides[i]] = FightMgr.Instance.FindNearSlot(slot.Point, Utils.allSides[i]);
				}
//				slot.nearSlot.Add(Side.Null, null);
				slot.nearSlot[(int)Side.Null] = null;

				for (int i = 0;i < Utils.straightSides.Length;i++) // Filling of the walls dictionary
				{
					slot.wallMask.Add(Utils.straightSides[i], false);
				}
			}

			Side direction;
			SlotTeleport teleport;
			SlotGravity sgTemp;
			//			foreach (Slot slot in SlotManager.Instance.DicSlot.Values) 
			for(int index = 0; index < listSlot.Count;index++)
			{	
				slot = listSlot[index];
				sgTemp = slot.slotGravity;
				direction = sgTemp.gravityDirection;
				if (slot[direction] != null) 
				{
					sgTemp = slot[direction].slotGravity;
					//来源取反
					sgTemp.fallingDirection = Utils.MirrorSide(direction);
				}

				teleport =slot.slotTeleport;

				if (teleport != null)
					teleport.Initialize();
			}
		}
		#endregion

		#region 生成障碍物
		private IEnumerator GenerateBlocks()
		{
//			yield return 0;

			DebugUtil.Info("生成障碍物");
			GameObject o;
			Slot s;
			Block b;
			Boss boss;
			Weed w;
			Branch brch;


			float bossX1 = 0;
			float bossX2 = 0;
			float bossY1 = 0;
			float bossY2 = 0;

			int blockVal = 0;
			for (int x = 0; x < field.width; x++)					
				for (int y = 0; y < field.height; y++) 
				{
					field.blocks[x,y] = _currentLevelProfile.GetBlock(x,Height-y-1);
					if (field.slots[x,y]) 
					{
						blockVal = field.blocks[x,y];
						if (blockVal > 0) 
						{
							if (blockVal <= 3) 
							{
								s = FightMgr.Instance.FindSlot(x, y);
								o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Block);
								o.name = "Block_" + x + "x" + y;
								//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								b = o.GetComponent<Block>();
								s.SetBlock(b);
								b.slot = s;
								b.level = field.blocks[x,y];
								b.Initialize();
							}
							if (blockVal == 4) {
								s = FightMgr.Instance.FindSlot(x, y);
								o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Weed);
								o.transform.position = s.transform.position;
								o.name = "Weed_" + x + "x" + y;
								//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								w = o.GetComponent<Weed>();
								s.SetBlock(w);
								w.slot = s;
								w.Initialize();
							}
							if (blockVal == 5) {
								s = FightMgr.Instance.FindSlot(x, y);
								o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Branch);
								o.transform.position = s.transform.position;
								o.name = "Branch_" + x + "x" + y;
								//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								brch = o.GetComponent<Branch>();
								s.SetBlock(brch);
								brch.slot = s;
								brch.Initialize();
							}
							if (blockVal == 20) 
							{
								//风车
								s = FightMgr.Instance.FindSlot(x, y);
								o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_FengChe);
								o.transform.position = s.transform.position;
								o.name = "FengChe_" + x + "x" + y;
								//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								FengChe fc = o.GetComponent<FengChe>();
								s.SetBlock(fc);
								fc.slot = s;
								fc.Initialize();
							}

							if (blockVal >= 6 && blockVal <= 9) 
							{
								s = FightMgr.Instance.FindSlot(x, y);
								s.IsBoss = true;
								o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Boss);
								o.transform.position = s.transform.position;
								o.name = "Boss_" + x + "x" + y;
								//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								boss = o.GetComponent<Boss>();
								s.SetBlock(boss);
								boss.slot = s;
								boss.Initialize(blockVal - 6);

								if(blockVal == 6)
								{
									bossX1 = s.transform.localPosition.x;
									bossY1 = s.transform.localPosition.y;

									_xBoss = x;
									_yBoss = y;
								}
								else if(blockVal == 9)
								{
									bossX2 = s.transform.localPosition.x;
									bossY2 = s.transform.localPosition.y;
								}
							}

							yield return 0;
						}
					}
				}

//			#if !FightTest
			//设置boss的位置
			Transform bossPos = SlotRoot.Find("BossPos");
			bossPos.localPosition = new Vector3((bossX1 + bossX2) * 0.5f,(bossY1),0f);

			GameObject goBossPos = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_BossPos);
			goBossPos.name = "Fight_BossPos";
			goBossPos.transform.SetParent(SlotRoot,false);
			goBossPos.transform.localPosition = new Vector3((bossX1 + bossX2) * 0.5f,(bossY2 + bossY1) * 0.5f,0f);

			Transform EnergyPos = SlotRoot.parent.Find("Effect/BossPosEffect");
			EnergyPos.position = goBossPos.transform.position;
			MyBossPosEffect = EnergyPos;

			Transform EnemyPosEffect = SlotRoot.parent.Find("Effect/EnemyPosEffect");
			OppBossPosEffect = EnemyPosEffect;
//			#endif


		}

		public static Transform MyBossPosEffect;
		public static Transform OppBossPosEffect;
		public static Transform TimeEnergyRecoveryPos;
		#endregion

		#region wall
		// Generation impassable walls
		private IEnumerator GenerateWalls()
		{
			DebugUtil.Info("生成墙");

			int x;
			int y;
			Slot near;
			Slot current;

			for (x = 0; x < field.width-1; x++)		
				for (y = 0; y < field.height; y++) 
				{
					field.wallsV[x,y] = _currentLevelProfile.GetWallV(x,Height-y-1);
					if (field.wallsV[x,y] && field.slots[x,y]) 
					{
						//						Debug.LogError("has wall");

						current = FightMgr.Instance.FindSlot(x, y);
						if (current != null) 
						{
							current.SetWall(Side.Right);
							near = current[Side.Right];

							if (near != null)
								near.SetWall(Side.Left);
						}
					}
				}

			for (x = 0; x < field.width; x++)	
				for (y = 0; y < field.height-1; y++) 
				{
					field.wallsH[x,y] = _currentLevelProfile.GetWallH(x, Height-y-2);
					if (field.wallsH[x,y] && field.slots[x,y]) 
					{
						//						Debug.LogError("has wall");

						current = FightMgr.Instance.FindSlot(x, y);
						if (current != null) 
						{
							current.SetWall(Side.Top);
							near = current[Side.Top];

							if (near != null)
								near.SetWall(Side.Bottom);
						}
					}
				}

			yield return 0;

			List<Pair> walls = new List<Pair>();
			Pair pair;
			Vector3 position;
			GameObject wall;
			Slot slot;
			List<Slot> list = FightMgr.Instance.GetSlotsList();
			for (int i = 0;i < list.Count;i++) 
			{
				//yield return 0;
				slot = list[i];
				foreach (Side side in Utils.straightSides) 
				{
					if (slot[side] != null)
						continue;
					pair = new Pair(slot.key, (slot.Point.X + Utils.SideOffsetX(side)) + "_" + (slot.Point.Y + Utils.SideOffsetY(side)));
					if (walls.Contains(pair))
						continue;


					//Debug.LogError("has wall:" + slot.Key + " side:" + side);
					position = new Vector3();
					position.x = Utils.SideOffsetX(side) * 35 + 0.353f;
					position.y = Utils.SideOffsetY(side) * 35 + 0.353f;

					wall = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Wall);
					//					wall.transform.position = position;
					//wall.transform.parent = slot.transform;
					wall.transform.SetParent(slot.transform,false);
					wall.transform.localPosition = position;
					wall.name = "Wall_" + side;
					if (Utils.SideOffsetY(side) != 0)
						wall.transform.Rotate(0, 0, 90);

					walls.Add(pair);

				}
			}

			yield return 0;
		}
		#endregion
			

		#region 生成基本卡牌布局
		// Generation chips
		private IEnumerator GenerateChips ()
		{
			DebugUtil.Info("生成基本卡牌");

			int x;
			int y;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++)
					field.chips[x,y] = _currentLevelProfile.GetChip(x,Height-y-1);

			field.FirstChipGeneration();
			yield return 0;

			int id;

			for (x = 0; x < field.width; x++) 
			{
				yield return 0;
				for (y = 0; y < field.height; y++) 
				{
					if (field.blocks[x, y] == FightDefine.Def_Block_Weed)
						continue;
					id = field.GetChip(x, y);
					if (id >= 0 && id != FightDefine.Def_Card_Stone && (field.blocks[x,y] == FightDefine.Def_Block_None || field.blocks[x,y] == FightDefine.Def_Block_Brch)) 
						GetNewSimpleChip(x, y,id);
					if (id == FightDefine.Def_Card_Stone && field.blocks[x,y] == FightDefine.Def_Block_None)
						GetNewStone(x, y);
				}
			}			
		}

		// Creating a simple random color chips
		public Card GetNewSimpleChip (int x, int y)
		{
			return GetNewSimpleChip(x, y, colorMask[Random.Range(0, field.chipCount)]);
		}

		// Creating a simple chip specified color
		public Card GetNewSimpleChip (int x, int y, int id) 
		{
			Slot slot = FightMgr.Instance.FindSlot(x, y);
			E_CardType cardType = (E_CardType)System.Enum.Parse(typeof(E_CardType),string.Format("Fight_SimpleCard{0}",id+1));
			Card card = CardMgr.Instance.GetOneCard(cardType);
			slot.SetChip(card);
			//card.transform.SetParent(slot.transform,false);

			//			DebugUtil.Debug("aa");
			return card;
		}

		public Card GetNewStone (int x, int y) 
		{
			Slot slot = FightMgr.Instance.FindSlot(x, y);
			//			Chip chipForSlot = slot.GetChip();
			//			if (chipForSlot != null)
			//				o.transform.position = chipForSlot.transform.position;
			//			
			//			Chip chip = o.GetComponent<Chip> ();
			//			slot.SetChip(chip);
			//			return chip;

			Card card = CardMgr.Instance.GetOneCard(E_CardType.Stone);
			slot.SetChip(card);
			//card.transform.position = position;
			//			card.transform.SetParent(slot.transform);
			return card;
		}
		#endregion

		#region 生成特殊卡牌
		// Generation bombs
		private IEnumerator GeneratePowerups ()
		{
			//yield return 0;
			DebugUtil.Info("生成特殊卡牌");

			int x;
			int y;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++)
					field.powerUps[x,y] = _currentLevelProfile.GetPowerup(x,Height - y - 1);

			PowerUps powerup;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++) 
				{
					if (field.powerUps[x,y] > 0 && field.slots[x,y]) 
					{
						powerup = PowerUps.powerupsNew.Find(pu => pu.levelEditorID == field.powerUps[x, y]);
						if (powerup != null)
							AddPowerup(x, y, powerup);
						yield return 0;
					}
				}
		}

		public Card AddPowerup(int x, int y, string powerupName) 
		{
			PowerUps powerup = PowerUps.powerupsNew.Find(pu => pu.name == powerupName);
			if (powerup == null)
				return null;

			return AddPowerup(x, y, powerup);
		}

		// Make a bomb in the specified location with the ability to transform simple chips in a bomb
		public Card AddPowerup(int x, int y, PowerUps powerup) 
		{
			Slot slot = FightMgr.Instance.FindSlot(x, y);
			Card chip = slot.GetChip();

			if (chip)
			{
				//DebugUtil.Debug("destroy card:" + chip.gameObject.name);
				if(chip.move)
					chip.gravity = false;

				GameObject.Destroy(chip.gameObject);
			}

			chip = GetNewBomb(slot.Point.X, slot.Point.Y, powerup, slot.transform.position);
			FightMgr.Instance.ListNewPowerUp.Add(chip);
			return chip;
		}

		public Card GetNewBomb(int x, int y, string powerup, Vector3 position) 
		{
			PowerUps p = PowerUps.powerupsNew.Find(pu => pu.name == powerup);
			return GetNewBomb(x,y,p,position);
		}

		public Card GetNewBomb(int x, int y,  PowerUps p, Vector3 position) 
		{
			if (p == null)
				return null;

//			string prefabKey = string.Format("{0}{1}",p.contentName,(p.color ? Card.chipTypes[id] : ""));
			#if FightTest
			string prefabKey = "FightNew/SpecialCards/" + p.contentName;
			#else
			string prefabKey = p.contentName;
			#endif
			GameObject o = FightMgr.Instance.LoadAndInstantiate(prefabKey);
			o.name = p.contentName;
			o.transform.position = position;

//			Card cardExist = FightMgr.Instance.FindSlot(x, y).GetChip();
//			if (cardExist != null)
//				o.transform.position = cardExist.transform.position;

			Card chip = o.GetComponent<Card>();
			FightMgr.Instance.FindSlot(x, y).SetChip(chip);
			return chip;
		}
		#endregion

		#region 生成Boss
		private IEnumerator GenerateBoss()
		{
			yield return 0;
			EnemyRoot = SlotRoot.Find("Enemy");
			GameObject go = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Boss_BingFa);
			PlayerBase player = go.GetComponent<PlayerBase>();
			go.transform.SetParent(EnemyRoot,false);
			go.SetActive(false);
			FightMgr.Instance.SetPlayerFsm(player,1,true);

			yield return 0;

			MyBossRoot = SlotRoot.Find("BossPos");
			go = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Boss_GongZhu);
			player = go.GetComponent<PlayerBase>();
			go.transform.SetParent(MyBossRoot,false);
//			go.SetActive(false);
			FightMgr.Instance.SetPlayerFsm(player,1,false);
		}
		#endregion

		#region 当前场景格子 
		/// <summary>
		/// 当前场景格子元素
		/// </summary>
		public class MatchField 
		{
			public int width;
			public int height;
			public int chipCount;
			public bool[,] slots;
			public int[,] teleport;
			public bool[,] generator;
			//		public bool[,] sugarDrop;
			public int[,] chips;
			public int[,] powerUps;
			public int[,] blocks;
			public int[,] jellies;
			public bool[,] wallsH;
			public bool[,] wallsV;

			//		public E_FieldTarget target = E_FieldTarget.KillBoss;
			public int targetValue = 0;


			public MatchField (int w, int h)
			{
				width = w;
				height = h;
				slots = new bool [w,h];
				generator = new bool[w, h];
				//			sugarDrop = new bool[w, h];
				teleport = new int[w,h];
				chips = new int [w,h];
				powerUps = new int [w,h];
				blocks = new int [w,h];
				jellies = new int [w,h];
				wallsV = new bool [w,h];
				wallsH = new bool [w,h];
			}

			public bool GetSlot (int x, int y){
				if (x >= 0 && x < width && y >= 0 && y < height) return slots[x,y];
				return false;
			}

			public int GetChip (int x, int y){
				if (x >= 0 && x < width && y >= 0 && y < height) return chips[x,y];
				return 0;
			}

			public void  NewRandomChip (int x, int y, bool unmatching)
			{
				if (chips[x,y] == -1) return;

				chips[x,y] = Random.Range(1, chipCount + 1);

				if (unmatching) 
				{
					List<int> list = new List<int>(chipCount);
					for(int i = 1;i <= chipCount;i++)
					{
						list.Add(i);
					}

					while(true)
					{
						if(
							(GetChip(x, y) == GetChip(x, y+1) && GetChip(x, y) == GetChip(x, y+2))
							|| (GetChip(x, y) == GetChip(x+1, y) && GetChip(x, y) == GetChip(x+2, y))
							|| (GetChip(x, y) == GetChip(x, y-1) && GetChip(x, y) == GetChip(x, y-2))
							|| (GetChip(x, y) == GetChip(x-1, y) && GetChip(x, y) == GetChip(x-2, y))
							|| (GetChip(x,y) == GetChip(x,y+1) && GetChip(x,y) == GetChip(x,y-1))
							|| (GetChip(x,y) == GetChip(x+1,y) && GetChip(x,y) == GetChip(x-1,y))
							)
						{
							list.Remove(GetChip(x,y));
							chips[x,y] = list[Random.Range(0, list.Count)];
						}
						else
						{
							break;
						}
					}

//					while (GetChip(x, y) == GetChip(x, y+1)
//						|| GetChip(x, y) == GetChip(x+1, y)
//						|| GetChip(x, y) == GetChip(x, y-1)
//						|| GetChip(x, y) == GetChip(x-1, y))
//					{
//						if(chipCount < 5)
//						{
//							if(
//								(GetChip(x, y) == GetChip(x, y+1) && GetChip(x, y) == GetChip(x, y+2))
//								|| (GetChip(x, y) == GetChip(x+1, y) && GetChip(x, y) == GetChip(x+2, y))
//								|| (GetChip(x, y) == GetChip(x, y-1) && GetChip(x, y) == GetChip(x, y-2))
//								|| (GetChip(x, y) == GetChip(x-1, y) && GetChip(x, y) == GetChip(x-2, y))
//							)
//							{
//								chips[x,y] = Random.Range(1, chipCount + 1);
//							}
//							else
//							{
//								if(
//									(GetChip(x,y) == GetChip(x,y+1) && GetChip(x,y) == GetChip(x,y-1))
//									|| (GetChip(x,y) == GetChip(x+1,y) && GetChip(x,y) == GetChip(x-1,y))
//								)
//								{
//									chips[x,y] = Random.Range(1, chipCount + 1);
//								}
//								else
//								{
//									break;
//								}
//							}
//						}
//						else
//						{
//							chips[x,y] = Random.Range(1, chipCount + 1);
//						}
//					}
				}
			}

			public void  FirstChipGeneration ()
			{
				int x;
				int y;

				// impose a mask slots
				for (x = 0; x < width; x++) {
					for (y = 0; y < height; y++) {
						if (!slots[x,y] || (blocks[x,y] > 0 && blocks[x,y] != 5))
							chips[x,y] = -1;
						if (blocks[x,y] == 5 && chips[x,y] == -1)
							chips[x,y] = 0;
					}
				}

				// replace random chips on nonrandom
				for (x = 0; x < width; x++)					
					for (y = 0; y < height; y++) 
						if (chips[x,y] == 0 && chips[x,y] != 9)
							NewRandomChip(x, y, true);	

				// nonrandom give chips to the normal (0 to 5)
				for (x = 0; x < width; x++)					
					for (y = 0; y < height; y++) 
						if (chips[x,y] > 0 && chips[x,y] != 9)
							chips[x,y] --;


				// shuffling color
				// create a deck of colors and shuffling its
				int[] a = {0, 1, 2, 3, 4, 5};
				int j;
				for (int i = 5; i > 0; i--) {
					j = Random.Range(0, i);
					a[j] = a[j] + a[i];
					a[i] = a[j] - a[i];
					a[j] = a[j] - a[i];
				}

				colorMask = a;

				// apply the results to the matrix shuffling chips	
				for (x = 0; x < width; x++)					
					for (y = 0; y < height; y++) 
						if (chips[x,y] >= 0 && chips[x,y] != 9)
							chips[x,y] = a[chips[x,y]];
			}

		}
		#endregion


		#region 炸障碍
		// Crush block function
		public void  BlockCrush (int x, int y, bool radius, bool force = false) 
		{
			BlockInterface b;
			Slot s;
			Card c;
			StoneChip sc;
			Side side;

			Slot slot = FightMgr.Instance.FindSlot(x, y);
			Transform parent = slot.transform;

			if (radius) 
			{
				Card cardTemp = slot.GetChip();
				E_CardType cardType = cardTemp == null ? E_CardType.None : cardTemp.CardType;
				for (int i = 0;i < Utils.straightSides.Length;i++) 
				{
					side = Utils.straightSides[i];
					b = null;
					s = null;
					c = null;
					sc = null;

					b = GetBlock(x + Utils.SideOffsetX(side), y + Utils.SideOffsetY(side));
					if (b && b.CanBeCrushedByNearSlot())
					{
						b.BlockCrush(force,parent.transform,false,cardType);
					}

					s = FightMgr.Instance.FindSlot(x + Utils.SideOffsetX(side), y + Utils.SideOffsetY(side));
					if (s) c = s.GetChip();
					if (c != null) sc = c.GetComponent<StoneChip>();
					if (sc != null) c.DestroyChip();
				}
			} 

			b = GetBlock(x, y);
			if (b) b.BlockCrush(force,parent,false,E_CardType.None);
		}

		// Request of block object
		public BlockInterface GetBlock ( int x ,   int y  )
		{
			if (field != null && field.GetSlot(x, y))
				return FightMgr.Instance.FindSlot(x, y).Block;		
			
			return null;
		}
		#endregion


		#region Boss技能
		/// <summary>
		/// 随机出一个木块
		/// </summary>
		public void CreateOneBlock(Slot s)
		{
			if(s != null && s.GetChip() && !s.Block && !s.IsGenerator)
			{
				int x = s.Point.X;
				int y = s.Point.Y;

				s.GetChip().HideChip(false);

				field.blocks[x,y] = 2;

				GameObject o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Block);;
				o.name = "Block_" + x + "x" + y;
				//								o.transform.parent = s.transform;
				o.transform.SetParent(s.transform,false);
				o.transform.position = s.transform.position;
				Block b = o.GetComponent<Block>();
				s.SetBlock(b);
				b.slot = s;
				b.level = field.blocks[x,y];
				b.Initialize();
			}
		}

		/// <summary>
		/// 查找目标位置
		/// </summary>
		/// <returns>The fly block target slots.</returns>
		public List<Slot> FindFlyBlockTargetSlots(int needCnt)
		{
			List<Slot> listAll = new List<Slot>();
			List<Slot> listRtn = new List<Slot>();

			Slot s = null;
			for(int i = 0;i < field.width;i++)
			{
				for(int j = 0;j < field.height;j++)
				{
					int id = field.GetChip(i, j);
					if(id > 0 && id != 9 && field.blocks[i,j] == 0)
					{
						s = FightMgr.Instance.FindSlot(i, j);
						if(s != null && s.GetChip() && !s.Block && !s.IsGenerator && !s.GetChip().IsLineCard && !s.GetChip().move)
						{
							listAll.Add(s);
						}
					}
				}
			}

			//随机找符合的位置
			while(needCnt > 0 && listAll.Count > 0)
			{
				int index = Random.Range(0,listAll.Count);
				listRtn.Add(listAll[index]);
				//先设值，防止快速播放
				field.blocks[listAll[index].Point.X,listAll[index].Point.Y] = 2;
				listAll.RemoveAt(index);
				needCnt--;
			}

			return listRtn;
		}


		private int _xBoss;
		private int _yBoss;
		/// <summary>
		/// 冰封的slot
		/// </summary>
		/// <returns>The ice path.</returns>
		public Dictionary<int, List<Slot>> FindIcePath()
		{
			List<Slot> listAdded = new List<Slot>();
			Dictionary<int,List<Slot>> dicIceSlot = new Dictionary<int, List<Slot>>();

			//先保存四个boss位置
			Slot s = FightMgr.Instance.FindSlot(_xBoss, _yBoss);
			if(s != null)
			{
				listAdded.Add(s);
			}

			s = FightMgr.Instance.FindSlot(_xBoss + 1, _yBoss);
			if(s != null)
			{
				listAdded.Add(s);
			}

			s = FightMgr.Instance.FindSlot(_xBoss, _yBoss - 1);
			if(s != null)
			{
				listAdded.Add(s);
			}

			s = FightMgr.Instance.FindSlot(_xBoss + 1, _yBoss - 1);
			if(s != null)
			{
				listAdded.Add(s);
			}

			//初始8个位置
			int index = 0;

			List<Point> nextList = new List<Point>();
			//左
			int x1 = _xBoss - 1;
			int y1 = _yBoss;
			Point p1 = new Point(x1,y1);
			nextList.Add(p1);
			//IceSlotNext(x1,y1,0,listAdded,dicIceSlot);
			//上
			int x2 = _xBoss;
			int y2 = _yBoss - 1;
			Point p2 = new Point(x2,y2);
			nextList.Add(p2);

			//右
			int x3 = _xBoss + 1;
			int y3 = _yBoss + 1;
			Point p3 = new Point(x3,y3);
			nextList.Add(p3);

			//下
			int x4 = _xBoss + 2;
			int y4 = _yBoss;
			Point p4 = new Point(x4,y4);
			nextList.Add(p4);

			int x5 = _xBoss + 2;
			int y5 = _yBoss - 1;
			Point p5 = new Point(x5,y5);
			nextList.Add(p5);

			int x6 = _xBoss + 1;
			int y6 = _yBoss - 2;
			Point p6 = new Point(x6,y6);
			nextList.Add(p6);

			int x7 = _xBoss;
			int y7 = _yBoss - 2;
			Point p7 = new Point(x7,y7);
			nextList.Add(p7);

			int x8 = _xBoss - 1;
			int y8 = _yBoss - 1;
			Point p8 = new Point(x8,y8);
			nextList.Add(p8);

			var listResult = IceSlotNext(nextList,index,listAdded,dicIceSlot);
			while(listResult.Count > 0)
			{
				index++;
				//拿下一个point
				List<Point> listNew = new List<Point>();
				for(int i = 0;i < listResult.Count;i++)
				{
					Point p = listResult[i];
					//上
					int _x = p.X;
					int _y = p.Y + 1;
					listNew.Add(new Point(_x,_y));
					//下
					_x = p.X;
					_y = p.Y - 1;
					listNew.Add(new Point(_x,_y));
					//左
					_x = p.X - 1;
					_y = p.Y;
					listNew.Add(new Point(_x,_y));
					//右
					_x = p.X + 1;
					_y = p.Y;
					listNew.Add(new Point(_x,_y));
				}

				listResult = listNew;

				listResult = IceSlotNext(listResult,index,listAdded,dicIceSlot);
			}

			return dicIceSlot;
		}

		private List<Point> IceSlotNext(List<Point> pointNext,int index,List<Slot> listAdded,Dictionary<int,List<Slot>> dicIceSlot)
		{
			List<Point> slotRtn = new List<Point>();

			Slot s = null;
			for(int i = 0;i < pointNext.Count;i++)
			{
				Point p = pointNext[i];
				if(IsIncludeSlotField(p.X,p.Y))
				{
					s = FightMgr.Instance.FindSlot(p.X, p.Y);
					if(s != null && !listAdded.Contains(s) && !s.IsBoss)
					{
						//需要冰封的slot
						slotRtn.Add(p);

						//存到字典
						List<Slot> list = null;
						if(!dicIceSlot.ContainsKey(index))
						{
							list = new List<Slot>();
							dicIceSlot.Add(index,list);
						}
						else
						{
							list = dicIceSlot[index];
						}
						list.Add(s);

						listAdded.Add(s);
					}
				}
			}

			return slotRtn;
		}

		private bool IsIncludeSlotField(int x,int y)
		{
			return x < field.width && y < field.height;
		}


		#endregion
	}
}
