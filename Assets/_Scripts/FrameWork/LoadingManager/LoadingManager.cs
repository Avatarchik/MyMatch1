using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFrameWork;
using UnityEngine;

public class LoadingManager : Manager
{
    private float fPercent = 0f;
    private UILabel PercentLab = null;
    private bool bStartUpdate = false;
    public AsyncOperation CurLoadingScene = null;
    public void ShowLoading()
    {
        AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).ShowUI(E_UIType.UILoadingPanel,typeof(BaseUI));
        fPercent = 0f;
    }

    public void HideLoading()
    {
        AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).HideUI(E_UIType.UILoadingPanel);
        CurLoadingScene = null;
        bStartUpdate = false;
    }

    public void GetPercentLabel()
    {
       BaseUI ui =  AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).GetUIByType(E_UIType.UILoadingPanel);
        if (ui != null && ui.transform.FindChild("Percent") != null)
            PercentLab = ui.transform.FindChild("Percent").GetComponent<UILabel>();
    }

    public void SetUpdateProgress(float per)
    {
        bStartUpdate = true;
        fPercent = per;
    }
    void Update()
    {
        if (CurLoadingScene != null)
        {
            if (PercentLab == null)
                GetPercentLabel();
            if (PercentLab != null)
                PercentLab.text = ((int)(CurLoadingScene.progress * 100f)) + "%";
        }
        else if(bStartUpdate == true)
        {
            if (PercentLab == null)
                GetPercentLabel();
            if (PercentLab != null)
                PercentLab.text = ((int)(fPercent * 100f)) + "%";
        }
    }
}
