using System;
using LuaInterface;

public class MyFrameWork_ResourceMgrWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.ResourceMgr), typeof(Manager));
		L.RegFunction("GetFileFullName", GetFileFullName);
		L.RegFunction("GetParentPathName", GetParentPathName);
		L.RegFunction("Init", Init);
		L.RegFunction("PreLoadMultiAsset", PreLoadMultiAsset);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("LoadAssetAndInstance", LoadAssetAndInstance);
		L.RegFunction("LoadAndInstanceGameObjectFromPreload", LoadAndInstanceGameObjectFromPreload);
		L.RegFunction("Update", Update);
		L.RegFunction("GetAsset", GetAsset);
		L.RegFunction("ReleaseAsset", ReleaseAsset);
		L.RegFunction("IsKeepInMemory", IsKeepInMemory);
		L.RegFunction("AddAssetToName", AddAssetToName);
		L.RegFunction("PushAssetStack", PushAssetStack);
		L.RegFunction("PopAssetStack", PopAssetStack);
		L.RegFunction("GC", GC);
		L.RegFunction("New", _CreateMyFrameWork_ResourceMgr);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("_inLoads", get__inLoads, set__inLoads);
		L.RegVar("_questWaiting", get__questWaiting, set__questWaiting);
		L.RegVar("_stackAsset", get__stackAsset, set__stackAsset);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_ResourceMgr(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.ResourceMgr class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFileFullName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string o = obj.GetFileFullName(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetParentPathName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			bool o = obj.GetParentPathName(arg0, ref arg1);
			LuaDLL.lua_pushboolean(L, o);
			LuaDLL.lua_pushstring(L, arg1);
			return 2;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PreLoadMultiAsset(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(MyFrameWork.ResourceMgr), typeof(string[]), typeof(System.Action<bool>)))
			{
				MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.ToObject(L, 1);
				string[] arg0 = ToLua.CheckStringArray(L, 2);
				System.Action<bool> arg1 = null;
				LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

				if (funcType3 != LuaTypes.LUA_TFUNCTION)
				{
					 arg1 = (System.Action<bool>)ToLua.ToObject(L, 3);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 3);
					arg1 = DelegateFactory.CreateDelegate(typeof(System.Action<bool>), func) as System.Action<bool>;
				}

				obj.PreLoadMultiAsset(arg0, arg1);
				return 0;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(MyFrameWork.ResourceMgr), typeof(string[]), typeof(System.Action<bool>), typeof(System.Type)))
			{
				MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.ToObject(L, 1);
				string[] arg0 = ToLua.CheckStringArray(L, 2);
				System.Action<bool> arg1 = null;
				LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

				if (funcType3 != LuaTypes.LUA_TFUNCTION)
				{
					 arg1 = (System.Action<bool>)ToLua.ToObject(L, 3);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 3);
					arg1 = DelegateFactory.CreateDelegate(typeof(System.Action<bool>), func) as System.Action<bool>;
				}

				System.Type arg2 = (System.Type)ToLua.ToObject(L, 4);
				obj.PreLoadMultiAsset(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: MyFrameWork.ResourceMgr.PreLoadMultiAsset");
			}
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
			ToLua.CheckArgsCount(L, 6);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			System.Action<object> arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (System.Action<object>)ToLua.CheckObject(L, 3, typeof(System.Action<object>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 3);
				arg1 = DelegateFactory.CreateDelegate(typeof(System.Action<object>), func) as System.Action<object>;
			}

			bool arg2 = LuaDLL.luaL_checkboolean(L, 4);
			bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
			System.Type arg4 = (System.Type)ToLua.CheckObject(L, 6, typeof(System.Type));
			obj.LoadAsset(arg0, arg1, arg2, arg3, arg4);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetAndInstance(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 6);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			System.Action<object> arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (System.Action<object>)ToLua.CheckObject(L, 3, typeof(System.Action<object>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 3);
				arg1 = DelegateFactory.CreateDelegate(typeof(System.Action<object>), func) as System.Action<object>;
			}

			bool arg2 = LuaDLL.luaL_checkboolean(L, 4);
			bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
			System.Type arg4 = (System.Type)ToLua.CheckObject(L, 6, typeof(System.Type));
			obj.LoadAssetAndInstance(arg0, arg1, arg2, arg3, arg4);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAndInstanceGameObjectFromPreload(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.GameObject o = obj.LoadAndInstanceGameObjectFromPreload(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			obj.Update();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAsset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			MyFrameWork.AssetInfo o = obj.GetAsset(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReleaseAsset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			obj.ReleaseAsset(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsKeepInMemory(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			obj.IsKeepInMemory(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddAssetToName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			string arg0 = ToLua.CheckString(L, 2);
			obj.AddAssetToName(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PushAssetStack(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			obj.PushAssetStack();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PopAssetStack(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			obj.PopAssetStack();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GC(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ResourceMgr));
			obj.GC();
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
	static int get__inLoads(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.List<MyFrameWork.RequestInfo> ret = obj._inLoads;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _inLoads on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__questWaiting(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.Queue<MyFrameWork.RequestInfo> ret = obj._questWaiting;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _questWaiting on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__stackAsset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.Stack<System.Collections.Generic.List<string>> ret = obj._stackAsset;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _stackAsset on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.ResourceMgr.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__inLoads(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.List<MyFrameWork.RequestInfo> arg0 = (System.Collections.Generic.List<MyFrameWork.RequestInfo>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<MyFrameWork.RequestInfo>));
			obj._inLoads = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _inLoads on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__questWaiting(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.Queue<MyFrameWork.RequestInfo> arg0 = (System.Collections.Generic.Queue<MyFrameWork.RequestInfo>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Queue<MyFrameWork.RequestInfo>));
			obj._questWaiting = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _questWaiting on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__stackAsset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.ResourceMgr obj = (MyFrameWork.ResourceMgr)o;
			System.Collections.Generic.Stack<System.Collections.Generic.List<string>> arg0 = (System.Collections.Generic.Stack<System.Collections.Generic.List<string>>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Stack<System.Collections.Generic.List<string>>));
			obj._stackAsset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _stackAsset on a nil value" : e.Message);
		}
	}
}

