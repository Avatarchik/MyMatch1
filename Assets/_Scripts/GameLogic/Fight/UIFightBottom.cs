/*******************************************************
 * 
 * 文件名(File Name)：             UIFightBottom
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/26 17:09:38
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
	public class SkillData
	{
		public int Id;
		public string SpriteName;
		public int Energy;
		public bool IsUsed;

		public SkillData(int id,string spriteName,int energy)
		{
			Id = id;
			SpriteName = spriteName;
			Energy = energy;
			IsUsed = false;
		}
	}

	public class UIFightBottom : MonoBehaviour 
	{
		//时间
		private TimerBehaviour _timer;
		private float _energy = 0;
		private float _energyAddSpeed = 0.3f;
		private UISprite[] _spriteArr = new UISprite[10];
		private UILabel _labelEnergy;

		//技能
		private Dictionary<int,SkillData> _dicSkillData;
		private List<int> _listSkillId;
		private UISprite _spriteSkillShow1;
		private UISprite _spriteSkillShow2;
		private UISprite _spriteSkillShow3;
		private UILabel _labelSkillShow1;
		private UILabel _labelSkillShow2;
		private UILabel _labelSkillShow3;

		private UISprite _spriteSkillNext;
		private UILabel _labelSkillNext;


		void Awake()
		{
			//
			InitSkillDataTemp();
		}

		private void InitSkillDataTemp()
		{
			_dicSkillData = new Dictionary<int,SkillData>(); 
			SkillData data = new SkillData(1,"skillicon-7",4);
			_dicSkillData.Add(data.Id,data);
			data = new SkillData(2,"skillicon-8",3);
			_dicSkillData.Add(data.Id,data);
			data = new SkillData(3,"skillicon-9",5);
			_dicSkillData.Add(data.Id,data);
			data = new SkillData(4,"skillicon-10",7);
			_dicSkillData.Add(data.Id,data);

			_listSkillId = new List<int>(_dicSkillData.Keys);
		}

		// Use this for initialization
		void Start () 
		{
			//skill
			_spriteSkillShow1 = transform.Find("Container_Skills/Sprite_Bubble_1/Sprite_Icon").GetComponent<UISprite>();
			_labelSkillShow1 = _spriteSkillShow1.transform.Find("Sprite_Shining/Sprite_Bottle/Label").GetComponent<UILabel>();
			_spriteSkillShow2 = transform.Find("Container_Skills/Sprite_Bubble_2/Sprite_Icon").GetComponent<UISprite>();
			_labelSkillShow2 = _spriteSkillShow2.transform.Find("Sprite_Shining/Sprite_Bottle/Label").GetComponent<UILabel>();
			_spriteSkillShow3 = transform.Find("Container_Skills/Sprite_Bubble_3/Sprite_Icon").GetComponent<UISprite>();
			_labelSkillShow3 = _spriteSkillShow3.transform.Find("Sprite_Shining/Sprite_Bottle/Label").GetComponent<UILabel>();
			_spriteSkillNext = transform.Find("Container_NextSkill/Sprite_Frame/Sprite").GetComponent<UISprite>();
			_labelSkillNext = transform.Find("Container_NextSkill/Sprite_Frame/Sprite_Cost/Label_Cost").GetComponent<UILabel>();

			_spriteSkillShow1.parent.GetComponent<UIButton>().onClick.Add(new EventDelegate(()=>{OnSkillBtnClick(_spriteSkillShow1,_labelSkillShow1);}));
			_spriteSkillShow2.parent.GetComponent<UIButton>().onClick.Add(new EventDelegate(()=>{OnSkillBtnClick(_spriteSkillShow2,_labelSkillShow2);}));
			_spriteSkillShow3.parent.GetComponent<UIButton>().onClick.Add(new EventDelegate(()=>{OnSkillBtnClick(_spriteSkillShow3,_labelSkillShow3);}));

			_spriteSkillShow1.spriteName = _dicSkillData[1].SpriteName;
			_labelSkillShow1.text = _dicSkillData[1].Energy.ToString();
			_dicSkillData[1].IsUsed = true;
			_spriteSkillShow2.spriteName = _dicSkillData[2].SpriteName;
			_labelSkillShow2.text = _dicSkillData[2].Energy.ToString();
			_dicSkillData[2].IsUsed = true;
			_spriteSkillShow3.spriteName = _dicSkillData[3].SpriteName;
			_labelSkillShow3.text = _dicSkillData[3].Energy.ToString();
			_dicSkillData[3].IsUsed = true;
			_spriteSkillNext.spriteName = _dicSkillData[4].SpriteName;
			_labelSkillNext.text = _dicSkillData[4].Energy.ToString();




			_labelEnergy = transform.Find("Sprite_EnergyBar/Sprite_EnergyBottle/Label").GetComponent<UILabel>();

			for(int i = 1;i <= 10;i++)
			{
				_spriteArr[i-1] = transform.Find(string.Format("Sprite_EnergyBar/Sprite_{0}",i)).GetComponent<UISprite>();
				_spriteArr[i-1].fillAmount = 0f;
			}

			_timer = TimerManager.GetTimer(this.gameObject);
			StartTimer(5*60);


		}

//		void Update()
//		{
//			_energy += _energyAddSpeed * Time.deltaTime;
//		}

		#region 时间
		private void StartTimer(uint leftSecs)
		{
			_timer.StartTimer(leftSecs,OnEverySecCallBack,OnFinishTimer);
		}

		private void OnEverySecCallBack(uint leftSecs)
		{
			SetLeftTime(leftSecs);
		}

		private void OnFinishTimer()
		{
			uint leftSecs = _timer.GetCurrentTime();
			SetLeftTime(leftSecs);
		}

		private void SetLeftTime(uint leftSecs)
		{
			_energy += _energyAddSpeed;

			SetEnergyBar();
		}

		private void SetEnergyBar()
		{
			_energy = Mathf.Min(_energy,10f);

			int max = Mathf.FloorToInt(_energy);
			for(int i = 0;i < 10;i++)
			{
				if(i < max)
					_spriteArr[i].fillAmount = 1f;
				else
					_spriteArr[i].fillAmount = 0f;
			}

			if(max < 10)
				_spriteArr[max].fillAmount = _energy - max;


			_labelEnergy.text = max.ToString();
		}

		#endregion

		#region 技能
		private void OnSkillBtnClick(UISprite sprite,UILabel label)
		{
			string skillIcon = sprite.spriteName;

			SkillData skillData = FindSkillDataBySpriteName(skillIcon);
			if(skillData == null) return;

			if(skillData.Energy <= _energy)
			{
				//允许释放
				//释放
				_energy -= skillData.Energy;
				SetEnergyBar();
				skillData.IsUsed = false;


				//改变当前图片
				sprite.spriteName = _spriteSkillNext.spriteName;
				label.text = _labelSkillNext.text;
				SkillData skillNext = FindSkillDataBySpriteName(_spriteSkillNext.spriteName);
				skillNext.IsUsed = true;

				//改变下一个,找当前空闲的技能
				SkillData skillNotUsed = skillNext = FindNotUsedSkill();
				if(skillNotUsed != null)
				{
					_spriteSkillNext.spriteName = skillNotUsed.SpriteName;
					_labelSkillNext.text = skillNotUsed.Energy.ToString();
				}
			}
		}

		private SkillData FindSkillDataBySpriteName(string spriteName)
		{
			SkillData skillData = null;
			for(int i = 0;i < _listSkillId.Count;i++)
			{
				if(spriteName == _dicSkillData[_listSkillId[i]].SpriteName)
				{
					skillData = _dicSkillData[_listSkillId[i]];
					break;
				}
			}

			return skillData;
		}

		private SkillData FindNotUsedSkill()
		{
			for(int i = 0;i < _listSkillId.Count;i++)
			{
				if(!_dicSkillData[_listSkillId[i]].IsUsed)
				{
					return _dicSkillData[_listSkillId[i]];
				}
			}

			return null;
		}
		#endregion

	}
}
