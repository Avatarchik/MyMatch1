using System;
using LuaInterface;

public class UIProgressBarWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UIProgressBar), typeof(UIWidgetContainer));
		L.RegFunction("Set", Set);
		L.RegFunction("Start", Start);
		L.RegFunction("ForceUpdate", ForceUpdate);
		L.RegFunction("OnPan", OnPan);
		L.RegFunction("New", _CreateUIProgressBar);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("current", get_current, set_current);
		L.RegVar("onDragFinished", get_onDragFinished, set_onDragFinished);
		L.RegVar("thumb", get_thumb, set_thumb);
		L.RegVar("numberOfSteps", get_numberOfSteps, set_numberOfSteps);
		L.RegVar("onChange", get_onChange, set_onChange);
		L.RegVar("cachedTransform", get_cachedTransform, null);
		L.RegVar("cachedCamera", get_cachedCamera, null);
		L.RegVar("foregroundWidget", get_foregroundWidget, set_foregroundWidget);
		L.RegVar("backgroundWidget", get_backgroundWidget, set_backgroundWidget);
		L.RegVar("fillDirection", get_fillDirection, set_fillDirection);
		L.RegVar("value", get_value, set_value);
		L.RegVar("alpha", get_alpha, set_alpha);
		L.RegFunction("OnDragFinished", UIProgressBar_OnDragFinished);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIProgressBar(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UIProgressBar class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Set(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UIProgressBar obj = (UIProgressBar)ToLua.CheckObject(L, 1, typeof(UIProgressBar));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.Set(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Start(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIProgressBar obj = (UIProgressBar)ToLua.CheckObject(L, 1, typeof(UIProgressBar));
			obj.Start();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceUpdate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIProgressBar obj = (UIProgressBar)ToLua.CheckObject(L, 1, typeof(UIProgressBar));
			obj.ForceUpdate();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPan(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIProgressBar obj = (UIProgressBar)ToLua.CheckObject(L, 1, typeof(UIProgressBar));
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.OnPan(arg0);
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
	static int get_current(IntPtr L)
	{
		ToLua.Push(L, UIProgressBar.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDragFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIProgressBar.OnDragFinished ret = obj.onDragFinished;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onDragFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_thumb(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UnityEngine.Transform ret = obj.thumb;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index thumb on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_numberOfSteps(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			int ret = obj.numberOfSteps;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index numberOfSteps on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onChange(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			System.Collections.Generic.List<EventDelegate> ret = obj.onChange;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onChange on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UnityEngine.Transform ret = obj.cachedTransform;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cachedTransform on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedCamera(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UnityEngine.Camera ret = obj.cachedCamera;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cachedCamera on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_foregroundWidget(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIWidget ret = obj.foregroundWidget;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index foregroundWidget on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_backgroundWidget(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIWidget ret = obj.backgroundWidget;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index backgroundWidget on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillDirection(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIProgressBar.FillDirection ret = obj.fillDirection;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fillDirection on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			float ret = obj.value;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index value on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alpha(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			float ret = obj.alpha;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alpha on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		try
		{
			UIProgressBar arg0 = (UIProgressBar)ToLua.CheckUnityObject(L, 2, typeof(UIProgressBar));
			UIProgressBar.current = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDragFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIProgressBar.OnDragFinished arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIProgressBar.OnDragFinished)ToLua.CheckObject(L, 2, typeof(UIProgressBar.OnDragFinished));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIProgressBar.OnDragFinished), func) as UIProgressBar.OnDragFinished;
			}

			obj.onDragFinished = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onDragFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_thumb(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Transform));
			obj.thumb = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index thumb on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_numberOfSteps(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.numberOfSteps = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index numberOfSteps on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onChange(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			System.Collections.Generic.List<EventDelegate> arg0 = (System.Collections.Generic.List<EventDelegate>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<EventDelegate>));
			obj.onChange = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onChange on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_foregroundWidget(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIWidget arg0 = (UIWidget)ToLua.CheckUnityObject(L, 2, typeof(UIWidget));
			obj.foregroundWidget = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index foregroundWidget on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_backgroundWidget(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIWidget arg0 = (UIWidget)ToLua.CheckUnityObject(L, 2, typeof(UIWidget));
			obj.backgroundWidget = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index backgroundWidget on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillDirection(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			UIProgressBar.FillDirection arg0 = (UIProgressBar.FillDirection)ToLua.CheckObject(L, 2, typeof(UIProgressBar.FillDirection));
			obj.fillDirection = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fillDirection on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.value = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index value on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alpha(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIProgressBar obj = (UIProgressBar)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.alpha = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alpha on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UIProgressBar_OnDragFinished(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UIProgressBar.OnDragFinished), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

