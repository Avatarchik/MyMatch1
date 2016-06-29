using System;
using LuaInterface;

public class UnityEngine_AudioClipWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.AudioClip), typeof(UnityEngine.Object));
		L.RegFunction("LoadAudioData", LoadAudioData);
		L.RegFunction("UnloadAudioData", UnloadAudioData);
		L.RegFunction("GetData", GetData);
		L.RegFunction("SetData", SetData);
		L.RegFunction("Create", Create);
		L.RegFunction("New", _CreateUnityEngine_AudioClip);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("length", get_length, null);
		L.RegVar("samples", get_samples, null);
		L.RegVar("channels", get_channels, null);
		L.RegVar("frequency", get_frequency, null);
		L.RegVar("loadType", get_loadType, null);
		L.RegVar("preloadAudioData", get_preloadAudioData, null);
		L.RegVar("loadState", get_loadState, null);
		L.RegVar("loadInBackground", get_loadInBackground, null);
		L.RegFunction("PCMReaderCallback", UnityEngine_AudioClip_PCMReaderCallback);
		L.RegFunction("PCMSetPositionCallback", UnityEngine_AudioClip_PCMSetPositionCallback);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_AudioClip(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.AudioClip obj = new UnityEngine.AudioClip();
			ToLua.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.AudioClip.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAudioData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioClip));
			bool o = obj.LoadAudioData();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadAudioData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioClip));
			bool o = obj.UnloadAudioData();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioClip));
			float[] arg0 = ToLua.CheckNumberArray<float>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			bool o = obj.GetData(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)ToLua.CheckObject(L, 1, typeof(UnityEngine.AudioClip));
			float[] arg0 = ToLua.CheckNumberArray<float>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			bool o = obj.SetData(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Create(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 5 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				UnityEngine.AudioClip o = UnityEngine.AudioClip.Create(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool), typeof(UnityEngine.AudioClip.PCMReaderCallback)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				UnityEngine.AudioClip.PCMReaderCallback arg5 = null;
				LuaTypes funcType6 = LuaDLL.lua_type(L, 6);

				if (funcType6 != LuaTypes.LUA_TFUNCTION)
				{
					 arg5 = (UnityEngine.AudioClip.PCMReaderCallback)ToLua.ToObject(L, 6);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 6);
					arg5 = DelegateFactory.CreateDelegate(typeof(UnityEngine.AudioClip.PCMReaderCallback), func) as UnityEngine.AudioClip.PCMReaderCallback;
				}

				UnityEngine.AudioClip o = UnityEngine.AudioClip.Create(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int), typeof(int), typeof(bool), typeof(UnityEngine.AudioClip.PCMReaderCallback), typeof(UnityEngine.AudioClip.PCMSetPositionCallback)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				UnityEngine.AudioClip.PCMReaderCallback arg5 = null;
				LuaTypes funcType6 = LuaDLL.lua_type(L, 6);

				if (funcType6 != LuaTypes.LUA_TFUNCTION)
				{
					 arg5 = (UnityEngine.AudioClip.PCMReaderCallback)ToLua.ToObject(L, 6);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 6);
					arg5 = DelegateFactory.CreateDelegate(typeof(UnityEngine.AudioClip.PCMReaderCallback), func) as UnityEngine.AudioClip.PCMReaderCallback;
				}

				UnityEngine.AudioClip.PCMSetPositionCallback arg6 = null;
				LuaTypes funcType7 = LuaDLL.lua_type(L, 7);

				if (funcType7 != LuaTypes.LUA_TFUNCTION)
				{
					 arg6 = (UnityEngine.AudioClip.PCMSetPositionCallback)ToLua.ToObject(L, 7);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 7);
					arg6 = DelegateFactory.CreateDelegate(typeof(UnityEngine.AudioClip.PCMSetPositionCallback), func) as UnityEngine.AudioClip.PCMSetPositionCallback;
				}

				UnityEngine.AudioClip o = UnityEngine.AudioClip.Create(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AudioClip.Create");
			}
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
	static int get_length(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			float ret = obj.length;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index length on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_samples(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			int ret = obj.samples;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index samples on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_channels(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			int ret = obj.channels;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index channels on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_frequency(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			int ret = obj.frequency;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index frequency on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			UnityEngine.AudioClipLoadType ret = obj.loadType;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loadType on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preloadAudioData(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			bool ret = obj.preloadAudioData;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index preloadAudioData on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadState(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			UnityEngine.AudioDataLoadState ret = obj.loadState;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loadState on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadInBackground(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AudioClip obj = (UnityEngine.AudioClip)o;
			bool ret = obj.loadInBackground;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loadInBackground on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnityEngine_AudioClip_PCMReaderCallback(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UnityEngine.AudioClip.PCMReaderCallback), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnityEngine_AudioClip_PCMSetPositionCallback(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UnityEngine.AudioClip.PCMSetPositionCallback), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

