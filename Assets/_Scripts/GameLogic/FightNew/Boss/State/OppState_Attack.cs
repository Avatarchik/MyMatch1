using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class OppState_Attack : StateBase 
	{
		public OppState_Attack(PlayerBase player):base(player)
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

//			Debug.Log("<color=orange>Opp enter attack:</color>" + skillData.SkillType + ",count = "+ FightMgr.Instance.EffectCount);

//			skillData.SkillType = SkillTable.SkillType.BinFaSkill;
			_skillData.Enqueue(skillData);
//			_targetBossId = (int)param1;
//			_currentHp = (int)param2;

//			_player.Play("skillAttack");

//			FightMgr.Instance.CurrentOppBoss.EAttackStatus = PlayerBase.E_AttackStatus.Begin;
			if(skillData.IsLastAttack)
				Time.timeScale = 0.2f;
			if(skillData.SkillType == SkillTable.SkillType.BinFaNormal)
				_player.Play("attack");
			else
				_player.Play("skillAttack");
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
			//        _player.transform.DOLocalMove(_oriPos,0.3f).OnComplete(()=>
			//            {
			//                _player.SwithchState(0,null,null);
			//            });
//			FightMgr.Instance.CurrentOppBoss.transform.LookAt(new Vector3(0,0,0));

//			_hasEnter = false;

			if(_skillData.Count > 0)
			{
				SkillData data = _skillData.Dequeue();
//				if(data.IsLastAttack)
//					Time.timeScale = 0.2f;
				DebugUtil.Info("<color=green>发射攻击特效</color> Hp:" + data.CurrentHp + " damage:" + data.Damage + " isLast:" + data.IsLastAttack); 

//				if(data.Damage > 0)
				_player.UseSkill(FightMgr.Instance.CurrentMyBoss,data.BossId,data.CurrentHp,data.SkillType,data.Damage,data.IsLastAttack,data.SkillId);
//				if(data.SkillId != 0)
//					FightMgr.Instance.PlaySkillEffect(data.SkillId,false);
			}
//			_player.SwithchState(StateDef.Idle);
//			FightMgr.Instance.CurrentOppBoss.EAttackStatus = PlayerBase.E_AttackStatus.End;
			if(clipName == "OppNormal")
			{
				FightMgr.Instance.CurrentOppBoss.IsNormalAttackPlaying = false;
				DebugUtil.Info("opp normalAtt is end");
			}
			else if(clipName == "OppSkill")
			{
				FightMgr.Instance.CurrentOppBoss.IsSkillAttackPlaying = false;
				DebugUtil.Info("opp skillmalAtt is end");
			}
		}

		public override SkillData GetSkillData()
		{
			return _skillData.Dequeue();
		}
	}
}