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

namespace Fight
{
	public class UIFight : BaseUI 
	{
		public const int MyBoss1Hp = 150;
		public const int MyBoss2Hp = 200;
		public const int MyBoss3Hp = 300;

        public const string BossAttacked = "BossAttacked";
		public const string BossCreateEvent = "BossCreate";
		public const string BossKillEvent = "BossKilled";
		public const string BeginStartTime = "BeginStartTime";

		private GameObject[] _goBossPos;

		private UISprite _spriteMyPrize1;
		private UISprite _spriteMyPrize2;
		private UISprite _spriteMyPrize3;

		private UISprite _spriteOppPrize1;
		private UISprite _spriteOppPrize2;
		private UISprite _spriteOppPrize3;

		private GameObject _goPrizeFly;


//		private UISlider _sliderMy;
//		private UISlider _sliderOpponent;

		#region 血条
		private UISprite _spriteMyHp;
		private UISprite _spriteMyHpTop;
		private UISprite _spriteOpponentHp;
		private UISprite _spriteOpponentHpTop;
		private UILabel _labelMyHp;
		private UILabel _labelOpponentHp;

		#endregion

		private Dictionary<int,UISlider> _dicSliderBoss;

		private UILabel _labelMyName;
		private UILabel _labelOppenentName;

		#region 提示
		private Transform _transInitNote;
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
		#endregion

		private ModuleFight _moduleFight;
		private ModuleFight moduleFight
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
			_spriteMyHp = transform.Find("FightTop/Hp/Container_My/Sprite_Frame/Sprite_Bottle/Sprite_Blood").GetComponent<UISprite>();
			_spriteOpponentHp = transform.Find("FightTop/Hp/Container_Oppo/Sprite_Frame/Sprite_Bottle/Sprite_Blood").GetComponent<UISprite>();
			_spriteMyHpTop = transform.Find("FightTop/Hp/Container_My/Sprite_Frame/Sprite_Bottle/Sprite_Blood_Lid").GetComponent<UISprite>();
			_spriteOpponentHpTop = transform.Find("FightTop/Hp/Container_Oppo/Sprite_Frame/Sprite_Bottle/Sprite_Blood_Lid").GetComponent<UISprite>();

			_labelMyHp = transform.Find("FightTop/Hp/Container_My/Sprite_Frame/Sprite_Bottle/Label_Blood").GetComponent<UILabel>();
			_labelOpponentHp = transform.Find("FightTop/Hp/Container_Oppo/Sprite_Frame/Sprite_Bottle/Label_Blood").GetComponent<UILabel>();

			_spriteMyPrize1 = transform.Find("FightTop/BossStatus/Container_My/Sprite1").GetComponent<UISprite>();
			_spriteMyPrize2 = transform.Find("FightTop/BossStatus/Container_My/Sprite2").GetComponent<UISprite>();
			_spriteMyPrize3 = transform.Find("FightTop/BossStatus/Container_My/Sprite3").GetComponent<UISprite>();

			_spriteMyPrize1.spriteName = "plan-22";
			_spriteMyPrize2.spriteName = "plan-22";
			_spriteMyPrize3.spriteName = "plan-22";

			_spriteOppPrize1 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite1").GetComponent<UISprite>();
			_spriteOppPrize2 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite2").GetComponent<UISprite>();
			_spriteOppPrize3 = transform.Find("FightTop/BossStatus/Container_Oppo/Sprite3").GetComponent<UISprite>();

			_spriteOppPrize1.spriteName = "plan-22";
			_spriteOppPrize2.spriteName = "plan-22";
			_spriteOppPrize3.spriteName = "plan-22";

			_dicSliderBoss = new Dictionary<int, UISlider>();
			UISlider sliderBoss = null;

