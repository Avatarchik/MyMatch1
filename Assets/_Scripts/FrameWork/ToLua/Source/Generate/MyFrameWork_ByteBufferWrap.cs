using System;
using LuaInterface;

public class MyFrameWork_ByteBufferWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MyFrameWork.ByteBuffer), typeof(System.Object));
		L.RegFunction("Close", Close);
		L.RegFunction("WriteByte", WriteByte);
		L.RegFunction("WriteInt", WriteInt);
		L.RegFunction("WriteShort", WriteShort);
		L.RegFunction("WriteLong", WriteLong);
		L.RegFunction("WriteFloat", WriteFloat);
		L.RegFunction("WriteDouble", WriteDouble);
		L.RegFunction("WriteString", WriteString);
		L.RegFunction("WriteBytes", WriteBytes);
		L.RegFunction("WriteBuffer", WriteBuffer);
		L.RegFunction("ReadByte", ReadByte);
		L.RegFunction("ReadInt", ReadInt);
		L.RegFunction("ReadShort", ReadShort);
		L.RegFunction("ReadLong", ReadLong);
		L.RegFunction("ReadFloat", ReadFloat);
		L.RegFunction("ReadDouble", ReadDouble);
		L.RegFunction("ReadString", ReadString);
		L.RegFunction("ReadBytes", ReadBytes);
		L.RegFunction("ReadBuffer", ReadBuffer);
		L.RegFunction("ToBytes", ToBytes);
		L.RegFunction("Flush", Flush);
		L.RegFunction("New", _CreateMyFrameWork_ByteBuffer);
		L.RegFunction("__tostring", Lua_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMyFrameWork_ByteBuffer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MyFrameWork.ByteBuffer obj = new MyFrameWork.ByteBuffer();
			ToLua.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && ToLua.CheckTypes(L, 1, typeof(byte[])))
		{
			byte[] arg0 = ToLua.CheckByteBuffer(L, 1);
			MyFrameWork.ByteBuffer obj = new MyFrameWork.ByteBuffer(arg0);
			ToLua.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MyFrameWork.ByteBuffer.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			obj.Close();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteByte(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			byte arg0 = (byte)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteByte(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteInt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteInt(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteShort(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			ushort arg0 = (ushort)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteShort(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteLong(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			long arg0 = (long)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteLong(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteFloat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteFloat(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteDouble(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			double arg0 = (double)LuaDLL.luaL_checknumber(L, 2);
			obj.WriteDouble(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			string arg0 = ToLua.CheckString(L, 2);
			obj.WriteString(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteBytes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
			obj.WriteBytes(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteBuffer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			LuaByteBuffer arg0 = new LuaByteBuffer(ToLua.CheckByteBuffer(L, 2));
			obj.WriteBuffer(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadByte(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			byte o = obj.ReadByte();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadInt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			int o = obj.ReadInt();
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadShort(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			ushort o = obj.ReadShort();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadLong(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			long o = obj.ReadLong();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadFloat(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			float o = obj.ReadFloat();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadDouble(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			double o = obj.ReadDouble();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			string o = obj.ReadString();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadBytes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			byte[] o = obj.ReadBytes();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadBuffer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			LuaInterface.LuaByteBuffer o = obj.ReadBuffer();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToBytes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			byte[] o = obj.ToBytes();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Flush(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MyFrameWork.ByteBuffer obj = (MyFrameWork.ByteBuffer)ToLua.CheckObject(L, 1, typeof(MyFrameWork.ByteBuffer));
			obj.Flush();
			return 0;
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

