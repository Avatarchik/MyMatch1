using System;
using LuaInterface;

public class UnityEngine_AssetBundleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.AssetBundle), typeof(UnityEngine.Object));
		L.RegFunction("LoadFromFileAsync", LoadFromFileAsync);
		L.RegFunction("LoadFromFile", LoadFromFile);
		L.RegFunction("LoadFromMemoryAsync", LoadFromMemoryAsync);
		L.RegFunction("LoadFromMemory", LoadFromMemory);
		L.RegFunction("Contains", Contains);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("LoadAssetAsync", LoadAssetAsync);
		L.RegFunction("LoadAssetWithSubAssets", LoadAssetWithSubAssets);
		L.RegFunction("LoadAssetWithSubAssetsAsync", LoadAssetWithSubAssetsAsync);
		L.RegFunction("LoadAllAssets", LoadAllAssets);
		L.RegFunction("LoadAllAssetsAsync", LoadAllAssetsAsync);
		L.RegFunction("Unload", Unload);
		L.RegFunction("GetAllAssetNames", GetAllAssetNames);
		L.RegFunction("GetAllScenePaths", GetAllScenePaths);
		L.RegFunction("New", _CreateUnityEngine_AssetBundle);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("mainAsset", get_mainAsset, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_AssetBundle(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.AssetBundle obj = new UnityEngine.AssetBundle();
			ToLua.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.AssetBundle.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFromFileAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.AssetBundleCreateRequest o = UnityEngine.AssetBundle.LoadFromFileAsync(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(uint)))
			{
				string arg0 = ToLua.ToString(L, 1);
				uint arg1 = (uint)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.AssetBundleCreateRequest o = UnityEngine.AssetBundle.LoadFromFileAsync(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromFileAsync");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFromFile(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.AssetBundle o = UnityEngine.AssetBundle.LoadFromFile(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(uint)))
			{
				string arg0 = ToLua.ToString(L, 1);
				uint arg1 = (uint)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.AssetBundle o = UnityEngine.AssetBundle.LoadFromFile(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromFile");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFromMemoryAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(byte[])))
			{
				byte[] arg0 = ToLua.CheckByteBuffer(L, 1);
				UnityEngine.AssetBundleCreateRequest o = UnityEngine.AssetBundle.LoadFromMemoryAsync(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(byte[]), typeof(uint)))
			{
				byte[] arg0 = ToLua.CheckByteBuffer(L, 1);
				uint arg1 = (uint)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.AssetBundleCreateRequest o = UnityEngine.AssetBundle.LoadFromMemoryAsync(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromMemoryAsync");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFromMemory(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(byte[])))
			{
				byte[] arg0 = ToLua.CheckByteBuffer(L, 1);
				UnityEngine.AssetBundle o = UnityEngine.AssetBundle.LoadFromMemory(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(byte[]), typeof(uint)))
			{
				byte[] arg0 = ToLua.CheckByteBuffer(L, 1);
				uint arg1 = (uint)LuaDLL.lua_tonumber(L, 2);
				UnityEngine.AssetBundle o = UnityEngine.AssetBundle.LoadFromMemory(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadFromMemory");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Contains(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.CheckObject(L, 1, typeof(UnityEngine.AssetBundle));
			string arg0 = ToLua.CheckString(L, 2);
			bool o = obj.Contains(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
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
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.Object o = obj.LoadAsset(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 3);
				UnityEngine.Object o = obj.LoadAsset(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAsset");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.AssetBundleRequest o = obj.LoadAssetAsync(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 3);
				UnityEngine.AssetBundleRequest o = obj.LoadAssetAsync(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetAsync");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetWithSubAssets(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.Object[] o = obj.LoadAssetWithSubAssets(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 3);
				UnityEngine.Object[] o = obj.LoadAssetWithSubAssets(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetWithSubAssets");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetWithSubAssetsAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.AssetBundleRequest o = obj.LoadAssetWithSubAssetsAsync(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(string), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 3);
				UnityEngine.AssetBundleRequest o = obj.LoadAssetWithSubAssetsAsync(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAssetWithSubAssetsAsync");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAllAssets(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				UnityEngine.Object[] o = obj.LoadAllAssets();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				UnityEngine.Object[] o = obj.LoadAllAssets(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAllAssets");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAllAssetsAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				UnityEngine.AssetBundleRequest o = obj.LoadAllAssetsAsync();
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UnityEngine.AssetBundle), typeof(System.Type)))
			{
				UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				UnityEngine.AssetBundleRequest o = obj.LoadAllAssetsAsync(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UnityEngine.AssetBundle.LoadAllAssetsAsync");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Unload(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.CheckObject(L, 1, typeof(UnityEngine.AssetBundle));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Unload(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllAssetNames(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.CheckObject(L, 1, typeof(UnityEngine.AssetBundle));
			string[] o = obj.GetAllAssetNames();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllScenePaths(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)ToLua.CheckObject(L, 1, typeof(UnityEngine.AssetBundle));
			string[] o = obj.GetAllScenePaths();
			ToLua.Push(L, o);
			return 1;
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
	static int get_mainAsset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.AssetBundle obj = (UnityEngine.AssetBundle)o;
			UnityEngine.Object ret = obj.mainAsset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mainAsset on a nil value" : e.Message);
		}
	}
}
