using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using LuaInterface;

namespace FightNew
{
	public enum E_SkillTypeMain
	{
		Normal = 1,
		Active = 2,
		Passive = 3,
	}

	public enum E_SkillTypeDetail
	{
		DoHarm = 1,//造成大量伤害
		AddMoves = 2,//增加步数
		//ResetSlot,//重置自己的三消块
		AddBlocks = 3,//增加木块
		ForbidAttack = 4,//禁止对方攻击
	}

	public class SkillModule
	{
		/// <summary>
		/// 没伤害下，是否不不需要放火球的技能
		/// </summary>
		/// <value><c>true</c> if this instance is no harm skill; otherwise, <c>false</c>.</value>
		public bool IsNoHarmSkill
		{
			get
			{
				return SkillTypeDetail == E_SkillTypeDetail.AddBlocks || SkillTypeDetail == E_SkillTypeDetail.AddMoves;
			}
		}

		/// <summary>
		/// 技能Id
		/// </summary>
		/// <value>The identifier.</value>
		public int Id{get;set;}

        /// <summary>
        /// 技能名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 技能描述
        /// </summary>
        public string Desc { get; set; }

		/// <summary>
		/// 技能主类型：普功、主动、被动
		/// </summary>
		/// <value>The skill type main.</value>
		public E_SkillTypeMain SkillTypeMain{get;set;}

		/// <summary>
		/// 技能类型
		/// </summary>
		/// <value>The skill type detail.</value>
		public E_SkillTypeDetail SkillTypeDetail{get;set;}

		/// <summary>
		/// 效果1
		/// </summary>
		/// <value>The effect number.</value>
		public string EffectNum{get;set;}

        /// <summary>
        /// 需要的能量
        /// </summary>
        /// <value>The need energy.</value>
        public int NeedEnergy { get; set; }

        /// <summary>
        /// 伤害系数
        /// </summary>
        /// <value>The harm para.</value>
        public float HarmPara{get;set;}

        /// <summary>
        /// cd（毫秒）
        /// </summary>
        /// <value>The C.</value>
        public int CoolDown { get; set; }

        /// <summary>
        /// 技能图标
        /// </summary>
        /// <value>The skill icon.</value>
        public string SkillIcon{get;set;}

		/// <summary>
		/// 动画名称
		/// </summary>
		/// <value>The name of the animation.</value>
		public string AnimationName{get;set; }

        /// <summary>
        /// 攻击方式
        /// </summary>
        /// <value>The attack fashion.</value>
        public int AttackFashion { get; set; }

        /// <summary>
        /// 施法特效
        /// </summary>
        /// <value>The effect.</value>
        public string Effect { get; set; }

        /// <summary>
        /// 攻击特效名字
        /// </summary>
        /// <value>The name of the attack prefab.</value>
        public string AttackPrefabName { get; set; }

        /// <summary>
        /// 声音
        /// </summary>
        /// <value>The name of the audio.</value>
        public string AudioName{get;set;}

	}

	public class SkillModuleMgr : Singleton<SkillModuleMgr>
	{
		private Dictionary<int,SkillModule> _dicSkillModule;

		public override void Init()
		{
			_dicSkillModule = new Dictionary<int, SkillModule>();

			//add test Data
			AddTestData();
		}

		public void AddSkill(SkillModule skill)
		{
			if(skill != null)
				_dicSkillModule[skill.Id] = skill;
		}

		public void ClearSkill()
		{
			_dicSkillModule.Clear();
		}

		public SkillModule GetSkill(int skillId)
		{
			SkillModule skill = null;
			_dicSkillModule.TryGetValue(skillId,out skill);
			return skill;
		}


//		read("skill")

		/// <summary>
		/// 最小需要能量的Id
		/// </summary>
		private int _minSkillId = 0;
		private int _minNeedEnergy = 100;
		/// <summary>
		/// 最小技能id
		/// </summary>
		/// <value>The minimum skill identifier.</value>
		public int MinSkillId
		{
			get
			{
				return _minSkillId;
			}
		}

		/// <summary>
		/// 最小能量
		/// </summary>
		/// <value>The minimum need energy.</value>
		public int MinNeedEnergy
		{
			get
			{
				return _minNeedEnergy;
			}
		}

