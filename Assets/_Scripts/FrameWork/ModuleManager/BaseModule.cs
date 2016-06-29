/*******************************************************
 * 
 * 文件名(File Name)：             BaseModule
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/10 13:51:40
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
	public class BaseModule 
	{
		public enum E_RegisterMode
		{
			NotRegister,
			AutoRegister,
			AlreadyRegister,
		}

		public event StateChangedEvent StateChanged;
		private E_ObjectState _state = E_ObjectState.Initial;
		public E_ObjectState State
		{
			get
			{
				return _state;
			}
			set
			{
				if(_state == value)
					return;

				E_ObjectState oldState = _state;
				_state = value;

				if(StateChanged != null)
				{
					StateChanged(this,_state,oldState);
				}

				OnStateChanged(_state, oldState);
			}
		}

		protected virtual void OnStateChanged(E_ObjectState newState, E_ObjectState oldState)
		{

		}

		private E_RegisterMode _registerMode = E_RegisterMode.NotRegister;
		/// <summary>
		/// 是否自动注册
		/// </summary>
		/// <value><c>true</c> if auto register; otherwise, <c>false</c>.</value>
		public bool AutoRegister
		{
			get
			{
				return _registerMode == E_RegisterMode.NotRegister ? false : true;
			}
			set
			{
				if(_registerMode == E_RegisterMode.NotRegister || _registerMode == E_RegisterMode.AutoRegister)
					_registerMode = value ? E_RegisterMode.AutoRegister : E_RegisterMode.NotRegister;
			}
		}

		public bool HasRegistered
		{
			get
			{
				return _registerMode == E_RegisterMode.AlreadyRegister;
			}
		}

		/// <summary>
		/// 把module加到管理类ModuleMgr中
		/// </summary>
		public void Load()
		{
			if(State != E_ObjectState.Initial)
				return;

			State = E_ObjectState.Loading;

			if(_registerMode == E_RegisterMode.AutoRegister)
			{
				//注册到管理类中
				ModuleMgr.Instance.Register(this);
				_registerMode = E_RegisterMode.AlreadyRegister; 
			}

			OnLoad();
			State = E_ObjectState.Ready;
		}

		protected virtual void OnLoad()
		{
		}

		public void Release()
		{
			if (State != E_ObjectState.Disabled)
			{
				State = E_ObjectState.Disabled;

				// ...
				if (_registerMode == E_RegisterMode.AlreadyRegister)
				{
					//unregister
					//ModuleMgr.Instance.UnRegister(this);
					_registerMode = E_RegisterMode.AutoRegister;
				}

                OnRelease();
			}
		}

		protected virtual void OnRelease()
		{

		}

	}
}
