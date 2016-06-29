using System;
using LuaInterface;

public class UnityEngine_ParticleAnimatorWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.ParticleAnimator), typeof(UnityEngine.Component));
		L.RegFunction("New", _CreateUnityEngine_ParticleAnimator);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("doesAnimateColor", get_doesAnimateColor, set_doesAnimateColor);
		L.RegVar("worldRotationAxis", get_worldRotationAxis, set_worldRotationAxis);
		L.RegVar("localRotationAxis", get_localRotationAxis, set_localRotationAxis);
		L.RegVar("sizeGrow", get_sizeGrow, set_sizeGrow);
		L.RegVar("rndForce", get_rndForce, set_rndForce);
		L.RegVar("force", get_force, set_force);
		L.RegVar("damping", get_damping, set_damping);
		L.RegVar("autodestruct", get_autodestruct, set_autodestruct);
		L.RegVar("colorAnimation", get_colorAnimation, set_colorAnimation);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_ParticleAnimator(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.ParticleAnimator obj = new UnityEngine.ParticleAnimator();
			ToLua.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.ParticleAnimator.New");
		}

		return 0;
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
	static int get_doesAnimateColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			bool ret = obj.doesAnimateColor;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index doesAnimateColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldRotationAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 ret = obj.worldRotationAxis;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldRotationAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localRotationAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 ret = obj.localRotationAxis;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localRotationAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sizeGrow(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			float ret = obj.sizeGrow;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index sizeGrow on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rndForce(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 ret = obj.rndForce;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndForce on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_force(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 ret = obj.force;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index force on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_damping(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			float ret = obj.damping;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index damping on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autodestruct(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			bool ret = obj.autodestruct;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index autodestruct on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colorAnimation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Color[] ret = obj.colorAnimation;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index colorAnimation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_doesAnimateColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.doesAnimateColor = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index doesAnimateColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_worldRotationAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.worldRotationAxis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldRotationAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localRotationAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.localRotationAxis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localRotationAxis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sizeGrow(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.sizeGrow = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index sizeGrow on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rndForce(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.rndForce = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndForce on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_force(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.force = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index force on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_damping(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.damping = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index damping on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autodestruct(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.autodestruct = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index autodestruct on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_colorAnimation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleAnimator obj = (UnityEngine.ParticleAnimator)o;
			UnityEngine.Color[] arg0 = ToLua.CheckObjectArray<UnityEngine.Color>(L, 2);
			obj.colorAnimation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index colorAnimation on a nil value" : e.Message);
		}
	}
}
