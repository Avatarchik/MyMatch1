using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyFrameWork
{
	public class StateMachine 
	{
		private Dictionary<uint,IState> _dicState;

		private IState _currentState;
		public IState GetCurrentState
		{
			get
			{
				return _currentState;
			}
		}

		public uint CurrentStateId
		{
			get
			{
				return _currentState == null ? 0 : _currentState.GetStateId();
			}
		}

		public StateMachine()
		{
			_dicState = new Dictionary<uint, IState>();

			_currentState = null;
		}

		public bool RegisterState(IState state)
		{
			if(state == null)
			{
				Debug.LogError("StateMachine.RegisterState state is null.");
				return false;
			}

			uint key = state.GetStateId();
			if(_dicState.ContainsKey(key))
			{
				Debug.LogError("StateMachine.RegisterState state have key:" + key);
				return false;
			}

			_dicState.Add(key,state);
			return true;
		}

		public bool RemoveState(uint stateId)
		{
			if(!_dicState.ContainsKey(stateId))
			{
				return false;
			}

			if(_currentState != null 
			   && _currentState.GetStateId() == stateId)
			{
				return false;
			}

			_dicState.Remove(stateId);
			return true;
		}

		public IState GetState(uint stateId)
		{
			IState state = null;
			_dicState.TryGetValue(stateId,out state);

			return state;
		}

		public void StopState(object param1,object param2)
		{
			if(_currentState == null)
				return;

			_currentState.OnLeave(null,param1,param2);
			_currentState = null;
		}


		public delegate void BetweenSwitchState(IState fromState,IState toState,object param1,object param2);
		public BetweenSwitchState BetweenSwitchStateCallBack = null;

		public bool SwitchState(uint newStateId,object param1 = null,object param2 = null,bool isForce = false)
		{
			if(_currentState != null && (!isForce && _currentState.GetStateId() == newStateId))
			{
				return false;
			}

			IState newState = null;
			_dicState.TryGetValue(newStateId,out newState);
			if(newState == null)
			{
				return false;
			}

//			if(_currentState != null)
//				Debug.Log("<color=red>switch</color>:" + _currentState + " "  + _currentState.GetStateId() + "->" + newStateId);

			if(_currentState != null)
			{
				_currentState.OnLeave(newState,param1,param2);
			}



			IState oldState = _currentState;
			_currentState = newState;

			if(BetweenSwitchStateCallBack != null)
			{
				BetweenSwitchStateCallBack(oldState,_currentState,param1,param2);
			}

			newState.OnEnter(this,oldState,param1,param2);

			return true;
		}


		public bool IsCurrentState(uint stateId)
		{
			return _currentState == null ? false : _currentState.GetStateId() == stateId;
		}

		public void OnUpdate()
		{
			if(_currentState != null)
			{
				_currentState.OnUpdate();
			}
		}

		public void OnFixedUpdate()
		{
			if(_currentState != null)
			{
				_currentState.OnFixedUpdate();
			}
		}

		public void OnLateUpdate()
		{
			if(_currentState != null)
			{
				_currentState.OnLateUpdate();
			}
		}
	}
}
