using System;
using LuaInterface;

public class System_Collections_IEnumeratorWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Collections.IEnumerator), null);
		L.RegFunction("MoveNext", MoveNext);
		L.RegFunction("Reset", Reset);
		L.RegFunction("New", _CreateSystem_Collections_IEnumerator);
		L.RegVar("Current", get_Current, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystem_Collections_IEnumerator(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "System.Collections.IEnumerator class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MoveNext(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.IEnumerator obj = (System.Collections.IEnumerator)ToLua.CheckObject(L, 1, typeof(System.Collections.IEnumerator));
			bool o = obj.MoveNext();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.IEnumerator obj = (System.Collections.IEnumerator)ToLua.CheckObject(L, 1, typeof(System.Collections.IEnumerator));
			obj.Reset();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Current(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);

			System.Collections.IEnumerator obj = (System.Collections.IEnumerator)o;
			object ret = obj.Current;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Current on a nil value" : e.Message);
		}
	}
}

