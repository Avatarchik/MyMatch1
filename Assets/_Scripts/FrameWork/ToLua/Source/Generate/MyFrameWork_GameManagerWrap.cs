using System;
using LuaInterface;

public class MyFrameWork_GameManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.GameManager), typeof(Manager));
		L.RegFunction("OnFindOpponentCBK", OnFindOpponentCBK);
		L.RegFunction("Init", Init);
		L.RegFunction("CheckExtractResource", CheckExtractResource);
		L.RegFunction("OnResourceInited", OnResourceInited);
		L.RegFunction("LoadFightScene", LoadFightScene);
		L.RegFunction("GetLastInputID", GetLastInputID);
		L.RegFunction("OnDelMatchedInfo", OnDelMatchedInfo);
		L.RegFunction("New", _CreateMyFrameWork_GameManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_GameManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.GameManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFindOpponentCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.OnFindOpponentCBK();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckExtractResource(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.CheckExtractResource();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnResourceInited(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.OnResourceInited();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFightScene(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.LoadFightScene();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLastInputID(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			string o = obj.GetLastInputID();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDelMatchedInfo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.GameManager obj = (MyFrameWork.GameManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.GameManager));
			obj.OnDelMatchedInfo();
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
}

