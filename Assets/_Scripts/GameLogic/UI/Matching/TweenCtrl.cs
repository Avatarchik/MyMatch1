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

	#region boss������Ч
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

        //�ҵ�����
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject.Find("UIMatchingPanel").transform.localPosition = Vector3.zero;

            //�ڵ���
            GameObject.Find("Sprite_Bg_Mask2").GetComponent<UISprite>().enabled = true; 
        }

        //�л���ս������
        if (Input.GetKeyDown(KeyCode.F))
        {
            FindOpponent();
        }

        //���¼���
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
#endif
        //�Ŵ�תȦ
        if (Enlarger.activeInHierarchy)
        {
            //�ֹ�ת������ת
            Enlarger.transform.RotateAround(Enlarger.transform.parent.position, Vector3.forward, -2);

            //����ת
            Enlarger.transform.localRotation = Quaternion.identity;

            
        }


    }

    /// <summary>
    /// �ҵ�ƥ����֣�1.����ʧ���Ŵ���ʧ��2.�ҷ������ַ���Ļ����
    /// </summary>
    public void FindOpponent()
    {
		//OppenentName.text = MyFrameWork.ModuleMgr.Instance.Get<ModuleUIMain>().mOpponent.nick;

        Line.SetActive(false);

        Circle.SetActive(false);

        gameObject.transform.localPosition = Vector3.zero; //ȷ�� Mactch��������Ļ��

        GameObject.Find("Sprite_Bg_Mask2").GetComponent<UISprite>().enabled = true;//ȷ���ڵ���mask2��ʾ


        Matching.GetComponent<TweenPosition>().enabled = true;

        Matched.GetComponent<TweenPosition>().enabled = true;


    }

    /// <summary>
    /// ��ѹ���м䣬1.�����Ч�͡���ս�����2.��Ļ������0.3���ֹͣ ; ����ص��������� Matched ����������
    /// </summary>
    public void ShowWords()
    {

        
        Effects.SetActive(true);

        AllPos = All.transform.position; //��¼����ǰλ��

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
    /// ����ս�����ֺ�1�룬1.����ս������ЧЧ����ʧ��2.���ַ����Ʋ�����3.�ҷ������ַ�������Ϣ����
    /// </summary>
    void StartFight()
    {
        //Effects.SetActive(false);

        //All.GetComponent<TweenTransform>().enabled = true; //All���ƶ�

        Cards.transform.parent = gameObject.transform;
        

        GameObject.Find("Sprite-halfBlackMask").GetComponent<UISprite>().enabled=true;

        All.SetActive(false);

        GameObject.Find("ScrollView_Panel").SetActive(false);

        GameObject.Find("Top_Bottom_Panel").SetActive(false);

        
       
    }

    /// <summary>
    /// ����ֹͣ
    /// </summary>
    void StopTremble()
    {
        
        All.GetComponent<TweenPosition>().enabled = false;

        //All.transform.position = AllPos;//��ԭ������ǰλ��

        All.transform.position = Vector3.zero; 
    }

    /// <summary>
    /// ��ʱ��������������ʤ��/ʧ�ܽ���
    /// </summary>
    public void ToUIWinPanel()
    {
        WinUI.GetComponent<TweenPosition>().enabled=true;
        
    }

}
