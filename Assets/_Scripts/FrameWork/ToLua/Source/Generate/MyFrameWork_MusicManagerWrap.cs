using System;
using LuaInterface;

public class MyFrameWork_MusicManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.MusicManager), typeof(Manager));
		L.RegFunction("LoadAudioClip", LoadAudioClip);
		L.RegFunction("CanPlayBackSound", CanPlayBackSound);
		L.RegFunction("PlayBacksound", PlayBacksound);
		L.RegFunction("CanPlaySoundEffect", CanPlaySoundEffect);
		L.RegFunction("Play", Play);
		L.RegFunction("PlaySoundEff", PlaySoundEff);
		L.RegFunction("New", _CreateMyFrameWork_MusicManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_MusicManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.MusicManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAudioClip(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.AudioClip o = obj.LoadAudioClip(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanPlayBackSound(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			bool o = obj.CanPlayBackSound();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBacksound(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			string arg0 = ToLua.CheckString(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.PlayBacksound(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanPlaySoundEffect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			bool o = obj.CanPlaySoundEffect();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
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
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			UnityEngine.AudioClip arg0 = (UnityEngine.AudioClip)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.AudioClip));
			UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
			obj.Play(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySoundEff(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.MusicManager obj = (MyFrameWork.MusicManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.MusicManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.PlaySoundEff(arg0);
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
	static int get_Instance(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.MusicManager.Instance);
		return 1;
	}
}

