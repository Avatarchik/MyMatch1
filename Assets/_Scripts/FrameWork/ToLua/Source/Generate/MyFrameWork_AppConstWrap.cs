using System;
using LuaInterface;

public class MyFrameWork_AppConstWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.AppConst), typeof(System.Object));
		L.RegFunction("New", _CreateMyFrameWork_AppConst);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegConstant("DebugMode", 0);
		L.RegConstant("ExampleMode", 0);
		L.RegConstant("UpdateMode", 0);
		L.RegConstant("LuaByteMode", 0);
		L.RegConstant("LuaBundleMode", 1);
		L.RegConstant("PrefabBundleMode", 0);
		L.RegConstant("TimerInterval", 1);
		L.RegConstant("GameFrameRate", 60);
		L.RegVar("ScriptsName", get_ScriptsName, null);
		L.RegVar("AppName", get_AppName, null);
		L.RegVar("LuaTempDir", get_LuaTempDir, null);
		L.RegVar("ExtName", get_ExtName, null);
		L.RegVar("AppPrefix", get_AppPrefix, null);
		L.RegVar("WebUrl_Android", get_WebUrl_Android, null);
		L.RegVar("WebUrl_IOS", get_WebUrl_IOS, null);
		L.RegVar("UserId", get_UserId, set_UserId);
		L.RegVar("SocketPort", get_SocketPort, set_SocketPort);
		L.RegVar("SocketAddress", get_SocketAddress, set_SocketAddress);
		L.RegVar("FrameworkRoot", get_FrameworkRoot, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_AppConst(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MyFrameWork.AppConst obj = new MyFrameWork.AppConst();
			ToLua.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MyFrameWork.AppConst.New");
		}

		return 0;
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
	static int get_ScriptsName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.ScriptsName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.AppName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaTempDir(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.LuaTempDir);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ExtName(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.ExtName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppPrefix(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.AppPrefix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WebUrl_Android(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.WebUrl_Android);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WebUrl_IOS(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.WebUrl_IOS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UserId(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.UserId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketPort(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, MyFrameWork.AppConst.SocketPort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketAddress(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.SocketAddress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FrameworkRoot(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.AppConst.FrameworkRoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UserId(IntPtr L)
	{
		try
		{
			string arg0 = ToLua.CheckString(L, 2);
			MyFrameWork.AppConst.UserId = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketPort(IntPtr L)
	{
		try
		{
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyFrameWork.AppConst.SocketPort = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketAddress(IntPtr L)
	{
		try
		{
			string arg0 = ToLua.CheckString(L, 2);
			MyFrameWork.AppConst.SocketAddress = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

