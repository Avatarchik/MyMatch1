/*
 * 
 * 文件名(File Name)：             FightControl
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/03/28 23:20:26
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using LuaInterface;
using DG.Tweening;
//using DG.Tweening;

namespace Fight
{
	public enum E_FightStatus
	{
		None,
		/// <summary>
		/// 加载场景
		/// </summary>
		Loading,
		/// <summary>
		/// 加载第一个
		/// </summary>
		LoadingBoss1,
		LoadingBoss2,
		LoadingBoss3,
		/// <summary>
		/// 预览三个boss
		/// </summary>
		PreviewScene,
		/// <summary>
		/// 打第一个boss
		/// </summary>
		FightBoss1,
		/// <summary>
		/// 打第二个boss
		/// </summary>
		FightBoss2,
		/// <summary>
		/// 打第三个boss
		/// </summary>
		FightBoss3,

		ChangeBattle,
		/// <summary>
		/// 结束了
		/// </summary>
		Finish,
	}

	public class FightControl : DDOLSingleton<FightControl>
	{
		public E_FightStatus EFightStatus = E_FightStatus.None;
		private Dictionary<int,Dragon> _dicBoss;

		public bool IsFighting
		{
			get
			{
				return EFightStatus == E_FightStatus.PreviewScene
					|| EFightStatus == E_FightStatus.LoadingBoss1
					|| EFightStatus == E_FightStatus.LoadingBoss2
					|| EFightStatus == E_FightStatus.LoadingBoss3
					|| EFightStatus == E_FightStatus.FightBoss1 
					|| EFightStatus == E_FightStatus.FightBoss2 
					|| EFightStatus == E_FightStatus.FightBoss3;
			}
		}

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

//		public string SlotName
//		{
//			get
//			{
//				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1)
//				{
//					return "Slot1";
//				}
//				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2)
//				{
//					return "Slot2";
//				}
//				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3)
//				{
//					return = "Slot3";
//				}
//				else
//				{
//					continue;
//				}
//			}
//		}

		public int Row = 9;
		public int Col = 9;

		public static int LevelRow;
		public static int LevelCol;
		public static GameObject GoUIRoot;

//		public GameObject PrefabSlot;
//		private Dictionary<string,GameObject> _dicCard;

		private string[] _preLoadAssetNames;
		void Awake()
		{
			GoUIRoot = GameObject.Find("UI Root");

			_dicBoss = new Dictionary<int, Dragon>();
		}

		private void OnEnable()
		{
			EventDispatcher.AddListener<object>("SERVER_FIGHT_RESULT", OnFightResultDataCBK);
		}

		private void OnDisable()
		{
			EventDispatcher.RemoveListener<object>("SERVER_FIGHT_RESULT", OnFightResultDataCBK);
		}

		public void LoadFightScene()
		{
			FightControl.Instance.EFightStatus = E_FightStatus.Loading;

			_preLoadAssetNames = new string[]
				{
					Slot.SlotEmptyPrefabName,Slot.WallPrefabName,
					Card.SimpleCard1,Card.SimpleCard2,Card.SimpleCard3,Card.SimpleCard4,Card.SimpleCard5,Card.SimpleCard6,
					//"Stone","StoneCrush",
					Block.crushEffect,Block.prefabName,
					Weed.prefabName,Weed.crushEffect,
					//Branch.crushEffect,
					Boss.prefabName,FightDefine.SpecialBlock,
					Card.CrossBombCard1,Card.CrossBombCard2,Card.CrossBombCard3,Card.CrossBombCard4,Card.CrossBombCard5,Card.CrossBombCard6,
					Card.HLineBombCard1,Card.HLineBombCard2,Card.HLineBombCard3,Card.HLineBombCard4,Card.HLineBombCard5,Card.HLineBombCard6,
					Card.VLineBombCard1,Card.VLineBombCard2,Card.VLineBombCard3,Card.VLineBombCard4,Card.VLineBombCard5,Card.VLineBombCard6,
					Card.UltraColorBomb,
//					Card.Card3d0,Card.Card3d1,Card.Card3d2,Card.Card3d3,Card.Card3d4,Card.Card3d5,
					"BossCreate","Lightning",
					FightDefine.LabelHPFlying,
					//破碎效果
					FightDefine.ParticleCardCrush

				};

			//预加载资源
			ResourceMgr.Instance.PreLoadMultiAsset(_preLoadAssetNames,(isSucess)=>{LoadFightTexture();});
		}

		private void LoadFightTexture()
		{
			//加载破碎效果图
			ResourceMgr.Instance.PreLoadMultiAsset(new string[]{FightDefine.CrushCard1,FightDefine.CrushCard2,FightDefine.CrushCard3,FightDefine.CrushCard4,FightDefine.CrushCard5,FightDefine.CrushCard6},
				(isSucess)=>{UIMgr.Instance.ShowUI(E_UIType.Fight,typeof(UIFight),OnUIShowed);},typeof(Texture2D));
		}
			

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

                Util.CallMethod("UIMainCtrl", "Recalculate_Gold_Trophy_After_Fight");
                

                EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,2);
				StartCoroutine(ShowUI(result == (int)Result.FIGHT_WIN,jiangBei,money));
			}
		}

		private IEnumerator ShowUI(bool isWin,int jiangBei,int money)
		{
			yield return new WaitForSeconds(1.2f);

			UIMgr.Instance.ShowUI(E_UIType.UIWinOrLosePanel,
				typeof(UIWinOrLose),
				(ui)=>
				{
					UIWinOrLose uiWinOrLoose = ui as UIWinOrLose;
					if(uiWinOrLoose)
					{
						uiWinOrLoose.ShowWinOrLoose(isWin,jiangBei,money);
						SessionControl.Instance.isPlaying = false;
						EFightStatus = E_FightStatus.None;
						Level.AllLevels.Clear();

						//清除所有卡牌
						CardManager.Instance.DestroyAllCard();
						SlotManager.Instance.Clear();
						ParticleControl.Instance.Clear();
						Weed.Clear();

						DestroyFightScene();
					}
				});
		}

		private void OnUIShowed(BaseUI ui)
		{
			_dicBoss.Clear();

			//test
			Transform transBossParent = GameObject.Find("BossParent").transform;
			_dicBoss.Add(1,transBossParent.Find("BossCamera1/micro_dragon_mobile").GetComponent<Dragon>());
			_dicBoss.Add(2,transBossParent.Find("BossCamera2/micro_werewolf_mobile").GetComponent<Dragon>());
			_dicBoss.Add(3,transBossParent.Find("BossCamera3/micro_orc_mobile").GetComponent<Dragon>());

			#if UNITY_EDITOR
				if(GameEntrance.Instance.IsTestFight)
				{
					Level.LoadTestLevel(GameEntrance.Instance.LevelId);
				}
				else
				{
					Level.LoadLevel();
				}
			#else
				Level.LoadLevel();
			#endif

		}

		/// <summary>
		/// 攻击敌方boss
		/// </summary>
		public void AttackBoss()
		{
			#if UNITY_EDITOR
			if(GameEntrance.Instance.IsTestFight)
				return;
			
			ModuleFight.AttackBoss();
			#else
			ModuleFight.AttackBoss();
			#endif
		}

		/// <summary>
		/// 播放boss受击或者死亡动画
		/// </summary>
		/// <param name="bossId">Boss identifier.</param>
		/// <param name="isHit">If set to <c>true</c> is hit.</param>
		public void PlayBossAnim(int bossId,bool isHit)
		{
			if(!_dicBoss.ContainsKey(bossId)) return;

			Dragon dragon = _dicBoss[bossId];
			if(dragon != null)
			{
				//飘血
				PlayHpFlying();

				if(isHit)
				{
//					dragon.IsPlayHitAnim = true;
					dragon.PlayAnim(Dragon.E_DragonAnim.get_hit_front);

					//Boss Skill
					bool isTriggerBossSkill = ModuleFight.IsTriggerBossSkill(bossId);
					if(isTriggerBossSkill)
					{
						
						switch(bossId)
						{
							case 1:
								//1.BOSS每受到一次攻击会对敌方区域放置一个障碍。
								FieldAssistant.Instance.CreateOneBlock();
//								FieldAssistant.Instance.CreateRowBlock();
	//							FieldAssistant.Instance.CreateNotDestroyableBlcok();
								break;
							case 2:
								//2.BOSS每损失10%血对敌方区域放置一排5个障碍。
								FieldAssistant.Instance.CreateRowBlock();
								break;
							case 3:
								//3.BOSS每掉20%血，对敌方区域放置2个无法消除的障碍，BOSS死亡后障碍消失
								FieldAssistant.Instance.CreateNotDestroyableBlcok();
								break;
							default:
//								FieldAssistant.Instance.CreateRowBlock();
								break;
						}
					}
				}
				else
				{
					DebugUtil.Debug("Boss is died,bossId:" + bossId);
					dragon.IsDead = true;
					dragon.PlayAnim(Dragon.E_DragonAnim.get_hit_front);
//					dragon.PlayAnim(Dragon.E_DragonAnim.die);
					MusicManager.Instance.PlaySoundEff("Music/KillBoss");
					FieldAssistant.Instance.StartCoroutine(FieldAssistant.Instance.ChangeNextScene());
				}
			}
		}
			

		public void DestroyFightScene()
		{
			EFightStatus = E_FightStatus.None;
			//_dicHpLabelFlying.Clear();
			_queueHpLabel.Clear();
			_dicBoss.Clear();
		}


		private Transform _transHpFly;
		public Transform TransHpFlyPos
		{
			set
			{
				_transHpFly = value;
				while(_queueHpLabel.Count > 0)
				{
					Destroy(_queueHpLabel.Dequeue().gameObject);
				}
				_queueHpLabel.Clear();
			}
			get
			{
				if(_transHpFly == null)
				{
					_transHpFly = FieldAssistant.Instance._goSlotCreate.transform.Find("BossPos/PiaoXuePanel");
				}

				return _transHpFly;
			}
		}

		//private Dictionary<UILabel,Tweener> _dicHpLabelFlying = new Dictionary<UILabel, Tweener>();
		private Queue<UILabel> _queueHpLabel = new Queue<UILabel>();
		private void PlayHpFlying()
		{
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

			label.text = (-ModuleFight.CLEINT_DAMAGE).ToString();
			label.transform.SetParent(TransHpFlyPos);
			label.transform.localPosition = new Vector3(0,-0.3f,0);
			label.transform.localScale = Vector3.one * 0.005f;
			label.depth = 213;
//			Color color = label.color;
//			color.a = 1f;
//			label.color = color;

			tweenPos.delay = Random.Range(0,500) / 1000f;
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

		public void ShowMsg(FightDefine.E_NoteMsgType msgType)
		{
			switch(msgType)
			{
				case FightDefine.E_NoteMsgType.NoMoves:
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("移动步数不足");
					break;
				case FightDefine.E_NoteMsgType.NoShuffle:
					(UIMgr.Instance.GetUIByType(E_UIType.Fight) as UIFight).ShowNoteMsg("没有消除的了，等吧");
					break;
				default:
					break;
			}
		}
	}
}
