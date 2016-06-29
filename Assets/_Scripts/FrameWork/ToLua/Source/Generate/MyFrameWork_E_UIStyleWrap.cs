using System;
using LuaInterface;

public class MyFrameWork_E_UIStyleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(MyFrameWork.E_UIStyle));
		L.RegVar("BackClose", get_BackClose, null);
		L.RegVar("Main", get_Main, null);
		L.RegVar("PopUp", get_PopUp, null);
		L.RegVar("TopBar", get_TopBar, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BackClose(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIStyle.BackClose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Main(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIStyle.Main);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PopUp(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIStyle.PopUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopBar(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIStyle.TopBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		MyFrameWork.E_UIStyle o = (MyFrameWork.E_UIStyle)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

