using System;
using LuaInterface;

public class UIWidgetWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UIWidget), typeof(UIRect));
		L.RegFunction("SetDimensions", SetDimensions);
		L.RegFunction("GetSides", GetSides);
		L.RegFunction("CalculateFinalAlpha", CalculateFinalAlpha);
		L.RegFunction("Invalidate", Invalidate);
		L.RegFunction("CalculateCumulativeAlpha", CalculateCumulativeAlpha);
		L.RegFunction("SetRect", SetRect);
		L.RegFunction("ResizeCollider", ResizeCollider);
		L.RegFunction("FullCompareFunc", FullCompareFunc);
		L.RegFunction("PanelCompareFunc", PanelCompareFunc);
		L.RegFunction("CalculateBounds", CalculateBounds);
		L.RegFunction("SetDirty", SetDirty);
		L.RegFunction("RemoveFromPanel", RemoveFromPanel);
		L.RegFunction("MarkAsChanged", MarkAsChanged);
		L.RegFunction("CreatePanel", CreatePanel);
		L.RegFunction("CheckLayer", CheckLayer);
		L.RegFunction("ParentHasChanged", ParentHasChanged);
		L.RegFunction("UpdateVisibility", UpdateVisibility);
		L.RegFunction("UpdateTransform", UpdateTransform);
		L.RegFunction("UpdateGeometry", UpdateGeometry);
		L.RegFunction("WriteToBuffers", WriteToBuffers);
		L.RegFunction("MakePixelPerfect", MakePixelPerfect);
		L.RegFunction("OnFill", OnFill);
		L.RegFunction("New", _CreateUIWidget);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", Lua_ToString);
		L.RegVar("onChange", get_onChange, set_onChange);
		L.RegVar("onPostFill", get_onPostFill, set_onPostFill);
		L.RegVar("mOnRender", get_mOnRender, set_mOnRender);
		L.RegVar("autoResizeBoxCollider", get_autoResizeBoxCollider, set_autoResizeBoxCollider);
		L.RegVar("hideIfOffScreen", get_hideIfOffScreen, set_hideIfOffScreen);
		L.RegVar("keepAspectRatio", get_keepAspectRatio, set_keepAspectRatio);
		L.RegVar("aspectRatio", get_aspectRatio, set_aspectRatio);
		L.RegVar("hitCheck", get_hitCheck, set_hitCheck);
		L.RegVar("panel", get_panel, set_panel);
		L.RegVar("geometry", get_geometry, set_geometry);
		L.RegVar("fillGeometry", get_fillGeometry, set_fillGeometry);
		L.RegVar("drawCall", get_drawCall, set_drawCall);
		L.RegVar("onRender", get_onRender, set_onRender);
		L.RegVar("drawRegion", get_drawRegion, set_drawRegion);
		L.RegVar("pivotOffset", get_pivotOffset, null);
		L.RegVar("width", get_width, set_width);
		L.RegVar("height", get_height, set_height);
		L.RegVar("color", get_color, set_color);
		L.RegVar("alpha", get_alpha, set_alpha);
		L.RegVar("isVisible", get_isVisible, null);
		L.RegVar("hasVertices", get_hasVertices, null);
		L.RegVar("rawPivot", get_rawPivot, set_rawPivot);
		L.RegVar("pivot", get_pivot, set_pivot);
		L.RegVar("depth", get_depth, set_depth);
		L.RegVar("raycastDepth", get_raycastDepth, null);
		L.RegVar("localCorners", get_localCorners, null);
		L.RegVar("localSize", get_localSize, null);
		L.RegVar("localCenter", get_localCenter, null);
		L.RegVar("worldCorners", get_worldCorners, null);
		L.RegVar("worldCenter", get_worldCenter, null);
		L.RegVar("drawingDimensions", get_drawingDimensions, null);
		L.RegVar("material", get_material, set_material);
		L.RegVar("mainTexture", get_mainTexture, set_mainTexture);
		L.RegVar("shader", get_shader, set_shader);
		L.RegVar("hasBoxCollider", get_hasBoxCollider, null);
		L.RegVar("minWidth", get_minWidth, null);
		L.RegVar("minHeight", get_minHeight, null);
		L.RegVar("border", get_border, set_border);
		L.RegFunction("HitCheck", UIWidget_HitCheck);
		L.RegFunction("OnPostFillCallback", UIWidget_OnPostFillCallback);
		L.RegFunction("OnDimensionsChanged", UIWidget_OnDimensionsChanged);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIWidget(IntPtr L)
	{
		return LuaDLL.tolua_error(L, "UIWidget class does not have a constructor function");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetDimensions(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.SetDimensions(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSides(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
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
	static int CalculateFinalAlpha(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			float o = obj.CalculateFinalAlpha(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Invalidate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Invalidate(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateCumulativeAlpha(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			float o = obj.CalculateCumulativeAlpha(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 5);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
			float arg3 = (float)LuaDLL.luaL_checknumber(L, 5);
			obj.SetRect(arg0, arg1, arg2, arg3);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResizeCollider(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.ResizeCollider();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FullCompareFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget arg0 = (UIWidget)ToLua.CheckUnityObject(L, 1, typeof(UIWidget));
			UIWidget arg1 = (UIWidget)ToLua.CheckUnityObject(L, 2, typeof(UIWidget));
			int o = UIWidget.FullCompareFunc(arg0, arg1);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PanelCompareFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget arg0 = (UIWidget)ToLua.CheckUnityObject(L, 1, typeof(UIWidget));
			UIWidget arg1 = (UIWidget)ToLua.CheckUnityObject(L, 2, typeof(UIWidget));
			int o = UIWidget.PanelCompareFunc(arg0, arg1);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateBounds(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && ToLua.CheckTypes(L, 1, typeof(UIWidget)))
			{
				UIWidget obj = (UIWidget)ToLua.ToObject(L, 1);
				UnityEngine.Bounds o = obj.CalculateBounds();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && ToLua.CheckTypes(L, 1, typeof(UIWidget), typeof(UnityEngine.Transform)))
			{
				UIWidget obj = (UIWidget)ToLua.ToObject(L, 1);
				UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.ToObject(L, 2);
				UnityEngine.Bounds o = obj.CalculateBounds(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.tolua_error(L, "invalid arguments to method: UIWidget.CalculateBounds");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetDirty(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.SetDirty();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveFromPanel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.RemoveFromPanel();
			return 0;
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
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.MarkAsChanged();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePanel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			UIPanel o = obj.CreatePanel();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckLayer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.CheckLayer();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParentHasChanged(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.ParentHasChanged();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateVisibility(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
			bool o = obj.UpdateVisibility(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateTransform(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool o = obj.UpdateTransform(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateGeometry(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool o = obj.UpdateGeometry(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteToBuffers(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 6);
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			BetterList<UnityEngine.Vector3> arg0 = (BetterList<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(BetterList<UnityEngine.Vector3>));
			BetterList<UnityEngine.Vector2> arg1 = (BetterList<UnityEngine.Vector2>)ToLua.CheckObject(L, 3, typeof(BetterList<UnityEngine.Vector2>));
			BetterList<UnityEngine.Color32> arg2 = (BetterList<UnityEngine.Color32>)ToLua.CheckObject(L, 4, typeof(BetterList<UnityEngine.Color32>));
			BetterList<UnityEngine.Vector3> arg3 = (BetterList<UnityEngine.Vector3>)ToLua.CheckObject(L, 5, typeof(BetterList<UnityEngine.Vector3>));
			BetterList<UnityEngine.Vector4> arg4 = (BetterList<UnityEngine.Vector4>)ToLua.CheckObject(L, 6, typeof(BetterList<UnityEngine.Vector4>));
			obj.WriteToBuffers(arg0, arg1, arg2, arg3, arg4);
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
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
			obj.MakePixelPerfect();
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
			UIWidget obj = (UIWidget)ToLua.CheckObject(L, 1, typeof(UIWidget));
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
	static int get_onChange(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.OnDimensionsChanged ret = obj.onChange;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onChange on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPostFill(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.OnPostFillCallback ret = obj.onPostFill;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onPostFill on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mOnRender(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall.OnRenderCallback ret = obj.mOnRender;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mOnRender on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoResizeBoxCollider(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.autoResizeBoxCollider;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index autoResizeBoxCollider on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hideIfOffScreen(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.hideIfOffScreen;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hideIfOffScreen on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_keepAspectRatio(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.AspectRatioSource ret = obj.keepAspectRatio;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index keepAspectRatio on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_aspectRatio(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			float ret = obj.aspectRatio;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index aspectRatio on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hitCheck(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.HitCheck ret = obj.hitCheck;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hitCheck on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_panel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIPanel ret = obj.panel;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index panel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_geometry(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIGeometry ret = obj.geometry;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index geometry on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillGeometry(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.fillGeometry;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fillGeometry on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_drawCall(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall ret = obj.drawCall;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index drawCall on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRender(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall.OnRenderCallback ret = obj.onRender;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onRender on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_drawRegion(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector4 ret = obj.drawRegion;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index drawRegion on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pivotOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector2 ret = obj.pivotOffset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pivotOffset on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_width(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.width;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index width on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.height;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index height on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_color(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Color ret = obj.color;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index color on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alpha(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			float ret = obj.alpha;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alpha on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isVisible(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.isVisible;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isVisible on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasVertices(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.hasVertices;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hasVertices on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rawPivot(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.Pivot ret = obj.rawPivot;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rawPivot on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pivot(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.Pivot ret = obj.pivot;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pivot on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.depth;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index depth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_raycastDepth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.raycastDepth;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index raycastDepth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localCorners(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int get_localSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int get_localCenter(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector3 ret = obj.localCenter;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index localCenter on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldCorners(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int get_worldCenter(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector3 ret = obj.worldCenter;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index worldCenter on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_drawingDimensions(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int get_material(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int get_mainTexture(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Texture ret = obj.mainTexture;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mainTexture on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shader(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Shader ret = obj.shader;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shader on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasBoxCollider(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool ret = obj.hasBoxCollider;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hasBoxCollider on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minWidth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.minWidth;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minWidth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minHeight(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int ret = obj.minHeight;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index minHeight on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_border(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector4 ret = obj.border;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index border on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onChange(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.OnDimensionsChanged arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIWidget.OnDimensionsChanged)ToLua.CheckObject(L, 2, typeof(UIWidget.OnDimensionsChanged));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIWidget.OnDimensionsChanged), func) as UIWidget.OnDimensionsChanged;
			}

			obj.onChange = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onChange on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPostFill(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.OnPostFillCallback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIWidget.OnPostFillCallback)ToLua.CheckObject(L, 2, typeof(UIWidget.OnPostFillCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIWidget.OnPostFillCallback), func) as UIWidget.OnPostFillCallback;
			}

			obj.onPostFill = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onPostFill on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mOnRender(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall.OnRenderCallback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIDrawCall.OnRenderCallback)ToLua.CheckObject(L, 2, typeof(UIDrawCall.OnRenderCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIDrawCall.OnRenderCallback), func) as UIDrawCall.OnRenderCallback;
			}

			obj.mOnRender = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mOnRender on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoResizeBoxCollider(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.autoResizeBoxCollider = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index autoResizeBoxCollider on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hideIfOffScreen(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.hideIfOffScreen = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hideIfOffScreen on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_keepAspectRatio(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.AspectRatioSource arg0 = (UIWidget.AspectRatioSource)ToLua.CheckObject(L, 2, typeof(UIWidget.AspectRatioSource));
			obj.keepAspectRatio = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index keepAspectRatio on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_aspectRatio(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.aspectRatio = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index aspectRatio on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hitCheck(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.HitCheck arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIWidget.HitCheck)ToLua.CheckObject(L, 2, typeof(UIWidget.HitCheck));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIWidget.HitCheck), func) as UIWidget.HitCheck;
			}

			obj.hitCheck = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index hitCheck on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_panel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIPanel arg0 = (UIPanel)ToLua.CheckUnityObject(L, 2, typeof(UIPanel));
			obj.panel = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index panel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_geometry(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIGeometry arg0 = (UIGeometry)ToLua.CheckObject(L, 2, typeof(UIGeometry));
			obj.geometry = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index geometry on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillGeometry(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.fillGeometry = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index fillGeometry on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_drawCall(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall arg0 = (UIDrawCall)ToLua.CheckUnityObject(L, 2, typeof(UIDrawCall));
			obj.drawCall = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index drawCall on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRender(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIDrawCall.OnRenderCallback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (UIDrawCall.OnRenderCallback)ToLua.CheckObject(L, 2, typeof(UIDrawCall.OnRenderCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(UIDrawCall.OnRenderCallback), func) as UIDrawCall.OnRenderCallback;
			}

			obj.onRender = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index onRender on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_drawRegion(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector4 arg0 = ToLua.ToVector4(L, 2);
			obj.drawRegion = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index drawRegion on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_width(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.width = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index width on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_height(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.height = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index height on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_color(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.color = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index color on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alpha(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.alpha = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index alpha on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rawPivot(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.Pivot arg0 = (UIWidget.Pivot)ToLua.CheckObject(L, 2, typeof(UIWidget.Pivot));
			obj.rawPivot = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index rawPivot on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pivot(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UIWidget.Pivot arg0 = (UIWidget.Pivot)ToLua.CheckObject(L, 2, typeof(UIWidget.Pivot));
			obj.pivot = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index pivot on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_depth(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.depth = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index depth on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_material(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
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
	static int set_mainTexture(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Texture));
			obj.mainTexture = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index mainTexture on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shader(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Shader arg0 = (UnityEngine.Shader)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.Shader));
			obj.shader = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index shader on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_border(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UIWidget obj = (UIWidget)o;
			UnityEngine.Vector4 arg0 = ToLua.ToVector4(L, 2);
			obj.border = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index border on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UIWidget_HitCheck(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UIWidget.HitCheck), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UIWidget_OnPostFillCallback(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UIWidget.OnPostFillCallback), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UIWidget_OnDimensionsChanged(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UIWidget.OnDimensionsChanged), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

