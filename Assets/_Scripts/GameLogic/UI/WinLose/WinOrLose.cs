using UnityEngine;
using System.Collections;
using MyFrameWork;

enum EnumFightResult
{
    None,
    Win,
    Draw,
    Lose,
}

/// <summary>
/// 调用方法ShowWinPanel之前，
/// 把我方和对方比分传给MedelMe，MedelOppo，
/// 获胜或者失败的杯数，金币数变化传给 CupNumChange ,GoldNumChange
/// Part1 胜利时候才有的横穿特效+胜利字样
/// Part2 结算界面，奖杯，金币的变化
/// 流程：      1.出“战斗结束”。             ShowWinPanel / ShowLoosePanel
///             2.出“胜利/失败”文字特效      开关：Part1_ForInVoke
///                                            ShowWinPanel 和 Tween 调用
///             3.画面变暗文字特效消失         文字进行0.5秒，调用Show2Close1
///             4.出结算画面                   Show2Close1
///             
/// 失败文字特效： 先出两层乌云，灰色上，紫色中间，交替变大，灰色变大时候变淡
///                 紫色的云变大的时候变深
///             1.0到0.2秒：           乌云渐浓，之后循环
///             2.0.2秒：出文字
///             3.0.5秒：出阴影，1.1秒阴影处??胙?短
/// 
/// 结算画面    1. 黑色盖板逐渐变深             Show2Close1
///             2. 0.5秒后文字特效“胜利/失败”删除   Show2Close1
///             3. 1秒后上围巾                   Show2Close1 ->(tween) ShowOppoMedals
///             4. 轮流跳奖牌                    ShowOppoMedals
///             5. 显示“获胜”文字
///             6. 显示获胜奖励，箱子，按钮      ShowGoldFrame
///             7. 奖励数字轮番跳出              箱子TWEEN结束后调用 ShowCupNum
/// 
/// </summary>
public class WinOrLose : MonoBehaviour
{

    
    //4.28
    public GameObject Ef6;

    public GameObject Ef6a;

    public GameObject Ef6b;

    public GameObject WinWords;

    public GameObject Ef6_Lose1;

    public GameObject Ef6_Lose2;

    public GameObject Ef6_Lose3;

    public GameObject LoseWords;

    public GameObject Part1;

    public GameObject C2_scarf;

    public GameObject BgMask;

    public GameObject Medel_Me1;

    public GameObject Medel_Me2;

    public GameObject Medel_Me3;

    public GameObject Medel_Oppo1;

    public GameObject Medel_Oppo2;

    public GameObject Medel_Oppo3;

    public GameObject WinWord;

    public GameObject DrawWord;

    public GameObject LoseWord;

    public GameObject Ef5;

    public GameObject Box_And_Confirm;

    private string _plus_or_minus = "+";

    public GameObject Chest;

    public GameObject CupNum;

    public GameObject GoldNum;

    public GameObject ExpNum;

    private EnumFightResult _enumFightResult;

    public int MedelMe = 3;
        
    public int MedelOppo = 1;

    public int CupNumChange = 888;

    public int GoldNumChange = 888;

    public int ExpNumChange = 888;

    public UILabel MyName;

    public UILabel OppName;

    public GameObject Fightfinish;
    
    


#if !KeyboardTest
    void Awake()
    {
        object[] dataHomeUserNick = Util.CallMethod("UIMainModule", "GetUserNick");

        if (dataHomeUserNick!=null)
        {
            MyName.text = dataHomeUserNick[0].ToString();
        }
    
        object[]dataMatched= Util.CallMethod("UIMainModule", "GetOpponentNick");

        if (dataMatched!=null)
        {
            OppName.text = dataMatched[0].ToString();
        }

        object[] dataExpNumChange = Util.CallMethod("FightModule", "GetAwardExp");

        if (dataExpNumChange!=null)
        {
            ExpNumChange = int.Parse(dataExpNumChange[0].ToString());
        }
    }

#endif

    void Update()
    {
#if KeyboardTest
        if (Input.GetKeyDown(KeyCode.W))
        {
            _enumFightResult = EnumFightResult.Win;

            Invoke("Part1_ForInVoke", 1f);

            Fightfinish.SetActive(true);

            Destroy(Fightfinish, 0.8f);
        }

        else if(Input.GetKeyDown(KeyCode.L))
        {
            _enumFightResult = EnumFightResult.Lose;

            Invoke("Part1_ForInVoke", 1f);

            Fightfinish.SetActive(true);

            Destroy(Fightfinish, 0.8f);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            _enumFightResult = EnumFightResult.Draw;

            

            Fightfinish.SetActive(true);

            Destroy(Fightfinish, 0.8f);

            Show2Close1();

        }


#endif
    }

