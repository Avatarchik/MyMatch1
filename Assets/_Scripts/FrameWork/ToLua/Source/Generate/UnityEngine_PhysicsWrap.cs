using System;
using LuaInterface;

public class UnityEngine_PhysicsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Physics");
		L.RegFunction("Raycast", Raycast);
		L.RegFunction("RaycastAll", RaycastAll);
		L.RegFunction("RaycastNonAlloc", RaycastNonAlloc);
		L.RegFunction("Linecast", Linecast);
		L.RegFunction("OverlapSphere", OverlapSphere);
		L.RegFunction("OverlapSphereNonAlloc", OverlapSphereNonAlloc);
		L.RegFunction("CapsuleCast", CapsuleCast);
		L.RegFunction("SphereCast", SphereCast);
		L.RegFunction("CapsuleCastAll", CapsuleCastAll);
		L.RegFunction("CapsuleCastNonAlloc", CapsuleCastNonAlloc);
		L.RegFunction("SphereCastAll", SphereCastAll);
		L.RegFunction("SphereCastNonAlloc", SphereCastNonAlloc);
		L.RegFunction("CheckSphere", CheckSphere);
		L.RegFunction("CheckCapsule", CheckCapsule);
		L.RegFunction("CheckBox", CheckBox);
		L.RegFunction("OverlapBox", OverlapBox);
		L.RegFunction("OverlapBoxNonAlloc", OverlapBoxNonAlloc);
		L.RegFunction("BoxCastAll", BoxCastAll);
		L.RegFunction("BoxCastNonAlloc", BoxCastNonAlloc);
		L.RegFunction("BoxCast", BoxCast);
		L.RegFunction("IgnoreCollision", IgnoreCollision);
		L.RegFunction("IgnoreLayerCollision", IgnoreLayerCollision);
		L.RegFunction("GetIgnoreLayerCollision", GetIgnoreLayerCollision);
		L.RegConstant("IgnoreRaycastLayer", 4);
		L.RegConstant("DefaultRaycastLayers", -5);
		L.RegConstant("AllLayers", -1);
		L.RegVar("gravity", get_gravity, set_gravity);
		L.RegVar("defaultContactOffset", get_defaultContactOffset, set_defaultContactOffset);
		L.RegVar("bounceThreshold", get_bounceThreshold, set_bounceThreshold);
		L.RegVar("solverIterationCount", get_solverIterationCount, set_solverIterationCount);
		L.RegVar("sleepThreshold", get_sleepThreshold, set_sleepThreshold);
		L.RegVar("queriesHitTriggers", get_queriesHitTriggers, set_queriesHitTriggers);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Raycast(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				bool o = UnityEngine.Physics.Raycast(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit arg1;
				bool o = UnityEngine.Physics.Raycast(arg0, out arg1);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg1);
				return 2;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, out arg2);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit arg1;
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.Raycast(arg0, out arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg1);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.QueryTriggerInteraction arg3 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 4);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, out arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit arg1;
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.Raycast(arg0, out arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg1);
				return 2;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, out arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit arg1;
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.Raycast(arg0, out arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg1);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				bool o = UnityEngine.Physics.Raycast(arg0, arg1, out arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.Raycast");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RaycastAll(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.QueryTriggerInteraction arg3 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.RaycastAll(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.RaycastAll");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RaycastNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit[] arg1 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 2);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(UnityEngine.RaycastHit[]), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit[] arg1 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit[] arg1 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				UnityEngine.RaycastHit[] arg1 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				int o = UnityEngine.Physics.RaycastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.RaycastNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Linecast(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				bool o = UnityEngine.Physics.Linecast(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				bool o = UnityEngine.Physics.Linecast(arg0, arg1, out arg2);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.Linecast(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.Linecast(arg0, arg1, out arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.QueryTriggerInteraction arg3 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 4);
				bool o = UnityEngine.Physics.Linecast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.RaycastHit arg2;
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.Linecast(arg0, arg1, out arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.Linecast");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapSphere(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapSphere(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapSphere(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.QueryTriggerInteraction arg3 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 4);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapSphere(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.OverlapSphere");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapSphereNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Collider[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				int o = UnityEngine.Physics.OverlapSphereNonAlloc(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Collider[]), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = UnityEngine.Physics.OverlapSphereNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Collider[]), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				int o = UnityEngine.Physics.OverlapSphereNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.OverlapSphereNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCast(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit arg4;
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, out arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg4);
				return 2;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit arg4;
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, out arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg4);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit arg4;
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, out arg4, arg5, arg6);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg4);
				return 2;
			}
			else if (count == 8 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit arg4;
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				UnityEngine.QueryTriggerInteraction arg7 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 8);
				bool o = UnityEngine.Physics.CapsuleCast(arg0, arg1, arg2, arg3, out arg4, arg5, arg6, arg7);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg4);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCast");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCast(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit arg2;
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, out arg2);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, out arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, out arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, out arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, out arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, out arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit arg2;
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, out arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg2);
				return 2;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				bool o = UnityEngine.Physics.SphereCast(arg0, arg1, arg2, out arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.SphereCast");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCastAll(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.CapsuleCastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.CapsuleCastAll(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.CapsuleCastAll(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.CapsuleCastAll(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCastAll");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCastNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit[] arg4 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 5);
				int o = UnityEngine.Physics.CapsuleCastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit[] arg4 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int o = UnityEngine.Physics.CapsuleCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit[] arg4 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				int o = UnityEngine.Physics.CapsuleCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 8 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Vector3 arg3 = ToLua.ToVector3(L, 4);
				UnityEngine.RaycastHit[] arg4 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				UnityEngine.QueryTriggerInteraction arg7 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 8);
				int o = UnityEngine.Physics.CapsuleCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CapsuleCastNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCastAll(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.SphereCastAll(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.SphereCastAll");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCastNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(UnityEngine.RaycastHit[]), typeof(float)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Ray), typeof(float), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Ray arg0 = ToLua.ToRay(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.RaycastHit[] arg2 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				int o = UnityEngine.Physics.SphereCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.SphereCastNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSphere(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				bool o = UnityEngine.Physics.CheckSphere(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.CheckSphere(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.QueryTriggerInteraction arg3 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 4);
				bool o = UnityEngine.Physics.CheckSphere(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CheckSphere");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckCapsule(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				bool o = UnityEngine.Physics.CheckCapsule(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.CheckCapsule(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.CheckCapsule(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CheckCapsule");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckBox(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				bool o = UnityEngine.Physics.CheckBox(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				bool o = UnityEngine.Physics.CheckBox(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = UnityEngine.Physics.CheckBox(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				bool o = UnityEngine.Physics.CheckBox(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.CheckBox");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapBox(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapBox(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapBox(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapBox(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.QueryTriggerInteraction arg4 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 5);
				UnityEngine.Collider[] o = UnityEngine.Physics.OverlapBox(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.OverlapBox");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapBoxNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Collider[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				int o = UnityEngine.Physics.OverlapBoxNonAlloc(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Collider[]), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				int o = UnityEngine.Physics.OverlapBoxNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Collider[]), typeof(UnityEngine.Quaternion), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				int o = UnityEngine.Physics.OverlapBoxNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Collider[]), typeof(UnityEngine.Quaternion), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Collider[] arg2 = ToLua.CheckObjectArray<UnityEngine.Collider>(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.QueryTriggerInteraction arg5 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 6);
				int o = UnityEngine.Physics.OverlapBoxNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.OverlapBoxNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCastAll(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.BoxCastAll(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.BoxCastAll(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.BoxCastAll(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.BoxCastAll(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				UnityEngine.RaycastHit[] o = UnityEngine.Physics.BoxCastAll(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.BoxCastAll");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCastNonAlloc(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[])))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				int o = UnityEngine.Physics.BoxCastNonAlloc(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				int o = UnityEngine.Physics.BoxCastNonAlloc(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(UnityEngine.Quaternion), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int o = UnityEngine.Physics.BoxCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(UnityEngine.Quaternion), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				int o = UnityEngine.Physics.BoxCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 8 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.RaycastHit[]), typeof(UnityEngine.Quaternion), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit[] arg3 = ToLua.CheckObjectArray<UnityEngine.RaycastHit>(L, 4);
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				UnityEngine.QueryTriggerInteraction arg7 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 8);
				int o = UnityEngine.Physics.BoxCastNonAlloc(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.BoxCastNonAlloc");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCast(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, out arg3);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(UnityEngine.Quaternion)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, out arg3, arg4);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(UnityEngine.Quaternion), typeof(float)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, out arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.QueryTriggerInteraction arg6 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 7);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(UnityEngine.Quaternion), typeof(float), typeof(int)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, out arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else if (count == 8 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(LuaInterface.LuaOut<UnityEngine.RaycastHit>), typeof(UnityEngine.Quaternion), typeof(float), typeof(int), typeof(UnityEngine.QueryTriggerInteraction)))
			{
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.RaycastHit arg3;
				UnityEngine.Quaternion arg4 = ToLua.ToQuaternion(L, 5);
				float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
				int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
				UnityEngine.QueryTriggerInteraction arg7 = (UnityEngine.QueryTriggerInteraction)ToLua.ToObject(L, 8);
				bool o = UnityEngine.Physics.BoxCast(arg0, arg1, arg2, out arg3, arg4, arg5, arg6, arg7);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.Push(L, arg3);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.BoxCast");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IgnoreCollision(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Collider), typeof(UnityEngine.Collider)))
			{
				UnityEngine.Collider arg0 = (UnityEngine.Collider)ToLua.ToObject(L, 1);
				UnityEngine.Collider arg1 = (UnityEngine.Collider)ToLua.ToObject(L, 2);
				UnityEngine.Physics.IgnoreCollision(arg0, arg1);
				return 0;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Collider), typeof(UnityEngine.Collider), typeof(bool)))
			{
				UnityEngine.Collider arg0 = (UnityEngine.Collider)ToLua.ToObject(L, 1);
				UnityEngine.Collider arg1 = (UnityEngine.Collider)ToLua.ToObject(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				UnityEngine.Physics.IgnoreCollision(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.IgnoreCollision");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IgnoreLayerCollision(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(int), typeof(int)))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.Physics.IgnoreLayerCollision(arg0, arg1);
				return 0;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(int), typeof(int), typeof(bool)))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				UnityEngine.Physics.IgnoreLayerCollision(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.Physics.IgnoreLayerCollision");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIgnoreLayerCollision(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool o = UnityEngine.Physics.GetIgnoreLayerCollision(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gravity(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.Physics.gravity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultContactOffset(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, UnityEngine.Physics.defaultContactOffset);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bounceThreshold(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, UnityEngine.Physics.bounceThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_solverIterationCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, UnityEngine.Physics.solverIterationCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sleepThreshold(IntPtr L)
	{
		LuaDLL.lua_pushnumber(L, UnityEngine.Physics.sleepThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_queriesHitTriggers(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, UnityEngine.Physics.queriesHitTriggers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gravity(IntPtr L)
	{
		try
		{
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			UnityEngine.Physics.gravity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultContactOffset(IntPtr L)
	{
		try
		{
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.Physics.defaultContactOffset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bounceThreshold(IntPtr L)
	{
		try
		{
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.Physics.bounceThreshold = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_solverIterationCount(IntPtr L)
	{
		try
		{
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.Physics.solverIterationCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sleepThreshold(IntPtr L)
	{
		try
		{
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.Physics.sleepThreshold = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_queriesHitTriggers(IntPtr L)
	{
		try
		{
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			UnityEngine.Physics.queriesHitTriggers = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

