using UnityEngine;
using System.Collections;

namespace FightNew
{
	/// <summary>
	/// 充能类型
	/// </summary>
	public enum E_AddEnergyType
	{
		Normal,//普通攻击
		Powup,//技能攻击
		Stone,//能量块消除
		Recovery,//每6秒回复
//		UseSkill,//释放技能
	}

	public enum E_MatchCountMsg
	{
		MatchCount_Three,
		MatchCount_Five,
		MatchCount_Seven,
		MatchCount_Nine,
		MatchCount_Eleven,
	}

	/// <summary>
	/// 卡牌类型
	/// </summary>
	public enum E_CardType
	{
		None = -1,

		//六种基本颜色
		Fight_SimpleCard1 = 0,
		Fight_SimpleCard2 = 1,
		Fight_SimpleCard3 = 2,
		Fight_SimpleCard4 = 3,
		Fight_SimpleCard5 = 4,
		Fight_SimpleCard6 = 5,
		Stone = 6,

		//障碍
		Block = 10,
		Weed = 11,
		Ice = 12,


		//特殊效果
		OneLine = 20,
		CrossLine = 21,
		ThreeLine = 22,

	}
	public class FightDefine
	{
		public enum E_NoteMsgType
		{
			None,
			NoMoves,
			NoShuffle,
			NotEnoughEnergy,
			OppUseAddMoves,
			ShowIceNotice,
		}

		public static string[] PreLoadPrefabNames = new string[]
			{
				//slot
				Prefab_Slot_Empty,//
				//wall
				Prefab_Wall,//
				//block
				Prefab_Boss,//
				Prefab_BossPos,
				Prefab_Weed,//
				Prefab_Particle_WeedCrush,//
				Prefab_Branch,//
//				Prefab_Particle_BranchCrush,
				Prefab_Block,//
				Prefab_Particle_BlockCrush,//
				//powerUp
				Prefab_OneLine,//
				Prefab_CrosseLine,//
				Prefab_ThreeLine,//
				Prefab_LineMask,//
				//card crush
				Prefab_Particle_CardCrush,//
				//simpleCard
				"Fight_SimpleCard1",
				"Fight_SimpleCard2",
				"Fight_SimpleCard3",
				"Fight_SimpleCard4",
				"Fight_SimpleCard5",
				"Fight_SimpleCard6",

				//boss出生特效
				"BossCreate",

				//label飘血
				LabelHPFlying,

				Prefab_MatchCount_Three,
				Prefab_MatchCount_Five,
				Prefab_MatchCount_Seven,
				Prefab_MatchCount_Nine,
				Prefab_MatchCount_Eleven,

				//boss
				Prefab_Boss_BingFa,
				Prefab_Boss_GongZhu,

				Prefab_Stone,
				Prefab_Particle_Stone,

				//boss skill
				Prefab_Skill_BinFaNormal,
				Prefab_Skill_BinFaSpecial,
				Prefab_Skill_GongZhuNormal,
				Prefab_Skill_GongZhuSpecial,
				Prefab_Skill_GongZhuRandomNormal,
				Prefab_Particle_EnergySmall,
				Prefab_Particle_PrizeFlying,
				Prefab_Particle_JiangBeiFlying,
				Prefab_Particle_CoinFlying,
				Prefab_Flying_Weed,
				Prefab_Flying_Block,
				Prefab_Skill_BinFen,
				Prefab_FengChe
			};

		public static string[] PreLoadTextues = new string[]
			{
				"CrushCard1",
				"CrushCard2",
				"CrushCard3",
				"CrushCard4",
				"CrushCard5",
				"CrushCard6"
			};

		//define
		public const int Def_Block_Weed = 4;
		public const int Def_Block_Brch = 5;
		public const int Def_Block_None = 0;
		public const int Def_Card_Stone = 9;

		//slot
		#if FightTest
		public const string Prefab_Slot_Empty = "FightNew/Slot/Fight_SlotEmpty";
		public const string Prefab_Wall = "FightNew/Slot/Fight_Wall";
		#else
		public const string Prefab_Slot_Empty = "Fight_SlotEmpty";
		public const string Prefab_Wall = "Fight_Wall";
		#endif
		public const string Format_SlotKey = "Slot_{0}x{1}";


		//simpleCard
		#if FightTest
		public const string Format_SimpleCardKey = "FightNew/SimpleCard/{0}";
		public const string Prefab_Stone = "FightNew/SimpleCard/Fight_Stone";
		public const string Prefab_Particle_Stone = "FightNew/Effect/Fight_StoneCrush";
		#else
		public const string Format_SimpleCardKey = "{0}";
		public const string Prefab_Stone = "Fight_Stone";
		public const string Prefab_Particle_Stone = "Fight_StoneCrush";
		#endif

		#if FightTest
		//boss
		public const string Prefab_Boss = "FightNew/Block/Fight_Boss";

		//weed
		public const string Prefab_Particle_WeedCrush = "FightNew/Effect/Fight_WeedCrush";
		public const string Prefab_Weed = "FightNew/Block/Fight_Weed";

		//branch
		public const string Prefab_Particle_BranchCrush = "FightNew/Effect/Fight_BranchCrush";
		public const string Prefab_Branch = "FightNew/Block/Fight_Branch";

