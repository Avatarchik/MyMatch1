using UnityEngine;
using System.Collections;
using MyFrameWork;

/// <summary>
/// 
/// /// 体现排名变动
///     总开关：激活 Container_All, 0.2秒放大完毕后，执行 ShowRollingNums
///     二选一开关：标志位 FightNew.FightMgr.Instance.PlayRankUp
///         OriginPos() 决定初始位置，在start中完成
///         Afterpoint1ShowRollingNums ， 决定特效的变化
///     

///             
///     =================第一种：200名内=====================
///     二选一开关：标志位 FightNew.FightMgr.Instance.PlayRankUp，方法 Afterpoint1ShowRollingNums()
///             整个画面从小变大：           0到0.2秒：                 
///     要求：  出现两个人的信息             0到0.2秒
///             出现两个人的排名             0.4秒， Showrollingnums 启用0.2秒Invoke
///             更换位置，同时展现箭头       0.4到0.9秒，等待： 属性控制
///                                          0.9秒，出现箭头
///                                          0.9-1.4秒，排名交换： 属性控制
///             if(有继续交换)
///             我方信息框移动到原来位置
///             对方信息框的内容更新
///             重新出现两个人信息
/// 
///             else
///             出特效                       1.4秒-按下确认键
/// 
/// 
///     =================第二种：200名外交换=================
///     要求：  除了排名提升的文字，其他从0到1出现，0.1秒
///             出文字-排名提升，同时出滚动
///             滚动结束后，变大，砸下去，出光点特效，
///             光点特效消失，出光柱循环特效。
///     顺序：  整个框从小到大      -       上一画面（战斗结算）按“确定”后自动显示
///             出现“排名提升XX位”的文字
///                 同时数字滚动开始        -       上个tween结束时调用Showrollingnums方法
///             整个数字组 放大，然后拍下       -       用时间控制，如果4个数字1.6秒
///             雪花特效散出      -       上个tween结束时调用EmitEffects方法,1秒后关闭特效
///             大光和小光特效     -      EmitLightPost触发Invoke，XX秒后执行
/// 
/// 
/// 
/// </summary>
public class RankChange : MonoBehaviour {


    public GameObject Effect_Ge;

    public GameObject Effect_Shi;

    public GameObject Effect_Bai;

    public GameObject Effect_Qian;

    public GameObject Whole_Nums;

    public float Whole_NumsBiggerAfter = 1.8f;

    public float inbetween = 0.2f;

    private TweenScale[] mTweenScale;

    public GameObject RankUpSet;

    public GameObject Effect_Snow;

    public GameObject EffectLightPost_Big;

    public GameObject EffectLightPost_Small;

    public int Rank_Change_Num = 8888;

    public UILabel Ge;

    public UILabel Shi;

    public UILabel Bai;

    public UILabel Qian;

    //5.20交换效果
    /// <summary>
    ///  检测是否在前200位，给标志位 FightNew.FightMgr.Instance.PlayRankUp 赋值
    /// </summary>
    public GameObject Container_All;

    public GameObject Me;

    public GameObject Oppo;

    public UILabel MyName_RankChange;

    public UILabel MyTrophy_RankChange;

    public UILabel MyRank_RankChange;

    public UITexture MyPhoto_RankChange;

    public UILabel OppoName_RankChange;

    public UILabel OppoTrophy_RankChange;

    public UILabel OppoRank_RankChange;

    public UITexture OppoPhoto_RankChange;

    public GameObject MeArrow;

    public GameObject OppoArrow;

    public GameObject MeRank;

    public GameObject OppoRank;

    public GameObject MeExceptArrowRank;

    //剩余交换次数
    private int ChangeTodo = 0;

    //已经进行的交换次数
    private int ChangeDone = 0;
    

    // Use this for initialization
    void Start () {

        if (FightNew.FightMgr.Instance.PlayRankUp)
        {
            //200名外，获得提升的名次
            SettleNums();

            MeSettingsRankUp();
        }
        else
        {
            //200名内,获得交换次数
            GetChangeNums();

            MeSettings();
        }
        

        

        //两套都要用到，根据_inFirst200进行初始化设置
        

        OriginPos();
    }

