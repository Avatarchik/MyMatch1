using System;
using LuaInterface;

public class UnityEngine_AsyncOperationWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.AsyncOperation), typeof(System.Object));
		L.RegFunction("New", _CreateUnityEngine_AsyncOperation);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("isDone", get_isDone, null);
		L.RegVar("progress", get_progress, null);
		L.RegVar("priority", get_priority, set_priority);
		L.RegVar("allowSceneActivation", get_allowSceneActivation, set_allowSceneActivation);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_AsyncOperation(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.AsyncOperation obj = new UnityEngine.AsyncOperation();
			ToLua.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.AsyncOperation.New");
		}

		return 0;
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
	static int get_isDone(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			bool ret = obj.isDone;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isDone on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_progress(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			float ret = obj.progress;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index progress on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_priority(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			int ret = obj.priority;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index priority on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allowSceneActivation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			bool ret = obj.allowSceneActivation;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index allowSceneActivation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_priority(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.priority = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index priority on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_allowSceneActivation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AsyncOperation obj = (UnityEngine.AsyncOperation)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.allowSceneActivation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index allowSceneActivation on a nil value" : e.Message);
		}
	}
}

