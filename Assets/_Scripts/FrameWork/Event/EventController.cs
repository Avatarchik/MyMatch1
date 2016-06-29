//#define LOG_ALL_MESSAGES
//#define LOG_ADD_LISTENER
//#define LOG_BROADCAST_MESSAGE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

namespace MyFrameWork
{
	/// <summary>
	/// 事件处理类。
	/// </summary>
	public class EventController
	{
		/// <summary>
		/// 释放变量
		/// </summary>
		public void ReleaseValue()
		{
			_theRouter.Clear();
			_permanentEvents.Clear();
		}

		public void OnApplicationQuit()
		{
			ReleaseValue();

			_theRouter = null;
			_permanentEvents = null;
		}

		private Dictionary<string, Delegate> _theRouter = new Dictionary<string, Delegate>();

		public Dictionary<string, Delegate> TheRouter
		{
			get { return _theRouter; }
		}

		/// <summary>
		/// 永久注册的事件列表
		/// </summary>
		private List<string> _permanentEvents = new List<string>();

		/// <summary>
		/// 标记为永久注册事件
		/// </summary>
		/// <param name="eventType"></param>
		public void MarkAsPermanent(string eventType)
		{
			#if LOG_ALL_MESSAGES
			DebugUtil.Info("Messenger MarkAsPermanent \t\"" + eventType + "\"");
			#endif
			if(!_permanentEvents.Contains(eventType))
			{
				_permanentEvents.Add(eventType);
			}
		}

		/// <summary>
		/// 判断是否已经包含事件
		/// </summary>
		/// <param name="eventType"></param>
		/// <returns></returns>
		public bool ContainsEvent(string eventType)
		{
			return _theRouter.ContainsKey(eventType);
		}

		/// <summary>
		/// 清除非永久性注册的事件
		/// </summary>
		public void Cleanup()
		{
			#if LOG_ALL_MESSAGES
			DebugUtil.Info("MESSENGER Cleanup. Make sure that none of necessary listeners are removed.");
			#endif
			List<string> eventToRemove = new List<string>();

			foreach (KeyValuePair<string, Delegate> pair in _theRouter)
			{
				bool wasFound = false;
				foreach (string Event in _permanentEvents)
				{
					if (pair.Key == Event)
					{
						wasFound = true;
						break;
					}
				}

				if (!wasFound)
					eventToRemove.Add(pair.Key);
			}

			foreach (string Event in eventToRemove)
			{
				_theRouter.Remove(Event);
			}
		}