    public void ShowWinPanel(int jiangbei, int jinbi,int myMedel,int oppMedel)
	{
        CupNumChange = jiangbei;

        GoldNumChange = jinbi;
        
        MedelMe = myMedel;

        MedelOppo = oppMedel;

        _enumFightResult = EnumFightResult.Win;

        //Part1_ForInVoke();
        Invoke("Part1_ForInVoke", 1f);

        //“战斗结束”出现到结束0.8秒
        Fightfinish.SetActive(false);

        Destroy(Fightfinish, 0.8f);
    }

    void Part1_ForInVoke()
    {
        if (_enumFightResult == EnumFightResult.Win)
        {
            Ef6.SetActive(true);
        }

        else if (_enumFightResult == EnumFightResult.Lose)
        {
            Ef6_Lose1.SetActive(true);

            Ef6_Lose2.SetActive(true); //在这tween结束后生效其他方法

            Ef6_Lose3.SetActive(true);
        }
    }

	public void ShowLoosePanel(int jiangbei, int jinbi, int myMedel, int oppMedel)
	{
        CupNumChange = jiangbei;

        GoldNumChange = jinbi;

        MedelMe = myMedel;

        MedelOppo = oppMedel;

        _enumFightResult = EnumFightResult.Lose;

        Invoke("Part1_ForInVoke", 1f);

        //“战斗结束”出现到结束0.8秒
        Fightfinish.SetActive(true);

        Destroy(Fightfinish, 0.8f);
    }

  

    public void ShowDrawPanel(int jiangbei, int jinbi, int myMedel, int oppMedel)
    {
        CupNumChange = jiangbei;

        GoldNumChange = jinbi;

        MedelMe = myMedel;

        MedelOppo = oppMedel;

        _enumFightResult = EnumFightResult.Draw;

        //“战斗结束背鱿值浇崾?8秒
        Fightfinish.SetActive(true);

        Destroy(Fightfinish, 0.8f);

        Show2Close1();
    }


    /// <summary>
    /// 点宝箱对应方法
    /// </summary>
    public void ClickChest()
    {
        
    }

    // 4.28 重做胜利/失败画面
    /// <summary>
    /// Ef6完毕后播放ef6b
    /// </summary>
    public void ShowEf6b()
    {
        if (_enumFightResult == EnumFightResult.Win)
        {
            Ef6a.SetActive(true);

            Ef6b.SetActive(true);
        }
    }

    /// <summary>
    /// ef6穿完后显示胜利文字
    /// </summary>
    public void ShowWords()
    {
        if (_enumFightResult == EnumFightResult.Win)
        {
            WinWords.SetActive(true);
        }
        else if (_enumFightResult == EnumFightResult.Lose)
        {
            LoseWords.SetActive(true);
        }
    }

    /// <summary>
    /// 胜利：“战斗结束”->“胜利”->调用
    /// 失败： 直接调用
    /// 过程：  0到0.8秒：    “战斗结束”
    ///         第0.5秒：      “胜利/失败”字样消失
    ///         0.5到0.8秒：   背景遮挡变深
    ///         第1秒：        出围巾
    ///         
    /// </summary>
    public void Show2Close1()
    {        
        Invoke("Show2Clos1AfterSeconds", 1f);

        BgMask.SetActive(true);

        Destroy(Part1, 0.5f);
    }

    public void Show2Clos1AfterSeconds()
    {
        //Part1.SetActive(false);

        C2_scarf.SetActive(true);
    }

    
    /// <summary>
    /// 奖章显示入口，读取服务器传来的比分，赋值给MedelMe和MedelOppo
    /// </summary>
    public void ShowOppoMedals()
    {
        if (MedelOppo > 0)
        {
            if (!Medel_Oppo1.activeInHierarchy)
            {
                Medel_Oppo1.SetActive(true);
            }
            else
            {
                if (!Medel_Oppo2.activeInHierarchy)
                {
                    Medel_Oppo2.SetActive(true);
                }
                else
                {
                    Medel_Oppo3.SetActive(true);
                }
            }
            
            MedelOppo -= 1;

            Invoke("ShowMeMedals", 0.3f);

        }
        else
        {
            ShowMeMedals();
        }

    }

