using System;
using LuaInterface;

public class MyFrameWork_BaseUIWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.BaseUI), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("GetUIType", GetUIType);
		L.RegFunction("Update", Update);
		L.RegFunction("OnDestroy", OnDestroy);
		L.RegFunction("Release", Release);
		L.RegFunction("UIInit", UIInit);
		L.RegFunction("Show", Show);
		L.RegFunction("Hide", Hide);
		L.RegFunction("ExecuteReturnLogic", ExecuteReturnLogic);
		L.RegFunction("New", _CreateMyFrameWork_BaseUI);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("CachedTransform", get_CachedTransform, null);
		L.RegVar("CachedGameObject", get_CachedGameObject, null);
		L.RegVar("State", get_State, null);
		L.RegVar("mUIType", get_mUIType, set_mUIType);
		L.RegVar("mUIStyle", get_mUIStyle, set_mUIStyle);
		L.RegVar("mUILayertype", get_mUILayertype, set_mUILayertype);
		L.RegVar("StateChanged", get_StateChanged, set_StateChanged);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_BaseUI(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.BaseUI class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUIType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			MyFrameWork.E_UIType o = obj.GetUIType();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			obj.Update();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDestroy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			obj.OnDestroy();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			obj.Release();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UIInit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			obj.UIInit();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Show(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			object[] arg0 = ToLua.ToParamsObject(L, 2, count - 1);
			obj.Show(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Hide(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			obj.Hide();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ExecuteReturnLogic(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
			bool o = obj.ExecuteReturnLogic();
			LuaDLL.lua_pushboolean(L, o);
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
	static int get_CachedTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			UnityEngine.Transform ret = obj.CachedTransform;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index CachedTransform on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CachedGameObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			UnityEngine.GameObject ret = obj.CachedGameObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index CachedGameObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_State(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_ObjectState ret = obj.State;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index State on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mUIType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_UIType ret = obj.mUIType;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUIType on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mUIStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_UIStyle ret = obj.mUIStyle;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUIStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mUILayertype(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_LayerType ret = obj.mUILayertype;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUILayertype on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StateChanged(IntPtr L)
	{
		ToLua.Push(L, new EventObject("MyFrameWork.BaseUI.StateChanged"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mUIType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_UIType arg0 = (MyFrameWork.E_UIType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIType));
			obj.mUIType = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUIType on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mUIStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_UIStyle arg0 = (MyFrameWork.E_UIStyle)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_UIStyle));
			obj.mUIStyle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUIStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mUILayertype(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)o;
			MyFrameWork.E_LayerType arg0 = (MyFrameWork.E_LayerType)ToLua.CheckObject(L, 2, typeof(MyFrameWork.E_LayerType));
			obj.mUILayertype = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUILayertype on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_StateChanged(IntPtr L)
	{
		MyFrameWork.BaseUI obj = (MyFrameWork.BaseUI)ToLua.CheckObject(L, 1, typeof(MyFrameWork.BaseUI));
		EventObject arg0 = null;

		if (LuaDLL.lua_isuserdata(L, 2) != 0)
		{
			arg0 = (EventObject)ToLua.ToObject(L, 2);
		}
		else
		{
			return LuaDLL.luaL_error(L, "The event 'MyFrameWork.BaseUI.StateChanged' can only appear on the left hand side of += or -= when used outside of the type 'MyFrameWork.BaseUI'");
		}

		if (arg0.op == EventOp.Add)
		{
			MyFrameWork.StateChangedEvent ev = (MyFrameWork.StateChangedEvent)DelegateFactory.CreateDelegate(typeof(MyFrameWork.StateChangedEvent), arg0.func);
			obj.StateChanged += ev;
		}
		else if (arg0.op == EventOp.Sub)
		{
			MyFrameWork.StateChangedEvent ev = (MyFrameWork.StateChangedEvent)LuaMisc.GetEventHandler(obj, typeof(MyFrameWork.BaseUI), "StateChanged");
			Delegate[] ds = ev.GetInvocationList();
			LuaState state = LuaState.Get(L);

			for (int i = 0; i < ds.Length; i++)
			{
				ev = (MyFrameWork.StateChangedEvent)ds[i];
				LuaDelegate ld = ev.Target as LuaDelegate;

				if (ld != null && ld.func == arg0.func)
				{
					obj.StateChanged -= ev;
					state.DelayDispose(ld.func);
					break;
				}
			}

			arg0.func.Dispose();
		}

		return 0;
	}
}

