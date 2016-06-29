
using UnityEngine;
using System.Collections;
using MyFrameWork;
using System.Collections.Generic;

/// <summary>
/// 
/// 
/// 
/// 
/// 1.点击“出战”后，等服务器回调，进入排队队列后，调出MatchingPanel
/// 
/// 2.服务器匹配到对手后，回调“LoadFightScene()”方法，此方法执行以下方法
///         1.用UIMgr销毁主界面
///         2.本脚本的 FindOpponent，本方法主要执行：
///                 让 Matched.SetActive(true);生效，
///                         0到0.3秒：     等待
///                         0.5秒：        Tween属性执行方法 StartFight
///                                        Effects：中间灯打开，一明，一暗
///                         0.3到1秒：     对手信息落下弹起落下
///                         2.5秒：        匹配画面所有物体休眠
///                         
///         3.APPMonoController.Instance.StartCoroutine(ToFightScene());
///                         0到2秒：       等待
///                         2秒：         加载战斗场景 FightNew.FightMgr.Instance.LoadFightScene();
///  
///     
/// </summary>
public class UIMatching : BaseUI {

    private GameObject Matched;

//    private GameObject Matching;

    private GameObject Effects;

    private GameObject Cards;

//    private GameObject All;

    #region boss出生特效
    private GameObject BossCreateEffect;

    private GameObject[] EffectSpots= new GameObject[3];

    private GameObject[] CardSpots = new GameObject[3];

    public void ShowEffect(int cardIndex)
    {
        GameObject effect = Instantiate<GameObject>(BossCreateEffect);
        //effect.gameObject.SetActive(false);
        effect.transform.SetParent(EffectSpots[cardIndex].transform, false);
        CardSpots[cardIndex].SetActive(false);
    }
    #endregion

    private UILabel SerchingLabel;

    private float timerEffect;

    private GameObject CancelBtn;

    private GameObject OppoCardContainer;

    private UITexture _effectUISprite;

    private bool _effectDown;

    private UILabel MyName;

    private UILabel MyTrophy;

    private UITexture MyFlag;

    private UITexture MyPhoto;

    private UILabel MyCity;

    private UILabel OppoName;

    private UILabel OppoTrophy;

    private UILabel OppoCity;

    private UITexture OppoPhoto;

    private UITexture OppoFlag;

    public static UIMatching _instance;

    public int TotalPhotoNum = 6;

    private Dictionary<string, string> NationalFlags = new Dictionary<string, string>();
    
