using System;
using LuaInterface;

public class MyFrameWork_LuaBehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.LuaBehaviour), typeof(Base));
		L.RegFunction("InvokeTesting", InvokeTesting);
		L.RegFunction("OnInit", OnInit);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("AddClick", AddClick);
		L.RegFunction("RemoveClick", RemoveClick);
		L.RegFunction("ClearClick", ClearClick);
		L.RegFunction("ReadPhoto", ReadPhoto);
		L.RegFunction("New", _CreateMyFrameWork_LuaBehaviour);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_LuaBehaviour(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.LuaBehaviour class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InvokeTesting(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			obj.InvokeTesting();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			UnityEngine.AssetBundle arg0 = (UnityEngine.AssetBundle)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.AssetBundle));
			string arg1 = ToLua.CheckString(L, 3);
			obj.OnInit(arg0, arg1);
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
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.GameObject o = obj.LoadAsset(arg0);
			ToLua.Push(L, o);
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
			ToLua.CheckArgsCount(L, 3);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 3);
			obj.AddClick(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveClick(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			obj.RemoveClick(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearClick(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			obj.ClearClick();
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
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.LuaBehaviour obj = (MyFrameWork.LuaBehaviour)ToLua.CheckObject(L, 1, typeof(MyFrameWork.LuaBehaviour));
			obj.ReadPhoto();
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
}

