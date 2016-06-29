using LuaInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FightNew;

namespace MyFrameWork
{
    public enum AttackType
    {
        NORMAL = 1,
        RANDOM = 2,
        SKILL_1 = 3,
    }
    public class FightDataManager : Manager
    {
        public int[] LevelId { get; set; }
        public int[] OpponentBossId;
        public Dictionary<int, int> DicOpponentBoss { get; set; }
        public Dictionary<int, int> DicOriOpponentBoss { get; set; }

        public int[] MyBossId;
        public Dictionary<int, int> DicMyBossHp { get; set; }

        public int TimeSec { get; set; }
        public int Moves { get; set; }
        public const int CLEINT_DAMAGE = 75;

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
                for (int i = 0; i < keys.Count; i++)
                {
                    hpNow += DicMyBossHp[keys[i]];
                }

                return hpNow;
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
                for (int i = 0; i < keys.Count; i++)
                {
                    hpNow += DicOpponentBoss[keys[i]];
                }

                return hpNow;
            }
        }

        public FightDataManager()
        {
            LevelId = new int[] { 1, 2, 3 };

            OpponentBossId = new int[3];
            DicOpponentBoss = new Dictionary<int, int>();
            DicOriOpponentBoss = new Dictionary<int, int>();
            _opponentOriTotalHp = 1000;
            MyBossId = new int[3];
            DicMyBossHp = new Dictionary<int, int>();
            _myOriTotalHp = 1000;
            TimeSec = 60 * 3;
            Moves = 1000;
        }

        public void SetData()
        {
            object[] data = Util.CallMethod("UIMainModule", "GetStages");
            LuaTable stages = data[0] as LuaTable;
            _opponentOriTotalHp = 0;
			_myOriTotalHp = 0;
            DicOpponentBoss.Clear();
            DicMyBossHp.Clear();
            DicOriOpponentBoss.Clear();
            for (int i = 1;i <= stages.Length;i++)
            {
                LuaTable MyStage = stages[i] as LuaTable;
                if(MyStage != null)
                {
					int LevelID = int.Parse(MyStage.GetStringField("id"));
                    int ID = int.Parse(MyStage.GetStringField("boss_id"));
                    int HP = int.Parse(MyStage.GetStringField("boss_hp"));
                    OpponentBossId[i - 1] = ID;
					LevelId[i - 1] = LevelID;
                    DicOpponentBoss.Add(ID, HP);
                    DicOriOpponentBoss.Add(ID, HP);
                    MyBossId[i - 1] = i;

					int mybossHp = 0;
					if(i == 1)
						mybossHp = UIFight.MyBoss1Hp;
					else if(i == 2)
						mybossHp = UIFight.MyBoss2Hp;
					else if(i == 3)
						mybossHp = UIFight.MyBoss3Hp;
					
					DicMyBossHp.Add(MyBossId[i - 1], mybossHp);
					_myOriTotalHp += mybossHp;

                    _opponentOriTotalHp += HP;
                }
            }

            if (stages.Length < 3)
            {
                DebugUtil.Error("不足三个场景");
            }

			moduleFight.SetData();
        }

        public void OnFightCBK(int boss_id,int currentHp,int harm, int attackType,int skillId,int param1,int param2)
        {
            
			moduleFight.TakeDamageMySelf(boss_id,currentHp,harm,attackType,skillId,param1,param2);
//            EventDispatcher.TriggerEvent<int,int>(UIFight.BossAttacked, boss_id, damage);
        }


		private ModuleFight _moduleFight;
		public ModuleFight moduleFight
		{
			get
			{
				if(_moduleFight == null)
				{
					_moduleFight = ModuleMgr.Instance.Get<ModuleFight>();
				}

				return _moduleFight;
			}
		}

        public void OnFightResultCBK(object obj)
        {
            if(obj != null)
            {
                EventDispatcher.TriggerEvent<object>("SERVER_FIGHT_RESULT", obj);
            }
        }

        public void OnRankChangeCBK(object obj)
        {
            if (obj != null)
                EventDispatcher.TriggerEvent<object>("SERVER_RANK_CHANGE", obj);
        }

        public void OnRankUpCBK(object obj)
        {
            if (obj != null)
                EventDispatcher.TriggerEvent<object>("SERVER_RANK_UP", obj);
        }

        /// <summary>
        /// 服务器推送：对手表情id
        /// </summary>
        /// <param name="face_id"></param>
        public void OnFaceCBK(int face_id)
        {
			EventDispatcher.TriggerEvent<int>(UIOppFace.OnShowFaceEvent,face_id);
        }

    }
}

