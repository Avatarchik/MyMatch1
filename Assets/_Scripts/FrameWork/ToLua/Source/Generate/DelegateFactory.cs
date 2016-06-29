using System;
using System.Collections.Generic;
using LuaInterface;

public static class DelegateFactory
{
	delegate Delegate DelegateValue(LuaFunction func);
	static Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();

	static DelegateFactory()
	{
		Register();
	}

	[NoToLuaAttribute]
	public static void Register()
	{
		dict.Clear();
		dict.Add(typeof(System.Action), System_Action);
		dict.Add(typeof(UnityEngine.Events.UnityAction), UnityEngine_Events_UnityAction);
		dict.Add(typeof(UnityEngine.Camera.CameraCallback), UnityEngine_Camera_CameraCallback);
		dict.Add(typeof(UnityEngine.AudioClip.PCMReaderCallback), UnityEngine_AudioClip_PCMReaderCallback);
		dict.Add(typeof(UnityEngine.AudioClip.PCMSetPositionCallback), UnityEngine_AudioClip_PCMSetPositionCallback);
		dict.Add(typeof(UnityEngine.Application.LogCallback), UnityEngine_Application_LogCallback);
		dict.Add(typeof(UnityEngine.Application.AdvertisingIdentifierCallback), UnityEngine_Application_AdvertisingIdentifierCallback);
		dict.Add(typeof(UIPanel.OnGeometryUpdated), UIPanel_OnGeometryUpdated);
		dict.Add(typeof(UIPanel.OnClippingMoved), UIPanel_OnClippingMoved);
		dict.Add(typeof(UIWidget.OnDimensionsChanged), UIWidget_OnDimensionsChanged);
		dict.Add(typeof(UIWidget.OnPostFillCallback), UIWidget_OnPostFillCallback);
		dict.Add(typeof(UIDrawCall.OnRenderCallback), UIDrawCall_OnRenderCallback);
		dict.Add(typeof(UIWidget.HitCheck), UIWidget_HitCheck);
		dict.Add(typeof(UIGrid.OnReposition), UIGrid_OnReposition);
		dict.Add(typeof(System.Comparison<UnityEngine.Transform>), System_Comparison_UnityEngine_Transform);
		dict.Add(typeof(SpringPanel.OnFinished), SpringPanel_OnFinished);
		dict.Add(typeof(UIScrollView.OnDragNotification), UIScrollView_OnDragNotification);
		dict.Add(typeof(UIProgressBar.OnDragFinished), UIProgressBar_OnDragFinished);
		dict.Add(typeof(UICenterOnChild.OnCenterCallback), UICenterOnChild_OnCenterCallback);
		dict.Add(typeof(UIInput.OnValidate), UIInput_OnValidate);
		dict.Add(typeof(EventDelegate.Callback), EventDelegate_Callback);
		dict.Add(typeof(System.Action<MyFrameWork.BaseUI>), System_Action_MyFrameWork_BaseUI);
		dict.Add(typeof(UIEventListener.VoidDelegate), UIEventListener_VoidDelegate);
		dict.Add(typeof(MyFrameWork.StateChangedEvent), MyFrameWork_StateChangedEvent);
		dict.Add(typeof(System.Action<NotiData>), System_Action_NotiData);
		dict.Add(typeof(System.Action<bool>), System_Action_bool);
		dict.Add(typeof(System.Action<object>), System_Action_object);
	}

    [NoToLuaAttribute]
    public static Delegate CreateDelegate(Type t, LuaFunction func = null)
    {
        DelegateValue create = null;

        if (!dict.TryGetValue(t, out create))
        {
            throw new LuaException(string.Format("Delegate {0} not register", LuaMisc.GetTypeName(t)));            
        }
        
        return create(func);        
    }

    [NoToLuaAttribute]
    public static Delegate RemoveDelegate(Delegate obj, LuaFunction func)
    {
        LuaState state = func.GetLuaState();
        Delegate[] ds = obj.GetInvocationList();

        for (int i = 0; i < ds.Length; i++)
        {
            LuaDelegate ld = ds[i].Target as LuaDelegate;

            if (ld != null && ld.func == func)
            {
                obj = Delegate.Remove(obj, ds[i]);
                state.DelayDispose(ld.func);
                break;
            }
        }

        return obj;
    }

	class System_Action_Event : LuaDelegate
	{
		public System_Action_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate System_Action(LuaFunction func)
	{
		if (func == null)
		{
			System.Action fn = delegate { };
			return fn;
		}

		System.Action d = (new System_Action_Event(func)).Call;
		return d;
	}

