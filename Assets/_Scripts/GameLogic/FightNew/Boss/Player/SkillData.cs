using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class SkillData
	{
		public int BossId{get;set;}
		public int CurrentHp{get;set;}
		public int Damage{get;set;}
		public SkillTable.SkillType SkillType{get;set;}
		public int SkillId{get;set;}

		private bool _isLastAttack = false;
		/// <summary>
		/// 是否最后一击
		/// </summary>
		/// <value><c>true</c> if this instance is last attack; otherwise, <c>false</c>.</value>
		public bool IsLastAttack
		{
			get
			{
				return _isLastAttack;
			}
			set
			{
				_isLastAttack = value;
				if(_isLastAttack)
				{
					DebugUtil.Info("<color=orange>设置最后一击</color> currentHp:" + CurrentHp + ",Damage:" + Damage);
				}
			}
		}


		/// <summary>
		/// 是否技能
		/// </summary>
		/// <value><c>true</c> if this instance is skill; otherwise, <c>false</c>.</value>
		public bool IsSkill
		{
			get
			{
				return SkillType == SkillTable.SkillType.GongZhuSkill || SkillType == SkillTable.SkillType.BinFaSkill;
			}
		}

	}
}