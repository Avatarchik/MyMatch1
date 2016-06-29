using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class EnergyLightMgr : MonoBehaviour 
	{
		public const string CloseSkillMask = "CloseSkillMask";
		private GameObject _goSkillNoteParticle;

		private Transform _transSkillBgCircle;

		void Awake()
		{
			_goSkill = transform.Find("Skill").gameObject;

			SkillBtn skillBtn1 =  transform.Find("Skill/SkillBtn1").GetComponent<SkillBtn>();
			SkillBtn skillBtn2 =  transform.Find("Skill/SkillBtn2").GetComponent<SkillBtn>();
			SkillBtn skillBtn3 =  transform.Find("Skill/SkillBtn3").GetComponent<SkillBtn>();
			SkillBtn skillBtn4 =  transform.Find("Skill/SkillBtn4").GetComponent<SkillBtn>();
//			SkillBtn skillBtn5 =  transform.Find("Skill/SkillBtn5").GetComponent<SkillBtn>();
			_skillBtns = new SkillBtn[]{skillBtn1,skillBtn2,skillBtn3,skillBtn4};

			Collider maskCollider = transform.Find("TextureSkillMask").GetComponent<Collider>();
			UIEventListener listener = UIEventListener.Get(maskCollider.gameObject);
			listener.onClick = OnMaskClick;

			UIEventListener listenerBoss = UIEventListener.Get(this.gameObject);
			listenerBoss.onClick = OnBossClick;

			_goSkillNoteParticle = transform.Find("FrostEnchant").gameObject;
			_goSkillNoteParticle.SetActive(false);

			_transSkillBgCircle = transform.Find("Skill/BgCircel");

			_goSkillMask = transform.Find("TextureSkillMask").gameObject;

		}

		private void OnEnable()
		{
			EventDispatcher.AddListener<GameObject>(CloseSkillMask, OnMaskClick);
		}

		void OnDisable()
		{
//			Debug.LogError("On disable");
			EventDispatcher.RemoveListener<GameObject>(CloseSkillMask, OnMaskClick);
		}

		private bool _isScaleSkillBgCircle = false;
		private float _scaleSkillBgCircleTime = 0f;
		private float _progress = 0f;
		void Update()
		{
			if(_isScaleSkillBgCircle)
			{
				_scaleSkillBgCircleTime = EasingFunctions.easeInOutQuad(_progress / 0.3f);

				_transSkillBgCircle.localScale = Vector3.Lerp(Vector3.zero,Vector3.one,_scaleSkillBgCircleTime);

				if(Mathf.Abs(_transSkillBgCircle.localScale.x - 1) <= 0.01f)
				{
					_transSkillBgCircle.localScale = Vector3.one;

					_isScaleSkillBgCircle = false;
					_scaleSkillBgCircleTime = 0f;
				}

				_progress += Time.deltaTime;
			}
		}

		#region 能量
		public EnergyLight[] EnergyLights;

		private float _energyPrevious = 0f;

		/// <summary>
		/// 刷新显示
		/// </summary>
		public void RefreshEnergy(float currentEnergy)
		{
			if(_energyPrevious < currentEnergy)
			{
				//点亮
				float left = currentEnergy;

				for(int i = 0;i < EnergyLights.Length;i++)
				{
					left = EnergyLights[i].Light(left);
				}

			}
			else if(_energyPrevious > currentEnergy)
			{
				//暗灯
				float left = currentEnergy;
				for(int i = 0;i < EnergyLights.Length;i++)
				{
					left = EnergyLights[i].UnLight(left);
				}
			}

			_energyPrevious = currentEnergy;

			//第二关以后才弹技能可以播放提示
			if(Fight.LevelProfile.NewCurrentLevelProfile.Level > 2)
			{
				if(_goSkill.activeSelf) return;
				else
					_goSkillNoteParticle.SetActive(currentEnergy >= SkillModuleMgr.Instance.MinNeedEnergy);
			}
		}
		#endregion

		#region 技能
		private GameObject _goSkill;

		private SkillBtn[] _skillBtns;

		private GameObject _goSkillMask;
		/// <summary>
		/// 显示区域
		/// </summary>
		/// <param name="disPlay">If set to <c>true</c> dis play.</param>
		public void ShowSkill(bool disPlay)
		{
			if(disPlay)
			{
				_goSkill.SetActive(true);
				_goSkillMask.SetActive(true);

				#if FightTest
				for(int i = 0;i < _skillBtns.Length;i++)
				{
					_skillBtns[i].gameObject.SetActive(true);
					_skillBtns[i].SetSkillId(1001);
				}
				#else
				BossData bossData = FightMgr.Instance.ModuleFight.CurrentMyBoss;
				for(int i = 0;i < _skillBtns.Length;i++)
				{
					if(i < bossData.SkillId.Length)
					{
						_skillBtns[i].gameObject.SetActive(true);
						_skillBtns[i].SetSkillId(bossData.SkillId[i]);
					}
					else
					{
						_skillBtns[i].gameObject.SetActive(false);
					}
				}
				#endif

				_scaleSkillBgCircleTime = 0f;
				_progress = 0f;
				_isScaleSkillBgCircle = true;

				_goSkillNoteParticle.SetActive(false);
			}
			else
			{
				_goSkill.SetActive(false);
				if(Fight.LevelProfile.NewCurrentLevelProfile.Level > 2)
					_goSkillNoteParticle.SetActive(_energyPrevious >= SkillModuleMgr.Instance.MinNeedEnergy);
			}
		}

		private void OnMaskClick(GameObject go)
		{
			_goSkill.SetActive(false);
			_goSkillMask.SetActive(false);

			if(Fight.LevelProfile.NewCurrentLevelProfile.Level > 2)
				_goSkillNoteParticle.SetActive(_energyPrevious >= SkillModuleMgr.Instance.MinNeedEnergy);
		}

//		private bool _isClickBoss = false;
		private void OnBossClick(GameObject go)
		{
			EventDispatcher.TriggerEvent(UIMyFace.OnHideFaceEvent);
			//显示技能选择面板
			FightMgr.Instance.ShowSkillPanel(true);
		}


		public void CloseUI()
		{
			_goSkill.SetActive(false);
			_goSkillMask.SetActive(false);
			_goSkillNoteParticle.SetActive(false);
		}
		#endregion

	}
}