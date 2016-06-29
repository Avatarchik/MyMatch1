using System;
using LuaInterface;

public class UnityEngine_ParticleEmitterWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.ParticleEmitter), typeof(UnityEngine.Component));
		L.RegFunction("ClearParticles", ClearParticles);
		L.RegFunction("Emit", Emit);
		L.RegFunction("Simulate", Simulate);
		L.RegFunction("New", _CreateUnityEngine_ParticleEmitter);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("emit", get_emit, set_emit);
		L.RegVar("minSize", get_minSize, set_minSize);
		L.RegVar("maxSize", get_maxSize, set_maxSize);
		L.RegVar("minEnergy", get_minEnergy, set_minEnergy);
		L.RegVar("maxEnergy", get_maxEnergy, set_maxEnergy);
		L.RegVar("minEmission", get_minEmission, set_minEmission);
		L.RegVar("maxEmission", get_maxEmission, set_maxEmission);
		L.RegVar("emitterVelocityScale", get_emitterVelocityScale, set_emitterVelocityScale);
		L.RegVar("worldVelocity", get_worldVelocity, set_worldVelocity);
		L.RegVar("localVelocity", get_localVelocity, set_localVelocity);
		L.RegVar("rndVelocity", get_rndVelocity, set_rndVelocity);
		L.RegVar("useWorldSpace", get_useWorldSpace, set_useWorldSpace);
		L.RegVar("rndRotation", get_rndRotation, set_rndRotation);
		L.RegVar("angularVelocity", get_angularVelocity, set_angularVelocity);
		L.RegVar("rndAngularVelocity", get_rndAngularVelocity, set_rndAngularVelocity);
		L.RegVar("particles", get_particles, set_particles);
		L.RegVar("particleCount", get_particleCount, null);
		L.RegVar("enabled", get_enabled, set_enabled);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_ParticleEmitter(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UnityEngine.ParticleEmitter class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearParticles(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.CheckObject(L, 1, typeof(UnityEngine.ParticleEmitter));
			obj.ClearParticles();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Emit(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.ParticleEmitter)))
			{
				UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.ToObject(L, 1);
				obj.Emit();
				return 0;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.ParticleEmitter), typeof(int)))
			{
				UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				obj.Emit(arg0);
				return 0;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.ParticleEmitter), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(float), typeof(UnityEngine.Color)))
			{
				UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.Color arg4 = ToLua.ToColor(L, 6);
				obj.Emit(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else if (count == 8 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.ParticleEmitter), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(float), typeof(UnityEngine.Color), typeof(float), typeof(float)))
			{
				UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.Color arg4 = ToLua.ToColor(L, 6);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 7);
				float arg6 = (float)LuaDLL.lua_tonumber(L, 8);
				obj.Emit(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.ParticleEmitter.Emit");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Simulate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)ToLua.CheckObject(L, 1, typeof(UnityEngine.ParticleEmitter));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.Simulate(arg0);
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
	static int get_emit(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool ret = obj.emit;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emit on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.minSize;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.maxSize;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minEnergy(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.minEnergy;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minEnergy on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxEnergy(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.maxEnergy;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxEnergy on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.minEmission;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.maxEmission;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_emitterVelocityScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.emitterVelocityScale;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emitterVelocityScale on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 ret = obj.worldVelocity;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 ret = obj.localVelocity;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rndVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 ret = obj.rndVelocity;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useWorldSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool ret = obj.useWorldSpace;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useWorldSpace on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rndRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool ret = obj.rndRotation;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndRotation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_angularVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.angularVelocity;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index angularVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rndAngularVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float ret = obj.rndAngularVelocity;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndAngularVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_particles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Particle[] ret = obj.particles;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index particles on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_particleCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			int ret = obj.particleCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index particleCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enabled(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool ret = obj.enabled;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index enabled on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_emit(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.emit = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emit on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.minSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.maxSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minEnergy(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.minEnergy = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minEnergy on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxEnergy(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.maxEnergy = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxEnergy on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.minEmission = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.maxEmission = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_emitterVelocityScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.emitterVelocityScale = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emitterVelocityScale on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_worldVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.worldVelocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.localVelocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rndVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.rndVelocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useWorldSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.useWorldSpace = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useWorldSpace on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rndRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.rndRotation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndRotation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_angularVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.angularVelocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index angularVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rndAngularVelocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.rndAngularVelocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rndAngularVelocity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_particles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			UnityEngine.Particle[] arg0 = ToLua.CheckObjectArray<UnityEngine.Particle>(L, 2);
			obj.particles = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index particles on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enabled(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleEmitter obj = (UnityEngine.ParticleEmitter)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.enabled = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index enabled on a nil value" : e.Message);
		}
	}
}

