using UnityEngine;
using System.Collections;
using MyFrameWork;
/// <summary>
/// 激活面板的时候检测杯数，决定竞技场号，逻辑写在Lua-UIArenaCtrl.lua
/// 竞技场号决定：
///         SV显示的位置
///         当前竞技场大小特效：
///             非当前竞技场：不激活tween（默认不激活）
///             当前竞技场：休眠掉房子，卡牌 -> 激活房子，卡牌各自的tween -> 激活房子
///         其他特效，如背光，文字，挂在那个子物体上，挂上后位置归零
/// 
/// </summary>

public class UIArena : BaseUI {

    

   
}