		/// <summary>
		/// 处理增加监听器前的事项， 检查 参数等
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="listenerBeingAdded"></param>
		private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
		{
			#if LOG_ALL_MESSAGES || LOG_ADD_LISTENER
			DebugUtil.Info("MESSENGER OnListenerAdding \t\"" + eventType + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
			#endif

			if (!_theRouter.ContainsKey(eventType))
			{
				_theRouter.Add(eventType, null);
			}

			Delegate d = _theRouter[eventType];
			if (d != null && d.GetType() != listenerBeingAdded.GetType())
			{
				throw new EventException(string.Format(
					"Try to add not correct event {0}. Current type is {1}, adding type is {2}.",
					eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
			}
		}

		/// <summary>
		/// 移除监听器之前的检查
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="listenerBeingRemoved"></param>
		private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
		{
			#if LOG_ALL_MESSAGES
			DebugUtil.Info("MESSENGER OnListenerRemoving \t\"" + eventType + "\"\t{" + listenerBeingRemoved.Target + " -> " + listenerBeingRemoved.Method + "}");
			#endif

			if (_theRouter == null || !_theRouter.ContainsKey(eventType))
			{
				return false;
			}

			Delegate d = _theRouter[eventType];
			if ((d != null) && (d.GetType() != listenerBeingRemoved.GetType()))
			{
				throw new EventException(string.Format(
					"Remove listener {0}\" failed, Current type is {1}, adding type is {2}.",
					eventType, d.GetType(), listenerBeingRemoved.GetType()));
			}
			else
				return true;
		}

		/// <summary>
		/// 移除监听器之后的处理。删掉事件
		/// </summary>
		/// <param name="eventType"></param>
		private void OnListenerRemoved(string eventType)
		{
			if (_theRouter.ContainsKey(eventType) && _theRouter[eventType] == null)
			{
				_theRouter.Remove(eventType);
			}
		}

		#region 增加监听器
		/// <summary>
		///  增加监听器， 不带参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void AddListener(string eventType, Action handler)
		{
			OnListenerAdding(eventType, handler);
			_theRouter[eventType] = (Action)_theRouter[eventType] + handler;
		}

		/// <summary>
		///  增加监听器， 1个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void AddListener<T>(string eventType, Action<T> handler)
		{
			OnListenerAdding(eventType, handler);
			_theRouter[eventType] = (Action<T>)_theRouter[eventType] + handler;
		}

		/// <summary>
		///  增加监听器， 2个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void AddListener<T, U>(string eventType, Action<T, U> handler)
		{
			OnListenerAdding(eventType, handler);
			_theRouter[eventType] = (Action<T, U>)_theRouter[eventType] + handler;
		}

		/// <summary>
		///  增加监听器， 3个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void AddListener<T, U, V>(string eventType, Action<T, U, V> handler)
		{
			OnListenerAdding(eventType, handler);
			_theRouter[eventType] = (Action<T, U, V>)_theRouter[eventType] + handler;
		}

		/// <summary>
		///  增加监听器， 4个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void AddListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
		{
			OnListenerAdding(eventType, handler);
			_theRouter[eventType] = (Action<T, U, V, W>)_theRouter[eventType] + handler;
		}
		#endregion

		#region 移除监听器

		/// <summary>
		///  移除监听器， 不带参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void RemoveListener(string eventType, Action handler)
		{
			if (OnListenerRemoving(eventType, handler))
			{
				_theRouter[eventType] = (Action)_theRouter[eventType] - handler;
				OnListenerRemoved(eventType);
			}
		}

		/// <summary>
		///  移除监听器， 1个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void RemoveListener<T>(string eventType, Action<T> handler)
		{
			if (OnListenerRemoving(eventType, handler))
			{
				_theRouter[eventType] = (Action<T>)_theRouter[eventType] - handler;
				OnListenerRemoved(eventType);
			}
		}

		/// <summary>
		///  移除监听器， 2个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void RemoveListener<T, U>(string eventType, Action<T, U> handler)
		{
			if (OnListenerRemoving(eventType, handler))
			{
				_theRouter[eventType] = (Action<T, U>)_theRouter[eventType] - handler;
				OnListenerRemoved(eventType);
			}
		}

		/// <summary>
		///  移除监听器， 3个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void RemoveListener<T, U, V>(string eventType, Action<T, U, V> handler)
		{
			if (OnListenerRemoving(eventType, handler))
			{
				_theRouter[eventType] = (Action<T, U, V>)_theRouter[eventType] - handler;
				OnListenerRemoved(eventType);
			}
		}

		/// <summary>
		///  移除监听器， 4个参数
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void RemoveListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
		{
			if (OnListenerRemoving(eventType, handler))
			{
				_theRouter[eventType] = (Action<T, U, V, W>)_theRouter[eventType] - handler;
				OnListenerRemoved(eventType);
			}
		}
		#endregion

		#region 触发事件
		private Delegate GetDelegate(string eventType)
		{
			Delegate d = null;
			if (!_theRouter.TryGetValue(eventType, out d))
			{
				DebugUtil.Info("没有设置事件监听：" + eventType);
			}

			return d;
		}
		/// <summary>
		///  触发事件， 不带参数触发
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void TriggerEvent(string eventType)
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			DebugUtil.Info("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif

			Delegate d = GetDelegate(eventType);
			if (d == null) return;

			var callbacks = d.GetInvocationList();
			for (int i = 0; i < callbacks.Length; i++)
			{
				Action callback = callbacks[i] as Action;

				if (callback == null)
				{
					throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
				}

				try
				{

					callback();
				}
				catch (Exception ex)
				{
					DebugUtil.Except(ex);
				}
			}
		}

		/// <summary>
		///  触发事件， 带1个参数触发
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void TriggerEvent<T>(string eventType, T arg1)
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			DebugUtil.Info("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif

			Delegate d = GetDelegate(eventType);
			if (d == null) return;

			var callbacks = d.GetInvocationList();
			for (int i = 0; i < callbacks.Length; i++)
			{
				Action<T> callback = callbacks[i] as Action<T>;

				if (callback == null)
				{
					throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
				}

				try
				{
					callback(arg1);
				}
				catch (Exception ex)
				{
					DebugUtil.Except(ex);
				}
			}
		}

		/// <summary>
		///  触发事件， 带2个参数触发
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			DebugUtil.Info("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif

			Delegate d = GetDelegate(eventType);
			if (d == null) return;

			var callbacks = d.GetInvocationList();
			for (int i = 0; i < callbacks.Length; i++)
			{
				Action<T, U> callback = callbacks[i] as Action<T, U>;

				if (callback == null)
				{
					throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
				}

				try
				{
					callback(arg1, arg2);
				}
				catch (Exception ex)
				{
					DebugUtil.Except(ex);
				}
			}
		}

		/// <summary>
		///  触发事件， 带3个参数触发
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			DebugUtil.Info("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif

			Delegate d = GetDelegate(eventType);
			if (d == null) return;

			var callbacks = d.GetInvocationList();
			for (int i = 0; i < callbacks.Length; i++)
			{
				Action<T, U, V> callback = callbacks[i] as Action<T, U, V>;

				if (callback == null)
				{
					throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
				}
				try
				{
					callback(arg1, arg2, arg3);
				}
				catch (Exception ex)
				{
					DebugUtil.Except(ex);
				}
			}
		}

		/// <summary>
		///  触发事件， 带4个参数触发
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="handler"></param>
		public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			DebugUtil.Info("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif

			Delegate d = GetDelegate(eventType);
			if (d == null) return;

			var callbacks = d.GetInvocationList();
			for (int i = 0; i < callbacks.Length; i++)
			{
				Action<T, U, V, W> callback = callbacks[i] as Action<T, U, V, W>;

				if (callback == null)
				{
					throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
				}
				try
				{
					callback(arg1, arg2, arg3, arg4);
				}
				catch (Exception ex)
				{
					DebugUtil.Except(ex);
				}
			}
		}

		#endregion
	}

}