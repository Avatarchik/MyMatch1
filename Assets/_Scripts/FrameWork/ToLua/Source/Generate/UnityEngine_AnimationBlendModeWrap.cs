using System;
using LuaInterface;

public class UnityEngine_AnimationBlendModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(UnityEngine.AnimationBlendMode));
		L.RegVar("Blend", get_Blend, null);
		L.RegVar("Additive", get_Additive, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Blend(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.AnimationBlendMode.Blend);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Additive(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.AnimationBlendMode.Additive);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UnityEngine.AnimationBlendMode o = (UnityEngine.AnimationBlendMode)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

