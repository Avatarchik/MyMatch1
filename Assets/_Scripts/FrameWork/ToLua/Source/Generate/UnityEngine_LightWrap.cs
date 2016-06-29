using System;
using LuaInterface;

public class UnityEngine_LightWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.Light), typeof(UnityEngine.Behaviour));
		L.RegFunction("AddCommandBuffer", AddCommandBuffer);
		L.RegFunction("RemoveCommandBuffer", RemoveCommandBuffer);
		L.RegFunction("RemoveCommandBuffers", RemoveCommandBuffers);
		L.RegFunction("RemoveAllCommandBuffers", RemoveAllCommandBuffers);
		L.RegFunction("GetCommandBuffers", GetCommandBuffers);
		L.RegFunction("GetLights", GetLights);
		L.RegFunction("New", _CreateUnityEngine_Light);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("type", get_type, set_type);
		L.RegVar("color", get_color, set_color);
		L.RegVar("intensity", get_intensity, set_intensity);
		L.RegVar("bounceIntensity", get_bounceIntensity, set_bounceIntensity);
		L.RegVar("shadows", get_shadows, set_shadows);
		L.RegVar("shadowStrength", get_shadowStrength, set_shadowStrength);
		L.RegVar("shadowBias", get_shadowBias, set_shadowBias);
		L.RegVar("shadowNormalBias", get_shadowNormalBias, set_shadowNormalBias);
		L.RegVar("shadowNearPlane", get_shadowNearPlane, set_shadowNearPlane);
		L.RegVar("range", get_range, set_range);
		L.RegVar("spotAngle", get_spotAngle, set_spotAngle);
		L.RegVar("cookieSize", get_cookieSize, set_cookieSize);
		L.RegVar("cookie", get_cookie, set_cookie);
		L.RegVar("flare", get_flare, set_flare);
		L.RegVar("renderMode", get_renderMode, set_renderMode);
		L.RegVar("alreadyLightmapped", get_alreadyLightmapped, set_alreadyLightmapped);
		L.RegVar("cullingMask", get_cullingMask, set_cullingMask);
		L.RegVar("commandBufferCount", get_commandBufferCount, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_Light(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.Light obj = new UnityEngine.Light();
			ToLua.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.Light.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCommandBuffer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.Light obj = (UnityEngine.Light)ToLua.CheckObject(L, 1, typeof(UnityEngine.Light));
			UnityEngine.Rendering.LightEvent arg0 = (UnityEngine.Rendering.LightEvent)ToLua.CheckObject(L, 2, typeof(UnityEngine.Rendering.LightEvent));
			UnityEngine.Rendering.CommandBuffer arg1 = (UnityEngine.Rendering.CommandBuffer)ToLua.CheckObject(L, 3, typeof(UnityEngine.Rendering.CommandBuffer));
			obj.AddCommandBuffer(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveCommandBuffer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.Light obj = (UnityEngine.Light)ToLua.CheckObject(L, 1, typeof(UnityEngine.Light));
			UnityEngine.Rendering.LightEvent arg0 = (UnityEngine.Rendering.LightEvent)ToLua.CheckObject(L, 2, typeof(UnityEngine.Rendering.LightEvent));
			UnityEngine.Rendering.CommandBuffer arg1 = (UnityEngine.Rendering.CommandBuffer)ToLua.CheckObject(L, 3, typeof(UnityEngine.Rendering.CommandBuffer));
			obj.RemoveCommandBuffer(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveCommandBuffers(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Light obj = (UnityEngine.Light)ToLua.CheckObject(L, 1, typeof(UnityEngine.Light));
			UnityEngine.Rendering.LightEvent arg0 = (UnityEngine.Rendering.LightEvent)ToLua.CheckObject(L, 2, typeof(UnityEngine.Rendering.LightEvent));
			obj.RemoveCommandBuffers(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAllCommandBuffers(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)ToLua.CheckObject(L, 1, typeof(UnityEngine.Light));
			obj.RemoveAllCommandBuffers();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCommandBuffers(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Light obj = (UnityEngine.Light)ToLua.CheckObject(L, 1, typeof(UnityEngine.Light));
			UnityEngine.Rendering.LightEvent arg0 = (UnityEngine.Rendering.LightEvent)ToLua.CheckObject(L, 2, typeof(UnityEngine.Rendering.LightEvent));
			UnityEngine.Rendering.CommandBuffer[] o = obj.GetCommandBuffers(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLights(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.LightType arg0 = (UnityEngine.LightType)ToLua.CheckObject(L, 1, typeof(UnityEngine.LightType));
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.Light[] o = UnityEngine.Light.GetLights(arg0, arg1);
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
	static int get_type(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightType ret = obj.type;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index type on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_color(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Color ret = obj.color;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index color on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_intensity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.intensity;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index intensity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bounceIntensity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.bounceIntensity;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bounceIntensity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadows(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightShadows ret = obj.shadows;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadows on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowStrength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.shadowStrength;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowStrength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowBias(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.shadowBias;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowBias on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowNormalBias(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.shadowNormalBias;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowNormalBias on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowNearPlane(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.shadowNearPlane;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowNearPlane on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_range(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.range;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index range on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spotAngle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.spotAngle;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spotAngle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cookieSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float ret = obj.cookieSize;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cookieSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cookie(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Texture ret = obj.cookie;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cookie on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flare(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Flare ret = obj.flare;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index flare on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightRenderMode ret = obj.renderMode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index renderMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alreadyLightmapped(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			bool ret = obj.alreadyLightmapped;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alreadyLightmapped on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cullingMask(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			int ret = obj.cullingMask;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cullingMask on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_commandBufferCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			int ret = obj.commandBufferCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index commandBufferCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightType arg0 = (UnityEngine.LightType)ToLua.CheckObject(L, 2, typeof(UnityEngine.LightType));
			obj.type = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index type on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_color(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.color = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index color on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_intensity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.intensity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index intensity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bounceIntensity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.bounceIntensity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bounceIntensity on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadows(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightShadows arg0 = (UnityEngine.LightShadows)ToLua.CheckObject(L, 2, typeof(UnityEngine.LightShadows));
			obj.shadows = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadows on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowStrength(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.shadowStrength = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowStrength on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowBias(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.shadowBias = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowBias on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowNormalBias(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.shadowNormalBias = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowNormalBias on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowNearPlane(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.shadowNearPlane = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shadowNearPlane on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_range(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.range = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index range on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spotAngle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.spotAngle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spotAngle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cookieSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.cookieSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cookieSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cookie(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Texture));
			obj.cookie = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cookie on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_flare(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.Flare arg0 = (UnityEngine.Flare)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Flare));
			obj.flare = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index flare on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_renderMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			UnityEngine.LightRenderMode arg0 = (UnityEngine.LightRenderMode)ToLua.CheckObject(L, 2, typeof(UnityEngine.LightRenderMode));
			obj.renderMode = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index renderMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alreadyLightmapped(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.alreadyLightmapped = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alreadyLightmapped on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cullingMask(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Light obj = (UnityEngine.Light)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.cullingMask = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cullingMask on a nil value" : e.Message);
		}
	}
}

