using System;
using UnityEngine;
using LuaInterface;

public static class LuaBinder
{
	public static void Bind(LuaState L)
	{
		float t = Time.realtimeSinceStartup;
		L.BeginModule(null);
		DebuggerWrap.Register(L);
		UIPanelWrap.Register(L);
		UILabelWrap.Register(L);
		UIGridWrap.Register(L);
		SpringPanelWrap.Register(L);
		UIScrollViewWrap.Register(L);
		UIScrollBarWrap.Register(L);
		UICenterOnChildWrap.Register(L);
		UIInputWrap.Register(L);
		UISpriteWrap.Register(L);
		TweenAlphaWrap.Register(L);
		TweenPositionWrap.Register(L);
		TweenScaleWrap.Register(L);
		UITextureWrap.Register(L);
		LoadingManagerWrap.Register(L);
		LuaUtilityManagerWrap.Register(L);
		UIRectWrap.Register(L);
		UIWidgetWrap.Register(L);
		UIWidgetContainerWrap.Register(L);
		UISliderWrap.Register(L);
		UIProgressBarWrap.Register(L);
		UIBasicSpriteWrap.Register(L);
		UITweenerWrap.Register(L);
		BaseWrap.Register(L);
		ManagerWrap.Register(L);
		L.BeginModule("UnityEngine");
		UnityEngine_ComponentWrap.Register(L);
		UnityEngine_BehaviourWrap.Register(L);
		UnityEngine_MonoBehaviourWrap.Register(L);
		UnityEngine_GameObjectWrap.Register(L);
		UnityEngine_TransformWrap.Register(L);
		UnityEngine_SpaceWrap.Register(L);
		UnityEngine_CameraWrap.Register(L);
		UnityEngine_CameraClearFlagsWrap.Register(L);
		UnityEngine_MaterialWrap.Register(L);
		UnityEngine_RendererWrap.Register(L);
		UnityEngine_MeshRendererWrap.Register(L);
		UnityEngine_SkinnedMeshRendererWrap.Register(L);
		UnityEngine_LightWrap.Register(L);
		UnityEngine_LightTypeWrap.Register(L);
		UnityEngine_ParticleEmitterWrap.Register(L);
		UnityEngine_ParticleRendererWrap.Register(L);
		UnityEngine_ParticleAnimatorWrap.Register(L);
		UnityEngine_ParticleSystemWrap.Register(L);
		UnityEngine_PhysicsWrap.Register(L);
		UnityEngine_ColliderWrap.Register(L);
		UnityEngine_BoxColliderWrap.Register(L);
		UnityEngine_MeshColliderWrap.Register(L);
		UnityEngine_SphereColliderWrap.Register(L);
		UnityEngine_CharacterControllerWrap.Register(L);
		UnityEngine_AnimationWrap.Register(L);
		UnityEngine_AnimationClipWrap.Register(L);
		UnityEngine_TrackedReferenceWrap.Register(L);
		UnityEngine_AnimationStateWrap.Register(L);
		UnityEngine_QueueModeWrap.Register(L);
		UnityEngine_PlayModeWrap.Register(L);
		UnityEngine_AudioClipWrap.Register(L);
		UnityEngine_AudioSourceWrap.Register(L);
		UnityEngine_ApplicationWrap.Register(L);
		UnityEngine_InputWrap.Register(L);
		UnityEngine_KeyCodeWrap.Register(L);
		UnityEngine_ScreenWrap.Register(L);
		UnityEngine_TimeWrap.Register(L);
		UnityEngine_RenderSettingsWrap.Register(L);
		UnityEngine_SleepTimeoutWrap.Register(L);
		UnityEngine_AsyncOperationWrap.Register(L);
		UnityEngine_AssetBundleWrap.Register(L);
		UnityEngine_BlendWeightsWrap.Register(L);
		UnityEngine_QualitySettingsWrap.Register(L);
		UnityEngine_AnimationBlendModeWrap.Register(L);
		UnityEngine_RenderTextureWrap.Register(L);
		UnityEngine_RigidbodyWrap.Register(L);
		UnityEngine_CapsuleColliderWrap.Register(L);
		UnityEngine_WrapModeWrap.Register(L);
		UnityEngine_TextureWrap.Register(L);
		UnityEngine_ShaderWrap.Register(L);
		UnityEngine_Texture2DWrap.Register(L);
		UnityEngine_WWWWrap.Register(L);
		L.BeginModule("Events");
		L.RegFunction("UnityAction", UnityEngine_Events_UnityAction);
		L.EndModule();
		L.EndModule();
		L.BeginModule("MyFrameWork");
		MyFrameWork_UtilWrap.Register(L);
		MyFrameWork_AppConstWrap.Register(L);
		MyFrameWork_LuaHelperWrap.Register(L);
		MyFrameWork_ByteBufferWrap.Register(L);
		MyFrameWork_LuaBehaviourWrap.Register(L);
		MyFrameWork_LuaDragWrap.Register(L);
		MyFrameWork_EventDispatcherWrap.Register(L);
		MyFrameWork_GameManagerWrap.Register(L);
		MyFrameWork_LuaManagerWrap.Register(L);
		MyFrameWork_UIMgrWrap.Register(L);
		MyFrameWork_BaseUIWrap.Register(L);
		MyFrameWork_MusicManagerWrap.Register(L);
		MyFrameWork_TimerManagerWrap.Register(L);
		MyFrameWork_ThreadManagerWrap.Register(L);
		MyFrameWork_NetworkManagerWrap.Register(L);
		MyFrameWork_FightDataManagerWrap.Register(L);
		MyFrameWork_ResourceMgrWrap.Register(L);
		MyFrameWork_E_UIStyleWrap.Register(L);
		MyFrameWork_E_UITypeWrap.Register(L);
		MyFrameWork_E_UIMaskStyleWrap.Register(L);
		MyFrameWork_E_LayerTypeWrap.Register(L);
		L.EndModule();
		L.BeginModule("System");
		L.RegFunction("Action", System_Action);
		L.EndModule();
		L.EndModule();
		Debugger.Log("Register lua type cost time: {0}", Time.realtimeSinceStartup - t);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnityEngine_Events_UnityAction(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(UnityEngine.Events.UnityAction), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int System_Action(IntPtr L)
	{
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate arg1 = DelegateFactory.CreateDelegate(typeof(System.Action), func);
			ToLua.Push(L, arg1);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

