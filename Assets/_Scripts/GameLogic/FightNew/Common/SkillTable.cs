using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class SkillTable 
	{
		public enum SkillType
		{
			GongZhuNormal = 1001,
			GongZhuSkill = 1002,
			GongZhuRandomNormal = 1003,

			BinFaNormal = 2001,
			BinFaSkill = 2002,

			BinFen = 3001,
		}

		private static Dictionary<SkillType,string> _dicSkill;
		public static void InitSkill()
		{
			if(_dicSkill != null) return;

			_dicSkill = new Dictionary<SkillType, string>();
			_dicSkill.Add(SkillType.GongZhuNormal,FightDefine.Prefab_Skill_GongZhuNormal);
			_dicSkill.Add(SkillType.GongZhuSkill,FightDefine.Prefab_Skill_GongZhuSpecial);
			_dicSkill.Add(SkillType.GongZhuRandomNormal,FightDefine.Prefab_Skill_GongZhuRandomNormal);

			_dicSkill.Add(SkillType.BinFaNormal,FightDefine.Prefab_Skill_BinFaNormal);
			_dicSkill.Add(SkillType.BinFaSkill,FightDefine.Prefab_Skill_BinFaSpecial);

			_dicSkill.Add(SkillType.BinFen,FightDefine.Prefab_Skill_BinFen);
		}

		public static string GetSkill(SkillType skillType)
		{
			return _dicSkill[skillType];
		}
	}
}