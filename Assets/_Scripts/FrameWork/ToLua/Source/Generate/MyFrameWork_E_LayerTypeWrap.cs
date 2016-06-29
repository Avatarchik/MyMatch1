using System;
using LuaInterface;

public class MyFrameWork_E_LayerTypeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(MyFrameWork.E_LayerType));
		L.RegVar("MainUI", get_MainUI, null);
		L.RegVar("NormalUI", get_NormalUI, null);
		L.RegVar("Tips", get_Tips, null);
		L.RegVar("TopBar", get_TopBar, null);
		L.RegVar("Notify", get_Notify, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MainUI(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_LayerType.MainUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NormalUI(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_LayerType.NormalUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Tips(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_LayerType.Tips);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopBar(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_LayerType.TopBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Notify(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_LayerType.Notify);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		MyFrameWork.E_LayerType o = (MyFrameWork.E_LayerType)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

