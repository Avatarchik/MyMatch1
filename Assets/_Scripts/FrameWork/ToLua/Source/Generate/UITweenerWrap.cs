using System;
using LuaInterface;

public class UITweenerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UITweener), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("SetOnFinished", SetOnFinished);
		L.RegFunction("AddOnFinished", AddOnFinished);
		L.RegFunction("RemoveOnFinished", RemoveOnFinished);
		L.RegFunction("Sample", Sample);
		L.RegFunction("PlayForward", PlayForward);
		L.RegFunction("PlayReverse", PlayReverse);
		L.RegFunction("Play", Play);
		L.RegFunction("ResetToBeginning", ResetToBeginning);
		L.RegFunction("Toggle", Toggle);
		L.RegFunction("SetStartToCurrentValue", SetStartToCurrentValue);
		L.RegFunction("SetEndToCurrentValue", SetEndToCurrentValue);
		L.RegFunction("New", _CreateUITweener);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("current", get_current, set_current);
		L.RegVar("method", get_method, set_method);
		L.RegVar("style", get_style, set_style);
		L.RegVar("animationCurve", get_animationCurve, set_animationCurve);
		L.RegVar("ignoreTimeScale", get_ignoreTimeScale, set_ignoreTimeScale);
		L.RegVar("delay", get_delay, set_delay);
		L.RegVar("duration", get_duration, set_duration);
		L.RegVar("steeperCurves", get_steeperCurves, set_steeperCurves);
		L.RegVar("tweenGroup", get_tweenGroup, set_tweenGroup);
		L.RegVar("onFinished", get_onFinished, set_onFinished);
		L.RegVar("eventReceiver", get_eventReceiver, set_eventReceiver);
		L.RegVar("callWhenFinished", get_callWhenFinished, set_callWhenFinished);
		L.RegVar("amountPerDelta", get_amountPerDelta, null);
		L.RegVar("tweenFactor", get_tweenFactor, set_tweenFactor);
		L.RegVar("direction", get_direction, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUITweener(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UITweener class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetOnFinished(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate)))
			{
				UITweener obj = (UITweener)ToLua.ToObject(L, 1);
				EventDelegate arg0 = (EventDelegate)ToLua.ToObject(L, 2);
				obj.SetOnFinished(arg0);
				return 0;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate.Callback)))
			{
				UITweener obj = (UITweener)ToLua.ToObject(L, 1);
				EventDelegate.Callback arg0 = null;
				LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

				if (funcType2 != LuaTypes.LUA_TFUNCTION)
				{
					 arg0 = (EventDelegate.Callback)ToLua.ToObject(L, 2);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 2);
					arg0 = DelegateFactory.CreateDelegate(typeof(EventDelegate.Callback), func) as EventDelegate.Callback;
				}

				obj.SetOnFinished(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UITweener.SetOnFinished");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddOnFinished(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate)))
			{
				UITweener obj = (UITweener)ToLua.ToObject(L, 1);
				EventDelegate arg0 = (EventDelegate)ToLua.ToObject(L, 2);
				obj.AddOnFinished(arg0);
				return 0;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate.Callback)))
			{
				UITweener obj = (UITweener)ToLua.ToObject(L, 1);
				EventDelegate.Callback arg0 = null;
				LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

				if (funcType2 != LuaTypes.LUA_TFUNCTION)
				{
					 arg0 = (EventDelegate.Callback)ToLua.ToObject(L, 2);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 2);
					arg0 = DelegateFactory.CreateDelegate(typeof(EventDelegate.Callback), func) as EventDelegate.Callback;
				}

				obj.AddOnFinished(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UITweener.AddOnFinished");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveOnFinished(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			EventDelegate arg0 = (EventDelegate)ToLua.CheckObject(L, 2, typeof(EventDelegate));
			obj.RemoveOnFinished(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sample(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.Sample(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayForward(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.PlayForward();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayReverse(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.PlayReverse();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Play(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetToBeginning(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.ResetToBeginning();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Toggle(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.Toggle();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStartToCurrentValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.SetStartToCurrentValue();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEndToCurrentValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UITweener obj = (UITweener)ToLua.CheckObject(L, 1, typeof(UITweener));
			obj.SetEndToCurrentValue();
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
		ToLua.Push(L, UITweener.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_method(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UITweener.Method ret = obj.method;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index method on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_style(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UITweener.Style ret = obj.style;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index style on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animationCurve(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UnityEngine.AnimationCurve ret = obj.animationCurve;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index animationCurve on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ignoreTimeScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			bool ret = obj.ignoreTimeScale;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreTimeScale on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_delay(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float ret = obj.delay;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index delay on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_duration(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float ret = obj.duration;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index duration on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_steeperCurves(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			bool ret = obj.steeperCurves;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index steeperCurves on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweenGroup(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			int ret = obj.tweenGroup;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index tweenGroup on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			System.Collections.Generic.List<EventDelegate> ret = obj.onFinished;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventReceiver(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UnityEngine.GameObject ret = obj.eventReceiver;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index eventReceiver on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_callWhenFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			string ret = obj.callWhenFinished;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index callWhenFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_amountPerDelta(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float ret = obj.amountPerDelta;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index amountPerDelta on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweenFactor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float ret = obj.tweenFactor;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index tweenFactor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_direction(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			AnimationOrTween.Direction ret = obj.direction;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index direction on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		try
		{
			UITweener arg0 = (UITweener)ToLua.CheckUnityObject(L, 2, typeof(UITweener));
			UITweener.current = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_method(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UITweener.Method arg0 = (UITweener.Method)ToLua.CheckObject(L, 2, typeof(UITweener.Method));
			obj.method = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index method on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_style(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UITweener.Style arg0 = (UITweener.Style)ToLua.CheckObject(L, 2, typeof(UITweener.Style));
			obj.style = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index style on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animationCurve(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UnityEngine.AnimationCurve arg0 = (UnityEngine.AnimationCurve)ToLua.CheckObject(L, 2, typeof(UnityEngine.AnimationCurve));
			obj.animationCurve = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index animationCurve on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ignoreTimeScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.ignoreTimeScale = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ignoreTimeScale on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_delay(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.delay = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index delay on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_duration(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.duration = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index duration on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_steeperCurves(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.steeperCurves = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index steeperCurves on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tweenGroup(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.tweenGroup = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index tweenGroup on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			System.Collections.Generic.List<EventDelegate> arg0 = (System.Collections.Generic.List<EventDelegate>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<EventDelegate>));
			obj.onFinished = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventReceiver(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			obj.eventReceiver = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index eventReceiver on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_callWhenFinished(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.callWhenFinished = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index callWhenFinished on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tweenFactor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UITweener obj = (UITweener)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.tweenFactor = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index tweenFactor on a nil value" : e.Message);
		}
	}
}

