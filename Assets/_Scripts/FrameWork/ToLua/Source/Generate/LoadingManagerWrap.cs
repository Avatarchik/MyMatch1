using System;
using LuaInterface;

public class LoadingManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LoadingManager), typeof(Manager));
		L.RegFunction("ShowLoading", ShowLoading);
		L.RegFunction("HideLoading", HideLoading);
		L.RegFunction("GetPercentLabel", GetPercentLabel);
		L.RegFunction("SetUpdateProgress", SetUpdateProgress);
		L.RegFunction("New", _CreateLoadingManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("CurLoadingScene", get_CurLoadingScene, set_CurLoadingScene);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLoadingManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "LoadingManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowLoading(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LoadingManager obj = (LoadingManager)ToLua.CheckObject(L, 1, typeof(LoadingManager));
			obj.ShowLoading();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideLoading(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LoadingManager obj = (LoadingManager)ToLua.CheckObject(L, 1, typeof(LoadingManager));
			obj.HideLoading();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPercentLabel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LoadingManager obj = (LoadingManager)ToLua.CheckObject(L, 1, typeof(LoadingManager));
			obj.GetPercentLabel();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetUpdateProgress(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LoadingManager obj = (LoadingManager)ToLua.CheckObject(L, 1, typeof(LoadingManager));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.SetUpdateProgress(arg0);
			return 0;
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
	static int get_CurLoadingScene(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LoadingManager obj = (LoadingManager)o;
			UnityEngine.AsyncOperation ret = obj.CurLoadingScene;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index CurLoadingScene on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CurLoadingScene(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LoadingManager obj = (LoadingManager)o;
			UnityEngine.AsyncOperation arg0 = (UnityEngine.AsyncOperation)ToLua.CheckObject(L, 2, typeof(UnityEngine.AsyncOperation));
			obj.CurLoadingScene = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index CurLoadingScene on a nil value" : e.Message);
		}
	}
}

