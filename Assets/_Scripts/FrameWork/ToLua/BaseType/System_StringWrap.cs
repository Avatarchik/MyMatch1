using System;
using LuaInterface;

public class System_StringWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.String), typeof(System.Object));
		L.RegFunction("Equals", Equals);
		L.RegFunction("Clone", Clone);
		L.RegFunction("GetTypeCode", GetTypeCode);
		L.RegFunction("CopyTo", CopyTo);
		L.RegFunction("ToCharArray", ToCharArray);
		L.RegFunction("Split", Split);
		L.RegFunction("Substring", Substring);
		L.RegFunction("Trim", Trim);
		L.RegFunction("TrimStart", TrimStart);
		L.RegFunction("TrimEnd", TrimEnd);
		L.RegFunction("Compare", Compare);
		L.RegFunction("CompareTo", CompareTo);
		L.RegFunction("CompareOrdinal", CompareOrdinal);
		L.RegFunction("EndsWith", EndsWith);
		L.RegFunction("IndexOfAny", IndexOfAny);
		L.RegFunction("IndexOf", IndexOf);
		L.RegFunction("LastIndexOf", LastIndexOf);
		L.RegFunction("LastIndexOfAny", LastIndexOfAny);
		L.RegFunction("Contains", Contains);
		L.RegFunction("IsNullOrEmpty", IsNullOrEmpty);
		L.RegFunction("Normalize", Normalize);
		L.RegFunction("IsNormalized", IsNormalized);
		L.RegFunction("Remove", Remove);
		L.RegFunction("PadLeft", PadLeft);
		L.RegFunction("PadRight", PadRight);
		L.RegFunction("StartsWith", StartsWith);
		L.RegFunction("Replace", Replace);
		L.RegFunction("ToLower", ToLower);
		L.RegFunction("ToLowerInvariant", ToLowerInvariant);
		L.RegFunction("ToUpper", ToUpper);
		L.RegFunction("ToUpperInvariant", ToUpperInvariant);
		L.RegFunction("ToString", ToString);
		L.RegFunction("Format", Format);
		L.RegFunction("Copy", Copy);
		L.RegFunction("Concat", Concat);
		L.RegFunction("Insert", Insert);
		L.RegFunction("Intern", Intern);
		L.RegFunction("IsInterned", IsInterned);
		L.RegFunction("Join", Join);
		L.RegFunction("GetEnumerator", GetEnumerator);
		L.RegFunction("GetHashCode", GetHashCode);
		L.RegFunction("New", _CreateSystem_String);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("Empty", get_Empty, null);
		L.RegVar("Length", get_Length, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystem_String(IntPtr L)
	{
        try
        {
            LuaTypes luatype = LuaDLL.lua_type(L, 1);

            if (luatype == LuaTypes.LUA_TSTRING)
            {
                string arg0 = LuaDLL.lua_tostring(L, 1);
                ToLua.PushObject(L, arg0);
                return 1;
            }
            else
            {
                return LuaDLL.tolua_error(L, "invalid arguments to method: String.New");
            }            
        }
        catch(Exception e)
        {
            return LuaDLL.toluaL_exception(L, e);
        }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool o = obj != null ? obj.Equals(arg0) : arg0 == null;
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				object arg0 = ToLua.ToVarObject(L, 2);
				bool o = obj != null ? obj.Equals(arg0) : arg0 == null;
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.StringComparison arg1 = (System.StringComparison)ToLua.ToObject(L, 3);
				bool o = obj != null ? obj.Equals(arg0, arg1) : arg0 == null;
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Equals");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clone(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			object o = obj.Clone();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTypeCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			System.TypeCode o = obj.GetTypeCode();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 5);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			char[] arg1 = ToLua.CheckCharBuffer(L, 3);
			int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
			int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
			obj.CopyTo(arg0, arg1, arg2, arg3);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToCharArray(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] o = obj.ToCharArray();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				char[] o = obj.ToCharArray(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.ToCharArray");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Split(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(System.StringSplitOptions)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				System.StringSplitOptions arg1 = (System.StringSplitOptions)ToLua.ToObject(L, 3);
				string[] o = obj.Split(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				string[] o = obj.Split(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(System.StringSplitOptions)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string[] arg0 = ToLua.CheckStringArray(L, 2);
				System.StringSplitOptions arg1 = (System.StringSplitOptions)ToLua.ToObject(L, 3);
				string[] o = obj.Split(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(int), typeof(System.StringSplitOptions)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string[] arg0 = ToLua.CheckStringArray(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				System.StringSplitOptions arg2 = (System.StringSplitOptions)ToLua.ToObject(L, 4);
				string[] o = obj.Split(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(System.StringSplitOptions)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				System.StringSplitOptions arg2 = (System.StringSplitOptions)ToLua.ToObject(L, 4);
				string[] o = obj.Split(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (ToLua.CheckParamsType(L, typeof(char), 2, count - 1))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.ToParamsChar(L, 2, count - 1);
				string[] o = obj.Split(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Split");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Substring(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj.Substring(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				string o = obj.Substring(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Substring");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Trim(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string o = obj.Trim();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (ToLua.CheckParamsType(L, typeof(char), 2, count - 1))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.ToParamsChar(L, 2, count - 1);
				string o = obj.Trim(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Trim");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrimStart(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			char[] arg0 = ToLua.CheckParamsChar(L, 2, count - 1);
			string o = obj.TrimStart(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrimEnd(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			char[] arg0 = ToLua.CheckParamsChar(L, 2, count - 1);
			string o = obj.TrimEnd(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Compare(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				int o = System.String.Compare(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				System.StringComparison arg2 = (System.StringComparison)ToLua.ToObject(L, 3);
				int o = System.String.Compare(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				int o = System.String.Compare(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.Globalization.CultureInfo), typeof(System.Globalization.CompareOptions)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				System.Globalization.CultureInfo arg2 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 3);
				System.Globalization.CompareOptions arg3 = (System.Globalization.CompareOptions)ToLua.ToObject(L, 4);
				int o = System.String.Compare(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(System.Globalization.CultureInfo)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				System.Globalization.CultureInfo arg3 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 4);
				int o = System.String.Compare(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				int o = System.String.Compare(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(System.StringComparison)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				System.StringComparison arg5 = (System.StringComparison)ToLua.ToObject(L, 6);
				int o = System.String.Compare(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 6 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				bool arg5 = LuaDLL.lua_toboolean(L, 6);
				int o = System.String.Compare(arg0, arg1, arg2, arg3, arg4, arg5);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(System.Globalization.CultureInfo), typeof(System.Globalization.CompareOptions)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				System.Globalization.CultureInfo arg5 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 6);
				System.Globalization.CompareOptions arg6 = (System.Globalization.CompareOptions)ToLua.ToObject(L, 7);
				int o = System.String.Compare(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 7 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool), typeof(System.Globalization.CultureInfo)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				bool arg5 = LuaDLL.lua_toboolean(L, 6);
				System.Globalization.CultureInfo arg6 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 7);
				int o = System.String.Compare(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Compare");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareTo(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int o = obj.CompareTo(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				object arg0 = ToLua.ToVarObject(L, 2);
				int o = obj.CompareTo(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.CompareTo");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareOrdinal(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				int o = System.String.CompareOrdinal(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int)))
			{
				string arg0 = ToLua.ToString(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
				int o = System.String.CompareOrdinal(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.CompareOrdinal");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndsWith(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool o = obj.EndsWith(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.StringComparison arg1 = (System.StringComparison)ToLua.ToObject(L, 3);
				bool o = obj.EndsWith(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(System.Globalization.CultureInfo)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				System.Globalization.CultureInfo arg2 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 4);
				bool o = obj.EndsWith(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.EndsWith");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IndexOfAny(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[])))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int o = obj.IndexOfAny(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.IndexOfAny(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.IndexOfAny(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.IndexOfAny");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IndexOf(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int o = obj.IndexOf(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int o = obj.IndexOf(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.IndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.IndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.StringComparison arg1 = (System.StringComparison)ToLua.ToObject(L, 3);
				int o = obj.IndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.IndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				System.StringComparison arg2 = (System.StringComparison)ToLua.ToObject(L, 4);
				int o = obj.IndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.IndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				System.StringComparison arg3 = (System.StringComparison)ToLua.ToObject(L, 5);
				int o = obj.IndexOf(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.IndexOf");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LastIndexOf(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int o = obj.LastIndexOf(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int o = obj.LastIndexOf(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.LastIndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.LastIndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.StringComparison arg1 = (System.StringComparison)ToLua.ToObject(L, 3);
				int o = obj.LastIndexOf(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.LastIndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				System.StringComparison arg2 = (System.StringComparison)ToLua.ToObject(L, 4);
				int o = obj.LastIndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.LastIndexOf(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				System.StringComparison arg3 = (System.StringComparison)ToLua.ToObject(L, 5);
				int o = obj.LastIndexOf(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.LastIndexOf");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LastIndexOfAny(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[])))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int o = obj.LastIndexOfAny(arg0);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int o = obj.LastIndexOfAny(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char[] arg0 = ToLua.CheckCharBuffer(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				int o = obj.LastIndexOfAny(arg0, arg1, arg2);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.LastIndexOfAny");
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
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
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
	static int IsNullOrEmpty(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			bool o = System.String.IsNullOrEmpty(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Normalize(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string o = obj.Normalize();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(System.Text.NormalizationForm)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				System.Text.NormalizationForm arg0 = (System.Text.NormalizationForm)ToLua.ToObject(L, 2);
				string o = obj.Normalize(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Normalize");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsNormalized(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				bool o = obj.IsNormalized();
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(System.Text.NormalizationForm)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				System.Text.NormalizationForm arg0 = (System.Text.NormalizationForm)ToLua.ToObject(L, 2);
				bool o = obj.IsNormalized(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.IsNormalized");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj.Remove(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				string o = obj.Remove(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Remove");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PadLeft(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj.PadLeft(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(char)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				char arg1 = (char)LuaDLL.lua_tonumber(L, 3);
				string o = obj.PadLeft(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.PadLeft");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PadRight(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj.PadRight(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(int), typeof(char)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				char arg1 = (char)LuaDLL.lua_tonumber(L, 3);
				string o = obj.PadRight(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.PadRight");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartsWith(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool o = obj.StartsWith(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(System.StringComparison)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				System.StringComparison arg1 = (System.StringComparison)ToLua.ToObject(L, 3);
				bool o = obj.StartsWith(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(System.Globalization.CultureInfo)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				System.Globalization.CultureInfo arg2 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 4);
				bool o = obj.StartsWith(arg0, arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.StartsWith");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Replace(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				string o = obj.Replace(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(char), typeof(char)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				char arg0 = (char)LuaDLL.lua_tonumber(L, 2);
				char arg1 = (char)LuaDLL.lua_tonumber(L, 3);
				string o = obj.Replace(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Replace");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToLower(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string o = obj.ToLower();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(System.Globalization.CultureInfo)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				System.Globalization.CultureInfo arg0 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 2);
				string o = obj.ToLower(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.ToLower");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToLowerInvariant(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			string o = obj.ToLowerInvariant();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToUpper(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string o = obj.ToUpper();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(System.Globalization.CultureInfo)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				System.Globalization.CultureInfo arg0 = (System.Globalization.CultureInfo)ToLua.ToObject(L, 2);
				string o = obj.ToUpper(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.ToUpper");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToUpperInvariant(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			string o = obj.ToUpperInvariant();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(string)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				string o = obj.ToString();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(System.IFormatProvider)))
			{
				System.String obj = (System.String)ToLua.ToObject(L, 1);
				System.IFormatProvider arg0 = (System.IFormatProvider)ToLua.ToObject(L, 2);
				string o = obj.ToString(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.ToString");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Format(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(object)))
			{
				string arg0 = ToLua.ToString(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				string o = System.String.Format(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string arg0 = ToLua.ToString(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				object arg2 = ToLua.ToVarObject(L, 3);
				string o = System.String.Format(arg0, arg1, arg2);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object), typeof(object)))
			{
				string arg0 = ToLua.ToString(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				object arg2 = ToLua.ToVarObject(L, 3);
				object arg3 = ToLua.ToVarObject(L, 4);
				string o = System.String.Format(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (ToLua.CheckTypes(L, 1, typeof(System.IFormatProvider), typeof(string)) && ToLua.CheckParamsType(L, typeof(object), 3, count - 2))
			{
				System.IFormatProvider arg0 = (System.IFormatProvider)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				object[] arg2 = ToLua.ToParamsObject(L, 3, count - 2);
				string o = System.String.Format(arg0, arg1, arg2);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (ToLua.CheckTypes(L, 1, typeof(string)) && ToLua.CheckParamsType(L, typeof(object), 2, count - 1))
			{
				string arg0 = ToLua.ToString(L, 1);
				object[] arg1 = ToLua.ToParamsObject(L, 2, count - 1);
				string o = System.String.Format(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Format");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Copy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = System.String.Copy(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Concat(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(object)))
			{
				object arg0 = ToLua.ToVarObject(L, 1);
				string o = System.String.Concat(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				string o = System.String.Concat(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(object), typeof(object)))
			{
				object arg0 = ToLua.ToVarObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				string o = System.String.Concat(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				string o = System.String.Concat(arg0, arg1, arg2);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object)))
			{
				object arg0 = ToLua.ToVarObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				object arg2 = ToLua.ToVarObject(L, 3);
				string o = System.String.Concat(arg0, arg1, arg2);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				string arg3 = ToLua.ToString(L, 4);
				string o = System.String.Concat(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object), typeof(object)))
			{
				object arg0 = ToLua.ToVarObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				object arg2 = ToLua.ToVarObject(L, 3);
				object arg3 = ToLua.ToVarObject(L, 4);
				string o = System.String.Concat(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (ToLua.CheckParamsType(L, typeof(string), 1, count))
			{
				string[] arg0 = ToLua.ToParamsString(L, 1, count);
				string o = System.String.Concat(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (ToLua.CheckParamsType(L, typeof(object), 1, count))
			{
				object[] arg0 = ToLua.ToParamsObject(L, 1, count);
				string o = System.String.Concat(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Concat");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Insert(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string o = obj.Insert(arg0, arg1);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Intern(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = System.String.Intern(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInterned(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = System.String.IsInterned(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Join(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string[])))
			{
				string arg0 = ToLua.ToString(L, 1);
				string[] arg1 = ToLua.CheckStringArray(L, 2);
				string o = System.String.Join(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(int), typeof(int)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string[] arg1 = ToLua.CheckStringArray(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				string o = System.String.Join(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.String.Join");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			System.CharEnumerator o = obj.GetEnumerator();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.String obj = (System.String)ToLua.CheckObject(L, 1, typeof(System.String));
			int o = obj.GetHashCode();
			LuaDLL.lua_pushinteger(L, o);
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
			string arg0 = ToLua.ToString(L, 1);
			string arg1 = ToLua.ToString(L, 2);
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
	static int get_Empty(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, System.String.Empty);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Length(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);

			System.String obj = (System.String)o;
			int ret = obj.Length;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Length on a nil value" : e.Message);
		}
	}
}