    protected override void OnAwake()
    {
        NationalFlags.Add("中国", "china");

        NationalFlags.Add("china", "china");

        NationalFlags.Add("australia", "australia");

        NationalFlags.Add("france", "france");

        NationalFlags.Add("japan", "japan");

        NationalFlags.Add("south korea", "south korea");

        NationalFlags.Add("thailand", "thailand");

        NationalFlags.Add("united states", "united states");

        base.OnAwake();

        _instance = this;
        
        Matched = transform.Find("All/Container_Matched").gameObject;

//        Matching = transform.Find("All/Container_Matching").gameObject;

        Effects = transform.Find("All/Container_MatchedEffects/T_Matched").gameObject;

        _effectUISprite = Effects.GetComponent<UITexture>();

        Cards = transform.Find("All/Container_Matched/Container_OppCards").gameObject;

//        All = transform.Find("All").gameObject;

        BossCreateEffect = Resources.Load("FightNew/Effect/BossCreate") as GameObject;
        //BossCreateEffect = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightNew.FightDefine.Prefab_BossCreate);

        EffectSpots[0] = transform.Find("All/Container_Matched/Container_OppCards/EffectSpots/EffectCard1").gameObject;

        EffectSpots[1] = transform.Find("All/Container_Matched/Container_OppCards/EffectSpots/EffectCard2").gameObject;

        EffectSpots[2] = transform.Find("All/Container_Matched/Container_OppCards/EffectSpots/EffectCard3").gameObject;

        CardSpots[0] = transform.Find("All/Container_Matched/Container_OppCards/Sprite_Icon_L").gameObject;

        CardSpots[1] = transform.Find("All/Container_Matched/Container_OppCards/Sprite_Icon_M").gameObject;

        CardSpots[2] = transform.Find("All/Container_Matched/Container_OppCards/Sprite_Icon_R").gameObject;

        SerchingLabel = transform.Find("All/Container_Matching/Label_Serching").GetComponent<UILabel>();

        CancelBtn = transform.Find("All/Container_Matching/Sprite_CancelBtn").gameObject;

        OppoCardContainer = transform.Find("All/Container_Matched/Container_OppCards").gameObject;

        MyName = transform.Find("All/Container_Matching/Label_PlayerName").GetComponent<UILabel>();

        GetMyName();

        MyTrophy = transform.Find("All/Container_Matching/Sprite_MyPhoto_Frame/S_Trophy/L_Trophy").GetComponent<UILabel>();

        GetMyTrophy();

        MyFlag = transform.Find("All/Container_Matching/Sprite_MyPhoto_Frame/T_NationalFlag").GetComponent<UITexture>();

        GetMyFlag();

        MyCity= transform.Find("All/Container_Matching/Sprite_MyPhoto_Frame/Label_City").GetComponent<UILabel>();

        GetMyCity();

        MyPhoto = transform.Find("All/Container_Matching/Sprite_MyPhoto_Frame/T_MyPhoto").GetComponent<UITexture>();

        GetMyPhoto();

        OppoName = transform.Find("All/Container_Matched/Container_PlayerName/Sprite_NameBg/Label_OppName").GetComponent<UILabel>();

        OppoTrophy = transform.Find("All/Container_Matched/Sprite_OppoPhoto_Frame/S_Trophy/L_Trophy").GetComponent<UILabel>();

        OppoCity = transform.Find("All/Container_Matched/Sprite_OppoPhoto_Frame/Label_City").GetComponent<UILabel>();

        OppoPhoto = transform.Find("All/Container_Matched/Sprite_OppoPhoto_Frame/T_OppoPhoto").GetComponent<UITexture>();

        OppoFlag = transform.Find("All/Container_Matched/Sprite_OppoPhoto_Frame/T_NationalFlag").GetComponent<UITexture>();

        UIEventListener.Get(CancelBtn).onClick += OnPressCancelBtn;

       
    }

    /// <summary>
    /// 读取玩家自己的名字，登录后保存在Lua的 UIMainModule 的 ServerHomeData
    /// </summary>
    void GetMyName()
    {
        object[] data = Util.CallMethod("UIMainModule", "GetUserNick");

        MyName.text = data[0].ToString();
          
    }

    void GetMyTrophy()
    {
        object[] data = Util.CallMethod("UIMainModule", "GetUserTrophy");

        MyTrophy.text = data[0].ToString();

    }

    void GetMyFlag()
    {
        object[] data = Util.CallMethod("UIMainModule", "GetUserFlag");

        string country="";

        if (NationalFlags.ContainsKey(data[0].ToString().ToLower()))
        {
            country = NationalFlags[data[0].ToString().ToLower()];

        }
        else
        {
            country = "default";
        }

        MyFlag.mainTexture = Resources.Load("Flags/" + country) as UnityEngine.Texture;

        //MyFlag.mainTexture = Resources.Load("Flags/" + "default") as UnityEngine.Texture;

    }

    void GetMyCity()
    {
        
        object[] data = Util.CallMethod("UIMainModule", "GetUserCity");

        MyCity.text = data[0].ToString();

        //MyCity.text = "猫狩";
    }

    void GetMyPhoto()
    {
        object[] data = Util.CallMethod("UIMainModule", "GetUserPhoto");

        MyPhoto.mainTexture = Resources.Load("Photos/" + int.Parse(data[0].ToString()) % TotalPhotoNum) as UnityEngine.Texture;

        //MyPhoto.mainTexture = Resources.Load("Photos/" + 5) as UnityEngine.Texture;

    }

    protected override void OnInit()
    {
        base.OnInit();

        mUIStyle = E_UIStyle.PopUp;

        mUILayertype = E_LayerType.NormalUI;
    }

