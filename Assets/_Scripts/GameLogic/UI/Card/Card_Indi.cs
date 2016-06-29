using UnityEngine;
using System.Collections;
/// <summary>
/// 挂在每张卡牌上的脚本
/// 做的事情有：
/// 1.显示卡牌的信息，卡牌的ID，等级，碎片，冷却时间
/// 
/// 2.卡牌被点击选中的时候
///     1). 检测ChoosenFlag是否休眠，如果休眠状态，到3)
///     2). 如果非休眠状态(之前点击过其他卡牌)，ChoosenFlag的原父物体归位
///         原父物体改变Y坐标，3个游戏物体图层减20，关闭按钮，激活碎片显示
///     3). 修改ChoosenFlag的父物体，将自身设为父物体
///         背景旗帜重新执行TS，改变Y坐标，3个游戏物体图层加20，显示按钮，休眠碎片
///     
/// 3.点击按钮的时候，做相应的交换
/// </summary>



public class Card_Indi : MonoBehaviour {

    
    #region 生成卡牌时赋予的值

//    public string CardID; //卡牌ID
//
//    public string CardColor; //卡牌成色
//
//    public int CardLv; //卡牌等级
//
//    public int CardPhaseLv; //卡牌阶数等级
//
//    public int FragmentNow; //现有碎片
//
//    public int FragmentNeeded; //突破需要碎片数量
//
//    public int LvCD;//升级冷却时间

    public GameObject ChoosenFlag; //选中旗帜

    #endregion

    private GameObject _part_fragment;

    private UISprite _cardMain;

    private UISprite _cardFrame;

    private UISprite _cardlvStar;

    private TweenScale mTweenScale;

    private TweenPosition mTweenPosition;

    private bool _flagLengthen;//执行Flag_Lengthen方法一次

    private bool _flagShorten;//执行Flag_Shorten方法一次


	private UILabel _labelCardLv;

	private ModuleCardItem _card;


    void Awake () {

        _cardMain=transform.Find("S_Card").GetComponent<UISprite>();

        _cardFrame = transform.Find("S_Card/S_Card_Frame").GetComponent<UISprite>();

        _cardlvStar = transform.Find("S_Card/S_Card_Lv").GetComponent<UISprite>();

        mTweenScale = transform.Find("S_Card").GetComponent<TweenScale>();

        mTweenPosition = transform.Find("S_Card").GetComponent<TweenPosition>();

        _part_fragment = transform.Find("S_CardExp_Frame").gameObject;

        

    }

    void Start()
    {
       UIEventListener.Get(gameObject).onClick += OnCardIndiClick; //注册点击方法
    }

	public void SetData(ModuleCardItem card)
	{
		_card = card;

		_labelCardLv.text = card.Level.ToString();
		//todo
	}
	
    /// <summary>
    /// 点击卡牌 执行的方法(主方法)
    /// </summary>
    /// <param name="go"></param>
    void OnCardIndiClick(GameObject go)
    {
        //DebugUtil.Info("OnCardIndiClick");

        if (ChoosenFlag.activeInHierarchy) //如果非第一次点卡牌
        {
            if (ChoosenFlag.transform.parent.gameObject==gameObject) //和上次点击是同一张，不做处理
            {
                DebugUtil.Info("same object. do nothing");

                return; 
            }
            else  //与上次点击不同
            {
                GameObject LastCard = ChoosenFlag.transform.parent.gameObject; //获取上次点击卡牌

                DebugUtil.Info("another card, " + LastCard.name);

                ReSetLastCard(LastCard);

                if (LastCard.name.Contains("Using")&& !gameObject.name.Contains("Using"))
                {
                    DebugUtil.Info("LastCard in Using ,and this not, enlarge the flag");

                    _flagLengthen = true; 
                }

                if (!LastCard.name.Contains("Using") && gameObject.name.Contains("Using"))
                {
                    DebugUtil.Info("LastCard not in Using,and this is, shorten the flag");

                    _flagShorten = true;
                }
            }
        }
        else //第一次点击卡牌
        {
            DebugUtil.Info("first click  " );

            if (gameObject.name.Contains("Using"))
            {
                _flagShorten = true;
            }

            ChoosenFlag.SetActive(true); //显示旗帜
        }

        ChangestoFlag(ChoosenFlag);

        ChangestoChoosenCard();

    }

    /// <summary>
    /// 重置上次点击的卡牌，原父物体改变Y坐标，3个游戏物体图层减20，激活碎片显示
    /// </summary>
    /// <param name="_lastCard"></param>
    void ReSetLastCard(GameObject _lastCard)
    {
        _lastCard.transform.Find("S_Card").GetComponent<TweenPosition>().PlayReverse();

        _lastCard.transform.Find("S_CardExp_Frame").gameObject.SetActive(true);//显示碎片

        _lastCard.transform.Find("S_Card").GetComponent<UISprite>().depth -= 20; //图层归位

        _lastCard.transform.Find("S_Card/S_Card_Frame").GetComponent<UISprite>().depth -= 20;

        _lastCard.transform.Find("S_Card/S_Card_Lv").GetComponent<UISprite>().depth -= 20;
    }

    /// <summary>
    /// 旗帜变长，加交换按钮
    /// </summary>
    void Flag_Lengthen(GameObject _flag)
    {
        _flag.GetComponent<UISprite>().height = 354;

        _flag.GetComponent<TweenPosition>().to = new Vector3(0, -45, 0);

        _flag.transform.Find("S_Replace_Btn").gameObject.SetActive(true);

        _flag.transform.Find("S_Info_Btn").localPosition = new Vector3(0, -54, 0);

        _flagLengthen = false;
    }

    /// <summary>
    /// 1、旗帜变短354到300，Y从-45到-19，2.取消交换按钮S_Replace_Btn，3.信息按钮下移，从-54到-86
    /// </summary>
    void Flag_Shorten(GameObject _flag)
    {
        _flag.GetComponent<UISprite>().height = 300;

        _flag.GetComponent<TweenPosition>().to = new Vector3(0,-19,0);

        _flag.transform.Find("S_Replace_Btn").gameObject.SetActive(false);

        _flag.transform.Find("S_Info_Btn").localPosition = new Vector3(0, -86, 0);

        _flagShorten = false;
    }

    /// <summary>
    /// 被选中卡片做出的改变
    /// </summary>
    void ChangestoChoosenCard()
    {
        _part_fragment.SetActive(false); //隐藏碎片

        _cardMain.depth += 20;//图层提升20

        _cardFrame.depth += 20;

        _cardlvStar.depth += 20;

        mTweenScale.ResetToBeginning();//被点卡牌变小再变大要两句

        mTweenScale.PlayForward();

        mTweenPosition.PlayForward();//被点卡牌位置上升只要这一句
    }

    /// <summary>
    /// 因为选中卡牌是否出战，旗帜长短/是否出现交换按钮
    /// </summary>
    void ChangestoFlag(GameObject _flag)
    {
        if (_flagLengthen == true)
        {
            Flag_Lengthen(_flag);
        }

        if (_flagShorten==true)
        {
            Flag_Shorten(_flag);
        }

        _flag.transform.SetParent(gameObject.transform);//指定父物体

        _flag.GetComponent<TweenPosition>().PlayForward();//用TP调整位置

        _flag.GetComponent<TweenScale>().ResetToBeginning();//被点卡牌背后旗帜变小再变大要两句

        _flag.GetComponent<TweenScale>().PlayForward();
    }

 

}
