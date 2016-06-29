using System;
using LuaInterface;

public class UnityEngine_KeyCodeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(UnityEngine.KeyCode));
		L.RegVar("None", get_None, null);
		L.RegVar("Backspace", get_Backspace, null);
		L.RegVar("Delete", get_Delete, null);
		L.RegVar("Tab", get_Tab, null);
		L.RegVar("Clear", get_Clear, null);
		L.RegVar("Return", get_Return, null);
		L.RegVar("Pause", get_Pause, null);
		L.RegVar("Escape", get_Escape, null);
		L.RegVar("Space", get_Space, null);
		L.RegVar("Keypad0", get_Keypad0, null);
		L.RegVar("Keypad1", get_Keypad1, null);
		L.RegVar("Keypad2", get_Keypad2, null);
		L.RegVar("Keypad3", get_Keypad3, null);
		L.RegVar("Keypad4", get_Keypad4, null);
		L.RegVar("Keypad5", get_Keypad5, null);
		L.RegVar("Keypad6", get_Keypad6, null);
		L.RegVar("Keypad7", get_Keypad7, null);
		L.RegVar("Keypad8", get_Keypad8, null);
		L.RegVar("Keypad9", get_Keypad9, null);
		L.RegVar("KeypadPeriod", get_KeypadPeriod, null);
		L.RegVar("KeypadDivide", get_KeypadDivide, null);
		L.RegVar("KeypadMultiply", get_KeypadMultiply, null);
		L.RegVar("KeypadMinus", get_KeypadMinus, null);
		L.RegVar("KeypadPlus", get_KeypadPlus, null);
		L.RegVar("KeypadEnter", get_KeypadEnter, null);
		L.RegVar("KeypadEquals", get_KeypadEquals, null);
		L.RegVar("UpArrow", get_UpArrow, null);
		L.RegVar("DownArrow", get_DownArrow, null);
		L.RegVar("RightArrow", get_RightArrow, null);
		L.RegVar("LeftArrow", get_LeftArrow, null);
		L.RegVar("Insert", get_Insert, null);
		L.RegVar("Home", get_Home, null);
		L.RegVar("End", get_End, null);
		L.RegVar("PageUp", get_PageUp, null);
		L.RegVar("PageDown", get_PageDown, null);
		L.RegVar("F1", get_F1, null);
		L.RegVar("F2", get_F2, null);
		L.RegVar("F3", get_F3, null);
		L.RegVar("F4", get_F4, null);
		L.RegVar("F5", get_F5, null);
		L.RegVar("F6", get_F6, null);
		L.RegVar("F7", get_F7, null);
		L.RegVar("F8", get_F8, null);
		L.RegVar("F9", get_F9, null);
		L.RegVar("F10", get_F10, null);
		L.RegVar("F11", get_F11, null);
		L.RegVar("F12", get_F12, null);
		L.RegVar("F13", get_F13, null);
		L.RegVar("F14", get_F14, null);
		L.RegVar("F15", get_F15, null);
		L.RegVar("Alpha0", get_Alpha0, null);
		L.RegVar("Alpha1", get_Alpha1, null);
		L.RegVar("Alpha2", get_Alpha2, null);
		L.RegVar("Alpha3", get_Alpha3, null);
		L.RegVar("Alpha4", get_Alpha4, null);
		L.RegVar("Alpha5", get_Alpha5, null);
		L.RegVar("Alpha6", get_Alpha6, null);
		L.RegVar("Alpha7", get_Alpha7, null);
		L.RegVar("Alpha8", get_Alpha8, null);
		L.RegVar("Alpha9", get_Alpha9, null);
		L.RegVar("Exclaim", get_Exclaim, null);
		L.RegVar("DoubleQuote", get_DoubleQuote, null);
		L.RegVar("Hash", get_Hash, null);
		L.RegVar("Dollar", get_Dollar, null);
		L.RegVar("Ampersand", get_Ampersand, null);
		L.RegVar("Quote", get_Quote, null);
		L.RegVar("LeftParen", get_LeftParen, null);
		L.RegVar("RightParen", get_RightParen, null);
		L.RegVar("Asterisk", get_Asterisk, null);
		L.RegVar("Plus", get_Plus, null);
		L.RegVar("Comma", get_Comma, null);
		L.RegVar("Minus", get_Minus, null);
		L.RegVar("Period", get_Period, null);
		L.RegVar("Slash", get_Slash, null);
		L.RegVar("Colon", get_Colon, null);
		L.RegVar("Semicolon", get_Semicolon, null);
		L.RegVar("Less", get_Less, null);
		L.RegVar("Equals", get_Equals, null);
		L.RegVar("Greater", get_Greater, null);
		L.RegVar("Question", get_Question, null);
		L.RegVar("At", get_At, null);
		L.RegVar("LeftBracket", get_LeftBracket, null);
		L.RegVar("Backslash", get_Backslash, null);
		L.RegVar("RightBracket", get_RightBracket, null);
		L.RegVar("Caret", get_Caret, null);
		L.RegVar("Underscore", get_Underscore, null);
		L.RegVar("BackQuote", get_BackQuote, null);
		L.RegVar("A", get_A, null);
		L.RegVar("B", get_B, null);
		L.RegVar("C", get_C, null);
		L.RegVar("D", get_D, null);
		L.RegVar("E", get_E, null);
		L.RegVar("F", get_F, null);
		L.RegVar("G", get_G, null);
		L.RegVar("H", get_H, null);
		L.RegVar("I", get_I, null);
		L.RegVar("J", get_J, null);
		L.RegVar("K", get_K, null);
		L.RegVar("L", get_L, null);
		L.RegVar("M", get_M, null);
		L.RegVar("N", get_N, null);
		L.RegVar("O", get_O, null);
		L.RegVar("P", get_P, null);
		L.RegVar("Q", get_Q, null);
		L.RegVar("R", get_R, null);
		L.RegVar("S", get_S, null);
		L.RegVar("T", get_T, null);
		L.RegVar("U", get_U, null);
		L.RegVar("V", get_V, null);
		L.RegVar("W", get_W, null);
		L.RegVar("X", get_X, null);
		L.RegVar("Y", get_Y, null);
		L.RegVar("Z", get_Z, null);
		L.RegVar("Numlock", get_Numlock, null);
		L.RegVar("CapsLock", get_CapsLock, null);
		L.RegVar("ScrollLock", get_ScrollLock, null);
		L.RegVar("RightShift", get_RightShift, null);
		L.RegVar("LeftShift", get_LeftShift, null);
		L.RegVar("RightControl", get_RightControl, null);
		L.RegVar("LeftControl", get_LeftControl, null);
		L.RegVar("RightAlt", get_RightAlt, null);
		L.RegVar("LeftAlt", get_LeftAlt, null);
		L.RegVar("LeftCommand", get_LeftCommand, null);
		L.RegVar("LeftApple", get_LeftApple, null);
		L.RegVar("LeftWindows", get_LeftWindows, null);
		L.RegVar("RightCommand", get_RightCommand, null);
		L.RegVar("RightApple", get_RightApple, null);
		L.RegVar("RightWindows", get_RightWindows, null);
		L.RegVar("AltGr", get_AltGr, null);
		L.RegVar("Help", get_Help, null);
		L.RegVar("Print", get_Print, null);
		L.RegVar("SysReq", get_SysReq, null);
		L.RegVar("Break", get_Break, null);
		L.RegVar("Menu", get_Menu, null);
		L.RegVar("Mouse0", get_Mouse0, null);
		L.RegVar("Mouse1", get_Mouse1, null);
		L.RegVar("Mouse2", get_Mouse2, null);
		L.RegVar("Mouse3", get_Mouse3, null);
		L.RegVar("Mouse4", get_Mouse4, null);
		L.RegVar("Mouse5", get_Mouse5, null);
		L.RegVar("Mouse6", get_Mouse6, null);
		L.RegVar("JoystickButton0", get_JoystickButton0, null);
		L.RegVar("JoystickButton1", get_JoystickButton1, null);
		L.RegVar("JoystickButton2", get_JoystickButton2, null);
		L.RegVar("JoystickButton3", get_JoystickButton3, null);
		L.RegVar("JoystickButton4", get_JoystickButton4, null);
		L.RegVar("JoystickButton5", get_JoystickButton5, null);
		L.RegVar("JoystickButton6", get_JoystickButton6, null);
		L.RegVar("JoystickButton7", get_JoystickButton7, null);
		L.RegVar("JoystickButton8", get_JoystickButton8, null);
		L.RegVar("JoystickButton9", get_JoystickButton9, null);
		L.RegVar("JoystickButton10", get_JoystickButton10, null);
		L.RegVar("JoystickButton11", get_JoystickButton11, null);
		L.RegVar("JoystickButton12", get_JoystickButton12, null);
		L.RegVar("JoystickButton13", get_JoystickButton13, null);
		L.RegVar("JoystickButton14", get_JoystickButton14, null);
		L.RegVar("JoystickButton15", get_JoystickButton15, null);
		L.RegVar("JoystickButton16", get_JoystickButton16, null);
		L.RegVar("JoystickButton17", get_JoystickButton17, null);
		L.RegVar("JoystickButton18", get_JoystickButton18, null);
		L.RegVar("JoystickButton19", get_JoystickButton19, null);
		L.RegVar("Joystick1Button0", get_Joystick1Button0, null);
		L.RegVar("Joystick1Button1", get_Joystick1Button1, null);
		L.RegVar("Joystick1Button2", get_Joystick1Button2, null);
		L.RegVar("Joystick1Button3", get_Joystick1Button3, null);
		L.RegVar("Joystick1Button4", get_Joystick1Button4, null);
		L.RegVar("Joystick1Button5", get_Joystick1Button5, null);
		L.RegVar("Joystick1Button6", get_Joystick1Button6, null);
		L.RegVar("Joystick1Button7", get_Joystick1Button7, null);
		L.RegVar("Joystick1Button8", get_Joystick1Button8, null);
		L.RegVar("Joystick1Button9", get_Joystick1Button9, null);
		L.RegVar("Joystick1Button10", get_Joystick1Button10, null);
		L.RegVar("Joystick1Button11", get_Joystick1Button11, null);
		L.RegVar("Joystick1Button12", get_Joystick1Button12, null);
		L.RegVar("Joystick1Button13", get_Joystick1Button13, null);
		L.RegVar("Joystick1Button14", get_Joystick1Button14, null);
		L.RegVar("Joystick1Button15", get_Joystick1Button15, null);
		L.RegVar("Joystick1Button16", get_Joystick1Button16, null);
		L.RegVar("Joystick1Button17", get_Joystick1Button17, null);
		L.RegVar("Joystick1Button18", get_Joystick1Button18, null);
		L.RegVar("Joystick1Button19", get_Joystick1Button19, null);
		L.RegVar("Joystick2Button0", get_Joystick2Button0, null);
		L.RegVar("Joystick2Button1", get_Joystick2Button1, null);
		L.RegVar("Joystick2Button2", get_Joystick2Button2, null);
		L.RegVar("Joystick2Button3", get_Joystick2Button3, null);
		L.RegVar("Joystick2Button4", get_Joystick2Button4, null);
		L.RegVar("Joystick2Button5", get_Joystick2Button5, null);
		L.RegVar("Joystick2Button6", get_Joystick2Button6, null);
		L.RegVar("Joystick2Button7", get_Joystick2Button7, null);
		L.RegVar("Joystick2Button8", get_Joystick2Button8, null);
		L.RegVar("Joystick2Button9", get_Joystick2Button9, null);
		L.RegVar("Joystick2Button10", get_Joystick2Button10, null);
		L.RegVar("Joystick2Button11", get_Joystick2Button11, null);
		L.RegVar("Joystick2Button12", get_Joystick2Button12, null);
		L.RegVar("Joystick2Button13", get_Joystick2Button13, null);
		L.RegVar("Joystick2Button14", get_Joystick2Button14, null);
		L.RegVar("Joystick2Button15", get_Joystick2Button15, null);
		L.RegVar("Joystick2Button16", get_Joystick2Button16, null);
		L.RegVar("Joystick2Button17", get_Joystick2Button17, null);
		L.RegVar("Joystick2Button18", get_Joystick2Button18, null);
		L.RegVar("Joystick2Button19", get_Joystick2Button19, null);
		L.RegVar("Joystick3Button0", get_Joystick3Button0, null);
		L.RegVar("Joystick3Button1", get_Joystick3Button1, null);
		L.RegVar("Joystick3Button2", get_Joystick3Button2, null);
		L.RegVar("Joystick3Button3", get_Joystick3Button3, null);
		L.RegVar("Joystick3Button4", get_Joystick3Button4, null);
		L.RegVar("Joystick3Button5", get_Joystick3Button5, null);
		L.RegVar("Joystick3Button6", get_Joystick3Button6, null);
		L.RegVar("Joystick3Button7", get_Joystick3Button7, null);
		L.RegVar("Joystick3Button8", get_Joystick3Button8, null);
		L.RegVar("Joystick3Button9", get_Joystick3Button9, null);
		L.RegVar("Joystick3Button10", get_Joystick3Button10, null);
		L.RegVar("Joystick3Button11", get_Joystick3Button11, null);
		L.RegVar("Joystick3Button12", get_Joystick3Button12, null);
		L.RegVar("Joystick3Button13", get_Joystick3Button13, null);
		L.RegVar("Joystick3Button14", get_Joystick3Button14, null);
		L.RegVar("Joystick3Button15", get_Joystick3Button15, null);
		L.RegVar("Joystick3Button16", get_Joystick3Button16, null);
		L.RegVar("Joystick3Button17", get_Joystick3Button17, null);
		L.RegVar("Joystick3Button18", get_Joystick3Button18, null);
		L.RegVar("Joystick3Button19", get_Joystick3Button19, null);
		L.RegVar("Joystick4Button0", get_Joystick4Button0, null);
		L.RegVar("Joystick4Button1", get_Joystick4Button1, null);
		L.RegVar("Joystick4Button2", get_Joystick4Button2, null);
		L.RegVar("Joystick4Button3", get_Joystick4Button3, null);
		L.RegVar("Joystick4Button4", get_Joystick4Button4, null);
		L.RegVar("Joystick4Button5", get_Joystick4Button5, null);
		L.RegVar("Joystick4Button6", get_Joystick4Button6, null);
		L.RegVar("Joystick4Button7", get_Joystick4Button7, null);
		L.RegVar("Joystick4Button8", get_Joystick4Button8, null);
		L.RegVar("Joystick4Button9", get_Joystick4Button9, null);
		L.RegVar("Joystick4Button10", get_Joystick4Button10, null);
		L.RegVar("Joystick4Button11", get_Joystick4Button11, null);
		L.RegVar("Joystick4Button12", get_Joystick4Button12, null);
		L.RegVar("Joystick4Button13", get_Joystick4Button13, null);
		L.RegVar("Joystick4Button14", get_Joystick4Button14, null);
		L.RegVar("Joystick4Button15", get_Joystick4Button15, null);
		L.RegVar("Joystick4Button16", get_Joystick4Button16, null);
		L.RegVar("Joystick4Button17", get_Joystick4Button17, null);
		L.RegVar("Joystick4Button18", get_Joystick4Button18, null);
		L.RegVar("Joystick4Button19", get_Joystick4Button19, null);
		L.RegVar("Joystick5Button0", get_Joystick5Button0, null);
		L.RegVar("Joystick5Button1", get_Joystick5Button1, null);
		L.RegVar("Joystick5Button2", get_Joystick5Button2, null);
		L.RegVar("Joystick5Button3", get_Joystick5Button3, null);
		L.RegVar("Joystick5Button4", get_Joystick5Button4, null);
		L.RegVar("Joystick5Button5", get_Joystick5Button5, null);
		L.RegVar("Joystick5Button6", get_Joystick5Button6, null);
		L.RegVar("Joystick5Button7", get_Joystick5Button7, null);
		L.RegVar("Joystick5Button8", get_Joystick5Button8, null);
		L.RegVar("Joystick5Button9", get_Joystick5Button9, null);
		L.RegVar("Joystick5Button10", get_Joystick5Button10, null);
		L.RegVar("Joystick5Button11", get_Joystick5Button11, null);
		L.RegVar("Joystick5Button12", get_Joystick5Button12, null);
		L.RegVar("Joystick5Button13", get_Joystick5Button13, null);
		L.RegVar("Joystick5Button14", get_Joystick5Button14, null);
		L.RegVar("Joystick5Button15", get_Joystick5Button15, null);
		L.RegVar("Joystick5Button16", get_Joystick5Button16, null);
		L.RegVar("Joystick5Button17", get_Joystick5Button17, null);
		L.RegVar("Joystick5Button18", get_Joystick5Button18, null);
		L.RegVar("Joystick5Button19", get_Joystick5Button19, null);
		L.RegVar("Joystick6Button0", get_Joystick6Button0, null);
		L.RegVar("Joystick6Button1", get_Joystick6Button1, null);
		L.RegVar("Joystick6Button2", get_Joystick6Button2, null);
		L.RegVar("Joystick6Button3", get_Joystick6Button3, null);
		L.RegVar("Joystick6Button4", get_Joystick6Button4, null);
		L.RegVar("Joystick6Button5", get_Joystick6Button5, null);
		L.RegVar("Joystick6Button6", get_Joystick6Button6, null);
		L.RegVar("Joystick6Button7", get_Joystick6Button7, null);
		L.RegVar("Joystick6Button8", get_Joystick6Button8, null);
		L.RegVar("Joystick6Button9", get_Joystick6Button9, null);
		L.RegVar("Joystick6Button10", get_Joystick6Button10, null);
		L.RegVar("Joystick6Button11", get_Joystick6Button11, null);
		L.RegVar("Joystick6Button12", get_Joystick6Button12, null);
		L.RegVar("Joystick6Button13", get_Joystick6Button13, null);
		L.RegVar("Joystick6Button14", get_Joystick6Button14, null);
		L.RegVar("Joystick6Button15", get_Joystick6Button15, null);
		L.RegVar("Joystick6Button16", get_Joystick6Button16, null);
		L.RegVar("Joystick6Button17", get_Joystick6Button17, null);
		L.RegVar("Joystick6Button18", get_Joystick6Button18, null);
		L.RegVar("Joystick6Button19", get_Joystick6Button19, null);
		L.RegVar("Joystick7Button0", get_Joystick7Button0, null);
		L.RegVar("Joystick7Button1", get_Joystick7Button1, null);
		L.RegVar("Joystick7Button2", get_Joystick7Button2, null);
		L.RegVar("Joystick7Button3", get_Joystick7Button3, null);
		L.RegVar("Joystick7Button4", get_Joystick7Button4, null);
		L.RegVar("Joystick7Button5", get_Joystick7Button5, null);
		L.RegVar("Joystick7Button6", get_Joystick7Button6, null);
		L.RegVar("Joystick7Button7", get_Joystick7Button7, null);
		L.RegVar("Joystick7Button8", get_Joystick7Button8, null);
		L.RegVar("Joystick7Button9", get_Joystick7Button9, null);
		L.RegVar("Joystick7Button10", get_Joystick7Button10, null);
		L.RegVar("Joystick7Button11", get_Joystick7Button11, null);
		L.RegVar("Joystick7Button12", get_Joystick7Button12, null);
		L.RegVar("Joystick7Button13", get_Joystick7Button13, null);
		L.RegVar("Joystick7Button14", get_Joystick7Button14, null);
		L.RegVar("Joystick7Button15", get_Joystick7Button15, null);
		L.RegVar("Joystick7Button16", get_Joystick7Button16, null);
		L.RegVar("Joystick7Button17", get_Joystick7Button17, null);
		L.RegVar("Joystick7Button18", get_Joystick7Button18, null);
		L.RegVar("Joystick7Button19", get_Joystick7Button19, null);
		L.RegVar("Joystick8Button0", get_Joystick8Button0, null);
		L.RegVar("Joystick8Button1", get_Joystick8Button1, null);
		L.RegVar("Joystick8Button2", get_Joystick8Button2, null);
		L.RegVar("Joystick8Button3", get_Joystick8Button3, null);
		L.RegVar("Joystick8Button4", get_Joystick8Button4, null);
		L.RegVar("Joystick8Button5", get_Joystick8Button5, null);
		L.RegVar("Joystick8Button6", get_Joystick8Button6, null);
		L.RegVar("Joystick8Button7", get_Joystick8Button7, null);
		L.RegVar("Joystick8Button8", get_Joystick8Button8, null);
		L.RegVar("Joystick8Button9", get_Joystick8Button9, null);
		L.RegVar("Joystick8Button10", get_Joystick8Button10, null);
		L.RegVar("Joystick8Button11", get_Joystick8Button11, null);
		L.RegVar("Joystick8Button12", get_Joystick8Button12, null);
		L.RegVar("Joystick8Button13", get_Joystick8Button13, null);
		L.RegVar("Joystick8Button14", get_Joystick8Button14, null);
		L.RegVar("Joystick8Button15", get_Joystick8Button15, null);
		L.RegVar("Joystick8Button16", get_Joystick8Button16, null);
		L.RegVar("Joystick8Button17", get_Joystick8Button17, null);
		L.RegVar("Joystick8Button18", get_Joystick8Button18, null);
		L.RegVar("Joystick8Button19", get_Joystick8Button19, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_None(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Backspace(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Backspace);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Delete(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Delete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Tab(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Tab);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Clear(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Clear);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Return(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Return);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Pause(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Pause);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Escape(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Escape);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Space(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Space);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keypad9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Keypad9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadPeriod(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadPeriod);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadDivide(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadDivide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadMultiply(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadMultiply);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadMinus(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadMinus);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadPlus(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadPlus);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadEnter(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeypadEquals(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.KeypadEquals);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UpArrow(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.UpArrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DownArrow(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.DownArrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightArrow(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightArrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftArrow(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftArrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Insert(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Insert);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Home(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Home);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_End(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.End);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PageUp(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.PageUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PageDown(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.PageDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Alpha9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Alpha9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Exclaim(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Exclaim);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DoubleQuote(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.DoubleQuote);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Hash(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Hash);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Dollar(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Dollar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Ampersand(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Ampersand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Quote(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Quote);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftParen(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftParen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightParen(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightParen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Asterisk(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Asterisk);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Plus(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Plus);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Comma(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Comma);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Minus(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Minus);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Period(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Period);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Slash(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Slash);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Colon(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Colon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Semicolon(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Semicolon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Less(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Less);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Equals(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Equals);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Greater(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Greater);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Question(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Question);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_At(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.At);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftBracket(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftBracket);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Backslash(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Backslash);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightBracket(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightBracket);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Caret(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Caret);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Underscore(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Underscore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BackQuote(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.BackQuote);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_A(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.A);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_B(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.B);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_C(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.C);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_D(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.D);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_E(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.E);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_F(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.F);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_G(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.G);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_H(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.H);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_I(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.I);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_J(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.J);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_K(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.K);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_L(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.L);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_M(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.M);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_N(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.N);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_O(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.O);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_P(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.P);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Q(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Q);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_R(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.R);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_S(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.S);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_T(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.T);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_U(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.U);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_V(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.V);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_W(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.W);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_X(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.X);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Y(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Y);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Z(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Z);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Numlock(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Numlock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CapsLock(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.CapsLock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ScrollLock(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.ScrollLock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightShift(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightShift);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftShift(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftShift);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightControl(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightControl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftControl(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftControl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightAlt(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightAlt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftAlt(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftAlt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftCommand(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftCommand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftApple(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftApple);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LeftWindows(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.LeftWindows);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightCommand(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightCommand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightApple(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightApple);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RightWindows(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.RightWindows);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AltGr(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.AltGr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Help(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Help);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Print(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Print);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SysReq(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.SysReq);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Break(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Break);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Menu(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Menu);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mouse6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Mouse6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JoystickButton19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.JoystickButton19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick1Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick1Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick2Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick2Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick3Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick3Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick4Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick4Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick5Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick5Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick6Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick6Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick7Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick7Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button0(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button0);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button1(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button2(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button3(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button4(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button4);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button5(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button5);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button6(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button6);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button7(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button7);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button8(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button8);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button9(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button9);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button10(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button10);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button11(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button11);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button12(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button12);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button13(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button13);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button14(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button14);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button15(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button15);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button16(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button16);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button17(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button17);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button18(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button18);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Joystick8Button19(IntPtr L)
	{
		ToLua.Push(L, UnityEngine.KeyCode.Joystick8Button19);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UnityEngine.KeyCode o = (UnityEngine.KeyCode)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

