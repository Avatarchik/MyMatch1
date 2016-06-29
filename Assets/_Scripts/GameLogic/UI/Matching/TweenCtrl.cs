using UnityEngine;
using System.Collections;

public class TweenCtrl : MonoBehaviour {


    public GameObject Matched;

    public GameObject Matching;

    public GameObject Effects;

    public GameObject Cards;
    
    public GameObject All;

    public GameObject Line;

    public GameObject Enlarger;

    public GameObject Circle;

    public GameObject Effect_Light;

    public GameObject WinUI;

    Vector3 AllPos;

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
		effect.transform.SetParent(EffectSpots[cardIndex].transform,false);
		CardSpots[cardIndex].SetActive(false);
	}
	#endregion


    public static TweenCtrl _instance;

    void Awake()
    {
        _instance = this;
    }

	private bool _initName = false;

    // Update is called once per frame
    void Update () {

#if KeyboardTest

        //找到对手
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject.Find("UIMatchingPanel").transform.localPosition = Vector3.zero;

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
        //放大镜转圈
        if (Enlarger.activeInHierarchy)
        {
            //又公转，又自转
            Enlarger.transform.RotateAround(Enlarger.transform.parent.position, Vector3.forward, -2);

            //不自转
            Enlarger.transform.localRotation = Quaternion.identity;

            
        }


    }

    /// <summary>
    /// 找到匹配对手，1.线消失，放大镜消失，2.我方，对手方屏幕下拉
    /// </summary>
    public void FindOpponent()
    {
		//OppenentName.text = MyFrameWork.ModuleMgr.Instance.Get<ModuleUIMain>().mOpponent.nick;

        Line.SetActive(false);

        Circle.SetActive(false);

        gameObject.transform.localPosition = Vector3.zero; //确保 Mactch界面在屏幕中

        GameObject.Find("Sprite_Bg_Mask2").GetComponent<UISprite>().enabled = true;//确保遮挡板mask2显示


        Matching.GetComponent<TweenPosition>().enabled = true;

        Matched.GetComponent<TweenPosition>().enabled = true;


    }

    /// <summary>
    /// 下压到中间，1.出现匦Ш汀岸哉健保哼2.屏幕抖动，0.3秒后停止 ; 这个回调方法挂在 Matched 下拉结束后
    /// </summary>
    public void ShowWords()
    {

        
        Effects.SetActive(true);

        AllPos = All.transform.position; //记录抖动前位置

        All.GetComponent<TweenPosition>().enabled = true;        

        Invoke("StopTremble", 0.2f);

        Invoke("ShowLight", 0.1f);

        Invoke("StartFight", 1f);
    }

    public void ShowLight()
    {
        Effect_Light.SetActive(true);
        
    }


    /// <summary>
    /// “对战”出现后1秒，1.“对战”和特效效果消失，2.对手方卡牌不懂，3.我方，对手方其他信息下拉
    /// </summary>
    void StartFight()
    {
        //Effects.SetActive(false);

        //All.GetComponent<TweenTransform>().enabled = true; //All的移动

        Cards.transform.parent = gameObject.transform;
        

        GameObject.Find("Sprite-halfBlackMask").GetComponent<UISprite>().enabled=true;

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

    /// <summary>
    /// 临时方法：点击后进入胜利/失败界面
    /// </summary>
    public void ToUIWinPanel()
    {
        WinUI.GetComponent<TweenPosition>().enabled=true;
        
    }

}
