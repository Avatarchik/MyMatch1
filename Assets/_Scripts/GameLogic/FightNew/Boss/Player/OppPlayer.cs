using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class OppPlayer : PlayerBase 
	{

		public OppPlayer()
		{
			IsOpp = true;
		}

		protected override void OnAwake()
		{
			//注册状态
			_fsm.RegisterState(new OppState_Idle(this));
			_fsm.RegisterState(new OppState_Attack(this));
			_fsm.RegisterState(new OppState_Hit(this));
			_fsm.RegisterState(new OppState_Dead(this));
			_fsm.RegisterState(new OppState_Win(this));
			SwithchState(StateDef.Idle);
		}
	}
}