			#if UNITY_EDITOR
			if(!GameEntrance.Instance.IsTestFight)
			{
				for(int i = 0;i < moduleFight.OpponentBossId.Length;i++)
				{
					sliderBoss = transform.Find(string.Format("SlotAll/Slot{0}/BossPos/BossHpSlider",i+1)).GetComponent<UISlider>();
					sliderBoss.value = 1f;
					_dicSliderBoss.Add(moduleFight.OpponentBossId[i],sliderBoss);
				}

				_labelMyName = transform.Find("FightTop/PlayerName/SpriteMyName/LabelMyName").GetComponent<UILabel>();
				_labelOppenentName = transform.Find("FightTop/PlayerName/SpriteOppoName/LabelOppoName").GetComponent<UILabel>();

	            object[] o = Util.CallMethod("UIMainModule", "GetUserNick");
				_labelMyName.text = o[0].ToString();

				o = Util.CallMethod("UIMainModule", "GetOpponentNick");
				_labelOppenentName.text = o[0].ToString();
			}
			else
			{

				_labelMyName = transform.Find("FightTop/PlayerName/SpriteMyName/LabelMyName").GetComponent<UILabel>();
				_labelOppenentName = transform.Find("FightTop/PlayerName/SpriteOppoName/LabelOppoName").GetComponent<UILabel>();

				sliderBoss = transform.Find(string.Format("SlotAll/Slot{0}/BossPos/BossHpSlider",1)).GetComponent<UISlider>();
				sliderBoss.value = 1f;
				_dicSliderBoss.Add(1,sliderBoss);
			}
#else
			for(int i = 0;i < moduleFight.OpponentBossId.Length;i++)
			{
				sliderBoss = transform.Find(string.Format("SlotAll/Slot{0}/BossPos/BossHpSlider",i+1)).GetComponent<UISlider>();
				sliderBoss.value = 1f;
				_dicSliderBoss.Add(moduleFight.OpponentBossId[i],sliderBoss);
			}

			_labelMyName = transform.Find("FightTop/PlayerName/SpriteMyName/LabelMyName").GetComponent<UILabel>();
			_labelOppenentName = transform.Find("FightTop/PlayerName/SpriteOppoName/LabelOppoName").GetComponent<UILabel>();

			object[] o = Util.CallMethod("UIMainModule", "GetUserNick");
			_labelMyName.text = o[0].ToString();

			o = Util.CallMethod("UIMainModule", "GetOpponentNick");
			_labelOppenentName.text = o[0].ToString();

#endif

            _labelNoteMsg = transform.Find("FightExtra/LabelNoteMsg").GetComponent<UILabel>();
			GameObject goTweenNote = transform.Find("FightExtra").gameObject;
			_tweenPosNoteMsg = goTweenNote.GetComponent<TweenPosition>();
			_tweenAlphaNoteMsg = goTweenNote.GetComponent<TweenAlpha>();
			goTweenNote.SetActive(false);


			_spriteMyHp.fillAmount = 1f;
			_spriteOpponentHp.fillAmount = 1f;
			_spriteMyHpTop.transform.localPosition = Vector3.up * -0.4f;
			_spriteOpponentHpTop.transform.localPosition = Vector3.up * -0.4f;

			_goBossPos = new GameObject[3];
			_goBossPos[0] = transform.Find("SlotAll/Slot1/BossPos").gameObject;
			_goBossPos[1] = transform.Find("SlotAll/Slot2/BossPos").gameObject;
			_goBossPos[2] = transform.Find("SlotAll/Slot3/BossPos").gameObject;

			#if UNITY_EDITOR
			if(!GameEntrance.Instance.IsTestFight)
			{
				_labelMyHp.text = moduleFight.MyCurrentTotalHp.ToString();
				_labelOpponentHp.text = moduleFight.OpponentCurrentTotalHp.ToString();
			}
			#else
				_labelMyHp.text = moduleFight.MyCurrentTotalHp.ToString();
				_labelOpponentHp.text = moduleFight.OpponentCurrentTotalHp.ToString();
			#endif

			_goPrizeFly = transform.Find("FightTop/SpritePrize").gameObject;
			_goPrizeFly.SetActive(false);


			_transInitNote = transform.Find("InitNote/Sprite_FightIntroFrame");

			#region 时间／步数／技能
			///步数
			_labelLeftMoves = transform.Find("FightTop/Container_Centre/Sprite_Frame/Sprite_Scarf/Sprite_Energy/Label_Energy").GetComponent<UILabel>();
			_labelAddMoves = transform.Find("FightTop/Container_Centre/Sprite_Frame/Sprite_Scarf/Sprite_Energy/Label_AddEnergy").GetComponent<UILabel>();

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
			_labelLeftTime = transform.Find("FightTop/Container_Centre/Sprite_Frame/Sprite_Clock/Label_Clock").GetComponent<UILabel>();
			_timer = TimerManager.GetTimer(this.gameObject);

