/*******************************************************
 * 
 * 文件名(File Name)：             ModuleFight
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/18 10:26:11
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class ModuleFight : BaseModule 
	{
		public int[] LevelId{get;set;}

		public int[] OpponentBossId;
//		public Dictionary<int,int> DicOpponentBoss{get;set;}
//		public Dictionary<int,int> DicOriOpponentBoss{get;set;}
		public Dictionary<int,BossData> DicOppBossData{get;set;}

		public int[] MyBossId;
		//public Dictionary<int,int> DicMyBossHp{get;set;}
		public Dictionary<int,BossData> DicMyBossData{get;set;}

		public int TimeSec{get;set;}
		public int Moves{get;set;}
		//public const int CLEINT_DAMAGE = 5;

		private int _myOriTotalHp;
		/// <summary>
		/// 我方原先所有HP
		/// </summary>
		/// <value>My ori total hp.</value>
		public int MyOriTotalHp
		{
			get
			{
				return _myOriTotalHp;
			}
		}

		/// <summary>
		/// 当前所有bossHp
		/// </summary>
		/// <value>My current total hp.</value>
		public int MyCurrentTotalHp
		{
			get
			{
				int hpNow = 0;

				for(int i = 0;i < MyBossId.Length;i++)
				{
					hpNow += DicMyBossData[MyBossId[i]].CurrentHp;
				}

				return hpNow;
			}
		}

		/// <summary>
		/// 对手获得的杯数
		/// </summary>
		/// <value>The opp get cup.</value>
		public int OppGetCup
	    {
			get
			{
				int cup = 0;

				for(int i = 0;i < MyBossId.Length;i++)
				{
					if(DicMyBossData[MyBossId[i]].CurrentHp <= 0)
					{
						cup++;
					}
				}

				return cup;
			}
		}

		/// <summary>
		/// 我获得的奖杯数
		/// </summary>
		/// <value>My get cup.</value>
		public int MyGetCup
	    {
			get
			{
				int cup = 0;

				for(int i = 0;i < OpponentBossId.Length;i++)
				{
					if(DicOppBossData[OpponentBossId[i]].CurrentHp <= 0)
					{
						cup++;
					}
				}

				return cup;
			}
		}

		private int _opponentOriTotalHp;
		/// <summary>
		/// 对方原先所有Hp
		/// </summary>
		/// <value>The opponent ori total hp.</value>
		public int OpponentOriTotalHp
		{
			get
			{
				return _opponentOriTotalHp;
			}
		}

		/// <summary>
		/// 对方现在所有Hp
		/// </summary>
		/// <value>The opponent current total hp.</value>
		public int OpponentCurrentTotalHp
		{
			get
			{
				int hpNow = 0;

				for(int i = 0;i < OpponentBossId.Length;i++)
				{
					hpNow += DicOppBossData[OpponentBossId[i]].CurrentHp;
				}
					
				return hpNow;
			}
		}

		/// <summary>
		/// 当前上阵的我方bossId
		/// </summary>
		public int CurrentMyBossId;
		/// <summary>
		/// 当前上阵的敌方bossId
		/// </summary>
		public int CurrentOppBossId;

		/// <summary>
		/// 当前上阵的我方Boss
		/// </summary>
		/// <value>The current my boss.</value>
		public BossData CurrentMyBoss
		{
			get
			{
				if(DicMyBossData.ContainsKey(CurrentMyBossId))
				{
					return DicMyBossData[CurrentMyBossId];
				}

				return null;
			}
		}

		/// <summary>
		/// 当前上阵的敌方Boss
		/// </summary>
		/// <value>The current opp boss.</value>
		public BossData CurrentOppBoss
		{
			get
			{
				if(DicOppBossData.ContainsKey(CurrentOppBossId))
				{
					return DicOppBossData[CurrentOppBossId];
				}

				return null;
			}
		}

		public ModuleFight()
		{
			AutoRegister = true;

			TimeSec = 60 * 10;
			Moves = 5;
		}


		/// <summary>
		/// 根据boss出战序列获取加成血量
		/// </summary>
		/// <param name="index">Index.</param>
		private int GetAddHpByBossBattleIndex(int index)
		{
			object[] objVals = Util.CallMethod("TablePlayerAttrCtrl", "GetCardSeatAddition", index, "hp");
			return int.Parse(objVals[0].ToString());
		}

		/// <summary>
		/// 根据boss出战序列获取加成攻击力
		/// </summary>
		/// <returns>The add attack by boss battle index.</returns>
		/// <param name="index">Index.</param>
		private int GetAddAttackByBossBattleIndex(int index)
		{
			object[] objVals = Util.CallMethod("TablePlayerAttrCtrl", "GetCardSeatAddition", index, "atk");
			return int.Parse(objVals[0].ToString());
		}
			
	    public void SetData()
		{
	        LevelId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).LevelId;
//			LevelId[0] = 5;
	        OpponentBossId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).OpponentBossId;
			var dicOppData = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).DicOpponentBoss;
			DicOppBossData = new Dictionary<int, BossData>();
			int bossKey;
			for(int i = 0;i < OpponentBossId.Length;i++)
			{
				bossKey = OpponentBossId[i];

				BossData bossData = new BossData();
				bossData.BossId = bossKey;
				bossData.CurrentHp = dicOppData[bossKey];
				bossData.RealCurrentHp = bossData.CurrentHp;
				bossData.OriHp = bossData.CurrentHp;
				bossData.Level = 1;
				DicOppBossData.Add(bossKey,bossData);
			}
				
			MyBossId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).MyBossId;
			dicOppData = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).DicMyBossHp;
			DicMyBossData = new Dictionary<int, BossData>();
			for(int i = 0;i < MyBossId.Length;i++)
			{
				bossKey = MyBossId[i];

				BossData bossData = new BossData();
				bossData.BossId = bossKey;
				bossData.CurrentHp = dicOppData[bossKey];
				if(i == 0)
				{
					//获取额外加成血量
					//现在只有第一个boss
					bossData.CurrentHp += GetAddHpByBossBattleIndex(1);
				}
				//获取额外加成攻击
				bossData.AddAttack = GetAddAttackByBossBattleIndex(i + 1);

				bossData.RealCurrentHp = bossData.CurrentHp;
				bossData.OriHp = bossData.CurrentHp;
				bossData.Level = 1;
				DicMyBossData.Add(bossKey,bossData);
			}

			_opponentOriTotalHp = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).OpponentOriTotalHp;
	        _myOriTotalHp = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).MyOriTotalHp;

			_bossSkillTrigger = new Dictionary<int, bool>();

			//第一个上阵boss
			CurrentMyBossId = MyBossId[0];
			CurrentOppBossId = OpponentBossId[0];
		}

		/// <summary>
		/// 自己boss充能
		/// </summary>
		/// <param name="type">Type.</param>
		public void AddEnergy(E_AddEnergyType type)
		{
			BossData boss = CurrentMyBoss;
			if(boss != null)
			{
				boss.AddEnergy(type);

//				if(boss.IsCanUseSkill)
//					SkillAttackBoss();
			}
			else
			{
				DebugUtil.Error("增加本方Boss能量,本方boss不存在");
			}
		}

		public void UseEnergy(int useEnergy)
		{
			BossData boss = CurrentMyBoss;
			if(boss != null)
			{
				boss.UseEnergy(useEnergy);
			}
			else
			{
				DebugUtil.Error("减少本方Boss能量,本方boss不存在");
			}
		}

		/// <summary>
		/// 技能攻击对方
		/// </summary>
		public void SkillAttackBoss(SkillModule skill)
		{
			BossData boss = CurrentMyBoss;
			BossData bossOpp = CurrentOppBoss;

			if(boss != null && bossOpp != null)
			{
				int bossHp = bossOpp.RealCurrentHp;

				if(bossHp <= 0) return;

				#if MinDamage
				int damage = 1;
				#else
				int damage = (int)(boss.NormalAttack * skill.HarmPara);
				#endif

				bossHp -= damage;
				if(bossHp < 0) bossHp = 0;
//				bossOpp.CurrentHp = bossHp;
				bossOpp.RealCurrentHp = bossHp;

				//通知服务端
				Util.CallMethod("Network", "ClientAttack",CurrentOppBossId, damage, (int)AttackType.SKILL_1,skill.Id,0,0);

//				Debug.Log("<color=orange>boss ：</color>" + CurrentOppBoss.CurrentHp.ToString() + " damage:" + (-damage).ToString());
				SkillData skillData = new SkillData();
				skillData.BossId = CurrentOppBossId;
				skillData.CurrentHp = bossOpp.RealCurrentHp;
				skillData.Damage = damage;
				skillData.SkillType = SkillTable.SkillType.GongZhuSkill;
				skillData.SkillId = skill.Id;
				skillData.IsLastAttack = bossHp <= 0;

				FightMgr.Instance.CurrentMyBoss.AddAttackAction(skillData);

//				boss.AddEnergy(E_AddEnergyType.UseSkill);
			}
			else
			{
				DebugUtil.Error("技能攻击对方Boss,本方boss不存在");
			}



			//原来直接播放技能伤害逻辑
//			BossData boss = CurrentMyBoss;
//			BossData bossOpp = CurrentOppBoss;
//
//			if(boss != null && bossOpp != null && boss.IsCanUseSkill)
//			{
//
//				int bossHp = bossOpp.RealCurrentHp;
//
//				if(bossHp <= 0) return;
//
//				#if MinDamage
//				int damage = 1;
//				#else
//				int damage = boss.SkillAttack;
//				#endif
//
//				bossHp -= damage;
//				if(bossHp < 0) bossHp = 0;
////				bossOpp.CurrentHp = bossHp;
//				bossOpp.RealCurrentHp = bossHp;
//
//				//通知服务端
//				Util.CallMethod("Network", "ClientAttack",CurrentOppBossId, damage, (int)AttackType.SKILL_1);
//
////				Debug.Log("<color=orange>boss ：</color>" + CurrentOppBoss.CurrentHp.ToString() + " damage:" + (-damage).ToString());
//				SkillData skillData = new SkillData();
//				skillData.BossId = CurrentOppBossId;
//				skillData.CurrentHp = bossOpp.RealCurrentHp;
//				skillData.Damage = damage;
//				skillData.SkillType = SkillTable.SkillType.GongZhuSkill;
//				skillData.IsLastAttack = bossHp <= 0;
//
//				FightMgr.Instance.CurrentMyBoss.AddAttackAction(skillData);
//
//				boss.AddEnergy(E_AddEnergyType.UseSkill);
//			}
//			else
//			{
//				DebugUtil.Error("技能攻击对方Boss,本方boss不存在");
//			}
		}

		/// <summary>
		/// 普通攻击对方
		/// </summary>
		public void NormalAttack(int continueCount,int firstDamage)
		{
			BossData bossOpp = CurrentOppBoss;
			BossData bossMy = CurrentMyBoss;

			if(bossOpp != null && bossMy != null)
			{
				int bossHp = bossOpp.RealCurrentHp;

				if(bossHp <= 0) return;

//				int damage = (int)(bossMy.NormalAttack * (1 + BossData.NormalAttackPara * (continueCount-1)));

				#if MinDamage
				int damage = 300;
				#else
				int damage = (int)(bossMy.NormalAttack * (1 + BossData.NormalAttackPara * (continueCount-1))) + firstDamage;
				#endif

				bossHp -= damage;
				if(bossHp < 0) bossHp = 0;
//				bossOpp.CurrentHp = bossHp;
				bossOpp.RealCurrentHp = bossHp;

		        //通知服务端
				Util.CallMethod("Network", "ClientAttack",CurrentOppBossId, damage,(int)AttackType.NORMAL,0,0,0);
//				Debug.Log("<color=orange>boss ：</color>" + CurrentOppBoss.CurrentHp.ToString() + " damage:" + (-damage).ToString());

				SkillData skillData = new SkillData();
				skillData.BossId = CurrentOppBossId;
				skillData.Damage = damage;
				skillData.CurrentHp = bossOpp.RealCurrentHp;
				skillData.SkillType = SkillTable.SkillType.GongZhuNormal;
				skillData.IsLastAttack = bossHp <= 0;
				skillData.SkillId = 0;

				FightMgr.Instance.CurrentMyBoss.AddAttackAction(skillData);
//				FightMgr.Instance.CurrentMyBoss.SwithchState(StateDef.Attack,skillData);

				//del
//				FightMgr.Instance.CurrentOppBoss.SwithchState(StateDef.Hurt);
//
//				//显示敌方扣血
//				EventDispatcher.TriggerEvent<int,int>("CLIENT_ATTACK", CurrentOppBossId,bossHp);
//
//				//播放动画
//				FightMgr.Instance.PlayBossAnim(CurrentOppBossId,bossHp > 0);
			}
			else
			{
				DebugUtil.Error("普通攻击对方Boss,对方boss不存在");
			}
		}

		/// <summary>
		/// 根据消除块个数，计算随机攻击伤害值
		/// </summary>
		/// <returns>The random damage.</returns>
		/// <param name="matchCardFinalCount">Match card final count.</param>
		private int GetRandomDamage(int matchCardFinalCount)
		{
			if(matchCardFinalCount <= 3)
			{
				return Random.Range(1,4);
			}
			else 
			{
				BossData bossMy = CurrentMyBoss;
				if(bossMy == null)
				{
					return 1;
				}

				if(matchCardFinalCount == 4)
				{
					return (int)(bossMy.NormalAttack * 0.1f * 5);
				}
				else if(matchCardFinalCount == 5)
				{
					return (int)(bossMy.NormalAttack * 0.1f * 8);
				}
				else
				{
					//(≥6之后，系数=5+0.5*消除块数量)
					return (int)(bossMy.NormalAttack * 0.1f * (5 + 0.5f * matchCardFinalCount));
				}
			}
		}

		/// <summary>
		/// 获取第一次攻击值
		/// </summary>
		/// <param name="matchCardFinalCount">Match card final count.</param>
		public int GetFirstRandomAttack(int matchCardFinalCount)
		{
			int damage = 0;
			BossData boss = CurrentOppBoss;

			if(boss != null)
			{
				int bossHp = boss.CurrentHp;

				if(bossHp <= 0) return damage;

				damage = GetRandomDamage(matchCardFinalCount);
			}

			return damage;
		}

		/// <summary>
		/// 随机
		/// </summary>
		public void NormalRandomAttack(int matchCardFinalCount,int firstDamage)
		{
			BossData boss = CurrentOppBoss;

			if(boss != null)
			{
				int bossHp = boss.RealCurrentHp;

				if(bossHp <= 0) return;

				int damage = GetRandomDamage(matchCardFinalCount) + firstDamage;

				bossHp -= damage;
				if(bossHp < 0) bossHp = 0;
//				boss.CurrentHp = bossHp;
				boss.RealCurrentHp = bossHp;

				//通知服务端
				Util.CallMethod("Network", "ClientAttack",CurrentOppBossId, damage, (int)AttackType.NORMAL,0,0,0);

				SkillData skillData = new SkillData();
				skillData.BossId = CurrentOppBossId;
				skillData.Damage = damage;
				skillData.CurrentHp = boss.RealCurrentHp;
				skillData.SkillType = SkillTable.SkillType.GongZhuRandomNormal;
				skillData.IsLastAttack = bossHp <= 0;
				skillData.SkillId = 0;

				FightMgr.Instance.CurrentMyBoss.AddAttackAction(skillData);
//				FightMgr.Instance.CurrentMyBoss.SwithchState(StateDef.Attack,skillData);

				//del
				//				FightMgr.Instance.CurrentOppBoss.SwithchState(StateDef.Hurt);
				//
				//				//显示敌方扣血
				//				EventDispatcher.TriggerEvent<int,int>("CLIENT_ATTACK", CurrentOppBossId,bossHp);
				//
				//				//播放动画
				//				FightMgr.Instance.PlayBossAnim(CurrentOppBossId,bossHp > 0);
			}
			else
			{
				DebugUtil.Error("普通攻击对方Boss,对方boss不存在");
			}
		}

		public SkillTable.SkillType GetSkillType(AttackType attackType)
		{
			if(attackType == AttackType.NORMAL)
			{
				return SkillTable.SkillType.BinFaNormal;
			}
			else if(attackType == AttackType.RANDOM)
			{
				return SkillTable.SkillType.BinFaNormal;
			}
			else if(attackType == AttackType.SKILL_1)
			{
				return SkillTable.SkillType.BinFaSkill;
			}

			return SkillTable.SkillType.BinFaNormal;
		}

		/// <summary>
		/// 自己受击
		/// </summary>
		/// <param name="bossId">Boss identifier.</param>
		/// <param name="currentHp">Current hp.</param>
		public void TakeDamageMySelf(int bossId,int currentHp,int damage,int attackType,int skillId,int param1,int parma2)
	 	{
	 		if(currentHp < 0) return;

//			Debug.LogError("receive skillId:" + skillId);

			AttackType attType = (AttackType)attackType;
			if(CurrentMyBoss != null && CurrentMyBossId == bossId)// && CurrentMyBoss.RealCurrentHp != currentHp)
			{
				//boss转向
//				Transform myBoss = FightMgr.Instance.CurrentMyBoss.transform;
//				FightMgr.Instance.CurrentOppBoss.transform.fo(new Vector3(myBoss.position.x,myBoss.position.y));
				SkillData skillData = new SkillData();
				skillData.BossId = bossId;
				skillData.CurrentHp = currentHp;
				skillData.Damage = damage;
				CurrentMyBoss.RealCurrentHp = currentHp;
				skillData.SkillType = GetSkillType(attType);
				skillData.IsLastAttack = currentHp <= 0;
				skillData.SkillId = skillId;

				if(FightMgr.Instance.CurrentOppBoss != null)
					FightMgr.Instance.CurrentOppBoss.AddAttackAction(skillData);
				//FightMgr.Instance.CurrentOppBoss.SwithchState(StateDef.Attack,skillData);

//				CurrentMyBoss.CurrentHp = currentHp;
//
//				EventDispatcher.TriggerEvent<int,int>(UIFight.BossAttacked, bossId, currentHp);
//
////				DebugUtil.Debug("TakeDamage");
//				FightMgr.Instance.CurrentMyBoss.SwithchState(StateDef.Hurt);
			}
	 	}


	//	private 
		private Dictionary<int,bool> _bossSkillTrigger;
		public void ClearBossSkill()
		{
			_bossSkillTrigger.Clear();
		}
		public bool IsTriggerBossSkill(int bossId)
		{
			return false;
//			if(bossId == 1)
//			{
//				if(_successMoveCountTemp < SessionControl.Instance.successMoveCount)
//				{
//					_successMoveCountTemp = SessionControl.Instance.successMoveCount;
//					return true;
//				}
//				else
//				{
//					return false;
//				}
//			}
//			else
//			{
//				float percent = GetBossHpPercent(bossId);
//				int p = Mathf.CeilToInt(percent);
//				if(_bossSkillTrigger.ContainsKey(p))
//					return false;
//
//				_bossSkillTrigger.Add(p,true);
//	//			if(bossId == 2 && p > 0 && p < 10)
//	//			{
//	//				DebugUtil.Info("boss2:" + percent);
//	////				Debug.Break();
//	//				return true;
//	//			}
//
//				if((bossId == 2 || bossId == 3) && p > 0 && p < 10 && p % 2 == 0)
//				{
//					DebugUtil.Info("boss3:" + percent);
//	//				Debug.Break();
//					return true;
//				}
//
//				return false;
//			}
		}

//		public float GetBossHpPercent(int bossId)
//		{
//			int currentHp = DicOppBossData[bossId].CurrentHp;
//			int oriHp = DicOriOpponentBoss[bossId];
//
//			return (float)currentHp / oriHp * 10;
//		}

	}
}
