using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class MyPlayer : PlayerBase 
	{
		public MyPlayer()
		{
			IsOpp = false;
		}

		protected override void OnAwake()
		{
			//注册状态
			_fsm.RegisterState(new MyState_Idle(this));
			_fsm.RegisterState(new MyState_Attack(this));
			_fsm.RegisterState(new MyState_Hit(this));
			_fsm.RegisterState(new MyState_Dead(this));
			_fsm.RegisterState(new MyState_Win(this));
			SwithchState(StateDef.Idle);
		}
	}
}
