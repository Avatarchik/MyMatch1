using UnityEngine;
using System.Collections;
using MyFrameWork;
using DG.Tweening;

namespace FightNew
{
	public class SkillBtn : MonoBehaviour 
	{
		public int BtnIndex{get;set;}

//		public int SkillId{get;set;}

		public SkillModule Skill{get;set;}

		private UILabel _labelNeedEnergy;

		private UISprite _spriteIcon;

		private GameObject _goSelBtn;
		private GameObject _goNotNeedEnergy;

		public const string RefreshEnergy = "RefreshEnergy";

		[SerializeField]
		private Vector3 _oriPos;
		[SerializeField]
		private Vector3 _targetPos;
		[SerializeField]
		private float _oriScale;
		[SerializeField]
		private float swapDuration = 0.5f;
		[SerializeField]
		private Vector3 normal;
		[SerializeField]
		private float _delayDisplay = 0f;

		void Awake()
		{
			Transform transBtn = this.transform.Find("SkillBtn");
			_spriteIcon = transBtn.GetComponent<UISprite>();
			UIButton btn = transBtn.GetComponent<UIButton>();
			btn.onClick.Add(new EventDelegate(OnBtnClick));
//			btn.GetComponent<Collider>()..Add(new EventDelegate(OnBtnClick));
			UIEventListener listener = UIEventListener.Get(btn.gameObject);
			listener.onPress = OnBtnPress;

			_labelNeedEnergy = transBtn.Find("SpriteEnergy/Label").GetComponent<UILabel>();

			_goSelBtn = transform.Find("SpriteBgSel").gameObject;
			_goNotNeedEnergy = transform.Find("SpriteNotEnoughEnergy").gameObject;
		}

		private bool _canMove = false;
		private float _timeAdd = 0f;
		private float _timeTemp = 0f;
		private float _progress = 0f;

		private Tween _tweenPos;
		private Tween _tweenScale;

		void Update()
		{

			if(_canMove)
			{
				if(_timeAdd >= _delayDisplay)
				{
					_timeTemp = EasingFunctions.easeInOutQuad(_progress / swapDuration);

//					if(this.gameObject.name == "SkillBtn1")
//					{
//						Debug.Log("1:" + this.transform.localPosition);
//					}
					this.transform.localPosition = Vector3.Lerp(_oriPos,_targetPos,_timeTemp) + normal * 30 * Mathf.Sin(3.14f * _timeTemp);
//					if(this.gameObject.name == "SkillBtn1")
//					{
//						Debug.Log("2:" + this.transform.localPosition);
//					}

					this.transform.localScale = Vector3.Lerp(Vector3.one * _oriScale,Vector3.one,_timeTemp);
				

					if(Mathf.Abs(this.transform.localPosition.x - _targetPos.x) <= 1f
						&& Mathf.Abs(this.transform.localPosition.y - _targetPos.y) <= 1f)
					{
						this.transform.localPosition = _targetPos;
						this.transform.localScale = Vector3.one;
						_canMove = false;
						_timeAdd = 0f;
						_timeTemp = 0f;
						_progress = 0f;
					}

					_progress += Time.deltaTime;
				}

				_timeAdd += Time.deltaTime;
			}
		}

		private void OnEnable()
		{
			EventDispatcher.AddListener<int>(RefreshEnergy, OnRefreshEnergy);
		}

		void OnDisable()
		{
			//			Debug.LogError("On disable");
			EventDispatcher.RemoveListener<int>(RefreshEnergy, OnRefreshEnergy);
		}

		private void OnBtnClick()
		{
			DebugUtil.Info("skill_" + Skill.Id + " clicked");

			FightMgr.Instance.TriggerSkill(Skill.Id);

			EventDispatcher.TriggerEvent<GameObject>(EnergyLightMgr.CloseSkillMask, null);
			_canMove = false;
		}

		private bool _pressState = false;

		private void OnBtnPress(GameObject obj,bool state)
		{
			if(_pressState == state)
				return;

			_pressState = state;

			_goSelBtn.SetActive(_pressState);
		}

		public void SetSkillId(int skillId)
		{
			Skill = SkillModuleMgr.Instance.GetSkill(skillId);
//			DebugUtil.Info("sprite Icon:" + Skill.SkillIcon);
			_spriteIcon.spriteName = Skill.SkillIcon;
			_labelNeedEnergy.text = Skill.NeedEnergy.ToString();

			_goSelBtn.SetActive(false);
			_goNotNeedEnergy.SetActive(FightMgr.Instance.CurrentBossEnergy < Skill.NeedEnergy);

			this.transform.localScale = Vector3.one * _oriScale;
			this.transform.localPosition = _oriPos;
			_canMove = true;
			_timeAdd = 0f;
			_timeTemp = 0f;
			_progress = 0f;

//			_tweenPos = this.transform.DOLocalPath(new Vector3[]{normal * 30,_targetPos},3f,PathType.Linear,PathMode.TopDown2D,10,Color.red);//.DOLocalJump(_targetPos,0.4f,1,0.4f);//(_targetPos,1f); 
//			_tweenPos.SetEase(Ease.InSine);
//			_tweenPos.SetDelay(_delayDisplay);
//			_tweenPos.SetAutoKill(true);
//
//			_tweenScale = this.transform.DOScale(Vector3.one,0.4f); 
//			_tweenScale.SetEase(Ease.InBack);
//			_tweenPos.Play();
		}

		private void OnRefreshEnergy(int currentEnergy)
		{
			_goNotNeedEnergy.SetActive(currentEnergy < Skill.NeedEnergy);
		}
	}
}