using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
	public interface IState
	{
		/// <summary>
		/// Gets the state identifier.
		/// </summary>
		/// <returns>The state identifier.</returns>
		uint GetStateId();
		/// <summary>
		/// Raises the enter event.
		/// </summary>
		void OnEnter(StateMachine fsm,IState prevState,object param1,object param2);
		/// <summary>
		/// Raises the leave event.
		/// </summary>
		void OnLeave(IState nextState,object param1,object param2);
		/// <summary>
		/// Raises the update event.
		/// </summary>
		void OnUpdate();
		/// <summary>
		/// Raises the fixed update event.
		/// </summary>
		void OnFixedUpdate();
		/// <summary>
		/// Raises the late update event.
		/// </summary>
		void OnLateUpdate();
	}
}
