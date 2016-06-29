/*******************************************************
 * 
 * 文件名(File Name)：             UIFight
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/14 15:04:51
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using DG.Tweening;

namespace FightNew
{
	public class UIFight : BaseUI 
	{
		public const int MyBoss1Hp = 368;
		public const int MyBoss2Hp = 0;
		public const int MyBoss3Hp = 0;

		public const string MyBossTakeDamage = "MyBossTakeDamage";
		public const string BossCreateEvent = "BossCreate";
		public const string BossKillEvent = "BossKilled";
		public const string BeginStartTime = "BeginStartTime";
		public const string BossDiedFlyCoinEvent = "BossDiedFlyCoin";
		public const string AddMoves = "AddMoves";
		public const string ShowSkillPanel = "ShowSkillPanel";

		private GameObject[] _goBossPos;

//		private UISprite _spriteMyPrize1;
//		private UISprite _spriteMyPrize2;
//		private UISprite _spriteMyPrize3;
//
//		private UISprite _spriteOppPrize1;
//		private UISprite _spriteOppPrize2;
//		private UISprite _spriteOppPrize3;
//
//		private GameObject _goPrizeFly;


//		private UISlider _sliderMy;
//		private UISlider _sliderOpponent;

		#region 血条
//		private UISprite _spriteMyHp;
//		private UISprite _spriteMyHpTop;
//		private UISprite _spriteOpponentHp;
//		private UISprite _spriteOpponentHpTop;
		private UILabel _labelMyHp;
		private UILabel _labelOpponentHp;

		private UISlider _sliderMyBoss;
		private UISlider _sliderOppBoss;

		#endregion

		//private Dictionary<int,UISlider> _dicSliderBoss;


		private UILabel _labelMyName;
		private UILabel _labelOppenentName;

		#region 提示
		private Transform _transInitNote;
		private GameObject _goFightfinish;
		#endregion


		#region 时间／步数／技能
		//时间
		private TimerBehaviour _timer;
		private UILabel _labelLeftTime;
		private int _passSec = -1;

		//步数
		private UILabel _labelLeftMoves;
		private int _oriLeftMoves = 0;
		private UILabel _labelAddMoves;

		private Tweener _tweenAddMovePos;
		private Tweener _tweenAddMoveAlpha;

		private int _currentEnergy;
		private UILabel _labelEnergy;

		private MoveMask _goMoveMask;
		private MoveMask goMoveMask
		{
			get
			{
				if(_goMoveMask == null)
					_goMoveMask = FieldMgr.SlotRoot.Find("MoveMask").GetComponent<MoveMask>();

				return _goMoveMask;
			}
		}
		#endregion

		private GameObject _goBloodNote;
		private TweenAlpha _tweenBloodNote;



		private ModuleFight _moduleFight;
		public ModuleFight moduleFight
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

		public override E_UIType GetUIType()
		{
			return E_UIType.Fight;
		}

		protected override void OnInit()
		{
			base.OnInit();
			mUIStyle = E_UIStyle.Main;
			mUILayertype = E_LayerType.MainUI;

			_animationStyle = E_UIShowAnimStyle.Normal;

		}

		protected override void OnAwake()
		{
			base.OnAwake();
//			_spriteMyHp = transform.Find("FightTop/Hp/Container_My/Sprite_Frame/Sprite_Bottle/Sprite_Blood").GetComponent<UISprite>();
//			_spriteOpponentHp = transform.Find("FightTop/Hp/Container_Oppo/Sprite_Frame/Sprite_Bottle/Sprite_Blood").GetComponent<UISprite>();
//			_spriteMyHpTop = transform.Find("FightTop/Hp/Container_My/Sprite_Frame/Sprite_Bottle/Sprite_Blood_Lid").GetComponent<UISprite>();
//			_spriteOpponentHpTop = transform.Find("FightTop/Hp/Container_Oppo/Sprite_Frame/Sprite_Bottle/Sprite_Blood_Lid").GetComponent<UISprite>();
//
			_labelOpponentHp = transform.Find("Slot/Enemy/BossHpSlider/LabelHp").GetComponent<UILabel>();
			_labelMyHp = transform.Find("Slot/BossPos/BossHpSlider/LabelHp").GetComponent<UILabel>();

//			_spriteMyPrize1 = transform.Find("FightTop/BossStatus/Container_My/Sprite1").GetComponent<UISprite>();
//			_spriteMyPrize2 = transform.Find("FightTop/BossStatus/Container_My/Sprite2").GetComponent<UISprite>();
//			_spriteMyPrize3 = transform.Find("FightTop/BossStatus/Container_My/Sprite3").GetComponent<UISprite>();
//
//			_spriteMyPrize1.spriteName = "plan-22";
//			_spriteMyPrize2.spriteName = "plan-22";
//			_spriteMyPrize3.spriteName = "plan-22";
//
//			_spriteOppPrize1 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite1").GetComponent<UISprite>();
//			_spriteOppPrize2 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite2").GetComponent<UISprite>();
//			_spriteOppPrize3 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite3").GetComponent<UISprite>();
//
//			_spriteOppPrize1.spriteName = "plan-22";
//			_spriteOppPrize2.spriteName = "plan-22";
//			_spriteOppPrize3.spriteName = "plan-22";

			_sliderMyBoss = transform.Find("Slot/BossPos/BossHpSlider").GetComponent<UISlider>();
			_sliderMyBoss.value = 1f;

			_sliderOppBoss = transform.Find("Slot/Enemy/BossHpSlider").GetComponent<UISlider>();
			_sliderOppBoss.value = 1f;

			_labelMyName = transform.Find("FightTop/PlayerName/LabelMyName").GetComponent<UILabel>();
			_labelOppenentName = transform.Find("FightTop/PlayerName/LabelOppoName").GetComponent<UILabel>();

			object[] o = Util.CallMethod("UIMainModule", "GetUserNick");
			_labelMyName.text = o[0].ToString();

			o = Util.CallMethod("UIMainModule", "GetOpponentNick");
			_labelOppenentName.text = o[0].ToString();

			_labelNoteMsg = transform.Find("FightExtra/LabelNoteMsg").GetComponent<UILabel>();
			GameObject goTweenNote = transform.Find("FightExtra").gameObject;
			_tweenPosNoteMsg = goTweenNote.GetComponent<TweenPosition>();
			_tweenAlphaNoteMsg = goTweenNote.GetComponent<TweenAlpha>();
			goTweenNote.SetActive(false);


			_goBloodNote = transform.Find("Effect/BloodNote").gameObject;
			_tweenBloodNote = _goBloodNote.GetComponent<TweenAlpha>();
			_goBloodNote.SetActive(false);
			_tweenBloodNote.SetOnFinished(new EventDelegate.Callback(()=>{_goBloodNote.SetActive(false);}));

//			_spriteMyHp.fillAmount = 1f;
//			_spriteOpponentHp.fillAmount = 1f;
//			_spriteMyHpTop.transform.localPosition = Vector3.up * -0.4f;
//			_spriteOpponentHpTop.transform.localPosition = Vector3.up * -0.4f;

			_goBossPos = new GameObject[3];
			_goBossPos[0] = transform.Find("Slot/BossPos").gameObject;
//			_goBossPos[1] = transform.Find("SlotAll/Slot2/BossPos").gameObject;
//			_goBossPos[2] = transform.Find("SlotAll/Slot3/BossPos").gameObject;

			_labelMyHp.text = moduleFight.MyCurrentTotalHp.ToString();
			_labelOpponentHp.text = moduleFight.OpponentCurrentTotalHp.ToString();

//			_goPrizeFly = transform.Find("Effect/SpritePrize").gameObject;
//			_goPrizeFly.SetActive(false);


			_transInitNote = transform.Find("InitNote/Sprite_FightIntroFrame");

			#region 时间／步数／技能
			_labelEnergy = transform.Find("FightTop/LabelEnergy").GetComponent<UILabel>();
			_currentEnergy = 0;

			///步数
			_labelLeftMoves = transform.Find("FightTop/Sprite_Moves/Sprite_BorderGreen/Sprite_Glass/Sprite_GlassTop/Sprite_Energy/Label_Energy").GetComponent<UILabel>();
			_labelAddMoves = transform.Find("FightTop/Sprite_Moves/Sprite_BorderGreen/Sprite_Glass/Sprite_GlassTop/Sprite_Energy/Label_AddEnergy").GetComponent<UILabel>();

			//tweenPos
			_tweenAddMovePos = _labelAddMoves.transform.DOLocalMoveY(17f,0.4f);
			_tweenAddMovePos.Pause();
			_labelLeftMoves.text = this.moduleFight.Moves.ToString();
			_tweenAddMovePos.SetAutoKill(false);
			//tweenAlpha
			_tweenAddMoveAlpha = DOTween.ToAlpha(GetTweenColor,SetTweenColor,0,0.4f);
			_tweenAddMoveAlpha.SetAutoKill(false);
			_tweenAddMoveAlpha.Pause();
			_tweenAddMoveAlpha.OnComplete(OnMoveTweenFinish);

			_labelAddMoves.gameObject.SetActive(false);

			///时间
			_labelLeftTime = transform.Find("FightTop/Sprite_Time/Sprite_Clock/Label_Clock").GetComponent<UILabel>();
			_timer = TimerManager.GetTimer(this.gameObject);

			int  min = (this.moduleFight.TimeSec % 3600) / 60;
			int second = (this.moduleFight.TimeSec % 3600) % 60;
			_labelLeftTime.text = string.Concat(min.ToString("d2"), ":", second.ToString("d2")); 

			#endregion

			_goFightfinish = transform.Find("Effect/Label_Finish").gameObject;
		}

		private void OnEnable()
		{
            EventDispatcher.AddListener<int, int>(MyBossTakeDamage, OnFightDataCBK);
            EventDispatcher.AddListener<int,int>("CLIENT_ATTACK",OnClientAttackBoss);

			EventDispatcher.AddListener<int>(BossCreateEvent,OnBossCreate);
			EventDispatcher.AddListener<PlayerBase>(BossKillEvent,OnBossDied);
			EventDispatcher.AddListener<PlayerBase>(BossDiedFlyCoinEvent,OnBossDiedFlyCoin);

			EventDispatcher.AddListener(BeginStartTime,OnBeginStartTime);
			EventDispatcher.AddListener<int,bool>(AddMoves,PlayAddMoveEffect);
		}

		void OnDisable()
		{
            EventDispatcher.RemoveListener<int,int>(MyBossTakeDamage, OnFightDataCBK);
            EventDispatcher.RemoveListener<int,int>("CLIENT_ATTACK", OnClientAttackBoss);
			EventDispatcher.RemoveListener<int>(BossCreateEvent,OnBossCreate);
			EventDispatcher.RemoveListener<PlayerBase>(BossDiedFlyCoinEvent,OnBossDiedFlyCoin);
			EventDispatcher.RemoveListener<PlayerBase>(BossKillEvent,OnBossDied);
			EventDispatcher.RemoveListener(BeginStartTime,OnBeginStartTime);
			EventDispatcher.RemoveListener<int,bool>(AddMoves,PlayAddMoveEffect);
		}

		private void OnFightDataCBK(int bossId,int bossHp)
		{
//			_spriteMyHp.fillAmount = (float)moduleFight.MyCurrentTotalHp / moduleFight.MyOriTotalHp;
//			if(_spriteMyHp.fillAmount<=0.01f)
//			{
//				_spriteMyHpTop.gameObject.SetActive(false);
//			}
//			else
//			{
//				_spriteMyHpTop.transform.localPosition = new Vector3(0.7f,-94.8f + _spriteMyHp.fillAmount * 95.2f);
//			}

			if(bossId != moduleFight.CurrentMyBossId || bossHp < 0) return;

			_sliderMyBoss.value = (float)bossHp / moduleFight.CurrentMyBoss.OriHp;

//			if(moduleFight.MyCurrentTotalHp <= MyBoss2Hp + MyBoss3Hp)
//				_spriteOppPrize1.spriteName = "14";
//			else if(moduleFight.MyCurrentTotalHp <=  MyBoss3Hp)
//				_spriteOppPrize2.spriteName = "14";
//			else if(moduleFight.MyCurrentTotalHp <= 0)
//				_spriteOppPrize3.spriteName = "14";

			_labelMyHp.text = moduleFight.MyCurrentTotalHp.ToString();

			//血量低于20%，弹提示
			if(_sliderMyBoss.value <= 0.2f)
			{
				ShowBlodeNote();
			}
		}

		private bool _hasDoAi = false;
		private void OnClientAttackBoss(int bossId,int currentHp)
		{
			if(bossId != moduleFight.CurrentOppBossId) return;
			moduleFight.CurrentOppBoss.CurrentHp = currentHp;
			_sliderOppBoss.value = ((float)(moduleFight.CurrentOppBoss.CurrentHp)) / moduleFight.CurrentOppBoss.OriHp;
//			_spriteOpponentHp.fillAmount = (float)moduleFight.OpponentCurrentTotalHp / moduleFight.OpponentOriTotalHp;
//			if(_spriteOpponentHp.fillAmount<=0.01f)
//			{
//				_spriteOpponentHpTop.gameObject.SetActive(false);
//			}
//			else
//			{
//				_spriteOpponentHpTop.transform.localPosition = new Vector3(0.7f,-94.8f + _spriteOpponentHp.fillAmount * 95.2f);
//			}
			_labelOpponentHp.text = moduleFight.OpponentCurrentTotalHp.ToString();

			if(Fight.LevelProfile.NewCurrentLevelProfile.Level == 4 && !_hasDoAi && _sliderOppBoss.value < 0.5f)
			{
				_hasDoAi = true;
				//模拟发一次攻击
				FightMgr.Instance.ModuleFight.TakeDamageMySelf(moduleFight.CurrentMyBossId,moduleFight.CurrentMyBoss.RealCurrentHp,0,(int)AttackType.SKILL_1,1002,0,0);
			}
		}


	   	private void OnBossCreate(int bossIndex)
		{
			GameObject goEffect = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("BossCreate");
			goEffect.transform.SetParent(_goBossPos[bossIndex].transform,false);
			_goBossPos[bossIndex].SetActive(true);
			//MusicManager.Instance.PlaySoundEff("Music/CreateBoss");

			for(int i = 0;i < FieldMgr.EnemyRoot.childCount;i++)
			{
				FieldMgr.EnemyRoot.GetChild(i).gameObject.SetActive(true);
			}
		}

		private void OnBossDiedFlyCoin(PlayerBase diePlayer)
		{
		}

		private void GetFlyPos_FromAndTo(PlayerBase diePlayer,ref Transform fromPos,ref Transform toPos)
		{
			if(FightMgr.Instance.CurrentMyBoss == diePlayer)
			{
				fromPos = FieldMgr.MyBossPosEffect;
				toPos = FieldMgr.OppBossPosEffect;
			}
			else
			{
				toPos = FieldMgr.MyBossPosEffect;
				fromPos = FieldMgr.OppBossPosEffect;
			}
		}

		private void OnBossDied(PlayerBase diePlayer)
		{
			GameObject goEffect = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("BossCreate");
			goEffect.transform.SetParent(diePlayer.transform,false);
//			_goBossPos[bossIndex].transform.DOScale(Vector3.one * 0.001f,1f);

			_goFightfinish.SetActive(true);
			Destroy(_goFightfinish, 0.8f);

			Transform fromPos = null;
			Transform toPos = null;
			GetFlyPos_FromAndTo(diePlayer,ref fromPos,ref toPos);

			float totalDalay = 0.1f;
			EnergyMgr.Instance.PrizeAnimationCount = 0;
			EnergyMgr.Instance.AddPrize(1,fromPos,toPos,totalDalay);
			EnergyMgr.Instance.AddJiangBei(12,fromPos,toPos,totalDalay);
			EnergyMgr.Instance.AddCoin(20,fromPos,toPos,totalDalay);

//			_goPrizeFly.transform.position = diePlayer.transform.position;
//			_goPrizeFly.transform.localScale = Vector3.one;
//			_goPrizeFly.SetActive(true);

//			Tweener tween = null;
//			UISprite changeSprite = null;
//			if(bossIndex == 0)
//			{
//				tween = _goPrizeFly.transform.DOMove(targetPlayer.transform.position,1.5f);
//				changeSprite = _spriteMyPrize1;
//			}
//			else if(bossIndex == 1)
//			{
//				tween = _goPrizeFly.transform.DOMove(_spriteMyPrize2.transform.position,1.5f);
//				changeSprite = _spriteMyPrize2;
//			}
//			else if(bossIndex == 2)
//			{
//				tween = _goPrizeFly.transform.DOMove(_spriteMyPrize3.transform.position,1.5f);
//				changeSprite = _spriteMyPrize3;
//			}
//			Tweener tweenScale = _goPrizeFly.transform.DOScale(new Vector3(0.33f,0.315f,1f),1.5f);
//			tweenScale.SetEase(Ease.InElastic);
//
//			tween.OnComplete(()=>{
//				_goPrizeFly.SetActive(false);
//			//	changeSprite.spriteName = "icon-14a";
//			});



			//回复步数
//			int step = FightMgr.Instance.movesCount;
//			step = Mathf.Min(step + 5,MAX_MOVES);
//			if(step - FightMgr.Instance.movesCount > 0)
//			{
//				PlayAddMoveEffect(step - FightMgr.Instance.movesCount);
//				FightMgr.Instance.movesCount = step;
//			}

			MusicManager.Instance.PlaySoundEff("Music/KillBoss");
		}

		protected override void OnUpdate(float deltaTime)
		{
			if(FightMgr.Instance.isPlaying)
			{
				if(_oriLeftMoves != FightMgr.Instance.movesCount)
				{
					_oriLeftMoves = FightMgr.Instance.movesCount;
					_labelLeftMoves.text = _oriLeftMoves.ToString();
				}

				if(_currentEnergy != (int)moduleFight.CurrentMyBoss.Energy)
				{
					_currentEnergy = (int)moduleFight.CurrentMyBoss.Energy;
					_labelEnergy.text = string.Format("能量：{0}",_currentEnergy);
				}
			}

//			if(Input.GetKeyDown(KeyCode.Space))
//			{
//				OnBossDied(FightMgr.Instance.CurrentOppBoss);
//			}
//			else if(Input.GetKeyDown(KeyCode.A))
//			{
//				OnBossDied(FightMgr.Instance.CurrentMyBoss);
//			}

			#if MinDamage
			if(Input.GetKeyDown(KeyCode.A))
			{
				SkillData skillData = new SkillData();
				skillData.BossId = moduleFight.CurrentOppBossId;
				skillData.Damage = 10;
				skillData.SkillType = SkillTable.SkillType.GongZhuRandomNormal;

				FightMgr.Instance.CurrentMyBoss.SwithchState(StateDef.Attack,skillData);
			}
			#endif
		}


		#region 时间
		private void OnBeginStartTime()
		{
			//显示提示
//			_transInitNote.localScale = Vector3.zero;
//			_transInitNote.DOScale(new Vector3(1f,1f,1f),0.3f);
//			_transInitNote.gameObject.SetActive(true);

			StartTimer((uint)moduleFight.TimeSec);
		}

		private void StartTimer(uint leftSecs)
		{
			_timer.StartTimer(leftSecs,OnEverySecCallBack,OnFinishTimer);
		}

		private void OnEverySecCallBack(uint leftSecs)
		{
			if(!FightMgr.Instance.IsFighting) return;

			_passedTime++;
			if(_passedTime == FIRST_RECOCER_TIME)
			{
				ShowNoteMsg("步数回复速度加快");
			}
			SetLeftTime(leftSecs);
		}

		private void OnFinishTimer()
		{
			if(!FightMgr.Instance.IsFighting) return;

			DebugUtil.Info("OnFinishTimer");

			uint leftSecs = _timer.GetCurrentTime();
			SetLeftTime(leftSecs);
			//Invoke(
		}

		/// <summary>
		/// 第一阶段，回一步时间
		/// </summary>
		private const uint RECOVER_HP_TIME_FIRST = 6;
		/// <summary>
		/// 第二阶段，回一步时间
		/// </summary>
		private const uint RECOVER_HP_TIME_SECOND = 3;
		/// <summary>
		/// 第一阶段持续时间/秒
		/// </summary>
		private const int FIRST_RECOCER_TIME = 2*30;
		/// <summary>
		/// 步数自动回复上限
		/// </summary>
		private const int MAX_MOVES = 8;
		/// <summary>
		/// 经过的时间
		/// </summary>
		private uint _passedTime;

		private void SetLeftTime(uint leftSecs)
		{
			uint  min = (leftSecs % 3600) / 60;
			uint second = (leftSecs % 3600) % 60;
			_labelLeftTime.text = string.Concat(min.ToString("d2"), ":", second.ToString("d2")); 

			if(_passedTime % BossData.RecoverEnergyTime == 0)
			{
				//每7秒回复能量
//				moduleFight.AddEnergy(E_AddEnergyType.Recovery);
				EnergyMgr.Instance.AddEnergySmall(_labelLeftTime.transform,E_AddEnergyType.Recovery);

			}

			uint targetTime = _passedTime > FIRST_RECOCER_TIME ? RECOVER_HP_TIME_SECOND : RECOVER_HP_TIME_FIRST;
			//5秒回复1步
			_passSec++;
			if(_passSec >= targetTime)
			{
				_passSec = 0;
				//超过最高步数，不加
				if(FightMgr.Instance.movesCount >= MAX_MOVES)
					return;
				
				PlayAddMoveEffect(1,false);
			}
		}

		private void PlayAddMoveEffect(int addMove,bool isForce)
		{
			if(!isForce && FightMgr.Instance.movesCount >=  MAX_MOVES)
			{
				return;
			}

			if(!isForce)
			{
				addMove = Mathf.Min(addMove, MAX_MOVES - FightMgr.Instance.movesCount);
			}
			addMoves += addMove;

			_labelAddMoves.text = string.Format("+{0}",addMoves);
			_labelAddMoves.gameObject.SetActive(true);
			Color color = _labelAddMoves.color;
			color.a = 1f;
			_labelAddMoves.color = color;

			_tweenAddMoveAlpha.Rewind();
			_tweenAddMovePos.Rewind();
			_tweenAddMovePos.PlayForward();
			_tweenAddMoveAlpha.PlayForward();
		}


		private Color GetTweenColor()
		{
			return _labelAddMoves.color;
		}
		private void SetTweenColor(Color color)
		{
			_labelAddMoves.color = color;
		}

		private int addMoves = 0;
		private void OnMoveTweenFinish()
		{
			//5秒回复1步
			FightMgr.Instance.movesCount += addMoves;
			addMoves = 0;
			_oriLeftMoves = FightMgr.Instance.movesCount;
			_labelLeftMoves.text = _oriLeftMoves.ToString();

			_tweenAddMoveAlpha.Rewind();
			_tweenAddMovePos.Rewind();
			_labelAddMoves.gameObject.SetActive(false);

			goMoveMask.gameObject.SetActive(false);
		}

		public void CheckToShowMoveMask()
		{
			//5秒回复1步
			uint targetTime = _passedTime > FIRST_RECOCER_TIME ? RECOVER_HP_TIME_SECOND : RECOVER_HP_TIME_FIRST;
			int leftTime = (int)targetTime - _passSec;
			if(leftTime > 1f)
			{
				goMoveMask.Show(leftTime);
				goMoveMask.gameObject.SetActive(true);
				FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);		
			}
		}
		#endregion

		public override void OnDestroy()
		{
			base.OnDestroy();

			_tweenAddMovePos.Kill();
			_tweenAddMovePos = null;

			_tweenAddMoveAlpha.Kill();
			_tweenAddMoveAlpha = null;
		}

		protected override void OnBtnClick(GameObject go)
		{
			if(go.name == "Sprite_FightIntroFrame")
			{
//				DebugUtil.Debug("notemask clicked");
				go.transform.parent.gameObject.SetActive(false);
			}
			else if(go.name == "FaceBtn1")
			{
				EventDispatcher.TriggerEvent<int>(UIMyFace.OnFaceBtnClickEvent,0);
			}
			else if(go.name == "FaceBtn2")
			{
				EventDispatcher.TriggerEvent<int>(UIMyFace.OnFaceBtnClickEvent,1);
			}
			else if(go.name == "FaceBtn3")
			{
				EventDispatcher.TriggerEvent<int>(UIMyFace.OnFaceBtnClickEvent,2);
			}
			else if(go.name == "FaceBtn4")
			{
				EventDispatcher.TriggerEvent<int>(UIMyFace.OnFaceBtnClickEvent,3);
			}
		}

		private UILabel _labelNoteMsg;
		private TweenPosition _tweenPosNoteMsg;
		private TweenAlpha _tweenAlphaNoteMsg;
		/// <summary>
		/// 显示提示内容
		/// </summary>
		/// <param name="msg">Message.</param>
		public void ShowNoteMsg(string msg)
		{
			
			_labelNoteMsg.text = msg;

			_tweenPosNoteMsg.ResetToBeginning();
			_tweenAlphaNoteMsg.ResetToBeginning();

			_tweenPosNoteMsg.gameObject.SetActive(true);
			_tweenPosNoteMsg.enabled = true;
			_tweenAlphaNoteMsg.enabled = true;

			_tweenPosNoteMsg.PlayForward();
			_tweenAlphaNoteMsg.PlayForward();
		}

		public void ShowBlodeNote()
		{
			_tweenBloodNote.ResetToBeginning();
			_goBloodNote.SetActive(true);
			_tweenBloodNote.enabled = true;
			_tweenBloodNote.PlayForward();
		}

	}
}
