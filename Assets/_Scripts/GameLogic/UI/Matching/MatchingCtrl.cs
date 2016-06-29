using UnityEngine;
using System.Collections;
using MyFrameWork;

/// <summary>
/// 点击“出战”后，等服务器回调，进入排队队列后，本脚本挂载的游戏物体移动到0，0，0.
/// 服务器匹配到对手后，回调“LoadFightScene()”方法，此方法执行两个方法
///         1.本脚本的 FindOpponent，本方法主要执行：
///                 让 Matched.SetActive(true);生效，
///                         0到0.3秒：     等待
///                         0.5秒：        Tween属性执行方法 StartFight
///                                        Effects：中间灯打开，一明，一暗
///                         0.3到1秒：     对手信息落下弹起落下
///                         2.5秒：        匹配画面所有物体休眠
///                         
///         2.APPMonoController.Instance.StartCoroutine(ToFightScene());
///                         0到2秒：       等待
///                         2秒：         加载战斗场景 FightNew.FightMgr.Instance.LoadFightScene();
///  
///     
/// </summary>
public class MatchingCtrl : MonoBehaviour {

    public GameObject Matched;

    public GameObject Matching;

    public GameObject Effects;

    public GameObject Cards;

    public GameObject All;

    private Vector3 AllPos;

    public UILabel MyPlayerName;

    public UILabel OppenentName;

    #region boss出生特效
    public GameObject BossCreateEffect;

    public GameObject[] EffectSpots;

    public GameObject[] CardSpots;

    public void ShowEffect(int cardIndex)
    {
        GameObject effect = Instantiate<GameObject>(BossCreateEffect);
        //effect.gameObject.SetActive(false);
        effect.transform.SetParent(EffectSpots[cardIndex].transform, false);
        CardSpots[cardIndex].SetActive(false);
    }
    #endregion


    // 5.20 新增内容

    public UILabel SerchingLabel;

   private string[] serchingArray=new string[4] { "",".","..","..."};

    private float timer;

    private float timerEffect;

    public GameObject CancelBtn;

    public GameObject OppoCardContainer;

    public static MatchingCtrl _instance;

    private UISprite _effectUISprite;

    private bool _effectDown;

    void Awake()
    {
        _instance = this;

        _effectUISprite = Effects.GetComponent<UISprite>();

        
    }

    private bool _initName = false;

    // Update is called once per frame
    void Update()
    {

        //if (SerchingLabel.gameObject.activeInHierarchy)
        //{
        //    timer += Time.deltaTime;

        //    SerchingLabel.text = "正在搜索对手 " + serchingArray[(int)(timer * 3) % 4];
        //}


        //红灯一闪一灭
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

            _effectUISprite.alpha = timerEffect*2;
        }
        

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
/// 找到匹配对手，
/// 1.正在搜索的文字消失，取消按钮消失
/// 2.对手信息在0.3秒内压下来 -> 压下后中间灯一亮一灭交替
///         同时，对手部分弹起，落下
///         
/// </summary>
public void FindOpponent()
    {
        gameObject.transform.localPosition = Vector3.zero; //确保 Mactch界面在屏幕中

        GameObject.Find("Sprite_Bg_Mask2").GetComponent<UISprite>().enabled = true;//确保遮挡板mask2显示

        Matched.SetActive(true);

        SerchingLabel.gameObject.SetActive(false);

        //取消按钮消失
        CancelBtn.SetActive(false);

        MusicManager.Instance.PlaySoundEff("Music/Matched");
    }

    /// <summary>
    /// 下压到中间，1.出现匦Ш汀岸哉健保哼2.屏幕抖动，0.3秒后停止 ; 这个回调方法挂在 Matched 下拉结束后
    /// </summary>
    public void ShowWords()
    {
        
        Effects.SetActive(true);

        //AllPos = All.transform.position; //记录抖动前位置

        //All.GetComponent<TweenPosition>().enabled = true;

        //Invoke("StopTremble", 0.2f);

        Invoke("StartFight", 2f);
    }



    /// <summary>
    /// “对战”出现后1秒，1.“对战”和特效效果消失，2.对手方卡牌不懂，3.我方，对手方其他信息下拉
    /// </summary>
    void StartFight()
    {
        //Effects.SetActive(false);

        //All.GetComponent<TweenTransform>().enabled = true; //All的移动

        Cards.transform.parent = gameObject.transform;

        UISprite[] mUISpriteArray = OppoCardContainer.GetComponentsInChildren<UISprite>();

        foreach (var item in mUISpriteArray)
        {
            item.enabled = false;
        }

        //GameObject.Find("Sprite-halfBlackMask").GetComponent<UISprite>().enabled = true;

        All.SetActive(false);

        GameObject.Find("ScrollView_Panel").SetActive(false);

        GameObject.Find("Top_Bottom_Panel").SetActive(false);



    }

    /// <summary>
    /// 抖动停止
    /// </summary>
    void StopTremble()
    {

        All.GetComponent<TweenPosition>().enabled = false;

        //All.transform.position = AllPos;//还原到抖动前位置

        All.transform.position = Vector3.zero;
    }

  

}
