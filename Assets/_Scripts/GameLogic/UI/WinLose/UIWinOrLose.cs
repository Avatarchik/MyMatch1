/*******************************************************
 * 
 * 文件名(File Name)：             UIWinOrLose
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/15 19:53:32
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using MyFrameWork;

public class UIWinOrLose : BaseUI 
{

	private UILabel _labelWinStar;
	private UILabel _labelWinGold;

	private UILabel _labelLooseStar;
	private UILabel _labelLooseGold;

	protected override void OnInit()
	{
		base.OnInit();

		mUIStyle = E_UIStyle.PopUp;
		mUILayertype = E_LayerType.NormalUI;
	}
    private WinOrLose _winOrLose = null;
    private WinOrLose winOrLose
    {
        get
        {
            if (_winOrLose == null)
            {
                _winOrLose = GetComponent<WinOrLose>();
            }

            return _winOrLose;
        }
        
    }


	public void ShowWinOrLoose(bool isWin,int jiangBei,int money)
	{
        int myMedel = ModuleMgr.Instance.Get<FightNew.ModuleFight>().MyGetCup;
		int oppMedel = ModuleMgr.Instance.Get<FightNew.ModuleFight>().OppGetCup;
        if (isWin)
		{
			winOrLose.ShowWinPanel(jiangBei, money, myMedel,oppMedel);
			//_labelWinStar.text = string.Format("+{0}",jiangBei);
			//_labelWinGold.text = string.Format("+{0}",money);
		}
		else
		{
			winOrLose.ShowLoosePanel(jiangBei, money, myMedel, oppMedel);
			//_labelLooseStar.text = jiangBei.ToString();
			//_labelLooseGold.text = money.ToString();
		}
	}

	protected override void OnBtnClick(GameObject go)
	{
		switch (go.name)
		{
			case "Sprite_ConfirmLoose":
			case "Sprite_ConfirmWin":
                if (FightNew.FightMgr.Instance.PlayRankChange)
                {
                    UIMgr.Instance.ShowUI(E_UIType.PanelRankChange, typeof(UIRankChange));
                    UIMgr.Instance.DestroyUI(E_UIType.UIWinOrLosePanel);
					UIMgr.Instance.HideUI(E_UIType.Fight);
                    FightNew.FightMgr.Instance.PlayRankChange = false;//标志位归位
                }
                else
                {
                    FightNew.FightMgr.Instance.ClearLevel();
                    UnityEngine.SceneManagement.SceneManager.LoadScene((int)SenceName.SENCE_MAIN);
                    Util.CallMethod("Game", "ShowUIMain"); //初始化完成
//					FightNew.FightMgr.Instance.IsInFightScene = false;
                }
				
                
                break;
			default:
				break;
		}
	}
}
