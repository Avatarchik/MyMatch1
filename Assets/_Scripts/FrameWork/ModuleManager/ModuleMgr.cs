/*******************************************************
 * 
 * 文件名(File Name)：             ModuleMgr
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/10 14:11:08
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MyFrameWork
{
	public class ModuleMgr :  Singleton<ModuleMgr>
	{
		private Dictionary<string,BaseModule> _dicModules;

		public override void Init()
		{
			_dicModules = new Dictionary<string, BaseModule>();
		}

//		protected override void OnReleaseValue()
//		{
//			_dicModules.Clear();
//		}
//
//		protected override void OnAppQuit()
//		{
//			_dicModules = null;
//		}

		#region get Module
		public BaseModule Get(string key)
		{
			if(_dicModules.ContainsKey(key))
				return _dicModules[key];

			return null;
		}

		public T Get<T>() where T : BaseModule
		{
			Type t = typeof(T);
			return Get(t.ToString()) as T;
		}
		#endregion

		#region 注册Module
		public void Register(BaseModule module)
		{
			Type t = module.GetType();
			Register(t.ToString(),module);
		}

		public void Register(string key,BaseModule module)
		{
			if(!_dicModules.ContainsKey(key))
				_dicModules.Add(key,module);
		}

		/// <summary>
		/// 加载所有module
		/// </summary>
		public void RegisterAllModules()
		{
			LoadModule(typeof(FightNew.ModuleFight));
			LoadModule(typeof(ModuleCard));
        }

		private void LoadModule(Type moduleType)
		{
			BaseModule bm = System.Activator.CreateInstance(moduleType) as BaseModule;
			bm.Load();
		}
		#endregion

		#region 取消注册Module
		/// <summary>
		/// Uns the register.
		/// </summary>
		/// <param name="module">Module.</param>
		public void UnRegister(BaseModule module)
		{
			Type t = module.GetType();
			UnRegister(t.ToString());
		}

		/// <summary>
		/// Uns the register.
		/// </summary>
		/// <param name="key">Key.</param>
		public void UnRegister(string key)
		{
			if(_dicModules.ContainsKey(key))
			{
				BaseModule module = _dicModules[key];
				module.Release();
				_dicModules.Remove(key);
				module = null;
			}
		}

		/// <summary>
		/// Uns the register all.
		/// </summary>
		public void UnRegisterAll()
		{
			List<string> _keyList = new List<string>(_dicModules.Keys);
			for (int i = 0;i < _keyList.Count;i++)
			{
				UnRegister(_keyList[i]);
			}

			_dicModules.Clear();
		}
		#endregion
	}
}
