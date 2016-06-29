using UnityEngine;
using System.Collections;
using MyFrameWork;
/// <summary>
/// 玩家升级动画播放要求
/// 1.底色瞬间出现，底框0.3秒内出现（带白色文字）
/// 2.星星+翅膀从大到小拍下（带等级文字）
/// 3.黄色4星从小到过大到正常，升级啦高度从小到过大到正常
/// 4.特效出现。特效17从淡到浓，特效10a从小到大，然后旋转，零星出特效18
/// 5.绿色文字出现，从小到过大到正常，每个卡座轮番出现，最后出现卡牌等级上限。
///     然后绿色文字变淡变大，
/// 6.绿色文字消失，出现闪动动画，白色值改变
/// 
/// 
/// 玩家升级动画时间控制
/// 需求                      开始秒           时长             上接               游戏物体           上接
/// 
/// 1.底框弹框                0                0.3                                  S_Frame
/// 
/// 2.发呆                    0.3              0.2               S_Frame            S_LvUp
/// 
/// 2.出现并等待              0.5              0.2
/// 
/// 2.星星+翅膀大到小         0.7              0.2                                  S_LvUp
///   
/// 3.黄星+升级啦             0.9              0.2+0.1           S_LvUp             S_LvUp_4Stars / S_LvUp_Lyrics
///  
/// 4.特效                    1.1              循环              S_LvUp_4Stars      S_Eft               
/// 
/// 5.绿色文字C1              1.5              1.5               S_Eft
/// 
/// 5.绿色文字C2              1.8              1.5               L_S1_HP_Add（Position）
/// 
/// 5.绿色文字C3              2.1              1.5               L_S2_HP_Add
/// 
/// 6.C1刷+白字更新           3                                  L_S1_HP_Add上挂L_S1_HP_Bef的脚本的ShowSplash()方法
/// 
/// 6.C2刷+白字更新           3.3                                L_S1_HP_Add上挂L_S1_HP_Bef的脚本的ShowSplash()方法
/// 
/// 6.C3刷+白字更新           3.6                                L_S1_HP_Add上挂L_S1_HP_Bef的脚本的ShowSplash()方法
/// 
/// 
/// </summary>
/// 



    

public class UIPlayerLvUp : BaseUI {

    private GameObject _firework;

    private Transform _fireworkfather;
    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        _firework = transform.FindChild("S_Frame/Eft/S_Eft18").gameObject;

        _fireworkfather = transform.FindChild("S_Frame/Eft");
    }

   
    //protected override void OnBtnClick(GameObject go)
    //{
    //    switch (go.name)
    //    {
    //        case "Sprite_ConfirmWin":
    //            break;
    //        default:
    //            break;
    //    }
    //}

    
    public void PlayFirework()
    {
        Invoke("Firework", Random.Range(1.1f, 2.1f));

        Invoke("Firework", Random.Range(3.1f, 5.1f));

        Invoke("Firework", Random.Range(7.1f, 8.1f));
    }

    void Firework()
    {
        GameObject go = Instantiate(_firework);

        go.transform.parent = _fireworkfather;

        go.transform.localPosition = new Vector3(Random.Range(-300, 300), Random.Range(450, 550), 0);

        go.SetActive(true);

        Destroy(go, 1);
        
    }
}
