using System;
using LuaInterface;

public class UnityEngine_SpaceWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(UnityEngine.Space));
		L.RegVar("World", get_World, null);
		L.RegVar("Self", get_Self, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_World(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.Space.World);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Self(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.Space.Self);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UnityEngine.Space o = (UnityEngine.Space)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

