using System;
using LuaInterface;

public class MyFrameWork_UtilWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.Util), typeof(System.Object));
		L.RegFunction("CreateDirectory", CreateDirectory);
		L.RegFunction("Int", Int);
		L.RegFunction("Float", Float);
		L.RegFunction("Long", Long);
		L.RegFunction("Random", Random);
		L.RegFunction("Uid", Uid);
		L.RegFunction("GetTime", GetTime);
		L.RegFunction("Child", Child);
		L.RegFunction("Peer", Peer);
		L.RegFunction("md5", md5);
		L.RegFunction("md5file", md5file);
		L.RegFunction("ClearChild", ClearChild);
		L.RegFunction("ClearMemory", ClearMemory);
		L.RegFunction("AppContentPath", AppContentPath);
		L.RegFunction("AddClick", AddClick);
		L.RegFunction("Log", Log);
		L.RegFunction("LogWarning", LogWarning);
		L.RegFunction("LogError", LogError);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("AddComponent", AddComponent);
		L.RegFunction("LoadPrefab", LoadPrefab);
		L.RegFunction("CallMethod", CallMethod);
		L.RegFunction("CheckEnvironment", CheckEnvironment);
		L.RegFunction("GetFullName", GetFullName);
		L.RegFunction("New", _CreateMyFrameWork_Util);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("WebURL", get_WebURL, null);
		L.RegVar("DataPath", get_DataPath, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_Util(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MyFrameWork.Util obj = new MyFrameWork.Util();
			ToLua.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MyFrameWork.Util.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateDirectory(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = MyFrameWork.Util.CreateDirectory(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Int(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object arg0 = ToLua.ToVarObject(L, 1);
			int o = MyFrameWork.Util.Int(arg0);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Float(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object arg0 = ToLua.ToVarObject(L, 1);
			float o = MyFrameWork.Util.Float(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Long(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			object arg0 = ToLua.ToVarObject(L, 1);
			long o = MyFrameWork.Util.Long(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Random(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(float), typeof(float)))
			{
				float arg0 = (float)LuaDLL.lua_tonumber(L, 1);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
				float o = MyFrameWork.Util.Random(arg0, arg1);
				LuaDLL.lua_pushnumber(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(int), typeof(int)))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				int o = MyFrameWork.Util.Random(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: MyFrameWork.Util.Random");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Uid(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = MyFrameWork.Util.Uid(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			long o = MyFrameWork.Util.GetTime();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Child(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Transform), typeof(string)))
			{
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				UnityEngine.GameObject o = MyFrameWork.Util.Child(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.GameObject), typeof(string)))
			{
				UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				UnityEngine.GameObject o = MyFrameWork.Util.Child(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: MyFrameWork.Util.Child");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Peer(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.Transform), typeof(string)))
			{
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				UnityEngine.GameObject o = MyFrameWork.Util.Peer(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.GameObject), typeof(string)))
			{
				UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				UnityEngine.GameObject o = MyFrameWork.Util.Peer(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: MyFrameWork.Util.Peer");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int md5(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = MyFrameWork.Util.md5(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int md5file(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = MyFrameWork.Util.md5file(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearChild(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.Transform));
			MyFrameWork.Util.ClearChild(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearMemory(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			MyFrameWork.Util.ClearMemory();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AppContentPath(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			string o = MyFrameWork.Util.AppContentPath();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddClick(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.GameObject));
			object arg1 = ToLua.ToVarObject(L, 2);
			MyFrameWork.Util.AddClick(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Log(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			MyFrameWork.Util.Log(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogWarning(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			MyFrameWork.Util.LogWarning(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogError(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			MyFrameWork.Util.LogError(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AssetBundle arg0 = (UnityEngine.AssetBundle)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.AssetBundle));
			string arg1 = ToLua.CheckString(L, 2);
			UnityEngine.GameObject o = MyFrameWork.Util.LoadAsset(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddComponent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.GameObject));
			string arg1 = ToLua.CheckString(L, 2);
			string arg2 = ToLua.CheckString(L, 3);
			UnityEngine.Component o = MyFrameWork.Util.AddComponent(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			UnityEngine.GameObject o = MyFrameWork.Util.LoadPrefab(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CallMethod(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			string arg0 = ToLua.CheckString(L, 1);
			string arg1 = ToLua.CheckString(L, 2);
			object[] arg2 = ToLua.ToParamsObject(L, 3, count - 2);
			object[] o = MyFrameWork.Util.CallMethod(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckEnvironment(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			bool o = MyFrameWork.Util.CheckEnvironment();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFullName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.GameObject));
			string o = MyFrameWork.Util.GetFullName(arg0);
			LuaDLL.lua_pushstring(L, o);
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
	static int get_WebURL(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.Util.WebURL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DataPath(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, MyFrameWork.Util.DataPath);
		return 1;
	}
}

