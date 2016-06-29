using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class OppState_Hit : StateBase 
	{
		public OppState_Hit(PlayerBase player):base(player)
		{

		}

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Hurt;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{
			_player.Play("hit");
		}
		public override void OnLeave(MyFrameWork.IState nextState, object param1, object param2)
		{
			
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

			_player.SwithchState(StateDef.Idle);
//			if(isDead)
//			{
//				_player.SwithchState(StateDef.Dead,null,null);
//			}
//			else
//			{
//				_player.SwithchState(StateDef.Idle,null,null);
//			}
		}
	}
}