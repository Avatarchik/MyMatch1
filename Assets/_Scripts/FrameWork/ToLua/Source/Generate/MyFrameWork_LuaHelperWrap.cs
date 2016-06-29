using System;
using LuaInterface;

public class MyFrameWork_LuaHelperWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("LuaHelper");
		L.RegFunction("GetType", GetType);
		L.RegFunction("GetGameManager", GetGameManager);
		L.RegFunction("GetPanelManager", GetPanelManager);
		L.RegFunction("GetResManager", GetResManager);
		L.RegFunction("GetNetManager", GetNetManager);
		L.RegFunction("GetMusicManager", GetMusicManager);
		L.RegFunction("GetFightDataManager", GetFightDataManager);
		L.RegFunction("GetLoadingManager", GetLoadingManager);
		L.RegFunction("GetLuaUtilityManager", GetLuaUtilityManager);
		L.RegFunction("Action", Action);
		L.RegFunction("VoidDelegate", VoidDelegate);
		L.RegFunction("OnCallLuaFunc", OnCallLuaFunc);
		L.RegFunction("OnJsonCallFunc", OnJsonCallFunc);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			System.Type o = MyFrameWork.LuaHelper.GetType(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGameManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.GameManager o = MyFrameWork.LuaHelper.GetGameManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPanelManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.UIMgr o = MyFrameWork.LuaHelper.GetPanelManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.ResourceMgr o = MyFrameWork.LuaHelper.GetResManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNetManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.NetworkManager o = MyFrameWork.LuaHelper.GetNetManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMusicManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.MusicManager o = MyFrameWork.LuaHelper.GetMusicManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFightDataManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.FightDataManager o = MyFrameWork.LuaHelper.GetFightDataManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLoadingManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LoadingManager o = MyFrameWork.LuaHelper.GetLoadingManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLuaUtilityManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LuaUtilityManager o = MyFrameWork.LuaHelper.GetLuaUtilityManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Action(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 1);
			System.Action o = MyFrameWork.LuaHelper.Action(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int VoidDelegate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 1);
			UIEventListener.VoidDelegate o = MyFrameWork.LuaHelper.VoidDelegate(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCallLuaFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaByteBuffer arg0 = new LuaByteBuffer(ToLua.CheckByteBuffer(L, 1));
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 2);
			MyFrameWork.LuaHelper.OnCallLuaFunc(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnJsonCallFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string arg0 = ToLua.CheckString(L, 1);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 2);
			MyFrameWork.LuaHelper.OnJsonCallFunc(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

