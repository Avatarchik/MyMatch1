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

namespace Fight
{
	public class ModuleFight : BaseModule 
	{
		public int[] LevelId{get;set;}
		public int[] OpponentBossId;
		public Dictionary<int,int> DicOpponentBoss{get;set;}
		public Dictionary<int,int> DicOriOpponentBoss{get;set;}

		public int[] MyBossId;
		public Dictionary<int,int> DicMyBossHp{get;set;}

		public int TimeSec{get;set;}
		public int Moves{get;set;}
		public const int CLEINT_DAMAGE = 10;

		private int _myOriTotalHp;
		public int MyOriTotalHp
		{
			get
			{
				return _myOriTotalHp;
			}
		}

		public int MyCurrentTotalHp
		{
			get
			{
				int hpNow = 0;
				List<int> keys = new List<int>(DicMyBossHp.Keys);
				for(int i = 0;i < keys.Count;i++)
				{
					hpNow += DicMyBossHp[keys[i]];
				}

				return hpNow;
			}
		}

		public int OppGetCup
	    {
			get
			{
				int cup = 0;
				List<int> keys = new List<int>(DicMyBossHp.Keys);
				for(int i = 0;i < keys.Count;i++)
				{
					if(DicMyBossHp[keys[i]] == 0)
					{
						cup++;
					}
				}

				return cup;
			}
		}

		public int MyGetCup
	    {
			get
			{
				int cup = 0;
				List<int> keys = new List<int>(DicOpponentBoss.Keys);
				for(int i = 0;i < keys.Count;i++)
				{
					if(DicOpponentBoss[keys[i]] == 0)
					{
						cup++;
					}
				}

				return cup;
			}
		}

		private int _opponentOriTotalHp;
		public int OpponentOriTotalHp
		{
			get
			{
				return _opponentOriTotalHp;
			}
		}
			
		public int OpponentCurrentTotalHp
		{
			get
			{
				int hpNow = 0;
				List<int> keys = new List<int>(DicOpponentBoss.Keys);
				for(int i = 0;i < keys.Count;i++)
				{
					hpNow += DicOpponentBoss[keys[i]];
				}

				return hpNow;
			}
		}

		public ModuleFight()
		{
			AutoRegister = true;

			TimeSec = 60 * 10;
			Moves = 7;
		}

		private int _successMoveCountTemp;
	    public void SetData()
		{
	        LevelId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).LevelId;
	        OpponentBossId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).OpponentBossId;
			DicOpponentBoss = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).DicOpponentBoss;
	        MyBossId = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).MyBossId;
	        DicMyBossHp = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).DicMyBossHp;
	        DicOriOpponentBoss = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).DicOriOpponentBoss;
			_opponentOriTotalHp = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).OpponentOriTotalHp;
	        _myOriTotalHp = AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData).MyOriTotalHp;

			_bossSkillTrigger = new Dictionary<int, bool>();
			_successMoveCountTemp = 0;
		}

		public void AttackBoss()
		{
			int bossHp = 0;
			int bossId = 0;
			if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
			{
				bossId = OpponentBossId[0];
			}
			else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
			{
				bossId = OpponentBossId[1];
			}
			else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
			{
				bossId = OpponentBossId[2];
			}
			else
			{
				DebugUtil.Error("状态不对，ModuleFight_AttackBoss()：" + FightControl.Instance.EFightStatus);
				return;
			}

			bossHp = DicOpponentBoss[bossId];

			if(bossHp <= 0) return;

			bossHp -= CLEINT_DAMAGE;
			if(bossHp < 0) bossHp = 0;
			DicOpponentBoss[bossId] = bossHp;

	        //通知服务端
	        Util.CallMethod("Network", "ClientAttack",bossId, CLEINT_DAMAGE);
			//MyFrameWork.NetworkManager.Instance.ClientSendFightDataMsg(bossId,CLEINT_DAMAGE);

			//自己显示敌方扣血
			EventDispatcher.TriggerEvent<int,int>("CLIENT_ATTACK", bossId,bossHp);

			//播放动画
			FightControl.Instance.PlayBossAnim(bossId,bossHp > 0);


		}

		public void TakeDamageMySelf(int bossId,int currentHp)
	 	{

	 		if(currentHp < 0) return;
	 
	 		for(int i = 0;i < MyBossId.Length;i++)
	 		{
	 			if(MyBossId[i] == bossId)
	 			{
	 				if(DicMyBossHp[MyBossId[i]] != currentHp)
	 				{
	 					DicMyBossHp[MyBossId[i]] = currentHp;
	 
						EventDispatcher.TriggerEvent<int,int>(UIFight.BossAttacked, bossId, currentHp);
	 					//EventDispatcher.TriggerEvent<int,int>(Protocol.MsgNo.SERVER_ATTACKED.ToString(), bossId, currentHp);
	 				}
	 
	 				break;
	 			}
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
			if(bossId == 1)
			{
				if(_successMoveCountTemp < SessionControl.Instance.successMoveCount)
				{
					_successMoveCountTemp = SessionControl.Instance.successMoveCount;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				float percent = GetBossHpPercent(bossId);
				int p = Mathf.CeilToInt(percent);
				if(_bossSkillTrigger.ContainsKey(p))
					return false;

				_bossSkillTrigger.Add(p,true);
	//			if(bossId == 2 && p > 0 && p < 10)
	//			{
	//				DebugUtil.Info("boss2:" + percent);
	////				Debug.Break();
	//				return true;
	//			}

				if((bossId == 2 || bossId == 3) && p > 0 && p < 10 && p % 2 == 0)
				{
					DebugUtil.Info("boss3:" + percent);
	//				Debug.Break();
					return true;
				}

				return false;
			}
		}

		public float GetBossHpPercent(int bossId)
		{
			int currentHp = DicOpponentBoss[bossId];
			int oriHp = DicOriOpponentBoss[bossId];

			return (float)currentHp / oriHp * 10;
		}

	}
}