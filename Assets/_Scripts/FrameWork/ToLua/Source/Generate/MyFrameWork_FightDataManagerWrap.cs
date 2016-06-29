using System;
using LuaInterface;

public class MyFrameWork_FightDataManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.FightDataManager), typeof(Manager));
		L.RegFunction("SetData", SetData);
		L.RegFunction("OnFightCBK", OnFightCBK);
		L.RegFunction("OnFightResultCBK", OnFightResultCBK);
		L.RegFunction("OnRankChangeCBK", OnRankChangeCBK);
		L.RegFunction("OnRankUpCBK", OnRankUpCBK);
		L.RegFunction("OnFaceCBK", OnFaceCBK);
		L.RegFunction("New", _CreateMyFrameWork_FightDataManager);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegConstant("CLEINT_DAMAGE", 75);
		L.RegVar("OpponentBossId", get_OpponentBossId, set_OpponentBossId);
		L.RegVar("MyBossId", get_MyBossId, set_MyBossId);
		L.RegVar("LevelId", get_LevelId, set_LevelId);
		L.RegVar("DicOpponentBoss", get_DicOpponentBoss, set_DicOpponentBoss);
		L.RegVar("DicOriOpponentBoss", get_DicOriOpponentBoss, set_DicOriOpponentBoss);
		L.RegVar("DicMyBossHp", get_DicMyBossHp, set_DicMyBossHp);
		L.RegVar("TimeSec", get_TimeSec, set_TimeSec);
		L.RegVar("Moves", get_Moves, set_Moves);
		L.RegVar("MyOriTotalHp", get_MyOriTotalHp, null);
		L.RegVar("MyCurrentTotalHp", get_MyCurrentTotalHp, null);
		L.RegVar("OpponentOriTotalHp", get_OpponentOriTotalHp, null);
		L.RegVar("OpponentCurrentTotalHp", get_OpponentCurrentTotalHp, null);
		L.RegVar("moduleFight", get_moduleFight, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_FightDataManager(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "MyFrameWork.FightDataManager class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			obj.SetData();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFightCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 8);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
			int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
			int arg4 = (int)LuaDLL.luaL_checknumber(L, 6);
			int arg5 = (int)LuaDLL.luaL_checknumber(L, 7);
			int arg6 = (int)LuaDLL.luaL_checknumber(L, 8);
			obj.OnFightCBK(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFightResultCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.OnFightResultCBK(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRankChangeCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.OnRankChangeCBK(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRankUpCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.OnRankUpCBK(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFaceCBK(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)ToLua.CheckObject(L, 1, typeof(MyFrameWork.FightDataManager));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.OnFaceCBK(arg0);
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
	static int get_OpponentBossId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] ret = obj.OpponentBossId;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index OpponentBossId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MyBossId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] ret = obj.MyBossId;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MyBossId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LevelId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] ret = obj.LevelId;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index LevelId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DicOpponentBoss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> ret = obj.DicOpponentBoss;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicOpponentBoss on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DicOriOpponentBoss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> ret = obj.DicOriOpponentBoss;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicOriOpponentBoss on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DicMyBossHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> ret = obj.DicMyBossHp;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicMyBossHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TimeSec(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.TimeSec;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index TimeSec on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Moves(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.Moves;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Moves on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MyOriTotalHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.MyOriTotalHp;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MyOriTotalHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MyCurrentTotalHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.MyCurrentTotalHp;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MyCurrentTotalHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OpponentOriTotalHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.OpponentOriTotalHp;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index OpponentOriTotalHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OpponentCurrentTotalHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int ret = obj.OpponentCurrentTotalHp;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index OpponentCurrentTotalHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_moduleFight(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			FightNew.ModuleFight ret = obj.moduleFight;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index moduleFight on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OpponentBossId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] arg0 = ToLua.CheckNumberArray<int>(L, 2);
			obj.OpponentBossId = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index OpponentBossId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MyBossId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] arg0 = ToLua.CheckNumberArray<int>(L, 2);
			obj.MyBossId = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index MyBossId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LevelId(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int[] arg0 = ToLua.CheckNumberArray<int>(L, 2);
			obj.LevelId = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index LevelId on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DicOpponentBoss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> arg0 = (System.Collections.Generic.Dictionary<int,int>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,int>));
			obj.DicOpponentBoss = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicOpponentBoss on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DicOriOpponentBoss(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> arg0 = (System.Collections.Generic.Dictionary<int,int>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,int>));
			obj.DicOriOpponentBoss = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicOriOpponentBoss on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DicMyBossHp(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			System.Collections.Generic.Dictionary<int,int> arg0 = (System.Collections.Generic.Dictionary<int,int>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<int,int>));
			obj.DicMyBossHp = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index DicMyBossHp on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TimeSec(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.TimeSec = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index TimeSec on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Moves(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MyFrameWork.FightDataManager obj = (MyFrameWork.FightDataManager)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.Moves = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Moves on a nil value" : e.Message);
		}
	}
}

