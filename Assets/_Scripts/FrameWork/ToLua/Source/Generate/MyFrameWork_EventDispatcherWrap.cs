using System;
using LuaInterface;

public class MyFrameWork_EventDispatcherWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.EventDispatcher), typeof(System.Object));
		L.RegFunction("ReleaseValue", ReleaseValue);
		L.RegFunction("OnApplicationQuit", OnApplicationQuit);
		L.RegFunction("MarkAsPermanent", MarkAsPermanent);
		L.RegFunction("Cleanup", Cleanup);
		L.RegFunction("AddListener", AddListener);
		L.RegFunction("RemoveListener", RemoveListener);
		L.RegFunction("TriggerEvent", TriggerEvent);
		L.RegFunction("New", _CreateMyFrameWork_EventDispatcher);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("TheRouter", get_TheRouter, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_EventDispatcher(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MyFrameWork.EventDispatcher obj = new MyFrameWork.EventDispatcher();
			ToLua.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MyFrameWork.EventDispatcher.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReleaseValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.EventDispatcher.ReleaseValue();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnApplicationQuit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.EventDispatcher.OnApplicationQuit();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MarkAsPermanent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			MyFrameWork.EventDispatcher.MarkAsPermanent(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Cleanup(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.EventDispatcher.Cleanup();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string arg0 = ToLua.CheckString(L, 1);
			System.Action arg1 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (System.Action)ToLua.CheckObject(L, 2, typeof(System.Action));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg1 = DelegateFactory.CreateDelegate(typeof(System.Action), func) as System.Action;
			}

			MyFrameWork.EventDispatcher.AddListener(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string arg0 = ToLua.CheckString(L, 1);
			System.Action arg1 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (System.Action)ToLua.CheckObject(L, 2, typeof(System.Action));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg1 = DelegateFactory.CreateDelegate(typeof(System.Action), func) as System.Action;
			}

			MyFrameWork.EventDispatcher.RemoveListener(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TriggerEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			MyFrameWork.EventDispatcher.TriggerEvent(arg0);
			return 0;
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
	static int get_TheRouter(IntPtr L)
	{
		ToLua.PushObject(L, MyFrameWork.EventDispatcher.TheRouter);
		return 1;
	}
}

