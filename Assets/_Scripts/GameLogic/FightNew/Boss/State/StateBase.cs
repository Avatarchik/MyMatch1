/*
 * 
 * 文件名(File Name)：             StateBase
 *
 * 作者(Author)：                  yangzj
 *
 * 创建时间(CreateTime):           2016/02/12 16:04:36
 *
 */

using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public abstract class StateBase : IState 
	{
		protected PlayerBase _player;

		public StateBase(PlayerBase player)
		{
			_player = player;
		}

		#region IState implementation

		public abstract uint GetStateId();

		public abstract void OnEnter(StateMachine fsm, IState prevState, object param1, object param2);

		public abstract void OnLeave(IState nextState, object param1, object param2);

		public abstract void OnUpdate();

		public abstract void OnFixedUpdate();

		public abstract void OnLateUpdate();

		#endregion

		/// <summary>
		/// 角色动画播放结束
		/// </summary>
		/// <param name="clipName">Clip name.</param>
		public virtual void AnimationEventEnd(string clipName)
		{

		}

		public virtual SkillData GetSkillData(){return null;}
//

//
//		public virtual void AnimationAttackOver(string clipName)
//		{
//
//		}

	}

	public class StateDef
	{
		public const uint Idle = 0;
		public const uint Attack = 1;
		public const uint Hurt = 2;
		public const uint Dead = 3;
		public const uint Win = 4;
	}
}
