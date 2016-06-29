using System;
using LuaInterface;

public class MyFrameWork_NetworkManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.NetworkManager), typeof(Manager));
		L.RegFunction("StartHeartBeat", StartHeartBeat);
		L.RegFunction("StopHeartBeat", StopHeartBeat);
		L.RegFunction("ReceiveHeartBeat", ReceiveHeartBeat);
		L.RegFunction("BadNetCondition", BadNetCondition);
		L.RegFunction("ReturnToLogin", ReturnToLogin);
		L.RegFunction("OnInit", OnInit);
		L.RegFunction("Unload", Unload);
		L.RegFunction("CallMethod", CallMethod);
		L.RegFunction("AddEvent", AddEvent);
		L.RegFunction("Execute", Execute);
		L.RegFunction("SendConnect", SendConnect);
		L.RegFunction("SendReConnect", SendReConnect);
		L.RegFunction("SetReConnect", SetReConnect);
		L.RegFunction("SendMessage", SendMessage);
		L.RegFunction("New", _CreateMyFrameWork_NetworkManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_NetworkManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.NetworkManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartHeartBeat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.StartHeartBeat();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopHeartBeat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			obj.StopHeartBeat(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReceiveHeartBeat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.ReceiveHeartBeat();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BadNetCondition(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.BadNetCondition();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReturnToLogin(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.ReturnToLogin();
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
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.OnInit();
			return 0;
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
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.Unload();
			return 0;
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
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			string arg0 = ToLua.CheckString(L, 2);
			object[] arg1 = ToLua.ToParamsObject(L, 3, count - 2);
			object[] o = obj.CallMethod(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			MyFrameWork.ByteBuffer arg1 = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 2, typeof(MyFrameWork.ByteBuffer));
			MyFrameWork.NetworkManager.AddEvent(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Execute(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.Execute(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendConnect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.SendConnect();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendReConnect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			obj.SendReConnect();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetReConnect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.SetReConnect(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessage(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.NetworkManager obj = (MyFrameWork.NetworkManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.NetworkManager));
			MyFrameWork.ByteBuffer arg0 = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 2, typeof(MyFrameWork.ByteBuffer));
			obj.SendMessage(arg0);
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
	static int get_Instance(IntPtr L)
	{
		ToLua.Push(L, MyFrameWork.NetworkManager.Instance);
		return 1;
	}
}