    /// <summary>
    /// 奖章显示（我方），与ShowOppoMedal一起使用，显示完毕后方法在这里调用
    /// </summary>
    public void ShowMeMedals()
    {
        if (MedelMe > 0)
        {
            if (!Medel_Me1.activeInHierarchy)
            {
                Medel_Me1.SetActive(true);
            }
            else
            {
                if (!Medel_Me2.activeInHierarchy)
                {
                    Medel_Me2.SetActive(true);
                }
                else
                {
                    Medel_Me3.SetActive(true);
                }
            }

            MedelMe -= 1;

            Invoke("ShowOppoMedals", 0.3f);
        }
        else
        {
            if (MedelOppo>0)
            {
                ShowOppoMedals();
            }
            else //我方奖牌显示完毕，且对方奖牌也显示完毕，进入文字显示
            {
                switch (_enumFightResult)
                {
                    case EnumFightResult.None:

                        break;

                    case EnumFightResult.Win:

                        WinWord.SetActive(true);

                        Ef5.SetActive(true);

                        break;

                    case EnumFightResult.Draw:

                        DrawWord.SetActive(true);

                        Chest.SetActive(false);

                        break;

                    case EnumFightResult.Lose:

                        LoseWord.SetActive(true);

                        Chest.SetActive(false);

                        break;

                    default:
                        break;
                }
                
            }
        }

    }

    /// <summary>
    /// 显示奖杯，金币条框，宝箱，确定
    /// </summary>
    public void ShowGoldFrame()
    {
        Invoke("After1SecondsShow", 0.2f);
    }

    void After1SecondsShow()
    {
        if (_enumFightResult == EnumFightResult.Lose)
        {
            _plus_or_minus = "";
            
        }

        if (_enumFightResult == EnumFightResult.Draw)
        {
            CupNumChange = 0;

            GoldNumChange = 0;
        }
        

        Box_And_Confirm.SetActive(true);

        //ShowCupNum();
    }

    /// <summary>
    /// 显示奖杯数，宝箱->等0.4秒 -> 显示数字（挂在Container_BoxCpnfirm上）
    /// </summary>
    public void ShowCupNum()
    {
        CupNum.GetComponent<UILabel>().text = _plus_or_minus + CupNumChange;

        CupNum.SetActive(true);
    }

 /// <summary>
 /// 显示金币数字，奖杯显示完毕后调用
 /// </summary>
    public void ShowGoldNum()
    {
        GoldNum.GetComponent<UILabel>().text = _plus_or_minus + GoldNumChange;

        GoldNum.SetActive(true);
    }

    /// <summary>
    /// 显示经验数字，金币显示完毕后调用
    /// </summary>
    public void ShowExpNum()
    {
        //失败也增加经验，下行为新增脚本，
        _plus_or_minus = "+";

        ExpNum.GetComponent<UILabel>().text = _plus_or_minus + ExpNumChange;

        ExpNum.SetActive(true);
    }

    /// <summary>
    /// 半透明遮罩被点到。tween快速结束
    /// </summary>
    public void OnMaskPressed()
    {

        if (_enumFightResult!=EnumFightResult.Win)
        {
            Chest.SetActive(false);

            _plus_or_minus = "";
        }

        Box_And_Confirm.GetComponent<TweenScale>().enabled = false;
        //显示_BoxCpnfirm
        Box_And_Confirm.SetActive(true);

        CupNum.GetComponent<UILabel>().text = _plus_or_minus + CupNumChange;

        TweenScale cup_TS = CupNum.GetComponent<TweenScale>();

        cup_TS.value = cup_TS.to;

        cup_TS.enabled = false;

        TweenRotation cup_TR = CupNum.GetComponent<TweenRotation>();

        cup_TR.value = Quaternion.identity;

        cup_TR.enabled = false;

        CupNum.SetActive(true);

        GoldNum.GetComponent<UILabel>().text = _plus_or_minus + GoldNumChange;

        TweenScale gold_TS = GoldNum.GetComponent<TweenScale>();

        gold_TS.value = gold_TS.to;

        gold_TS.enabled = false;

        TweenRotation gold_TR = GoldNum.GetComponent<TweenRotation>();

        gold_TR.value = Quaternion.identity;

        gold_TR.enabled = false;

        GoldNum.SetActive(true);

        //失败也增加经验，下行为新增脚本，替代下下行
        ExpNum.GetComponent<UILabel>().text ="+" + ExpNumChange;
        //ExpNum.GetComponent<UILabel>().text = _plus_or_minus + ExpNumChange;

        TweenScale exp_TS = ExpNum.GetComponent<TweenScale>();

        exp_TS.value = exp_TS.to;

        exp_TS.enabled = false;

        TweenRotation exp_TR = ExpNum.GetComponent<TweenRotation>();

        exp_TR.value = Quaternion.identity;

        exp_TR.enabled = false;

        ExpNum.SetActive(true);
    }
}




