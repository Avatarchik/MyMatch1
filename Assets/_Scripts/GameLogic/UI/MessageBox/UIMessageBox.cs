using UnityEngine;
using System.Collections;
using MyFrameWork;

public class UIMessageBox : BaseUI
{
    private UILabel lbMsg;
    private UILabel lbCenter;
    private UILabel lbRight;
    private UILabel lbLeft;

    private GameObject btnLeft;
    private GameObject btnRight;
    private GameObject btnCenter;
//    private GameObject btnClose;

    public override E_UIType GetUIType()
    {
        return E_UIType.PanelMessageBox;
    }

    protected override void OnInit()
    {
        base.OnInit();
        mUIStyle = E_UIStyle.PopUp;
        mUILayertype = E_LayerType.Tips;
        _animationStyle = E_UIShowAnimStyle.CenterScaleBigNormal;

        lbMsg = GameUtility.FindDeepChild<UILabel>(this.gameObject, "Message");
        btnCenter = GameUtility.FindDeepChild(this.gameObject, "Btns/CenterBtn").gameObject;
        btnRight = GameUtility.FindDeepChild(this.gameObject, "Btns/RightBtn").gameObject;
        btnLeft = GameUtility.FindDeepChild(this.gameObject, "Btns/LeftBtn").gameObject;
//        btnClose = GameUtility.FindDeepChild(this.gameObject, "Btns/Close").gameObject;
        lbCenter = GameUtility.FindDeepChild<UILabel>(this.gameObject, "Btns/CenterBtn/Label");
        lbLeft = GameUtility.FindDeepChild<UILabel>(this.gameObject, "Btns/LeftBtn/Label");
        lbRight = GameUtility.FindDeepChild<UILabel>(this.gameObject, "Btns/RightBtn/Label");
        ResetWindow();
    }

    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "Close":
                AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).CloseMessageBox();
                break;
        }
    }

    public void ResetWindow()
    {
        // Òþ²ØËùÓÐ°´Å¥
        NGUITools.SetActive(btnCenter, false);
        NGUITools.SetActive(btnLeft, false);
        NGUITools.SetActive(btnRight, false);
    }

    public void SetMsg(string msg)
    {
        lbMsg.text = msg;
    }

    public void SetCenterBtnCallBack(string msg, UIEventListener.VoidDelegate callBack)
    {
        lbCenter.text = msg;
        NGUITools.SetActive(btnCenter, true);
		UIEventListener.Get(btnCenter).onClick = (go)=>
			{
				if(callBack != null)
					callBack(go);

				UIMgr.Instance.HideUI(E_UIType.PanelMessageBox);
			};
    }

    public void SetLeftBtnCallBack(string msg, UIEventListener.VoidDelegate callBack)
    {
        lbLeft.text = msg;
        NGUITools.SetActive(btnLeft, true);
		UIEventListener.Get(btnLeft).onClick = (go)=>
			{
				if(callBack != null)
					callBack(go);

				UIMgr.Instance.HideUI(E_UIType.PanelMessageBox);
			};
    }

    public void SetRightBtnCallBack(string msg, UIEventListener.VoidDelegate callBack)
    {
        lbRight.text = msg;
        NGUITools.SetActive(btnRight, true);
		UIEventListener.Get(btnRight).onClick = (go)=>
			{
				if(callBack != null)
					callBack(go);
				
				UIMgr.Instance.HideUI(E_UIType.PanelMessageBox);
			};
    }
}
