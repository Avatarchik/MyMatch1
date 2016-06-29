using System;
using LuaInterface;

public class LuaUtilityManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaUtilityManager), typeof(Manager));
		L.RegFunction("AddChildToTarget", AddChildToTarget);
		L.RegFunction("AddChildToPos", AddChildToPos);
		L.RegFunction("Des", Des);
		L.RegFunction("RegisterDelegation", RegisterDelegation);
		L.RegFunction("UpdateTouchMove", UpdateTouchMove);
		L.RegFunction("AdjustFingerMove", AdjustFingerMove);
		L.RegFunction("OpenMatchingPanel", OpenMatchingPanel);
		L.RegFunction("CloseMatchingPanel", CloseMatchingPanel);
		L.RegFunction("DelMatchedInfo", DelMatchedInfo);
		L.RegFunction("Flying", Flying);
		L.RegFunction("SendGameInfoToSdk", SendGameInfoToSdk);
		L.RegFunction("ReadPhoto", ReadPhoto);
		L.RegFunction("PlayerPrefs_SetString_Lua", PlayerPrefs_SetString_Lua);
		L.RegFunction("PlayerPrefs_GetString_Lua", PlayerPrefs_GetString_Lua);
		L.RegFunction("ShowUICardPanel", ShowUICardPanel);
		L.RegFunction("New", _CreateLuaUtilityManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("mUISprite", get_mUISprite, set_mUISprite);
		L.RegVar("startPosFlag", get_startPosFlag, set_startPosFlag);
		L.RegVar("MouseDownStartPos", get_MouseDownStartPos, set_MouseDownStartPos);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaUtilityManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "LuaUtilityManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChildToTarget(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 3, typeof(UnityEngine.Transform));
			int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
			UnityEngine.GameObject o = obj.AddChildToTarget(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChildToPos(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 5);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 3, typeof(UnityEngine.Transform));
			int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
			int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
			UnityEngine.GameObject o = obj.AddChildToPos(arg0, arg1, arg2, arg3);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Des(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			LuaTable arg0 = ToLua.CheckLuaTable(L, 2);
			obj.Des(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterDelegation(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.RegisterDelegation();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateTouchMove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.UpdateTouchMove();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdjustFingerMove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.AdjustFingerMove();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OpenMatchingPanel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.OpenMatchingPanel();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseMatchingPanel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.CloseMatchingPanel();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DelMatchedInfo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.DelMatchedInfo();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Flying(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 6);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.GameObject arg1 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 3, typeof(UnityEngine.GameObject));
			UnityEngine.GameObject arg2 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 4, typeof(UnityEngine.GameObject));
			UnityEngine.GameObject arg3 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 5, typeof(UnityEngine.GameObject));
			int arg4 = (int)LuaDLL.luaL_checknumber(L, 6);
			obj.Flying(arg0, arg1, arg2, arg3, arg4);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendGameInfoToSdk(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string arg2 = ToLua.CheckString(L, 4);
			obj.SendGameInfoToSdk(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadPhoto(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.ReadPhoto(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayerPrefs_SetString_Lua(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.PlayerPrefs_SetString_Lua(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayerPrefs_GetString_Lua(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string o = obj.PlayerPrefs_GetString_Lua(arg0, arg1);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowUICardPanel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)ToLua.CheckObject(L, 1, typeof(LuaUtilityManager));
			obj.ShowUICardPanel();
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
	static int get_mUISprite(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			UISprite ret = obj.mUISprite;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUISprite on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startPosFlag(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			bool ret = obj.startPosFlag;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startPosFlag on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MouseDownStartPos(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			UnityEngine.Vector3 ret = obj.MouseDownStartPos;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MouseDownStartPos on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mUISprite(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			UISprite arg0 = (UISprite)ToLua.CheckUnityObject(L, 2, typeof(UISprite));
			obj.mUISprite = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mUISprite on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startPosFlag(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.startPosFlag = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startPosFlag on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MouseDownStartPos(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			LuaUtilityManager obj = (LuaUtilityManager)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.MouseDownStartPos = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MouseDownStartPos on a nil value" : e.Message);
		}
	}
}

