/*******************************************************
 * 
 * 文件名(File Name)：             UIDefine
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/02/29 12:51:24
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
    /// <summary>
    /// 状态变化
    /// </summary>
    public delegate void StateChangedEvent(object sender, E_ObjectState newState, E_ObjectState oldState);

    public enum E_ObjectState
    {
        /// <summary>
        /// The none.
        /// </summary>
        None,
        /// <summary>
        /// The initial.
        /// </summary>
        Initial,
        /// <summary>
        /// The loading.
        /// </summary>
        Loading,
        /// <summary>
        /// The ready.
        /// </summary>
        Ready,
        /// <summary>
        /// The disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// The closing.
        /// </summary>
        Closing
    }

        public enum E_UIType
        {
            /// <summary>
            /// The none.
            /// </summary>
            None = -1,
            //test ui
            PanelMessageBox,
            UIMainPanel,
            UILoginPanel,
            UILoadingPanel,
            UINamingPanel,
            PanelTestTopBar,

			UIWinOrLosePanel,
			Fight,
            PanelRankChange,
            PanelMatching,
            UIPlayerLvUpPanel,
            UIArenaPanel,
            UINetConditionPanel,
            UICardPanel,

        //formal ui
    }
        /// <summary>
        /// 窗口样式
        /// </summary>
        public enum E_UIStyle
        {
            BackClose,//有后退和关闭按钮
            Main,//主界面
            PopUp,//弹出界面
            TopBar,//常态条
        }

        public enum E_UIShowAnimStyle
        {
            Normal,
            /// <summary>
            /// 中间由小变大
            /// </summary>
            CenterScaleBigNormal,
            /// <summary>
            /// 由上往下
            /// </summary>
            TopToSlide,
            /// <summary>
            /// 由下往上
            /// </summary>
            DownToSlide,
            /// <summary>
            /// 由左往中
            /// </summary>
            LeftToSlide,
            /// <summary>
            /// 由右往中
            /// </summary>
            RightToSlde,
        }

        public enum E_UIMaskStyle
        {
            /// <summary>
            /// 无背景
            /// </summary>
            None,
            /// <summary>
            /// 半透明背景
            /// </summary>
            BlackAlpha,
            /// <summary>
            /// 无背景，但有boxclider关闭组件
            /// </summary>
            Alpha,
        }

}
