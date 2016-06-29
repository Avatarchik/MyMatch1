/*******************************************************
 * 
 * 文件名(File Name)：             BaseUI
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/02/29 12:44:09
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace MyFrameWork
{
    public delegate bool BoolDelegate();
    public class BaseUI : MonoBehaviour 
	{
		#region Cache gameObject & transfrom

		private Transform _cachedTransform;
		/// <summary>
		/// Gets the cached transform.
		/// </summary>
		/// <value>The cached transform.</value>
		public Transform CachedTransform
		{
			get
			{
				if (!_cachedTransform)
				{
					_cachedTransform = this.transform;
				}
				return _cachedTransform;
			}
		}

		private GameObject _cachedGameObject;
		/// <summary>
		/// Gets the cached game object.
		/// </summary>
		/// <value>The cached game object.</value>
		public GameObject CachedGameObject
		{
			get
			{
				if (!_cachedGameObject)
				{
					_cachedGameObject = this.gameObject;
				}
				return _cachedGameObject;
			}
		}

		#endregion

		#region UIType & EnumObjectState
		/// <summary>
		/// The state.
		/// </summary>
		protected E_ObjectState state = E_ObjectState.None;

        // Return处理逻辑
        private event BoolDelegate returnPreLogic = null;

        /// <summary>
        /// Occurs when state changed.
        /// </summary>
        public event StateChangedEvent StateChanged;

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		public E_ObjectState State
		{
			protected set
			{
				if (value != state)
				{
					E_ObjectState oldState = state;
					state = value;
					if (null != StateChanged)
					{
						StateChanged(this, state, oldState);
					}
				}
			}
			get { return this.state; }
		}

        /// <summary>
        /// Gets the type of the user interface.
        /// </summary>
        /// <returns>The user interface type.</returns>
        public virtual E_UIType GetUIType() { return _uiType; }

        public E_UIType mUIType
        {
            set { _uiType = value; }
            get { return _uiType; }
        }

        protected E_UIType _uiType { set; get; }
        #endregion

        /// <summary>
        /// 按钮碰撞体
        /// </summary>
        private List<Collider> _listCollider = new List<Collider>();
		/// <summary>
		/// 打开方式
		/// </summary>
		/// <value>The animation style.</value>
		protected E_UIShowAnimStyle _animationStyle{set;get;}
		/// <summary>
		/// 遮挡层样式
		/// </summary>
		/// <value>The mask stype.</value>
		protected E_UIMaskStyle _maskStype{set;get;}

        public E_UIStyle mUIStyle
        {
            set { _uiStyle = value; }
            get { return _uiStyle; }
        }
        /// <summary>
        /// 窗口样式
        /// </summary>
        /// <value>The user interface stype.</value>
        protected E_UIStyle _uiStyle{set;get;}

        public E_LayerType mUILayertype
        {
            set { _uiLayerType = value; }
            get { return _uiLayerType; }
        }
		protected E_LayerType _uiLayerType{set;get;}


		protected float _openDuration = 0.3f;


		#region MONO methods
		void Awake()
		{
			this.State = E_ObjectState.Initial;

			OnAwake ();
		}

		// Use this for initialization
		void Start () 
		{
			OnStart ();
		}
		
		// Update is called once per frame
		public virtual void Update () 
		{
			if (E_ObjectState.Ready == this.state) 
			{
				OnUpdate(Time.deltaTime);
			}
		}

		public virtual void OnDestroy()
		{
            AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).SetPanelDepth(this,false);

			_listCollider.Clear();
			_listCollider = null;
		}
		#endregion

		/// <summary>
		/// Release this instance.
		/// </summary>
		public void Release()
		{
			this.State = E_ObjectState.Closing;
			GameObject.Destroy (CachedGameObject);
			OnRelease ();
		}

		protected virtual void OnAwake()
		{
			this.State = E_ObjectState.Loading;
		}

		protected virtual void OnStart()
		{

		}

		protected virtual void OnUpdate(float deltaTime)
		{

		}

		protected virtual void OnRelease()
		{
			this.OnPlayCloseUIAudio();
		}



		/// <summary>
		/// 播放打开界面音乐
		/// </summary>
		protected virtual void OnPlayOpenUIAudio()
		{

		}

		/// <summary>
		/// 播放关闭界面音乐
		/// </summary>
		protected virtual void OnPlayCloseUIAudio()
		{

		}

		/// <summary>
		/// 点击按钮
		/// </summary>
		/// <param name="go">被点击的对象</param>
		protected virtual void OnBtnClick(GameObject go)
		{

		}
			
		public void UIInit()
		{
			OnInit();
		}

		/// <summary>
		/// 初始化，还没有显示，可以在重载中设置打开界面的动画方式，以及背景样式
		/// </summary>
		protected virtual void OnInit()
		{
			_uiStyle = E_UIStyle.BackClose;
			_animationStyle = E_UIShowAnimStyle.Normal;
			_maskStype = E_UIMaskStyle.None;
			_uiLayerType = E_LayerType.NormalUI;

			Collider[] colliders = this.GetComponentsInChildren<Collider>(true);
			for(int i = 0,len = colliders.Length;i<len;i++)
			{
				Collider collider = colliders[i];
				UIEventListener listener = UIEventListener.Get(collider.gameObject);
             
				listener.onClick = OnBtnClick;

				_listCollider.Add(collider);
			}
		}
			
		public void Show(params object[] param)
		{
			CachedGameObject.SetActive(true);
			this.State = E_ObjectState.Ready;

			//播放音乐
			this.OnPlayOpenUIAudio();

			OnShow(param);

			switch(_animationStyle)
			{
				case E_UIShowAnimStyle.Normal:
					ShowNormal();
					break;
				case E_UIShowAnimStyle.CenterScaleBigNormal:
					ShowCenterScaleBigNormal();
					break;
				case E_UIShowAnimStyle.LeftToSlide:
					ShowLeftToSlide(true);
					break;
				case E_UIShowAnimStyle.RightToSlde:
					ShowLeftToSlide(false);
					break;
				case E_UIShowAnimStyle.TopToSlide:
					ShowTopToSlide(true);
					break;
				case E_UIShowAnimStyle.DownToSlide:
					ShowTopToSlide(false);
					break;
			}
		}

		protected virtual void OnShow(params object[] param)
		{
		}

		public void Hide()
		{
			CachedGameObject.SetActive(false);
            AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).SetPanelDepth(this,false);

			this.OnPlayCloseUIAudio();

			OnHide();
		}

		protected virtual void OnHide()
		{
			this.State = E_ObjectState.Disabled;
		}

        /// <summary>
        /// 界面在退出或者用户点击返回之前都可以注册执行逻辑
        /// </summary>
        protected void RegisterReturnLogic(BoolDelegate newLogic)
        {
            returnPreLogic = newLogic;
        }

        public bool ExecuteReturnLogic()
        {
            if (returnPreLogic == null)
                return false;
            else
                return returnPreLogic();
        }


        #region 各种打开效果
        void ShowNormal()
		{
		}

		void ShowCenterScaleBigNormal()
		{
			TweenScale scale = this.CachedTransform.GetOrAddComponent<TweenScale>();
			scale.from = Vector3.zero;
			scale.to = Vector3.one;
			scale.duration = _openDuration;
//			scale.SetOnFinished(()=>
//				{
//					if(!isOpen)
//					{
//						MonoBehaviour.Destroy(go.gameObject);
//						_dicPanels.Remove(go.PanelType);
//					}
//				}
//			);

//			if(!isOpen)
//			{
//				scale.Play(false);
//			}
		}

		void ShowLeftToSlide(bool isFromLeft)
		{
			TweenPosition pos = this.CachedTransform.GetOrAddComponent<TweenPosition>();
			pos.from = isFromLeft ? Vector3.left * 700 : Vector3.right * 700;
			pos.to = Vector3.zero;
			pos.duration = _openDuration;

//			pos.SetOnFinished(()=>
//				{
//					if(!isOpen)
//					{
//						MonoBehaviour.Destroy(go.gameObject);
//						_dicPanels.Remove(go.PanelType);
//					}
//				}
//			);
				
//			if(!isOpen)
//			{
//				pos.Play(false);
//			}

		}

		void ShowTopToSlide(bool isFromTop)
		{
			TweenPosition pos = this.CachedTransform.GetOrAddComponent<TweenPosition>();
			pos.from = isFromTop ? Vector3.up * 700 : Vector3.down * 700;
			pos.to = Vector3.zero;
			pos.duration = _openDuration;
//			pos.SetOnFinished(()=>
//				{
//					if(!isOpen)
//					{
//						MonoBehaviour.Destroy(go.gameObject);
//						_dicPanels.Remove(go.PanelType);
//					}
//				}
//			);
//
//			go.gameObject.SetActive(true);
//
//			if(!isOpen)
//			{
//				pos.Play(false);
//			}

		}
		#endregion
	}
}
