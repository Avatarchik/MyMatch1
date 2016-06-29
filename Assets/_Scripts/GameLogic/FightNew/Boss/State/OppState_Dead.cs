using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class OppState_Dead : StateBase 
	{
//		private bool _hasEnter = false;
		public OppState_Dead(PlayerBase player):base(player)
		{

		}

		#region implemented abstract members of StateBase
		public override uint GetStateId()
		{
			return StateDef.Dead;
		}
		public override void OnEnter(MyFrameWork.StateMachine fsm, MyFrameWork.IState prevState, object param1, object param2)
		{
//			FightMgr.Instance.EffectCount++;
//			_hasEnter = true;
			//Debug.Log("<color=orange>Opp enter dead:</color>" + FightMgr.Instance.EffectCount);
			_player.Play("dead");

			FightMgr.Instance.BeginPlayDead(_player);
		}
		public override void OnLeave(MyFrameWork.IState nextState, object param1, object param2)
		{
//			if(_hasEnter)
//			{
//				FightMgr.Instance.EffectCount--;
//				FightMgr.Instance.EndPlayDead(_player);
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
//			if(_hasEnter)
//			{
//				_hasEnter = false;
//				FightMgr.Instance.EffectCount--;
//				FightMgr.Instance.EndPlayDead(_player);
//			}

			FightMgr.Instance.EndPlayDead(_player);
			FightMgr.Instance.WinOrLooseAnimationIsPlaying = false;
//			Debug.Log("<color=orange>Opp leave dead:</color>" + FightMgr.Instance.EffectCount);
		}
	}
}