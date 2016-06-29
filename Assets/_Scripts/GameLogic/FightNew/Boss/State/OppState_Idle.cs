using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class OppState_Idle : StateBase 
	{
		public OppState_Idle(PlayerBase player):base(player)
		{

		}

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Idle;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{
//			Debug.Log("<color=green>Opp enter Idle</color>, from:" + prevState.GetStateId());
//			DebugUtil.Debug("Opp change Idle state");
			_player.Play("idle");
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
	}
}