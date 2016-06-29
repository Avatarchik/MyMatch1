using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class MyState_Idle : StateBase 
	{
		public MyState_Idle(PlayerBase player):base(player)
		{

		}

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Idle;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{
//			Debug.Log("<color=green>My enter Idle</color>,form:" + prevState.GetStateId());
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