	protected override void OnUpdate(float deltaTime)
	{
        //红灯一闪一灭
        RedLightShining();

#if KeyboardTest

        //找到对手
        if (Input.GetKeyDown(KeyCode.M))
        {
           gameObject.transform.localPosition = Vector3.zero;

            //遮挡板
            GameObject.Find("Sprite_Bg_Mask2").GetComponent<UISprite>().enabled = true;
        }

        //切换到战斗画面
        if (Input.GetKeyDown(KeyCode.F))
        {
            FindOpponent();
        }

        //重新加载
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
#endif

    }

    /// <summary>
    /// 匹配成功红灯单闪灭
    /// </summary>
    void RedLightShining()
    {

        if (Effects.activeInHierarchy)
        {
            

            if (timerEffect * 2 < 0)
            {
                _effectDown = false;
            }


            else if (timerEffect * 2 > 1)
            {
                _effectDown = true;
            }

            if (!_effectDown)
            {
                timerEffect += Time.deltaTime;
            }
            else
            {
                timerEffect -= Time.deltaTime;
            }

            _effectUISprite.alpha = timerEffect * 2;
        }
    }



/// <summary>
/// 找到匹配对手，
/// 1.正在搜索的文字消失，取消按钮消失
/// 2.对手信息在0.3秒内压下来 -> 压下后中间灯一亮一灭交替
///         同时，对手部分弹起，落下
/// </summary>
public void FindOpponent()
    {
        GetOppoInfo();

        Matched.SetActive(true);

        SerchingLabel.gameObject.SetActive(false);

        //取消按钮消失
        CancelBtn.SetActive(false);

        MusicManager.Instance.PlaySoundEff("Music/Matched");

        Invoke("ShowWords", 0.5f);
    }

    /// <summary>
    /// 这个回调方法挂在 Matched 下拉结束后，Matched出现后0.5秒执行，，1.灯闪灭，2.屏幕抖动，0.3秒后停止 ; 
    /// </summary>
    public void ShowWords()
    {
        Effects.SetActive(true);

        Invoke("StartFight", 3f);
    }



    /// <summary>
    /// “对战”出现后1秒，1.“对战”和特效效果消失，2.对手方卡牌不懂，3.我方，对手方其他信息下拉
    /// </summary>
    void StartFight()
    {
        Cards.transform.parent = gameObject.transform;

        UISprite[] mUISpriteArray = OppoCardContainer.GetComponentsInChildren<UISprite>();

        foreach (var item in mUISpriteArray)
        {
            item.enabled = false;
        }

        UIMgr.Instance.DestroyUI(E_UIType.PanelMatching);

    }

    void GetOppoInfo()
    {
        object[] dataOppoNick = Util.CallMethod("UIMainModule", "GetOpponentNick");
        
        OppoName.text = dataOppoNick[0].ToString();
        
        object[] dataOppoTrophy = Util.CallMethod("UIMainModule", "GetOpponentTrophy");

        OppoTrophy.text = dataOppoTrophy[0].ToString();

        object[] dataOppoCity = Util.CallMethod("UIMainModule", "GetOpponentCity");

        OppoCity.text = dataOppoCity[0].ToString();

        //OppoCity.text = "猫狩";

        object[] dataOppoPhoto = Util.CallMethod("UIMainModule", "GetOpponentPhoto");

        OppoPhoto.mainTexture = Resources.Load("Photos/" + int.Parse(dataOppoPhoto[0].ToString()) % TotalPhotoNum) as UnityEngine.Texture;

        //OppoPhoto.mainTexture = Resources.Load("Photos/" + 5) as UnityEngine.Texture;

        object[] dataOppoFlag = Util.CallMethod("UIMainModule", "GetOpponentFlag");

        string country = "";

        if (NationalFlags.ContainsKey(dataOppoFlag[0].ToString().ToLower()))
        {
            country = NationalFlags[dataOppoFlag[0].ToString().ToLower()];
        }
        else
        {
            country = "default";
        }

        OppoFlag.mainTexture = Resources.Load("Flags/" + country) as UnityEngine.Texture;

        //OppoFlag.mainTexture = Resources.Load("Flags/" + "default") as UnityEngine.Texture;

    }

    void OnPressCancelBtn(GameObject go)
    {
        Util.CallMethod("UIMainCtrl", "SendCancelMatch");
    }

}
