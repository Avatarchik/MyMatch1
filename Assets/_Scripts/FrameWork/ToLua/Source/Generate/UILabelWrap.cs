using System;
using LuaInterface;

public class UILabelWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UILabel), typeof(UIWidget));
		L.RegFunction("GetSides", GetSides);
		L.RegFunction("MarkAsChanged", MarkAsChanged);
		L.RegFunction("ProcessText", ProcessText);
		L.RegFunction("MakePixelPerfect", MakePixelPerfect);
		L.RegFunction("AssumeNaturalSize", AssumeNaturalSize);
		L.RegFunction("GetCharacterIndexAtPosition", GetCharacterIndexAtPosition);
		L.RegFunction("GetWordAtPosition", GetWordAtPosition);
		L.RegFunction("GetWordAtCharacterIndex", GetWordAtCharacterIndex);
		L.RegFunction("GetUrlAtPosition", GetUrlAtPosition);
		L.RegFunction("GetUrlAtCharacterIndex", GetUrlAtCharacterIndex);
		L.RegFunction("GetCharacterIndex", GetCharacterIndex);
		L.RegFunction("PrintOverlay", PrintOverlay);
		L.RegFunction("OnFill", OnFill);
		L.RegFunction("ApplyOffset", ApplyOffset);
		L.RegFunction("ApplyShadow", ApplyShadow);
		L.RegFunction("CalculateOffsetToFit", CalculateOffsetToFit);
		L.RegFunction("SetCurrentProgress", SetCurrentProgress);
		L.RegFunction("SetCurrentPercent", SetCurrentPercent);
		L.RegFunction("SetCurrentSelection", SetCurrentSelection);
		L.RegFunction("Wrap", Wrap);
		L.RegFunction("UpdateNGUIText", UpdateNGUIText);
		L.RegFunction("New", _CreateUILabel);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("keepCrispWhenShrunk", get_keepCrispWhenShrunk, set_keepCrispWhenShrunk);
		L.RegVar("finalFontSize", get_finalFontSize, null);
		L.RegVar("isAnchoredHorizontally", get_isAnchoredHorizontally, null);
		L.RegVar("isAnchoredVertically", get_isAnchoredVertically, null);
		L.RegVar("material", get_material, set_material);
		L.RegVar("bitmapFont", get_bitmapFont, set_bitmapFont);
		L.RegVar("trueTypeFont", get_trueTypeFont, set_trueTypeFont);
		L.RegVar("ambigiousFont", get_ambigiousFont, set_ambigiousFont);
		L.RegVar("text", get_text, set_text);
		L.RegVar("defaultFontSize", get_defaultFontSize, null);
		L.RegVar("fontSize", get_fontSize, set_fontSize);
		L.RegVar("fontStyle", get_fontStyle, set_fontStyle);
		L.RegVar("alignment", get_alignment, set_alignment);
		L.RegVar("applyGradient", get_applyGradient, set_applyGradient);
		L.RegVar("gradientTop", get_gradientTop, set_gradientTop);
		L.RegVar("gradientBottom", get_gradientBottom, set_gradientBottom);
		L.RegVar("spacingX", get_spacingX, set_spacingX);
		L.RegVar("spacingY", get_spacingY, set_spacingY);
		L.RegVar("useFloatSpacing", get_useFloatSpacing, set_useFloatSpacing);
		L.RegVar("floatSpacingX", get_floatSpacingX, set_floatSpacingX);
		L.RegVar("floatSpacingY", get_floatSpacingY, set_floatSpacingY);
		L.RegVar("effectiveSpacingY", get_effectiveSpacingY, null);
		L.RegVar("effectiveSpacingX", get_effectiveSpacingX, null);
		L.RegVar("overflowEllipsis", get_overflowEllipsis, set_overflowEllipsis);
		L.RegVar("overflowWidth", get_overflowWidth, set_overflowWidth);
		L.RegVar("supportEncoding", get_supportEncoding, set_supportEncoding);
		L.RegVar("symbolStyle", get_symbolStyle, set_symbolStyle);
		L.RegVar("overflowMethod", get_overflowMethod, set_overflowMethod);
		L.RegVar("multiLine", get_multiLine, set_multiLine);
		L.RegVar("localCorners", get_localCorners, null);
		L.RegVar("worldCorners", get_worldCorners, null);
		L.RegVar("drawingDimensions", get_drawingDimensions, null);
		L.RegVar("maxLineCount", get_maxLineCount, set_maxLineCount);
		L.RegVar("effectStyle", get_effectStyle, set_effectStyle);
		L.RegVar("effectColor", get_effectColor, set_effectColor);
		L.RegVar("effectDistance", get_effectDistance, set_effectDistance);
		L.RegVar("processedText", get_processedText, null);
		L.RegVar("printedSize", get_printedSize, null);
		L.RegVar("localSize", get_localSize, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUILabel(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UILabel class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSides(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Transform));
			UnityEngine.Vector3[] o = obj.GetSides(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MarkAsChanged(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.MarkAsChanged();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ProcessText(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.ProcessText();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MakePixelPerfect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.MakePixelPerfect();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AssumeNaturalSize(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.AssumeNaturalSize();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCharacterIndexAtPosition(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector2), typeof(bool)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				int o = obj.GetCharacterIndexAtPosition(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector3), typeof(bool)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				int o = obj.GetCharacterIndexAtPosition(arg0, arg1);
				LuaDLL.lua_pushinteger(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UILabel.GetCharacterIndexAtPosition");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWordAtPosition(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector2)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
				string o = obj.GetWordAtPosition(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector3)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				string o = obj.GetWordAtPosition(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UILabel.GetWordAtPosition");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWordAtCharacterIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			string o = obj.GetWordAtCharacterIndex(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUrlAtPosition(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector2)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
				string o = obj.GetUrlAtPosition(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(UnityEngine.Vector3)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				string o = obj.GetUrlAtPosition(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UILabel.GetUrlAtPosition");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUrlAtCharacterIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			string o = obj.GetUrlAtCharacterIndex(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCharacterIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.KeyCode arg1 = (UnityEngine.KeyCode)ToLua.CheckObject(L, 3, typeof(UnityEngine.KeyCode));
			int o = obj.GetCharacterIndex(arg0, arg1);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrintOverlay(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 7);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			UIGeometry arg2 = (UIGeometry)ToLua.CheckObject(L, 4, typeof(UIGeometry));
			UIGeometry arg3 = (UIGeometry)ToLua.CheckObject(L, 5, typeof(UIGeometry));
			UnityEngine.Color arg4 = ToLua.ToColor(L, 6);
			UnityEngine.Color arg5 = ToLua.ToColor(L, 7);
			obj.PrintOverlay(arg0, arg1, arg2, arg3, arg4, arg5);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFill(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			BetterList<UnityEngine.Vector3> arg0 = (BetterList<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(BetterList<UnityEngine.Vector3>));
			BetterList<UnityEngine.Vector2> arg1 = (BetterList<UnityEngine.Vector2>)ToLua.CheckObject(L, 3, typeof(BetterList<UnityEngine.Vector2>));
			BetterList<UnityEngine.Color32> arg2 = (BetterList<UnityEngine.Color32>)ToLua.CheckObject(L, 4, typeof(BetterList<UnityEngine.Color32>));
			obj.OnFill(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ApplyOffset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			BetterList<UnityEngine.Vector3> arg0 = (BetterList<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(BetterList<UnityEngine.Vector3>));
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			UnityEngine.Vector2 o = obj.ApplyOffset(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ApplyShadow(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 8);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			BetterList<UnityEngine.Vector3> arg0 = (BetterList<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(BetterList<UnityEngine.Vector3>));
			BetterList<UnityEngine.Vector2> arg1 = (BetterList<UnityEngine.Vector2>)ToLua.CheckObject(L, 3, typeof(BetterList<UnityEngine.Vector2>));
			BetterList<UnityEngine.Color32> arg2 = (BetterList<UnityEngine.Color32>)ToLua.CheckObject(L, 4, typeof(BetterList<UnityEngine.Color32>));
			int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
			int arg4 = (int)LuaDLL.luaL_checknumber(L, 6);
			float arg5 = (float)LuaDLL.luaL_checknumber(L, 7);
			float arg6 = (float)LuaDLL.luaL_checknumber(L, 8);
			obj.ApplyShadow(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateOffsetToFit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			string arg0 = ToLua.CheckString(L, 2);
			int o = obj.CalculateOffsetToFit(arg0);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentProgress(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.SetCurrentProgress();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentPercent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.SetCurrentPercent();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentSelection(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.SetCurrentSelection();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Wrap(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(string), typeof(LuaInterface.LuaOut<string>)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = null;
				bool o = obj.Wrap(arg0, out arg1);
				LuaDLL.lua_pushboolean(L, o);
				LuaDLL.lua_pushstring(L, arg1);
				return 2;
			}
			else if (count == 4 && ToLua.CheckTypes(L, 1, typeof(UILabel), typeof(string), typeof(LuaInterface.LuaOut<string>), typeof(int)))
			{
				UILabel obj = (UILabel)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = null;
				int arg2 = (int)LuaDLL.lua_tonumber(L, 4);
				bool o = obj.Wrap(arg0, out arg1, arg2);
				LuaDLL.lua_pushboolean(L, o);
				LuaDLL.lua_pushstring(L, arg1);
				return 2;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UILabel.Wrap");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateNGUIText(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UILabel obj = (UILabel)ToLua.CheckObject(L, 1, typeof(UILabel));
			obj.UpdateNGUIText();
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
	static int get_keepCrispWhenShrunk(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Crispness ret = obj.keepCrispWhenShrunk;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index keepCrispWhenShrunk on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_finalFontSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.finalFontSize;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index finalFontSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isAnchoredHorizontally(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.isAnchoredHorizontally;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isAnchoredHorizontally on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isAnchoredVertically(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.isAnchoredVertically;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isAnchoredVertically on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_material(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Material ret = obj.material;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index material on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bitmapFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UIFont ret = obj.bitmapFont;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bitmapFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trueTypeFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Font ret = obj.trueTypeFont;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index trueTypeFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ambigiousFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Object ret = obj.ambigiousFont;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ambigiousFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_text(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			string ret = obj.text;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index text on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultFontSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.defaultFontSize;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index defaultFontSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fontSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.fontSize;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fontSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fontStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.FontStyle ret = obj.fontStyle;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fontStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alignment(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			NGUIText.Alignment ret = obj.alignment;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alignment on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_applyGradient(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.applyGradient;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index applyGradient on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gradientTop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color ret = obj.gradientTop;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gradientTop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gradientBottom(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color ret = obj.gradientBottom;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gradientBottom on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spacingX(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.spacingX;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacingX on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spacingY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.spacingY;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacingY on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useFloatSpacing(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.useFloatSpacing;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useFloatSpacing on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_floatSpacingX(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float ret = obj.floatSpacingX;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index floatSpacingX on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_floatSpacingY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float ret = obj.floatSpacingY;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index floatSpacingY on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_effectiveSpacingY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float ret = obj.effectiveSpacingY;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectiveSpacingY on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_effectiveSpacingX(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float ret = obj.effectiveSpacingX;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectiveSpacingX on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overflowEllipsis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.overflowEllipsis;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowEllipsis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overflowWidth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.overflowWidth;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowWidth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportEncoding(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.supportEncoding;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index supportEncoding on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_symbolStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			NGUIText.SymbolStyle ret = obj.symbolStyle;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index symbolStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overflowMethod(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Overflow ret = obj.overflowMethod;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowMethod on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_multiLine(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool ret = obj.multiLine;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index multiLine on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localCorners(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector3[] ret = obj.localCorners;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localCorners on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldCorners(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector3[] ret = obj.worldCorners;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldCorners on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_drawingDimensions(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector4 ret = obj.drawingDimensions;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index drawingDimensions on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxLineCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int ret = obj.maxLineCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxLineCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_effectStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Effect ret = obj.effectStyle;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_effectColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color ret = obj.effectColor;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_effectDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector2 ret = obj.effectDistance;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectDistance on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_processedText(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			string ret = obj.processedText;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index processedText on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_printedSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector2 ret = obj.printedSize;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index printedSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector2 ret = obj.localSize;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_keepCrispWhenShrunk(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Crispness arg0 = (UILabel.Crispness)ToLua.CheckObject(L, 2, typeof(UILabel.Crispness));
			obj.keepCrispWhenShrunk = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index keepCrispWhenShrunk on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_material(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Material arg0 = (UnityEngine.Material)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Material));
			obj.material = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index material on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bitmapFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UIFont arg0 = (UIFont)ToLua.CheckUnityObject(L, 2, typeof(UIFont));
			obj.bitmapFont = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index bitmapFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_trueTypeFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Font arg0 = (UnityEngine.Font)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Font));
			obj.trueTypeFont = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index trueTypeFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ambigiousFont(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Object));
			obj.ambigiousFont = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ambigiousFont on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_text(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.text = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index text on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fontSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.fontSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fontSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fontStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.FontStyle arg0 = (UnityEngine.FontStyle)ToLua.CheckObject(L, 2, typeof(UnityEngine.FontStyle));
			obj.fontStyle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fontStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alignment(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			NGUIText.Alignment arg0 = (NGUIText.Alignment)ToLua.CheckObject(L, 2, typeof(NGUIText.Alignment));
			obj.alignment = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alignment on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_applyGradient(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.applyGradient = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index applyGradient on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gradientTop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.gradientTop = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gradientTop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gradientBottom(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.gradientBottom = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gradientBottom on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spacingX(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.spacingX = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacingX on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spacingY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.spacingY = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index spacingY on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useFloatSpacing(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.useFloatSpacing = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useFloatSpacing on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_floatSpacingX(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.floatSpacingX = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index floatSpacingX on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_floatSpacingY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.floatSpacingY = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index floatSpacingY on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overflowEllipsis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.overflowEllipsis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowEllipsis on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overflowWidth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.overflowWidth = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowWidth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_supportEncoding(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.supportEncoding = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index supportEncoding on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_symbolStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			NGUIText.SymbolStyle arg0 = (NGUIText.SymbolStyle)ToLua.CheckObject(L, 2, typeof(NGUIText.SymbolStyle));
			obj.symbolStyle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index symbolStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overflowMethod(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Overflow arg0 = (UILabel.Overflow)ToLua.CheckObject(L, 2, typeof(UILabel.Overflow));
			obj.overflowMethod = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index overflowMethod on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_multiLine(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.multiLine = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index multiLine on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxLineCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.maxLineCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxLineCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_effectStyle(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UILabel.Effect arg0 = (UILabel.Effect)ToLua.CheckObject(L, 2, typeof(UILabel.Effect));
			obj.effectStyle = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectStyle on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_effectColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.effectColor = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_effectDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UILabel obj = (UILabel)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.effectDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index effectDistance on a nil value" : e.Message);
		}
	}
}

