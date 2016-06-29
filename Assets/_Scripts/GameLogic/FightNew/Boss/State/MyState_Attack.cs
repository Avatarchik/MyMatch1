using UnityEngine;
using System.Collections;
using MyFrameWork;
using System.Collections.Generic;

namespace FightNew
{
	public class MyState_Attack : StateBase 
	{
		public MyState_Attack(PlayerBase player):base(player)
		{

		}

		private Queue<SkillData> _skillData = new Queue<SkillData>();
//		private int _targetBossId;
//		private int _currentHp;

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Attack;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{
//			FightMgr.Instance.EffectCount++;

			SkillData skillData = (SkillData)param1;

//			Debug.Log("<color=orange>My enter attack:</color>" + skillData.SkillType + ",count = " + FightMgr.Instance.EffectCount);
			_skillData.Enqueue(skillData);
//			FightMgr.Instance.CurrentMyBoss.EAttackStatus = PlayerBase.E_AttackStatus.Begin; 
			if(skillData.IsLastAttack)
				Time.timeScale = 0.2f;
			if(skillData.SkillType == SkillTable.SkillType.GongZhuNormal || skillData.SkillType == SkillTable.SkillType.GongZhuRandomNormal)
			{
//				Debug.Log("my attack");
				_player.Play("attack");
			}
			else
			{
//				Debug.Log("my skillAttack");
//				Debug.Break();
				_player.Play("skillAttack");
			}
		}
		public override void OnLeave(MyFrameWork.IState nextState, object param1, object param2)
		{
//			if(_hasEnter)
//			{
//				_hasEnter = false;
//				FightMgr.Instance.EffectCount--;
//			}
		}
		public override void OnUpdate()
		{
			
		}
		public override void OnFixedUpdate()
		{
			
		}
		public override void OnLateUpdate()
		{
			
		}

		#endregion

		public override void AnimationEventEnd(string clipName)
		{
//			_hasEnter = false;

//			Debug.Log("<color=orange>AnimationEventEnd</color>");
			if(_skillData.Count > 0)
			{
				SkillData data = _skillData.Dequeue();
	//			Debug.Log(data.CurrentHp);
				DebugUtil.Info("<color=green>发射攻击特效</color> Hp:" + data.CurrentHp + " damage:" + data.Damage + " isLast:" + data.IsLastAttack);
			
//				if(data.Damage > 0)

				_player.UseSkill(FightMgr.Instance.CurrentOppBoss,data.BossId,data.CurrentHp,data.SkillType,data.Damage,data.IsLastAttack,data.SkillId);
//				else if(data.SkillId != 0)
//					FightMgr.Instance.PlaySkillEffect(data.SkillId,false);
			}
//			_player.SwithchState(StateDef.Idle);

			//FightMgr.Instance.CurrentMyBoss.EAttackStatus = PlayerBase.E_AttackStatus.End;
			if(clipName == "MyNotmal1")
			{
				FightMgr.Instance.CurrentMyBoss.IsNormalAttackPlaying = false;
				DebugUtil.Info("my normalAtt is end");
			}
			else if(clipName == "MySkill2")
			{
				FightMgr.Instance.CurrentMyBoss.IsSkillAttackPlaying = false;
				DebugUtil.Info("my skllAtt is end");
			}
		}

		public override SkillData GetSkillData()
		{
			return _skillData.Dequeue();
		}
	}
}