			int  min = (this.moduleFight.TimeSec % 3600) / 60;
			int second = (this.moduleFight.TimeSec % 3600) % 60;
			_labelLeftTime.text = string.Concat(min.ToString("d2"), ":", second.ToString("d2")); 

			#endregion
		}

		private void OnEnable()
		{
            EventDispatcher.AddListener<int, int>(BossAttacked, OnFightDataCBK);
            EventDispatcher.AddListener<int,int>("CLIENT_ATTACK",OnClientAttackBoss);

            EventDispatcher.AddListener<int>(BossCreateEvent,OnBossCreate);
			EventDispatcher.AddListener<int>(BossKillEvent,OnBossDied);

			EventDispatcher.AddListener(BeginStartTime,OnBeginStartTime);
		}

		void OnDisable()
		{
            EventDispatcher.RemoveListener<int,int>(BossAttacked, OnFightDataCBK);
            EventDispatcher.RemoveListener<int,int>("CLIENT_ATTACK", OnClientAttackBoss);
			EventDispatcher.RemoveListener<int>(BossCreateEvent,OnBossCreate);
			EventDispatcher.RemoveListener<int>(BossKillEvent,OnBossDied);
			EventDispatcher.RemoveListener(BeginStartTime,OnBeginStartTime);
		}

		private void OnFightDataCBK(int bossId,int bossHp)
		{
			_spriteMyHp.fillAmount = (float)moduleFight.MyCurrentTotalHp / moduleFight.MyOriTotalHp;
			if(_spriteMyHp.fillAmount<=0.01f)
			{
				_spriteMyHpTop.gameObject.SetActive(false);
			}
			else
			{
				_spriteMyHpTop.transform.localPosition = new Vector3(0.7f,-94.8f + _spriteMyHp.fillAmount * 95.2f);
			}



			if(moduleFight.MyCurrentTotalHp <= MyBoss2Hp + MyBoss3Hp)
				_spriteOppPrize1.spriteName = "14";
			else if(moduleFight.MyCurrentTotalHp <=  MyBoss3Hp)
				_spriteOppPrize2.spriteName = "14";
			else if(moduleFight.MyCurrentTotalHp <= 0)
				_spriteOppPrize3.spriteName = "14";

			_labelMyHp.text = moduleFight.MyCurrentTotalHp.ToString();
		}

		private void OnClientAttackBoss(int bossId,int currentHp)
		{
			_dicSliderBoss[bossId].value = (float)currentHp / moduleFight.DicOriOpponentBoss[bossId];
			_spriteOpponentHp.fillAmount = (float)moduleFight.OpponentCurrentTotalHp / moduleFight.OpponentOriTotalHp;
			if(_spriteOpponentHp.fillAmount<=0.01f)
			{
				_spriteOpponentHpTop.gameObject.SetActive(false);
			}
			else
			{
				_spriteOpponentHpTop.transform.localPosition = new Vector3(0.7f,-94.8f + _spriteOpponentHp.fillAmount * 95.2f);
			}
			_labelOpponentHp.text = moduleFight.OpponentCurrentTotalHp.ToString();
		}


	   	private void OnBossCreate(int bossIndex)
		{
			GameObject goEffect = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("BossCreate");
			goEffect.transform.SetParent(_goBossPos[bossIndex].transform,false);
			_goBossPos[bossIndex].SetActive(true);
			MusicManager.Instance.PlaySoundEff("Music/CreateBoss");

		}

