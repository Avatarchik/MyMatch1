using System;
using LuaInterface;

public class MyFrameWork_LuaManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.LuaManager), typeof(Manager));
		L.RegFunction("InitStart", InitStart);
		L.RegFunction("DoFile", DoFile);
		L.RegFunction("CallFunction", CallFunction);
		L.RegFunction("LuaGC", LuaGC);
		L.RegFunction("Close", Close);
		L.RegFunction("New", _CreateMyFrameWork_LuaManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_LuaManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.LuaManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitStart(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaManager obj = (MyFrameWork.LuaManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaManager));
			obj.InitStart();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DoFile(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.LuaManager obj = (MyFrameWork.LuaManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaManager));
			string arg0 = ToLua.CheckString(L, 2);
			object[] o = obj.DoFile(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CallFunction(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.LuaManager obj = (MyFrameWork.LuaManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaManager));
			string arg0 = ToLua.CheckString(L, 2);
			object[] arg1 = ToLua.ToParamsObject(L, 3, count - 2);
			object[] o = obj.CallFunction(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaGC(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaManager obj = (MyFrameWork.LuaManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaManager));
			obj.LuaGC();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaManager obj = (MyFrameWork.LuaManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaManager));
			obj.Close();
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

