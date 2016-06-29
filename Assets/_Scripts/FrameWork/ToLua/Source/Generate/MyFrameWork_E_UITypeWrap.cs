using System;
using LuaInterface;

public class MyFrameWork_E_UITypeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(MyFrameWork.E_UIType));
		L.RegVar("None", get_None, null);
		L.RegVar("PanelMessageBox", get_PanelMessageBox, null);
		L.RegVar("UIMainPanel", get_UIMainPanel, null);
		L.RegVar("UILoginPanel", get_UILoginPanel, null);
		L.RegVar("UILoadingPanel", get_UILoadingPanel, null);
		L.RegVar("UINamingPanel", get_UINamingPanel, null);
		L.RegVar("PanelTestTopBar", get_PanelTestTopBar, null);
		L.RegVar("UIWinOrLosePanel", get_UIWinOrLosePanel, null);
		L.RegVar("Fight", get_Fight, null);
		L.RegVar("PanelRankChange", get_PanelRankChange, null);
		L.RegVar("PanelMatching", get_PanelMatching, null);
		L.RegVar("UIPlayerLvUpPanel", get_UIPlayerLvUpPanel, null);
		L.RegVar("UIArenaPanel", get_UIArenaPanel, null);
		L.RegVar("UINetConditionPanel", get_UINetConditionPanel, null);
		L.RegVar("UICardPanel", get_UICardPanel, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_None(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PanelMessageBox(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.PanelMessageBox);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIMainPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UIMainPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UILoginPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UILoginPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UILoadingPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UILoadingPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UINamingPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UINamingPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PanelTestTopBar(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.PanelTestTopBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIWinOrLosePanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UIWinOrLosePanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Fight(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.Fight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PanelRankChange(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.PanelRankChange);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PanelMatching(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.PanelMatching);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIPlayerLvUpPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UIPlayerLvUpPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIArenaPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UIArenaPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UINetConditionPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UINetConditionPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UICardPanel(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.E_UIType.UICardPanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		MyFrameWork.E_UIType o = (MyFrameWork.E_UIType)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

