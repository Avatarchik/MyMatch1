using System;
using LuaInterface;

public class MyFrameWork_E_UIMaskStyleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(MyFrameWork.E_UIMaskStyle));
		L.RegVar("None", get_None, null);
		L.RegVar("BlackAlpha", get_BlackAlpha, null);
		L.RegVar("Alpha", get_Alpha, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_None(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIMaskStyle.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BlackAlpha(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIMaskStyle.BlackAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIMaskStyle.Alpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		MyFrameWork.E_UIMaskStyle o = (MyFrameWork.E_UIMaskStyle)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