    /// <summary>
    /// 我方数据赋值,
    /// </summary>
    void MeSettings()
    {
#if !KeyboardTest
        object[] MyName = Util.CallMethod("FightModule", "RankChange_GetMyName");
        MyName_RankChange.text = MyName[0].ToString();
        object[] MyRank = Util.CallMethod("FightModule", "RankChange_GetMyRank",ChangeDone);
        MyRank_RankChange.text = MyRank[0].ToString();
        object[] MyTrophy = Util.CallMethod("FightModule", "RankChange_GetMyTrophy");
        MyTrophy_RankChange.text = MyTrophy[0].ToString();
        object[] data = Util.CallMethod("UIMainModule", "GetUserPhoto");
        MyPhoto_RankChange.mainTexture = Resources.Load("Photos/" + int.Parse(data[0].ToString()) % 6) as UnityEngine.Texture;
       
#endif
    }

    void MeSettingsRankUp()
    {
#if !KeyboardTest
        object[] MyName = Util.CallMethod("UIMainModule", "GetUserNick");
        MyName_RankChange.text = MyName[0].ToString();
        object[] MyTrophy = Util.CallMethod("FightModule", "RankUp_GetMyTrophy");
        MyTrophy_RankChange.text = MyTrophy[0].ToString();
        object[] data = Util.CallMethod("UIMainModule", "GetUserPhoto");
        MyPhoto_RankChange.mainTexture = Resources.Load("Photos/" + int.Parse(data[0].ToString()) % 6) as UnityEngine.Texture;

#endif
    }

    /// <summary>
    /// 对方数据赋值,参数是已经交换的次数。默认0
    /// </summary>
    void OppoSettings(int num)
    {
#if !KeyboardTest
        object[] OppoName = Util.CallMethod("FightModule", "RankChange_GetOppoName", ChangeDone);
        OppoName_RankChange.text = OppoName[0].ToString();
        object[] OppoRank = Util.CallMethod("FightModule", "RankChange_GetOppoRank", ChangeDone);
        OppoRank_RankChange.text = OppoRank[0].ToString();
        object[] OppoTrophy = Util.CallMethod("FightModule", "RankChange_GetOppoTrophy", ChangeDone);
        OppoTrophy_RankChange.text = OppoTrophy[0].ToString();
        object[] OppoPhoto = Util.CallMethod("FightModule", "RankChange_GetOppoPhoto", ChangeDone);
        OppoPhoto_RankChange.mainTexture = Resources.Load("Photos/" + int.Parse(OppoPhoto[0].ToString()) % 6) as UnityEngine.Texture;
        //OppoPhoto_RankChange.mainTexture = Resources.Load("Photos/" + 0 % 6) as UnityEngine.Texture;
        object[] MyRank = Util.CallMethod("FightModule", "RankChange_GetMyRank", ChangeDone);
        MyRank_RankChange.text = MyRank[0].ToString();
#endif
    }

    /// <summary>
    /// 获得需要交换的次数
    /// </summary>
    void GetChangeNums()
    {
#if KeyboardTest
        ChangeTodo=3;
#else
        object[] OppoTrophy = Util.CallMethod("FightModule", "RankChange_RankChangeNums");
        ChangeTodo = int.Parse(OppoTrophy[0].ToString());
        //Debug.Log("....................ChangeTodo: "+ ChangeTodo);
#endif
    }



    /// <summary>
    /// 整个排名交换页面出现后，调用。
    /// </summary>
    public void ShowRollingNums()
    {
        Invoke("Afterpoint1ShowRollingNums", 0.1f);
    }


