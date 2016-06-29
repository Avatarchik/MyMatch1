using System;
using LuaInterface;

public class SpringPanelWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SpringPanel), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Begin", Begin);
		L.RegFunction("New", _CreateSpringPanel);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("current", get_current, set_current);
		L.RegVar("target", get_target, set_target);
		L.RegVar("strength", get_strength, set_strength);
		L.RegVar("onFinished", get_onFinished, set_onFinished);
		L.RegFunction("OnFinished", SpringPanel_OnFinished);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSpringPanel(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "SpringPanel class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Begin(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.GameObject));
			UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
			float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
			SpringPanel o = SpringPanel.Begin(arg0, arg1, arg2);
			ToLua.Push(L, o);
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
	static int get_current(IntPtr L)
	{
		ToLua.Push(L, SpringPanel.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			UnityEngine.Vector3 ret = obj.target;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index target on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_strength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			float ret = obj.strength;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index strength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			SpringPanel.OnFinished ret = obj.onFinished;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		try
		{
			SpringPanel arg0 = (SpringPanel)ToLua.CheckUnityObject(L, 2, typeof(SpringPanel));
			SpringPanel.current = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.target = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index target on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_strength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.strength = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index strength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SpringPanel obj = (SpringPanel)o;
			SpringPanel.OnFinished arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (SpringPanel.OnFinished)ToLua.CheckObject(L, 2, typeof(SpringPanel.OnFinished));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(SpringPanel.OnFinished), func) as SpringPanel.OnFinished;
			}

			obj.onFinished = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SpringPanel_OnFinished(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(SpringPanel.OnFinished), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

