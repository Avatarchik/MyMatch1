using UnityEngine;
using System;
using System.Collections.Generic;
using LuaInterface;
using MyFrameWork;
using BindType = ToLuaMenu.BindType;

namespace MyFrameWork
{
    public static class CustomSettings
    {
        public static string FrameworkPath = AppConst.FrameworkRoot;
        public static string saveDir = FrameworkPath + "/ToLua/Source/Generate/";
        public static string luaDir = FrameworkPath + "/Lua/";
        public static string toluaBaseType = FrameworkPath + "/ToLua/BaseType/";


        //导出时强制做为静态类的类型(注意customTypeList 还要添加这个类型才能导出)
        //unity 有些类作为sealed class, 其实完全等价于静态类
        public static List<Type> staticClassTypes = new List<Type>
    {
        typeof(UnityEngine.Application),
        typeof(UnityEngine.Time),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.SleepTimeout),
        typeof(UnityEngine.Input),
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.Physics),
        typeof(UnityEngine.RenderSettings),
        typeof(UnityEngine.QualitySettings),
    };

        //附加导出委托类型(在导出委托时, customTypeList 中牵扯的委托类型都会导出， 无需写在这里)
        public static DelegateType[] customDelegateList =
        {
        _DT(typeof(Action)),
        //_DT(typeof(Action<GameObject>)).SetAbrName("ActionGo"),
        _DT(typeof(UnityEngine.Events.UnityAction)),
    };

        //在这里添加你要导出注册到lua的类型列表
        public static BindType[] customTypeList =
        {                
        //------------------------为例子导出--------------------------------
        //_GT(typeof(TestEventListener)),                
        //_GT(typeof(TestAccount)),
        //_GT(typeof(Dictionary<int, TestAccount>)).SetLibName("AccountMap"),                
        //_GT(typeof(KeyValuePair<int, TestAccount>)),    
        //-------------------------------------------------------------------
        
        _GT(typeof(Debugger)),

        _GT(typeof(Component)),
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),
        _GT(typeof(GameObject)),
        _GT(typeof(Transform)),
        _GT(typeof(Space)),

        _GT(typeof(Camera)),
        _GT(typeof(CameraClearFlags)),
        _GT(typeof(Material)),
        _GT(typeof(Renderer)),
        _GT(typeof(MeshRenderer)),
        _GT(typeof(SkinnedMeshRenderer)),
        _GT(typeof(Light)),
        _GT(typeof(LightType)),
        _GT(typeof(ParticleEmitter)),
        _GT(typeof(ParticleRenderer)),
        _GT(typeof(ParticleAnimator)),
        _GT(typeof(ParticleSystem)),
        _GT(typeof(Physics)),
        _GT(typeof(Collider)),
        _GT(typeof(BoxCollider)),
        _GT(typeof(MeshCollider)),
        _GT(typeof(SphereCollider)),
        _GT(typeof(CharacterController)),
        _GT(typeof(Animation)),
        _GT(typeof(AnimationClip)),
        _GT(typeof(TrackedReference)),
        _GT(typeof(AnimationState)),
        _GT(typeof(QueueMode)),
        _GT(typeof(PlayMode)),
        _GT(typeof(AudioClip)),
        _GT(typeof(AudioSource)),
        _GT(typeof(Application)),
        _GT(typeof(Input)),
        _GT(typeof(KeyCode)),
        _GT(typeof(Screen)),
        _GT(typeof(Time)),
        _GT(typeof(RenderSettings)),
        _GT(typeof(SleepTimeout)),
        _GT(typeof(AsyncOperation)),
        _GT(typeof(AssetBundle)),
        _GT(typeof(BlendWeights)),
        _GT(typeof(QualitySettings)),
        _GT(typeof(AnimationBlendMode)),
        _GT(typeof(RenderTexture)),
        _GT(typeof(Rigidbody)),
        _GT(typeof(CapsuleCollider)),
        _GT(typeof(WrapMode)),
        _GT(typeof(Texture)),
        _GT(typeof(Shader)),
        _GT(typeof(Texture2D)),
        _GT(typeof(WWW)),
        
       

        //for NGUI
        _GT(typeof(UIPanel)),
        _GT(typeof(UILabel)),
        _GT(typeof(UIGrid)),
        _GT(typeof(SpringPanel)),
        _GT(typeof(UIScrollView)),
        _GT(typeof(UIScrollBar)),
        _GT(typeof(UICenterOnChild)),
        _GT(typeof(UIInput)),
        _GT(typeof(UISprite)),
        _GT(typeof(TweenAlpha)),
        _GT(typeof(TweenPosition)),
        _GT(typeof(TweenScale)),
        _GT(typeof(UITexture)),

        //for MyFrameWork
        _GT(typeof(MyFrameWork.Util)),
        //_GT(typeof(WrapGrid)),
        _GT(typeof(AppConst)),
        _GT(typeof(LuaHelper)),
        _GT(typeof(ByteBuffer)),
        _GT(typeof(LuaBehaviour)),
        _GT(typeof(LuaDrag)),
        _GT(typeof(EventDispatcher)),

        _GT(typeof(GameManager)),
        _GT(typeof(LuaManager)),
        _GT(typeof(UIMgr)),
        _GT(typeof(BaseUI)),
        _GT(typeof(MusicManager)),
        _GT(typeof(TimerManager)),
        _GT(typeof(ThreadManager)),
        _GT(typeof(NetworkManager)),
        _GT(typeof(FightDataManager)),
        _GT(typeof(LoadingManager)),
        _GT(typeof(LuaUtilityManager)),
        _GT(typeof(ResourceMgr)),
        _GT(typeof(E_UIStyle)), 
        _GT(typeof(E_UIType)),
        _GT(typeof(E_UIMaskStyle)),
        _GT(typeof(E_LayerType)),
    };

        //重载函数，相同参数个数，相同位置out参数匹配出问题时, 需要强制匹配解决
        //使用方法参见例子14
        public static List<Type> outList = new List<Type>()
        {

        };

        static BindType _GT(Type t)
        {
            return new BindType(t);
        }

        static DelegateType _DT(Type t)
        {
            return new DelegateType(t);
        }
    }
}
