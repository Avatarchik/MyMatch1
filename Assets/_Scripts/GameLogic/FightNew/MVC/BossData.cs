using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class BossData
	{
		#region 能量
		/// <summary>
		/// 最大能量
		/// </summary>
		public const float MaxEnergy = 9f;
		/// <summary>
		/// Boss周围1格增加的能量
		/// </summary>
		public const float NormalAddEnergy = 0.15f;
		/// <summary>
		/// 特殊效果打到Boss增加的能量
		/// </summary>
		public const float PowupAddEnergy = NormalAddEnergy * 2.5f;
		/// <summary>
		/// 能量块消除增加的能量
		/// </summary>
		public const float EnergyStone = 1f;

		/// <summary>
		/// 每6秒，回复能量
		/// </summary>
		public const float RecoverEnergyTime = 6f;
		/// <summary>
		/// 每6秒回复的能量数
		/// </summary>
		public const float RecoverEnergy = 0.3f;
		#endregion

		#region 攻击
		/// <summary>
		/// 普通攻击系数
		/// </summary>
		public const float NormalAttackPara = 0.2f;

		private int _addAttack = 0;
		/// <summary>
		/// 根据玩家等级和卡槽位获得的额外攻击力
		/// </summary>
		public int AddAttack
		{
			set
			{
				if(value < 0)
					_addAttack = 0;
				else
					_addAttack = value;

//				DebugUtil.Info("<color=ourange>额外攻击力:</color>:" + AddAttack);
			}
			get
			{
				return _addAttack;
			}
		}

		/// <summary>
		/// 普通攻击力：普通攻击伤害 ＝ NormalAttack ＊ （1 ＋ NormalAttackPara ＊ 连消次数）
		/// </summary>
		/// <value>The normal attack.</value>
		public int NormalAttack
		{
			get
			{
				//根据BossId和Level拿攻击力
				return 10 + AddAttack;
			}
		}

		/// <summary>
		/// 技能攻击系数：技能攻击伤害值 ＝ 	NormalAttack * (0.5+0.69*消耗能量)
		/// </summary>
		/// <value>The skill attack.</value>
		public int SkillAttack
		{
			get
			{
				//根据BossId和Level拿技能攻击系数
				return (int)((1.5f + 0.69f * 3) * NormalAttack);
			}
		}
		#endregion

		public int BossId{get;set;}
		public int Level{get;set;}
		public int OriHp{get;set;}
		public int CurrentHp{get;set;}
		public int RealCurrentHp{get;set;}
		public bool IsDead
		{
			get
			{
				return CurrentHp <= 0;
			}
		}

		private float _energy;
		/// <summary>
		/// 当前能量
		/// </summary>
		/// <value>The energy.</value>
		public float Energy
		{
			set
			{
				if(value < 0)
					value = 0;
				
				if(value >= MaxEnergy)
					_energy = MaxEnergy;
				else
					_energy = value;
			}
			get
			{
				return _energy;
			}
		}

		private int[] _skillId = new int[]{1001,1002,1003,1004};
		/// <summary>
		/// 技能列表
		/// </summary>
		/// <value>The skill identifier.</value>
		public int[] SkillId
		{
			get
			{
				return _skillId;
			}
		}

		public BossData()
		{
			_energy = 0f;
		}
			
		/// <summary>
		/// 充能
		/// </summary>
		/// <param name="type">Type.</param>
		public void AddEnergy(E_AddEnergyType type)
		{
			float addEnergy = 0f;

			if(type == E_AddEnergyType.Normal)
			{
				addEnergy = NormalAddEnergy;
			}
			else if(type == E_AddEnergyType.Powup)
			{
				addEnergy = PowupAddEnergy;
			}
			else if(type == E_AddEnergyType.Stone)
			{
				addEnergy = EnergyStone;
			}
			else if(type == E_AddEnergyType.Recovery)
			{
				addEnergy = RecoverEnergy;
			}
//			else if(type == E_AddEnergyType.UseSkill)
//				addEnergy = -3;//-MaxEnergy;

//			DebugUtil.Debug("加能量：" + addEnergy);
			Energy += addEnergy;

			FightMgr.Instance.RefreshEnergy(Energy);

			if(!FightMgr.Instance.IsTriggerEnergyForTutorial && Energy >= SkillModuleMgr.Instance.MinNeedEnergy)
			{
				FightMgr.Instance.IsTriggerEnergyForTutorial = true;
				//能量满了，通知新手引导
				EventDispatcher.TriggerEvent<int,int>(FightDefine.Event_HasEnergy, (int)Energy,SkillModuleMgr.Instance.MinSkillId);
			}
		}

		/// <summary>
		/// 使用能量
		/// </summary>
		/// <param name="useEnergy">Use energy.</param>
		public void UseEnergy(int useEnergy)
		{
			Energy -= useEnergy;
//			DebugUtil.Error("Current Energy:" + Energy + " use Energy:" + useEnergy);
			FightMgr.Instance.RefreshEnergy(Energy);
		}

		/// <summary>
		/// 是否可以释放技能
		/// </summary>
		/// <value><c>true</c> if this instance is can use skill; otherwise, <c>false</c>.</value>
		public bool IsCanUseSkill
		{
			get
			{
				return _energy >= 3;//MaxEnergy;
			}
		}

	}
}