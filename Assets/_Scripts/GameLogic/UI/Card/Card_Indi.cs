using UnityEngine;
using System.Collections;
/// <summary>
/// ����ÿ�ſ����ϵĽű�
/// ���������У�
/// 1.��ʾ���Ƶ���Ϣ�����Ƶ�ID���ȼ�����Ƭ����ȴʱ��
/// 
/// 2.���Ʊ����ѡ�е�ʱ��
///     1). ���ChoosenFlag�Ƿ����ߣ��������״̬����3)
///     2). ���������״̬(֮ǰ�������������)��ChoosenFlag��ԭ�������λ
///         ԭ������ı�Y���꣬3����Ϸ����ͼ���20���رհ�ť��������Ƭ��ʾ
///     3). �޸�ChoosenFlag�ĸ����壬��������Ϊ������
///         ������������ִ��TS���ı�Y���꣬3����Ϸ����ͼ���20����ʾ��ť��������Ƭ
///     
/// 3.�����ť��ʱ������Ӧ�Ľ���
/// </summary>



public class Card_Indi : MonoBehaviour {

    
    #region ���ɿ���ʱ�����ֵ

//    public string CardID; //����ID
//
//    public string CardColor; //���Ƴ�ɫ
//
//    public int CardLv; //���Ƶȼ�
//
//    public int CardPhaseLv; //���ƽ����ȼ�
//
//    public int FragmentNow; //������Ƭ
//
//    public int FragmentNeeded; //ͻ����Ҫ��Ƭ����
//
//    public int LvCD;//������ȴʱ��

    public GameObject ChoosenFlag; //ѡ������

    #endregion

    private GameObject _part_fragment;

    private UISprite _cardMain;

    private UISprite _cardFrame;

    private UISprite _cardlvStar;

    private TweenScale mTweenScale;

    private TweenPosition mTweenPosition;

    private bool _flagLengthen;//ִ��Flag_Lengthen����һ��

    private bool _flagShorten;//ִ��Flag_Shorten����һ��


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
       UIEventListener.Get(gameObject).onClick += OnCardIndiClick; //ע��������
    }

	public void SetData(ModuleCardItem card)
	{
		_card = card;

		_labelCardLv.text = card.Level.ToString();
		//todo
	}
	
    /// <summary>
    /// ������� ִ�еķ���(������)
    /// </summary>
    /// <param name="go"></param>
    void OnCardIndiClick(GameObject go)
    {
        //DebugUtil.Info("OnCardIndiClick");

        if (ChoosenFlag.activeInHierarchy) //����ǵ�һ�ε㿨��
        {
            if (ChoosenFlag.transform.parent.gameObject==gameObject) //���ϴε����ͬһ�ţ���������
            {
                DebugUtil.Info("same object. do nothing");

                return; 
            }
            else  //���ϴε����ͬ
            {
                GameObject LastCard = ChoosenFlag.transform.parent.gameObject; //��ȡ�ϴε������

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
        else //��һ�ε������
        {
            DebugUtil.Info("first click  " );

            if (gameObject.name.Contains("Using"))
            {
                _flagShorten = true;
            }

            ChoosenFlag.SetActive(true); //��ʾ����
        }

        ChangestoFlag(ChoosenFlag);

        ChangestoChoosenCard();

    }

    /// <summary>
    /// �����ϴε���Ŀ��ƣ�ԭ������ı�Y���꣬3����Ϸ����ͼ���20��������Ƭ��ʾ
    /// </summary>
    /// <param name="_lastCard"></param>
    void ReSetLastCard(GameObject _lastCard)
    {
        _lastCard.transform.Find("S_Card").GetComponent<TweenPosition>().PlayReverse();

        _lastCard.transform.Find("S_CardExp_Frame").gameObject.SetActive(true);//��ʾ��Ƭ

        _lastCard.transform.Find("S_Card").GetComponent<UISprite>().depth -= 20; //ͼ���λ

        _lastCard.transform.Find("S_Card/S_Card_Frame").GetComponent<UISprite>().depth -= 20;

        _lastCard.transform.Find("S_Card/S_Card_Lv").GetComponent<UISprite>().depth -= 20;
    }

    /// <summary>
    /// ���ı䳤���ӽ�����ť
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
    /// 1�����ı��354��300��Y��-45��-19��2.ȡ��������ťS_Replace_Btn��3.��Ϣ��ť���ƣ���-54��-86
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
    /// ��ѡ�п�Ƭ�����ĸı�
    /// </summary>
    void ChangestoChoosenCard()
    {
        _part_fragment.SetActive(false); //������Ƭ

        _cardMain.depth += 20;//ͼ������20

        _cardFrame.depth += 20;

        _cardlvStar.depth += 20;

        mTweenScale.ResetToBeginning();//���㿨�Ʊ�С�ٱ��Ҫ����

        mTweenScale.PlayForward();

        mTweenPosition.PlayForward();//���㿨��λ������ֻҪ��һ��
    }

    /// <summary>
    /// ��Ϊѡ�п����Ƿ��ս�����ĳ���/�Ƿ���ֽ�����ť
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

        _flag.transform.SetParent(gameObject.transform);//ָ��������

        _flag.GetComponent<TweenPosition>().PlayForward();//��TP����λ��

        _flag.GetComponent<TweenScale>().ResetToBeginning();//���㿨�Ʊ������ı�С�ٱ��Ҫ����

        _flag.GetComponent<TweenScale>().PlayForward();
    }

 

}