		public void AddTestData()
		{
			#if !FightTest
            object[] obj = Util.CallMethod("TableSkillCtrl", "Get_All_Skills");

            LuaTable skills = obj[0] as LuaTable;

            DebugUtil.Info("skills.Length: " + skills.Length);

            SkillModule skill = null;

            for (int i = 1; i <= skills.Length; i++)
            {
                skill = new SkillModule();
                LuaTable _skill = skills[i] as LuaTable;
                skill.Id = int.Parse( _skill.GetStringField("id"));
                skill.Name = _skill.GetStringField("name");
                skill.Desc = _skill.GetStringField("desc");
                skill.SkillTypeMain = (E_SkillTypeMain)int.Parse(_skill.GetStringField("skill_type_main"));
                skill.SkillTypeDetail = (E_SkillTypeDetail)int.Parse(_skill.GetStringField("skill_type_detail"));
                skill.EffectNum = _skill.GetStringField("effect_num");
                skill.NeedEnergy = int.Parse(_skill.GetStringField("need_energy"));
                skill.HarmPara = float.Parse(_skill.GetStringField("harm_para"));
                skill.CoolDown = int.Parse(_skill.GetStringField("cooldown"));
                skill.SkillIcon = _skill.GetStringField("skill_icon");
                skill.AnimationName = _skill.GetStringField("animation_name");
                skill.AttackFashion = int.Parse(_skill.GetStringField("attack_fashion"));
                skill.Effect = _skill.GetStringField("effect");
                skill.AttackPrefabName= _skill.GetStringField("attack_prefab_name");
                skill.AudioName = _skill.GetStringField("audio_name");

				if(_minNeedEnergy > skill.NeedEnergy)
				{
					_minSkillId = skill.Id;
					_minNeedEnergy = skill.NeedEnergy;
				}

                AddSkill(skill);
            }

			#else
            //1001 造成大额伤害
            SkillModule skill = new SkillModule();
			skill.Id = 1001;
			skill.SkillTypeMain = E_SkillTypeMain.Active;
			skill.SkillTypeDetail = E_SkillTypeDetail.DoHarm;
			skill.EffectNum = "";
			skill.SkillIcon = "icon-9";
			skill.NeedEnergy = 0;
			skill.HarmPara = 2;
			skill.AnimationName = string.Empty;
			skill.AudioName = string.Empty;
			skill.CoolDown = 0;
			skill.AttackFashion = 1;
			skill.Effect = string.Empty;
			AddSkill(skill);

			//1007 增加5步（可以突破上限）
			skill = new SkillModule();
			skill.Id = 1002;
			skill.SkillTypeMain = E_SkillTypeMain.Active;
			skill.SkillTypeDetail = E_SkillTypeDetail.AddMoves;
			skill.EffectNum = "5";
			skill.SkillIcon = "icon-7";
			skill.NeedEnergy = 0;
			skill.HarmPara = 2;
			skill.AnimationName = string.Empty;
			skill.AudioName = string.Empty;
			skill.CoolDown = 0;
			skill.AttackFashion = 1;
			skill.Effect = string.Empty;
			AddSkill(skill);

			//1003 放置障碍
			skill = new SkillModule();
			skill.Id = 1003;
			skill.SkillTypeMain = E_SkillTypeMain.Active;
			skill.SkillTypeDetail = E_SkillTypeDetail.AddBlocks;
			skill.EffectNum = "4";
			skill.SkillIcon = "icon-8";
			skill.NeedEnergy = 0;
			skill.HarmPara = 2;
			skill.AnimationName = string.Empty;
			skill.AudioName = string.Empty;
			skill.CoolDown = 0;
			skill.AttackFashion = 1;
			skill.Effect = string.Empty;
			AddSkill(skill);

			//1004 冰封对手
			skill = new SkillModule();
			skill.Id = 1004;
			skill.SkillTypeMain = E_SkillTypeMain.Active;
			skill.SkillTypeDetail = E_SkillTypeDetail.ForbidAttack;
			skill.EffectNum = "2.5";
			skill.SkillIcon = "icon-10";
			skill.NeedEnergy = 0;
			skill.HarmPara = 2;
			skill.AnimationName = string.Empty;
			skill.AudioName = string.Empty;
			skill.CoolDown = 0;
			skill.AttackFashion = 1;
			skill.Effect = string.Empty;
			AddSkill(skill);
			#endif
		}
	}
}