    void Afterpoint1ShowRollingNums()
    {
        //在前200，排名交换
        if (FightNew.FightMgr.Instance.PlayRankUp==false)
        {
            //Debug.Log("ChangeTodo : " + ChangeTodo);

            //Debug.Log("ChangeDone: " + ChangeDone);

            OppoSettings(ChangeDone);

            ChangeTodo--;

            ChangeDone++;

            //Debug.Log("........................1");
            //归位
            BackToOriginPositionAndTween();

            
            //0秒出现排名，0.9到1.3秒进行交换
            Me.SetActive(true);

            Oppo.SetActive(true);

            //0.3秒出排名
            Invoke("ShowRank", 0.3f);

            //1.2秒箭头出现
            Invoke("ShowArrow", 1.2f);
        }
        else
        {
            Me.SetActive(true);

            RankUpSet.SetActive(true);

            StartNumRolling();

            FightNew.FightMgr.Instance.PlayRankUp = false;//改为默认值：false
        }
        
    }

    /// <summary>
    /// 显示双方排名
    /// </summary>
    void ShowRank()
    {
        MeRank.SetActive(true);

        OppoRank.SetActive(true);
    }

    /// <summary>
    /// 出现箭头，双方信息出现后xx秒调用
    /// </summary>
    void ShowArrow()
    {
        Debug.Log("ShowArrow");

        MeArrow.SetActive(true);

        OppoArrow.SetActive(true);
    }

    /// <summary>
    /// 箭头最大的时候调用，查看是否还要交换要进行,信息框交换后执行
    /// </summary>
    public void isChangeOn()
    {

        if (ChangeTodo<=0)
        {
            Invoke("ShowEffect", 0.1f);
        }
        else
        {
            Effect_Snow.SetActive(false);

            Effect_Snow.SetActive(true);

            Invoke("DisaArrow_Rank", 0.5f);

            Invoke("MoveMePos", 1f);

            Invoke("Afterpoint1ShowRollingNums", 1.5f);
        }
    }

    void DisaArrow_Rank()
    {
        MeArrow.SetActive(false);

        OppoArrow.SetActive(false);

        MeRank.SetActive(false);

        OppoRank.SetActive(false);
    }

    /// <summary>
    /// 两个信息框位置下移，对手信息框淡出
    /// </summary>
    void MoveMePos()
    {
        TempTweenPos(Me);

        TempTweenPos(Oppo);

        TempTweenAlpha(Oppo);
    }

    /// <summary>
    /// 临时改变位置的tween，用完就删
    /// </summary>
    /// <param name="GO"></param>
    void TempTweenPos( GameObject GO)
    {
        TweenPosition temp = GO.AddComponent<TweenPosition>();

        temp.from = new Vector3(GO.transform.localPosition.x, GO.transform.localPosition.y);

        temp.to = new Vector3(GO.transform.localPosition.x, GO.transform.localPosition.y-184);

        temp.duration = 0.15f;

        temp.PlayForward();

        Destroy(temp, 0.15f);
    }

    /// <summary>
    /// 临时改变透明度的tween，用完就删
    /// </summary>
    /// <param name="GO"></param>
    void TempTweenAlpha(GameObject GO)
    {
        TweenAlpha temp = GO.AddComponent<TweenAlpha>();

        temp.from = 1;

        temp.to = 0;

        temp.duration = 0.1f;

        temp.PlayForward();

        Destroy(temp, 0.1f);
    }


    /// <summary>
    /// 双方信息框归位，Tween归位,之前关闭的，比如MeArrow，都要在Me.SetActive(false);后开启，否则tween、
    /// 不会复位
    /// </summary>
    void BackToOriginPositionAndTween()
    {
        Me.SetActive(false);

        Oppo.SetActive(false);

        MeArrow.SetActive(true);

        OppoArrow.SetActive(true);

        Oppo.GetComponent<UISprite>().alpha = 1;

        Me.transform.localPosition = new Vector3(20, -102);

        Oppo.transform.localPosition = new Vector3(-27, 82);

        MeRank.SetActive(true);

        OppoRank.SetActive(true);

        TweenBack(Me);

        TweenBack(Oppo);

        MeArrow.SetActive(false);

        OppoArrow.SetActive(false);

        MeRank.SetActive(false);

        OppoRank.SetActive(false);

      
    }

