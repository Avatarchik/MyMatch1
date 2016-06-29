using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class MyState_Win : StateBase 
	{
		public MyState_Win(PlayerBase player):base(player)
		{

		}

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Win;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{			
			_player.Play("win");
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
		}
	}
}