		private void OnBossDied(int bossIndex)
		{
			GameObject goEffect = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("BossCreate");
			goEffect.transform.SetParent(_goBossPos[bossIndex].transform,false);
			_goBossPos[bossIndex].transform.DOScale(Vector3.one * 0.001f,1f);
			_goPrizeFly.transform.position = _goBossPos[bossIndex].transform.position;
			_goPrizeFly.transform.localScale = Vector3.one;
			_goPrizeFly.SetActive(true);

			Tweener tween = null;
			UISprite changeSprite = null;
			if(bossIndex == 0)
			{
				tween = _goPrizeFly.transform.DOMove(_spriteMyPrize1.transform.position,1.5f);
				changeSprite = _spriteMyPrize1;
			}
			else if(bossIndex == 1)
			{
				tween = _goPrizeFly.transform.DOMove(_spriteMyPrize2.transform.position,1.5f);
				changeSprite = _spriteMyPrize2;
			}
			else if(bossIndex == 2)
			{
				tween = _goPrizeFly.transform.DOMove(_spriteMyPrize3.transform.position,1.5f);
				changeSprite = _spriteMyPrize3;
			}
			Tweener tweenScale = _goPrizeFly.transform.DOScale(new Vector3(0.33f,0.315f,1f),1.5f);
			tweenScale.SetEase(Ease.InElastic);

			tween.OnComplete(()=>{_goPrizeFly.SetActive(false);changeSprite.spriteName = "icon-14a";});

			int step = SessionControl.Instance.movesCount;
			step = Mathf.Min(step + 5,MAX_MOVES);
			if(step - SessionControl.Instance.movesCount > 0)
			{
				PlayAddMoveEffect(step - SessionControl.Instance.movesCount);
				SessionControl.Instance.movesCount = step;
			}

			MusicManager.Instance.PlaySoundEff("Music/KillBoss");
		}

		protected override void OnUpdate(float deltaTime)
		{
			if(SessionControl.Instance.isPlaying
				&&_oriLeftMoves != SessionControl.Instance.movesCount)
			{
				_oriLeftMoves = SessionControl.Instance.movesCount;
				_labelLeftMoves.text = _oriLeftMoves.ToString();
			}
		}


		#region 时间
		private void OnBeginStartTime()
		{
			_transInitNote.localScale = Vector3.zero;
			_transInitNote.DOScale(new Vector3(1f,1f,1f),0.3f);
			_transInitNote.gameObject.SetActive(true);
			StartTimer((uint)moduleFight.TimeSec);
		}

		private void StartTimer(uint leftSecs)
		{
			_timer.StartTimer(leftSecs,OnEverySecCallBack,OnFinishTimer);
		}

		private void OnEverySecCallBack(uint leftSecs)
		{
			_passedTime++;
			SetLeftTime(leftSecs);
		}

		private void OnFinishTimer()
		{
			DebugUtil.Info("OnFinishTimer");

			uint leftSecs = _timer.GetCurrentTime();
			SetLeftTime(leftSecs);
			//Invoke(
		}

		/// <summary>
		/// 前两分钟，每6秒回一步
		/// </summary>
		private const uint RECOVER_HP_TIME_FIRST = 7;
		/// <summary>
		/// 两分钟后，每3秒回一步
		/// </summary>
		private const uint RECOVER_HP_TIME_SECOND = 3;
		/// <summary>
		/// 前几分钟回步数
		/// </summary>
		private const int FIRST_RECOCER_TIME = 2*60;
		/// <summary>
		/// 最高步数
		/// </summary>
		private const int MAX_MOVES = 10;
		/// <summary>
		/// 经过的时间
		/// </summary>
		private uint _passedTime;

		private void SetLeftTime(uint leftSecs)
		{
			uint  min = (leftSecs % 3600) / 60;
			uint second = (leftSecs % 3600) % 60;
			_labelLeftTime.text = string.Concat(min.ToString("d2"), ":", second.ToString("d2")); 

			uint targetTime = _passedTime > FIRST_RECOCER_TIME ? RECOVER_HP_TIME_SECOND : RECOVER_HP_TIME_FIRST;
			//5秒回复1步
			_passSec++;
			if(_passSec >= targetTime)
			{
				_passSec = 0;
				//超过最高步数，不加
				if(SessionControl.Instance.movesCount >= MAX_MOVES)
					return;
				
				PlayAddMoveEffect(1);
			}
		}

		private void PlayAddMoveEffect(int addMove)
		{
			_labelAddMoves.text = string.Format("+{0}",addMove);
			_labelAddMoves.gameObject.SetActive(true);
			Color color = _labelAddMoves.color;
			color.a = 1f;
			_labelAddMoves.color = color;
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

		private void OnMoveTweenFinish()
		{
			//5秒回复1步
			if(SessionControl.Instance.movesCount < MAX_MOVES)
			{
				SessionControl.Instance.movesCount++;
			}

			_oriLeftMoves = SessionControl.Instance.movesCount;
			_labelLeftMoves.text = _oriLeftMoves.ToString();

			_tweenAddMoveAlpha.Rewind();
			_tweenAddMovePos.Rewind();
			_labelAddMoves.gameObject.SetActive(false);
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
	}
}