	class UnityEngine_Events_UnityAction_Event : LuaDelegate
	{
		public UnityEngine_Events_UnityAction_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.Events.UnityAction fn = delegate { };
			return fn;
		}

		UnityEngine.Events.UnityAction d = (new UnityEngine_Events_UnityAction_Event(func)).Call;
		return d;
	}

	class UnityEngine_Camera_CameraCallback_Event : LuaDelegate
	{
		public UnityEngine_Camera_CameraCallback_Event(LuaFunction func) : base(func) { }

		public void Call(UnityEngine.Camera param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UnityEngine_Camera_CameraCallback(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.Camera.CameraCallback fn = delegate { };
			return fn;
		}

		UnityEngine.Camera.CameraCallback d = (new UnityEngine_Camera_CameraCallback_Event(func)).Call;
		return d;
	}

	class UnityEngine_AudioClip_PCMReaderCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMReaderCallback_Event(LuaFunction func) : base(func) { }

		public void Call(float[] param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UnityEngine_AudioClip_PCMReaderCallback(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.AudioClip.PCMReaderCallback fn = delegate { };
			return fn;
		}

		UnityEngine.AudioClip.PCMReaderCallback d = (new UnityEngine_AudioClip_PCMReaderCallback_Event(func)).Call;
		return d;
	}

	class UnityEngine_AudioClip_PCMSetPositionCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMSetPositionCallback_Event(LuaFunction func) : base(func) { }

		public void Call(int param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UnityEngine_AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.AudioClip.PCMSetPositionCallback fn = delegate { };
			return fn;
		}

		UnityEngine.AudioClip.PCMSetPositionCallback d = (new UnityEngine_AudioClip_PCMSetPositionCallback_Event(func)).Call;
		return d;
	}

	class UnityEngine_Application_LogCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_LogCallback_Event(LuaFunction func) : base(func) { }

		public void Call(string param0,string param1,UnityEngine.LogType param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UnityEngine_Application_LogCallback(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.Application.LogCallback fn = delegate { };
			return fn;
		}

		UnityEngine.Application.LogCallback d = (new UnityEngine_Application_LogCallback_Event(func)).Call;
		return d;
	}

	class UnityEngine_Application_AdvertisingIdentifierCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_AdvertisingIdentifierCallback_Event(LuaFunction func) : base(func) { }

		public void Call(string param0,bool param1,string param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UnityEngine_Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		if (func == null)
		{
			UnityEngine.Application.AdvertisingIdentifierCallback fn = delegate { };
			return fn;
		}

		UnityEngine.Application.AdvertisingIdentifierCallback d = (new UnityEngine_Application_AdvertisingIdentifierCallback_Event(func)).Call;
		return d;
	}

	class UIPanel_OnGeometryUpdated_Event : LuaDelegate
	{
		public UIPanel_OnGeometryUpdated_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UIPanel_OnGeometryUpdated(LuaFunction func)
	{
		if (func == null)
		{
			UIPanel.OnGeometryUpdated fn = delegate { };
			return fn;
		}

		UIPanel.OnGeometryUpdated d = (new UIPanel_OnGeometryUpdated_Event(func)).Call;
		return d;
	}

	class UIPanel_OnClippingMoved_Event : LuaDelegate
	{
		public UIPanel_OnClippingMoved_Event(LuaFunction func) : base(func) { }

