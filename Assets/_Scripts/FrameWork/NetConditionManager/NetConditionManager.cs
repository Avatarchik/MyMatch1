using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFrameWork;

class NetConditionManager : Manager
{
    public void ShowNetCondition()
    {
        AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).ShowUI(E_UIType.UINetConditionPanel, typeof(BaseUI));
    }

    public void HideNetCondition()
    {
        AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).HideUI(E_UIType.UINetConditionPanel);
    }
}
