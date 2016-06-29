using System;
using System.Collections.Generic;
using LuaInterface;

public class System_DelegateWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Delegate), typeof(System.Object));
		L.RegFunction("CreateDelegate", CreateDelegate);
		L.RegFunction("DynamicInvoke", DynamicInvoke);
		L.RegFunction("Clone", Clone);
		L.RegFunction("GetObjectData", GetObjectData);
		L.RegFunction("GetInvocationList", GetInvocationList);
		L.RegFunction("Combine", Combine);
		L.RegFunction("Remove", Remove);
		L.RegFunction("RemoveAll", RemoveAll);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("GetHashCode", GetHashCode);
		L.RegFunction("Equals", Equals);
		L.RegFunction("New", _CreateSystem_Delegate);
		L.RegFunction("__add", op_Addition);
		L.RegFunction("__sub", op_Subtraction);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("Method", get_Method, null);
		L.RegVar("Target", get_Target, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystem_Delegate(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "System.Delegate class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateDelegate(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(System.Reflection.MethodInfo)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Reflection.MethodInfo arg1 = (System.Reflection.MethodInfo)ToLua.ToObject(L, 2);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(System.Reflection.MethodInfo), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Reflection.MethodInfo arg1 = (System.Reflection.MethodInfo)ToLua.ToObject(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(System.Type), typeof(string)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(object), typeof(string)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(object), typeof(System.Reflection.MethodInfo)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				System.Reflection.MethodInfo arg2 = (System.Reflection.MethodInfo)ToLua.ToObject(L, 3);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(System.Type), typeof(string), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(object), typeof(string), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(object), typeof(System.Reflection.MethodInfo), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				System.Reflection.MethodInfo arg2 = (System.Reflection.MethodInfo)ToLua.ToObject(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(System.Type), typeof(string), typeof(bool), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Type arg1 = (System.Type)ToLua.ToObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && ToLua.CheckTypes(L, 1, typeof(System.Type), typeof(object), typeof(string), typeof(bool), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				string arg2 = ToLua.ToString(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				System.Delegate o = System.Delegate.CreateDelegate(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.Delegate.CreateDelegate");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DynamicInvoke(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			object[] arg0 = ToLua.ToParamsObject(L, 2, count - 1);
			object o = obj.DynamicInvoke(arg0);
			ToLua.Push(L, o);
			return 1;
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
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
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
	static int GetObjectData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			System.Runtime.Serialization.SerializationInfo arg0 = (System.Runtime.Serialization.SerializationInfo)ToLua.CheckObject(L, 2, typeof(System.Runtime.Serialization.SerializationInfo));
			System.Runtime.Serialization.StreamingContext arg1 = (System.Runtime.Serialization.StreamingContext)ToLua.CheckObject(L, 3, typeof(System.Runtime.Serialization.StreamingContext));
			obj.GetObjectData(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInvocationList(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			System.Delegate[] o = obj.GetInvocationList();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Combine(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(System.Delegate), typeof(System.Delegate)))
			{
				System.Delegate arg0 = (System.Delegate)ToLua.ToObject(L, 1);
				System.Delegate arg1 = (System.Delegate)ToLua.ToObject(L, 2);
				System.Delegate o = System.Delegate.Combine(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (ToLua.CheckParamsType(L, typeof(System.Delegate), 1, count))
			{
				System.Delegate[] arg0 = ToLua.ToParamsObject<System.Delegate>(L, 1, count);
				System.Delegate o = System.Delegate.Combine(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: System.Delegate.Combine");
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
			ToLua.CheckArgsCount(L, 2);
			System.Delegate arg0 = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			System.Delegate arg1 = (System.Delegate)ToLua.CheckObject(L, 2, typeof(System.Delegate));
			System.Delegate o = System.Delegate.Remove(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Delegate arg0 = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			System.Delegate arg1 = (System.Delegate)ToLua.CheckObject(L, 2, typeof(System.Delegate));
			System.Delegate o = System.Delegate.RemoveAll(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Subtraction(IntPtr L)
	{
        try
        {
            ToLua.CheckArgsCount(L, 2);
            Delegate arg0 = (Delegate)ToLua.CheckObject(L, 1, typeof(Delegate));
            LuaTypes type = LuaDLL.lua_type(L, 2);

            if (type == LuaTypes.LUA_TFUNCTION)
            {
                LuaState state = LuaState.Get(L);
                LuaFunction func = ToLua.ToLuaFunction(L, 2);
                Delegate[] ds = arg0.GetInvocationList();

                for (int i = 0; i < ds.Length; i++)
                {
                    LuaDelegate ld = ds[i].Target as LuaDelegate;

                    if (ld != null && ld.func == func)
                    {
                        arg0 = Delegate.Remove(arg0, ds[i]);
                        state.DelayDispose(ld.func);
                        break;
                    }
                }

                func.Dispose();
                ToLua.Push(L, arg0);
                return 1;
            }
            else
            {
                Delegate arg1 = (Delegate)ToLua.CheckObject(L, 2, typeof(Delegate));
                arg0 = Delegate.Remove(arg0, arg1);
                ToLua.Push(L, arg0);
                return 1;
            }
        }
        catch (Exception e)
        {
            return LuaDLL.toluaL_exception(L, e);
        }
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Addition(IntPtr L)
	{
        try
        {
            ToLua.CheckArgsCount(L, 2);
            Delegate arg0 = ToLua.CheckObject(L, 1, typeof(Delegate)) as Delegate;
            LuaTypes type = LuaDLL.lua_type(L, 2);

            if (type == LuaTypes.LUA_TFUNCTION)
            {
                LuaFunction func = ToLua.ToLuaFunction(L, 2);
                Type t = arg0.GetType();
                Delegate arg1 = DelegateFactory.CreateDelegate(t, func);
                Delegate arg2 = Delegate.Combine(arg0, arg1);
                ToLua.Push(L, arg2);
                return 1;
            }
            else
            {
                Delegate arg1 = ToLua.ToObject(L, 2) as Delegate;
                Delegate o = Delegate.Combine(arg0, arg1);
                ToLua.Push(L, o);
                return 1;
            }
        }
        catch (Exception e)
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
			System.Delegate arg0 = (System.Delegate)ToLua.ToObject(L, 1);
			System.Delegate arg1 = (System.Delegate)ToLua.ToObject(L, 2);
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
	static int Destroy(IntPtr L)
	{
        Delegate arg0 = ToLua.CheckObject(L, 1, typeof(Delegate)) as Delegate;
        Delegate[] ds = arg0.GetInvocationList();

        for (int i = 0; i < ds.Length; i++)
        {
            LuaDelegate ld = ds[i].Target as LuaDelegate;

            if (ld != null && ld.func != null)
            {                
                ld.func.Dispose();                
            }
        }

        return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
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
	static int Equals(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Delegate obj = (System.Delegate)ToLua.CheckObject(L, 1, typeof(System.Delegate));
			object arg0 = ToLua.ToVarObject(L, 2);
			bool o = obj != null ? obj.Equals(arg0) : arg0 == null;
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
	static int get_Method(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);

			System.Delegate obj = (System.Delegate)o;
			System.Reflection.MethodInfo ret = obj.Method;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Method on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Target(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);

			System.Delegate obj = (System.Delegate)o;
			object ret = obj.Target;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Target on a nil value" : e.Message);
		}
	}
}