    void TweenBack(GameObject GO)
    {
        TweenPosition[] tpArr = GO.GetComponentsInChildren<TweenPosition>();

        //Debug.Log(GO.name + " TweenPosition nums: " + tpArr.Length);

        foreach (var item in tpArr)
        {
            item.ResetToBeginning();

            item.enabled = true;
           
            //Debug.Log("TweenPosition..." + item.gameObject.name);
        }

        
        TweenScale[] tsArr = GO.GetComponentsInChildren<TweenScale>();

        //Debug.Log(GO.name + " TweenScale nums: " + tsArr.Length);

        foreach (var item in tsArr)
        {
            item.ResetToBeginning();

            item.enabled = true;
           
            //Debug.Log("TweenScale..."+item.gameObject.name);
        }

    }


    /// <summary>
    /// 第一套：特效,没有交换时，最后播放
    /// </summary>
    public void ShowEffect()
    {
        EffectLightPost_Big.transform.localPosition = new Vector3(48, 166, 0);

        EffectLightPost_Small.transform.localPosition = new Vector3(48, 166, 0);

        EffectLightPost_Big.SetActive(true);

        EffectLightPost_Small.SetActive(true);

        Effect_Snow.SetActive(false);

        Effect_Snow.SetActive(true);
        
    }

    /// <summary>
    /// 根据 _inFirst200 ， 决定初始位置
    /// </summary>
    void OriginPos()
    {
        if (FightNew.FightMgr.Instance.PlayRankUp)//没进前200，
        {
            //在中间位置
            Me.transform.localPosition = new Vector3(0, -54, 0);

            TweenPosition[] tpArr = Me.GetComponents<TweenPosition>();

            foreach (var item in tpArr)
            {
                item.enabled = false;
            }

            TweenScale[] tsArr = MeExceptArrowRank.GetComponents<TweenScale>();

            foreach (var item in tsArr)
            {
                item.enabled = false;
            }

            Oppo.SetActive(false);

            Effect_Snow.transform.localPosition = new Vector3(80, 165, 0);
        }
        else
        {
            Effect_Snow.transform.localPosition = new Vector3(-183, 239, 0);
        }

    }


    #region 第二套：不交换排名




