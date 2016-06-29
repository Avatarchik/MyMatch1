using System;
using LuaInterface;

public class MyFrameWork_UIMgrWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.UIMgr), typeof(Manager));
		L.RegFunction("Init", Init);
		L.RegFunction("OnReleaseValue", OnReleaseValue);
		L.RegFunction("OnAppQuit", OnAppQuit);
		L.RegFunction("GetCurrentOpenUI", GetCurrentOpenUI);
		L.RegFunction("CreateUI", CreateUI);
		L.RegFunction("CreateUI_LUA", CreateUI_LUA);
		L.RegFunction("ShowUIAndCloseOthers", ShowUIAndCloseOthers);
		L.RegFunction("ShowUIAndCloseOthers_LUA", ShowUIAndCloseOthers_LUA);
		L.RegFunction("GetUIByType", GetUIByType);
		L.RegFunction("ShowUI", ShowUI);
		L.RegFunction("ShowUI_LUA", ShowUI_LUA);
		L.RegFunction("HideUI", HideUI);
		L.RegFunction("DestroyUI", DestroyUI);
		L.RegFunction("Update", Update);
		L.RegFunction("SetPanelDepth", SetPanelDepth);
		L.RegFunction("OnTopReturn", OnTopReturn);
		L.RegFunction("ShowMessageBox", ShowMessageBox);
		L.RegFunction("CloseMessageBox", CloseMessageBox);
		L.RegFunction("ChangeLevel", ChangeLevel);
		L.RegFunction("GetInput", GetInput);
		L.RegFunction("GetNamingInput", GetNamingInput);
		L.RegFunction("New", _CreateMyFrameWork_UIMgr);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("_listCmd", get__listCmd, set__listCmd);
		L.RegVar("Instance", get_Instance, null);
		L.RegVar("UIContainer", get_UIContainer, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_UIMgr(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.UIMgr class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnReleaseValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.OnReleaseValue();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnAppQuit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.OnAppQuit();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCurrentOpenUI(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			System.Collections.Generic.List<MyFrameWork.E_UIType> o = obj.GetCurrentOpenUI();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateUI(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			System.Type arg1 = (System.Type)ToLua.CheckObject(L, 3, typeof(System.Type));
			System.Action<MyFrameWork.BaseUI> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (System.Action<MyFrameWork.BaseUI>)ToLua.CheckObject(L, 4, typeof(System.Action<MyFrameWork.BaseUI>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 4);
				arg2 = DelegateFactory.CreateDelegate(typeof(System.Action<MyFrameWork.BaseUI>), func) as System.Action<MyFrameWork.BaseUI>;
			}

			obj.CreateUI(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateUI_LUA(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			obj.CreateUI_LUA(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUIAndCloseOthers(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			System.Type arg1 = (System.Type)ToLua.CheckObject(L, 3, typeof(System.Type));
			System.Action<MyFrameWork.BaseUI> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (System.Action<MyFrameWork.BaseUI>)ToLua.CheckObject(L, 4, typeof(System.Action<MyFrameWork.BaseUI>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 4);
				arg2 = DelegateFactory.CreateDelegate(typeof(System.Action<MyFrameWork.BaseUI>), func) as System.Action<MyFrameWork.BaseUI>;
			}

			bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
			object[] arg4 = ToLua.ToParamsObject(L, 6, count - 5);
			obj.ShowUIAndCloseOthers(arg0, arg1, arg2, arg3, arg4);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUIAndCloseOthers_LUA(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			object[] arg2 = ToLua.ToParamsObject(L, 4, count - 3);
			obj.ShowUIAndCloseOthers_LUA(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUIByType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			MyFrameWork.BaseUI o = obj.GetUIByType(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUI(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			System.Type arg1 = (System.Type)ToLua.CheckObject(L, 3, typeof(System.Type));
			System.Action<MyFrameWork.BaseUI> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (System.Action<MyFrameWork.BaseUI>)ToLua.CheckObject(L, 4, typeof(System.Action<MyFrameWork.BaseUI>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 4);
				arg2 = DelegateFactory.CreateDelegate(typeof(System.Action<MyFrameWork.BaseUI>), func) as System.Action<MyFrameWork.BaseUI>;
			}

			bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
			bool arg4 = LuaDLL.luaL_checkboolean(L, 6);
			object[] arg5 = ToLua.ToParamsObject(L, 7, count - 6);
			obj.ShowUI(arg0, arg1, arg2, arg3, arg4, arg5);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUI_LUA(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			bool arg2 = LuaDLL.luaL_checkboolean(L, 4);
			bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
			object[] arg4 = ToLua.ToParamsObject(L, 6, count - 5);
			obj.ShowUI_LUA(arg0, arg1, arg2, arg3, arg4);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideUI(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			obj.HideUI(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyUI(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			obj.DestroyUI(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.Update();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPanelDepth(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			MyFrameWork.BaseUI arg0 = (MyFrameWork.BaseUI)ToLua.CheckUnityObject(L, 2, typeof(MyFrameWork.BaseUI));
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.SetPanelDepth(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnTopReturn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.OnTopReturn();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowMessageBox(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(MyFrameWork.UIMgr), typeof(string)))
			{
				MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				obj.ShowMessageBox(arg0);
				return 0;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(MyFrameWork.UIMgr), typeof(string), typeof(string), typeof(UIEventListener.VoidDelegate)))
			{
				MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				UIEventListener.VoidDelegate arg2 = null;
				LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

				if (funcType4 != LuaTypes.LUA_TFUNCTION)
				{
					 arg2 = (UIEventListener.VoidDelegate)ToLua.ToObject(L, 4);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 4);
					arg2 = DelegateFactory.CreateDelegate(typeof(UIEventListener.VoidDelegate), func) as UIEventListener.VoidDelegate;
				}

				obj.ShowMessageBox(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(MyFrameWork.UIMgr), typeof(string), typeof(string), typeof(UIEventListener.VoidDelegate), typeof(string), typeof(UIEventListener.VoidDelegate)))
			{
				MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				UIEventListener.VoidDelegate arg2 = null;
				LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

				if (funcType4 != LuaTypes.LUA_TFUNCTION)
				{
					 arg2 = (UIEventListener.VoidDelegate)ToLua.ToObject(L, 4);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 4);
					arg2 = DelegateFactory.CreateDelegate(typeof(UIEventListener.VoidDelegate), func) as UIEventListener.VoidDelegate;
				}

				string arg3 = ToLua.ToString(L, 5);
				UIEventListener.VoidDelegate arg4 = null;
				LuaTypes funcType6 = LuaDLL.lua_type(L, 6);

				if (funcType6 != LuaTypes.LUA_TFUNCTION)
				{
					 arg4 = (UIEventListener.VoidDelegate)ToLua.ToObject(L, 6);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 6);
					arg4 = DelegateFactory.CreateDelegate(typeof(UIEventListener.VoidDelegate), func) as UIEventListener.VoidDelegate;
				}

				obj.ShowMessageBox(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: MyFrameWork.UIMgr.ShowMessageBox");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseMessageBox(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			obj.CloseMessageBox();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeLevel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.ChangeLevel(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInput(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			string o = obj.GetInput();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNamingInput(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.UIMgr));
			string o = obj.GetNamingInput();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_ToString(IntPtr L)
	{
		object obj = ToLua.ToObject(L, 1);

		if (obj != null)
		{
			LuaDLL.lua_pushstring(L, obj.ToString());
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}

		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__listCmd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)o;
			System.Collections.Generic.List<MyFrameWork.Command> ret = obj._listCmd;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _listCmd on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.UIMgr.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIContainer(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)o;
			UnityEngine.Transform ret = obj.UIContainer;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index UIContainer on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__listCmd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.UIMgr obj = (MyFrameWork.UIMgr)o;
			System.Collections.Generic.List<MyFrameWork.Command> arg0 = (System.Collections.Generic.List<MyFrameWork.Command>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<MyFrameWork.Command>));
			obj._listCmd = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _listCmd on a nil value" : e.Message);
		}
	}
}