		public void Call(UIPanel param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UIPanel_OnClippingMoved(LuaFunction func)
	{
		if (func == null)
		{
			UIPanel.OnClippingMoved fn = delegate { };
			return fn;
		}

		UIPanel.OnClippingMoved d = (new UIPanel_OnClippingMoved_Event(func)).Call;
		return d;
	}

	class UIWidget_OnDimensionsChanged_Event : LuaDelegate
	{
		public UIWidget_OnDimensionsChanged_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UIWidget_OnDimensionsChanged(LuaFunction func)
	{
		if (func == null)
		{
			UIWidget.OnDimensionsChanged fn = delegate { };
			return fn;
		}

		UIWidget.OnDimensionsChanged d = (new UIWidget_OnDimensionsChanged_Event(func)).Call;
		return d;
	}

	class UIWidget_OnPostFillCallback_Event : LuaDelegate
	{
		public UIWidget_OnPostFillCallback_Event(LuaFunction func) : base(func) { }

		public void Call(UIWidget param0,int param1,BetterList<UnityEngine.Vector3> param2,BetterList<UnityEngine.Vector2> param3,BetterList<UnityEngine.Color32> param4)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.PushObject(param2);
			func.PushObject(param3);
			func.PushObject(param4);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UIWidget_OnPostFillCallback(LuaFunction func)
	{
		if (func == null)
		{
			UIWidget.OnPostFillCallback fn = delegate { };
			return fn;
		}

		UIWidget.OnPostFillCallback d = (new UIWidget_OnPostFillCallback_Event(func)).Call;
		return d;
	}

	class UIDrawCall_OnRenderCallback_Event : LuaDelegate
	{
		public UIDrawCall_OnRenderCallback_Event(LuaFunction func) : base(func) { }

		public void Call(UnityEngine.Material param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UIDrawCall_OnRenderCallback(LuaFunction func)
	{
		if (func == null)
		{
			UIDrawCall.OnRenderCallback fn = delegate { };
			return fn;
		}

		UIDrawCall.OnRenderCallback d = (new UIDrawCall_OnRenderCallback_Event(func)).Call;
		return d;
	}

	class UIWidget_HitCheck_Event : LuaDelegate
	{
		public UIWidget_HitCheck_Event(LuaFunction func) : base(func) { }

		public bool Call(UnityEngine.Vector3 param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			bool ret = func.CheckBoolean();
			func.EndPCall();
			return ret;
		}
	}

	public static Delegate UIWidget_HitCheck(LuaFunction func)
	{
		if (func == null)
		{
			UIWidget.HitCheck fn = delegate { return false; };
			return fn;
		}

		UIWidget.HitCheck d = (new UIWidget_HitCheck_Event(func)).Call;
		return d;
	}

	class UIGrid_OnReposition_Event : LuaDelegate
	{
		public UIGrid_OnReposition_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UIGrid_OnReposition(LuaFunction func)
	{
		if (func == null)
		{
			UIGrid.OnReposition fn = delegate { };
			return fn;
		}

		UIGrid.OnReposition d = (new UIGrid_OnReposition_Event(func)).Call;
		return d;
	}

	class System_Comparison_UnityEngine_Transform_Event : LuaDelegate
	{
		public System_Comparison_UnityEngine_Transform_Event(LuaFunction func) : base(func) { }

		public int Call(UnityEngine.Transform param0,UnityEngine.Transform param1)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			int ret = (int)func.CheckNumber();
			func.EndPCall();
			return ret;
		}
	}

	public static Delegate System_Comparison_UnityEngine_Transform(LuaFunction func)
	{
		if (func == null)
		{
			System.Comparison<UnityEngine.Transform> fn = delegate { return 0; };
			return fn;
		}

		System.Comparison<UnityEngine.Transform> d = (new System_Comparison_UnityEngine_Transform_Event(func)).Call;
		return d;
	}

	class SpringPanel_OnFinished_Event : LuaDelegate
	{
		public SpringPanel_OnFinished_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate SpringPanel_OnFinished(LuaFunction func)
	{
		if (func == null)
		{
			SpringPanel.OnFinished fn = delegate { };
			return fn;
		}

		SpringPanel.OnFinished d = (new SpringPanel_OnFinished_Event(func)).Call;
		return d;
	}

	class UIScrollView_OnDragNotification_Event : LuaDelegate
	{
		public UIScrollView_OnDragNotification_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UIScrollView_OnDragNotification(LuaFunction func)
	{
		if (func == null)
		{
			UIScrollView.OnDragNotification fn = delegate { };
			return fn;
		}

		UIScrollView.OnDragNotification d = (new UIScrollView_OnDragNotification_Event(func)).Call;
		return d;
	}

	class UIProgressBar_OnDragFinished_Event : LuaDelegate
	{
		public UIProgressBar_OnDragFinished_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate UIProgressBar_OnDragFinished(LuaFunction func)
	{
		if (func == null)
		{
			UIProgressBar.OnDragFinished fn = delegate { };
			return fn;
		}

		UIProgressBar.OnDragFinished d = (new UIProgressBar_OnDragFinished_Event(func)).Call;
		return d;
	}

	class UICenterOnChild_OnCenterCallback_Event : LuaDelegate
	{
		public UICenterOnChild_OnCenterCallback_Event(LuaFunction func) : base(func) { }

		public void Call(UnityEngine.GameObject param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UICenterOnChild_OnCenterCallback(LuaFunction func)
	{
		if (func == null)
		{
			UICenterOnChild.OnCenterCallback fn = delegate { };
			return fn;
		}

		UICenterOnChild.OnCenterCallback d = (new UICenterOnChild_OnCenterCallback_Event(func)).Call;
		return d;
	}

	class UIInput_OnValidate_Event : LuaDelegate
	{
		public UIInput_OnValidate_Event(LuaFunction func) : base(func) { }

		public char Call(string param0,int param1,char param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			char ret = (char)func.CheckNumber();
			func.EndPCall();
			return ret;
		}
	}

	public static Delegate UIInput_OnValidate(LuaFunction func)
	{
		if (func == null)
		{
			UIInput.OnValidate fn = delegate { return '\0'; };
			return fn;
		}

		UIInput.OnValidate d = (new UIInput_OnValidate_Event(func)).Call;
		return d;
	}

	class EventDelegate_Callback_Event : LuaDelegate
	{
		public EventDelegate_Callback_Event(LuaFunction func) : base(func) { }

		public void Call()
		{
			func.Call();
		}
	}

	public static Delegate EventDelegate_Callback(LuaFunction func)
	{
		if (func == null)
		{
			EventDelegate.Callback fn = delegate { };
			return fn;
		}

		EventDelegate.Callback d = (new EventDelegate_Callback_Event(func)).Call;
		return d;
	}

	class System_Action_MyFrameWork_BaseUI_Event : LuaDelegate
	{
		public System_Action_MyFrameWork_BaseUI_Event(LuaFunction func) : base(func) { }

		public void Call(MyFrameWork.BaseUI param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate System_Action_MyFrameWork_BaseUI(LuaFunction func)
	{
		if (func == null)
		{
			System.Action<MyFrameWork.BaseUI> fn = delegate { };
			return fn;
		}

		System.Action<MyFrameWork.BaseUI> d = (new System_Action_MyFrameWork_BaseUI_Event(func)).Call;
		return d;
	}

	class UIEventListener_VoidDelegate_Event : LuaDelegate
	{
		public UIEventListener_VoidDelegate_Event(LuaFunction func) : base(func) { }

		public void Call(UnityEngine.GameObject param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate UIEventListener_VoidDelegate(LuaFunction func)
	{
		if (func == null)
		{
			UIEventListener.VoidDelegate fn = delegate { };
			return fn;
		}

		UIEventListener.VoidDelegate d = (new UIEventListener_VoidDelegate_Event(func)).Call;
		return d;
	}

	class MyFrameWork_StateChangedEvent_Event : LuaDelegate
	{
		public MyFrameWork_StateChangedEvent_Event(LuaFunction func) : base(func) { }

		public void Call(object param0,MyFrameWork.E_ObjectState param1,MyFrameWork.E_ObjectState param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate MyFrameWork_StateChangedEvent(LuaFunction func)
	{
		if (func == null)
		{
			MyFrameWork.StateChangedEvent fn = delegate { };
			return fn;
		}

		MyFrameWork.StateChangedEvent d = (new MyFrameWork_StateChangedEvent_Event(func)).Call;
		return d;
	}

	class System_Action_NotiData_Event : LuaDelegate
	{
		public System_Action_NotiData_Event(LuaFunction func) : base(func) { }

		public void Call(NotiData param0)
		{
			func.BeginPCall();
			func.PushObject(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate System_Action_NotiData(LuaFunction func)
	{
		if (func == null)
		{
			System.Action<NotiData> fn = delegate { };
			return fn;
		}

		System.Action<NotiData> d = (new System_Action_NotiData_Event(func)).Call;
		return d;
	}

	class System_Action_bool_Event : LuaDelegate
	{
		public System_Action_bool_Event(LuaFunction func) : base(func) { }

		public void Call(bool param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate System_Action_bool(LuaFunction func)
	{
		if (func == null)
		{
			System.Action<bool> fn = delegate { };
			return fn;
		}

		System.Action<bool> d = (new System_Action_bool_Event(func)).Call;
		return d;
	}

	class System_Action_object_Event : LuaDelegate
	{
		public System_Action_object_Event(LuaFunction func) : base(func) { }

		public void Call(object param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public static Delegate System_Action_object(LuaFunction func)
	{
		if (func == null)
		{
			System.Action<object> fn = delegate { };
			return fn;
		}

		System.Action<object> d = (new System_Action_object_Event(func)).Call;
		return d;
	}

}