    /// <summary>
    /// 仅提示上升位数，不交换排名，根据服务器传送来的排名变化确定每个位数的数字
    /// </summary>
    void SettleNums()
    {
        //Rank_Change_Num=
        object [] data=Util.CallMethod("FightModule", "RankUp_GetMyRankChange");

        Rank_Change_Num = int.Parse(data[0].ToString());

        Qian.text = Rank_Change_Num / 1000 + "";

        Bai.text = Rank_Change_Num % 1000 / 100 + "";

        Shi.text = Rank_Change_Num % 1000 % 100 / 10 + "";

        Ge.text = Rank_Change_Num % 1000 % 100 % 10 + "";

        DebugUtil.Info("4 digitals: "+Ge.gameObject.transform.localPosition);

        if (Rank_Change_Num / 1000==0)
        {
            Qian.enabled = false;

            Vector3 V3bai = Bai.gameObject.transform.localPosition;

            Bai.gameObject.transform.localPosition = new Vector3(V3bai.x - 16, V3bai.y, V3bai.z);

            TweenPosition tpbai=Bai.gameObject.GetComponent<TweenPosition>();

            tpbai.from.x = V3bai.x - 16;

            tpbai.to.x= V3bai.x - 16;

            Vector3 V3shi = Shi.gameObject.transform.localPosition;

            Shi.gameObject.transform.localPosition = new Vector3(V3shi.x - 16, V3shi.y, V3shi.z);

            TweenPosition tpshi = Shi.gameObject.GetComponent<TweenPosition>();

            tpshi.from.x = V3shi.x - 16;

            tpshi.to.x = V3shi.x - 16;

            Vector3 V3ge = Ge.gameObject.transform.localPosition;

            Ge.gameObject.transform.localPosition = new Vector3(V3ge.x - 16, V3ge.y, V3ge.z);

            TweenPosition tpge = Ge.gameObject.GetComponent<TweenPosition>();

            tpge.from.x = V3ge.x - 16;

            tpge.to.x = V3ge.x - 16;

            DebugUtil.Info("3 digitals: " + Ge.gameObject.transform.localPosition);

            if (Rank_Change_Num % 1000 / 100==0)
            {
                Bai.enabled = false;

                Vector3 V3shi2 = Shi.gameObject.transform.localPosition;

                Shi.gameObject.transform.localPosition = new Vector3(V3shi2.x - 12, V3shi2.y, V3shi2.z);

                tpshi.from.x = V3shi2.x - 12;

                tpshi.to.x = V3shi2.x - 12;

                Vector3 V3ge2 = Ge.gameObject.transform.localPosition;

                Ge.gameObject.transform.localPosition = new Vector3(V3ge2.x - 12, V3ge2.y, V3ge2.z);

                tpge.from.x = V3ge2.x - 12;

                tpge.to.x = V3ge2.x - 12;

                DebugUtil.Info("2 digitals: " + Ge.gameObject.transform.localPosition);

                if (Rank_Change_Num % 1000 % 100 / 10==0)
                {
                    Shi.enabled = false;

                    Vector3 V3ge3 = Ge.gameObject.transform.localPosition;

                    Ge.gameObject.transform.localPosition = new Vector3(V3ge3.x - 17, V3ge3.y, V3ge3.z);

                    tpge.from.x = V3ge3.x - 17;

                    tpge.to.x = V3ge3.x - 17;

                    DebugUtil.Info("1 digitals: " + Ge.gameObject.transform.localPosition);
                }
            }
        }
    }

    //200名外大幅升级
    void StartNumRolling()
    {
        Effect_Ge.SetActive(true);

        Invoke("ActivateShi", inbetween);

        Invoke("WholeBigger", Whole_NumsBiggerAfter);
    }

    void ActivateShi()
    {
        Effect_Shi.SetActive(true);

        Invoke("ActivateBai", inbetween);
    }

    void ActivateBai()
    {
        Effect_Bai.SetActive(true);

        Invoke("ActivateQian", inbetween);
    }

    void ActivateQian()
    {
        Effect_Qian.SetActive(true);
    }

    
    /// <summary>
    /// 整体变大
    /// </summary>
    public void WholeBigger()
    {
        mTweenScale = Whole_Nums.GetComponents<TweenScale>();

        foreach (var item in mTweenScale)
        {
            item.enabled = true;
        }
    }

    /// <summary>
    /// 出光点特效
    /// </summary>
    public void EmitEffects()
    {
        Effect_Snow.SetActive(true);

        Invoke("CloseEmitSpotLight", 1f);

        Invoke("EmitLightPost", 0.3f);
    }

    void CloseEmitSpotLight()
    {
        Effect_Snow.SetActive(false);
    }

    /// <summary>
    /// 光柱特效启动，0.2秒放大，一直旋转
    /// </summary>
    void EmitLightPost()
    {
        EffectLightPost_Big.SetActive(true);

        EffectLightPost_Small.SetActive(true);
    }

#endregion

#region 两套都通用
    /// <summary>
    /// 点击确定按钮，回到主界面
    /// </summary>
    public void ConfirmBtn()
    {
        //Debug.Log("回到主界面");
        FightNew.FightMgr.Instance.ClearLevel();
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)SenceName.SENCE_MAIN);
		UIMgr.Instance.DestroyUI(E_UIType.Fight);
        Util.CallMethod("Game", "ShowUIMain"); //初始化完成
//		FightNew.FightMgr.Instance.IsInFightScene = false;
    }

    /// <summary>
    /// 点击分享按钮，分享到朋友圈
    /// </summary>
    public void ShareBtn()
    {
        Debug.Log("分享到朋友圈");
    }

#endregion



}