		//block
		public const string Prefab_Particle_BlockCrush = "FightNew/Effect/Fight_BlockCrush";
		public const string Prefab_Block = "FightNew/Block/Fight_Block";

		public const string Prefab_BossPos = "FightNew/Block/Fight_BossPos";
		public const string Prefab_FengChe = "FightNew/Block/Fight_FengChe";
		#else
		//weed
		public const string Prefab_Boss = "Fight_Boss";
		public const string Prefab_BossPos = "Fight_BossPos";
		public const string Prefab_Particle_WeedCrush = "Fight_WeedCrush";
		public const string Prefab_Weed = "Fight_Weed";

		//branch
		public const string Prefab_Particle_BranchCrush = "Fight_BranchCrush";
		public const string Prefab_Branch = "Fight_Branch";

		//block
		public const string Prefab_Particle_BlockCrush = "Fight_BlockCrush";
		public const string Prefab_Block = "Fight_Block";

		public const string Prefab_FengChe = "Fight_FengChe";
		#endif

		//PowerUp
		#if FightTest
		public const string Prefab_OneLine = "OneLine";
		public const string Prefab_CrosseLine = "CrossLine";
		public const string Prefab_ThreeLine = "ThreeLine";
		#else
		public const string Prefab_OneLine = "OneLine";
		public const string Prefab_CrosseLine = "CrossLine";
		public const string Prefab_ThreeLine = "ThreeLine";
		#endif

		//卡牌破碎图片名
		public const string CrushCardFormat = "CrushCard{0}";
		//effect
		#if FightTest
		public const string Prefab_Particle_CardCrush = "FightNew/Effect/Fight_ParticleCardCrush";
		public const string Prefab_LineMask = "FightNew/Effect/LineMask";
		public const string Prefab_MatchCount_Three = "FightNew/MatchCount/MatchCount_Three";
		public const string Prefab_MatchCount_Five = "FightNew/MatchCount/MatchCount_Five";
		public const string Prefab_MatchCount_Seven = "FightNew/MatchCount/MatchCount_Seven";
		public const string Prefab_MatchCount_Nine = "FightNew/MatchCount/MatchCount_Nine";
		public const string Prefab_MatchCount_Eleven = "FightNew/MatchCount/MatchCount_Eleven";
		public const string Prefab_Particle_EnergySmall = "FightNew/Effect/EnergySmall";
		public const string Prefab_Particle_PrizeFlying = "FightNew/Effect/PrizeFlying";
		public const string Prefab_Particle_JiangBeiFlying = "FightNew/Effect/JiangBeiFlying";
		public const string Prefab_Particle_CoinFlying = "FightNew/Effect/CoinFlying";
		#else
		public const string Prefab_Particle_CardCrush = "Fight_ParticleCardCrush";
		public const string Prefab_LineMask = "LineMask";
		public const string Prefab_MatchCount_Three = "MatchCount_Three";
		public const string Prefab_MatchCount_Five = "MatchCount_Five";
		public const string Prefab_MatchCount_Seven = "MatchCount_Seven";
		public const string Prefab_MatchCount_Nine = "MatchCount_Nine";
		public const string Prefab_MatchCount_Eleven = "MatchCount_Eleven";

		public const string Prefab_Particle_EnergySmall = "EnergySmall";
		public const string Prefab_Particle_PrizeFlying = "PrizeFlying";
		public const string Prefab_Particle_JiangBeiFlying = "JiangBeiFlying";
		public const string Prefab_Particle_CoinFlying = "CoinFlying";
		#endif

		/// <summary>
		/// 飘血
		/// </summary>
		public const string LabelHPFlying = "LabelHPFlying";

		#if FightTest
		public const string Prefab_Boss_BingFa = "FightNew/Boss/BingFa";
		public const string Prefab_Boss_GongZhu = "FightNew/Boss/GongZhu";
		#else
		public const string Prefab_Boss_BingFa = "BingFa";
		public const string Prefab_Boss_GongZhu = "GongZhu";
		#endif

		public const string Prefab_Skill_BinFaNormal = "FrostTinyObj";
		public const string Prefab_Skill_BinFaSpecial = "FrostMegaObj";
		public const string Prefab_Skill_GongZhuNormal = "FireTinyObj";
		public const string Prefab_Skill_GongZhuSpecial = "FireMegaObj";
		public const string Prefab_Skill_GongZhuRandomNormal = "LifeTinyObj";

		public const string Prefab_Skill_BinFen = "FrostNormalObj";

        public const string Event_GameOver = "Event_GameOver";
        public const string Event_LevelLoadOver = "Event_LevelLoadOver";
        public const string Event_HasNewBomb = "Event_HasNewBomb";
		public const string Event_HasEnergy = "Event_HasEnergy";
        public const string Event_ObjectsBling = "Event_ObjectsBling";

        //技能，飞出道具
        public const string Prefab_Flying_Weed = "Flying_Weed";

		#if FightTest
		public const string Prefab_Flying_Block = "FightNew/Block/Flying_Block";
		#else
		public const string Prefab_Flying_Block = "Flying_Block";
		#endif
	}
}
