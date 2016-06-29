using System;
using LuaInterface;

public class UICenterOnChildWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UICenterOnChild), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Recenter", Recenter);
		L.RegFunction("CenterOn", CenterOn);
		L.RegFunction("New", _CreateUICenterOnChild);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("springStrength", get_springStrength, set_springStrength);
		L.RegVar("nextPageThreshold", get_nextPageThreshold, set_nextPageThreshold);
		L.RegVar("onFinished", get_onFinished, set_onFinished);
		L.RegVar("onCenter", get_onCenter, set_onCenter);
		L.RegVar("centeredObject", get_centeredObject, null);
		L.RegFunction("OnCenterCallback", UICenterOnChild_OnCenterCallback);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUICenterOnChild(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UICenterOnChild class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Recenter(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UICenterOnChild obj = (UICenterOnChild)ToLua.CheckObject(L, 1, typeof(UICenterOnChild));
			obj.Recenter();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CenterOn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UICenterOnChild obj = (UICenterOnChild)ToLua.CheckObject(L, 1, typeof(UICenterOnChild));
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Transform));
			obj.CenterOn(arg0);
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
	static int get_springStrength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			float ret = obj.springStrength;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index springStrength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nextPageThreshold(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			float ret = obj.nextPageThreshold;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index nextPageThreshold on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
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
	static int get_onCenter(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			UICenterOnChild.OnCenterCallback ret = obj.onCenter;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onCenter on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_centeredObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			UnityEngine.GameObject ret = obj.centeredObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index centeredObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_springStrength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.springStrength = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index springStrength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_nextPageThreshold(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.nextPageThreshold = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index nextPageThreshold on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
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
	static int set_onCenter(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UICenterOnChild obj = (UICenterOnChild)o;
			UICenterOnChild.OnCenterCallback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UICenterOnChild.OnCenterCallback)ToLua.CheckObject(L, 2, typeof(UICenterOnChild.OnCenterCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UICenterOnChild.OnCenterCallback), func) as UICenterOnChild.OnCenterCallback;
			}

			obj.onCenter = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onCenter on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UICenterOnChild_OnCenterCallback(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UICenterOnChild.OnCenterCallback), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
