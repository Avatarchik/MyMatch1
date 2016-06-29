using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFrameWork;
using UnityEngine;
using LuaInterface;

/// <summary>
/// RegisterDelegation() 总注册方法，在Lua的UIMainCtrl.InitPanel() 调用
/// </summary>
public class LuaUtilityManager : Manager
{
    public UISprite mUISprite;
    public bool startPosFlag = true;
    public Vector3 MouseDownStartPos = new Vector3();
    private UIScrollView mUIScrollView = null;
    private UIScrollView sUIScrollView = null;
    private bool _isMakingFlyingsExp;
    private bool _isMakingFlyingsGold;
    private bool _isMakingFlyingsTrophy;
    private int _expCreated;
    private int _goldCreated;
    private int _trophyCreated;
    private float _tempTimerExp;
    private float _tempTimerGold;
    private float _tempTimerTrophy;
    private GameObject _goExp;
    private GameObject _goExpFrom;
    private GameObject _goExpTo;
    private int _numsExp;
    private GameObject _goGold;
    private GameObject _goGoldFrom;
    private GameObject _goGoldTo;
    private int _numsGold;
    private GameObject _goTrophy;
    private GameObject _goTrophyFrom;
    private GameObject _goTrophyTo;
    private int _numsTrophy;


    /// <summary>
    /// 添加子节点
    /// </summary>
    public GameObject AddChildToTarget(GameObject child, Transform target,int idx)
    {
        GameObject obj = GameObject.Instantiate(child);
        obj.transform.parent = target;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.name = idx.ToString();
        return obj;
    }

    /// <summary>
    /// 添加子节点
    /// </summary>
    public GameObject AddChildToPos(GameObject child, Transform target, int idx,int cellHeight)
    {
        GameObject obj = GameObject.Instantiate(child);
        obj.transform.parent = target;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(0, -cellHeight * (idx - 1), 0);
        obj.name = idx.ToString();
        return obj;
    }

    public void Des(LuaTable objectTable)
    {
        if(objectTable != null)
        {
            object[] list = objectTable.ToArray();
            foreach (var item in list)
            {
                if(item != null)
                    GameObject.DestroyImmediate(item as GameObject);
            }
        }
    }

    //在Lua的UIMainCtrl.InitPanel() 调用
    public void RegisterDelegation()
    {
        //总的横向Scrollview
        if (mUIScrollView == null)
            mUIScrollView = GameObject.Find("ScrollView_Panel").GetComponent<UIScrollView>();

        mUIScrollView.onDragFinished += Lua_IconChange_HideTop;


        //排名Scrollview
        if (sUIScrollView == null)
            sUIScrollView = GameObject.Find("Scroll View_Rank").GetComponent<UIScrollView>();

        sUIScrollView.onDragStarted += AdjustFingerMove;

        mUIScrollView.onDragStarted += AdjustFingerMove;

        mUIScrollView.onDragFinished += Regain_RankUIScrollView;

        sUIScrollView.onDragFinished += Regain_WholeUIScrollView;
    }
    
    void Lua_IconChange_HideTop()
    {
        Util.CallMethod("UIMainCtrl", "IconChange_HideTop");
    }

    public void UpdateTouchMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownStartPos = Input.mousePosition;
			DebugUtil.Info("GetMouseDownPos:" + MouseDownStartPos.ToString());
            //Debug.Log(GameObject.Find("ScrollView_Panel").GetComponent<UIScrollView>().horizontalScrollBar.value);
        } 
    }

    public void AdjustFingerMove()
    {
        object[] result = Util.CallMethod("UIMainCtrl", "GetPageNum");
        if (result.Length > 0 && int.Parse(result[0].ToString()) != 5)
            return;

        Vector3 nowFingerPos = Input.mousePosition;

        Vector3 vecMoveDir = nowFingerPos - MouseDownStartPos;
        vecMoveDir = vecMoveDir.normalized;
        Vector3 UpDir = Vector3.up;
        float dotvalue = Vector3.Dot(vecMoveDir, UpDir);
        if (dotvalue < 0.70f && dotvalue > -0.70f)//水平拖拽
        {
			DebugUtil.Info(dotvalue.ToString());
			DebugUtil.Info("水平拖拽！激活横向ScrollView！！");
            Forbid_RankUIScrollView();
            Regain_WholeUIScrollView();

        }
        else//竖直拖拽
        {
			DebugUtil.Info(dotvalue.ToString());
			DebugUtil.Info("竖直拖拽！激活纵向ScrollView！！");
            Regain_RankUIScrollView();
            Forbid_WholeUIScrollView();
        }
    }

//     public void AdjustFingerMove()
//     {
//         if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
//         {
//             Vector3 startFingerPos = new Vector3();
// 
//             if (Input.GetMouseButtonDown(0) && startPosFlag == true)
//             {
//                 Debug.Log("======开始触摸=====");
// 
//                 startFingerPos = Input.mousePosition;
// 
//                 startPosFlag = false;
//             }
// 
//             if (Input.GetMouseButtonUp(0))
//             {
//                 Debug.Log("======释放触摸=====");
//                 startPosFlag = true;
//                 Regain_RankUIScrollView();
//                 Regain_WholeUIScrollView();
//             }
// 
//             Vector3 nowFingerPos = Input.mousePosition;
// 
//             Vector3 vecMoveDir = nowFingerPos - startFingerPos;
//             vecMoveDir = vecMoveDir.normalized;
//             Vector3 UpDir = Vector3.up;
//             float dotvalue = Vector3.Dot(vecMoveDir, UpDir);
//             if (dotvalue < 0.70f && dotvalue > -0.70f)//水平拖拽
//             {
//                 Debug.Log(dotvalue.ToString());
//                 Debug.Log("水平拖拽！激活横向ScrollView！！");
//                 Forbid_RankUIScrollView();
//                 Regain_WholeUIScrollView();
// 
//             }
//             else//竖直拖拽
//             {
//                 Debug.Log(dotvalue.ToString());
//                 Debug.Log("竖直拖拽！激活纵向ScrollView！！");
//                 Regain_RankUIScrollView();
//                 Forbid_WholeUIScrollView();
//             }
//         }
//     }

    /// <summary>
    /// 禁用排名滑动窗口
    /// </summary>
    void Forbid_RankUIScrollView()
    {
        if (sUIScrollView == null)
            sUIScrollView = GameObject.Find("Scroll View_Rank").GetComponent<UIScrollView>();

        sUIScrollView.enabled = false;

		DebugUtil.Info("禁止：排名滑动窗口");
    }

    void Regain_RankUIScrollView()
    {
        if (sUIScrollView == null)
            sUIScrollView = GameObject.Find("Scroll View_Rank").GetComponent<UIScrollView>();

        sUIScrollView.enabled = true;

		DebugUtil.Info("恢复：排名滑动窗口");
    }

    /// <summary>
    /// 禁用整个主界面的滑动窗口
    /// </summary>
    void Forbid_WholeUIScrollView()
    {
        if (mUIScrollView == null)
            mUIScrollView = GameObject.Find("ScrollView_Panel").GetComponent<UIScrollView>();

        mUIScrollView.enabled = false;

		DebugUtil.Info("禁止：主界面滑动窗口");

    }

    void Regain_WholeUIScrollView()
    {
        if (mUIScrollView == null)
            mUIScrollView = GameObject.Find("ScrollView_Panel").GetComponent<UIScrollView>();

        mUIScrollView.enabled = true;

		DebugUtil.Info("恢复：主界面滑动窗口");
    }

    

    void Update()
    {
        if (_isMakingFlyingsExp)
        {
            MakingFlyingS("exp",_goExp,_goExpFrom,_goExpTo,_numsExp);
        }
        if (_isMakingFlyingsGold)
        {
            MakingFlyingS("gold", _goGold, _goGoldFrom, _goGoldTo, _numsGold);
        }
        if (_isMakingFlyingsTrophy)
        {
            MakingFlyingS("trophy", _goTrophy, _goTrophyFrom, _goTrophyTo, _numsTrophy);
        }

    }

    void RecordMousePos()
    {

    }

    void Forbid_Scrollview()
    {

    }

    /// <summary>
    /// 以下三条和匹配界面有关，供lua调用，打开，关闭，加载完毕显示
    /// </summary>
    public void OpenMatchingPanel()
    {
        UIMgr.Instance.ShowUI(E_UIType.PanelMatching, typeof(UIMatching),OnMatchingPanelShowed);
    }

    public void CloseMatchingPanel()
    {
        UIMgr.Instance.DestroyUI(E_UIType.PanelMatching);

        EventDispatcher.TriggerEvent("MatchUICanceled");
    }

    public void DelMatchedInfo()
    {
        EventDispatcher.TriggerEvent("DelMatchedInfo");
    }

    void OnMatchingPanelShowed(BaseUI ui)
    {
        EventDispatcher.TriggerEvent("MatchUIReady");
    }

    /// <summary>
    /// 飞金币,经验，奖杯等
    /// </summary>
    /// <param name="go"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void Flying(string typestr,GameObject toCopy,GameObject from, GameObject to,int n)
    {
       
        switch (typestr)
        {
            case "exp":

                _isMakingFlyingsExp = true;

                _goExp = toCopy;

                _goExpFrom = from;

                _goExpTo = to;

                _numsExp = n;

                break;

            case "gold":

                _isMakingFlyingsGold = true;

                _goGold = toCopy;

                _goGoldFrom = from;

                _goGoldTo = to;

                _numsGold = n;

                break;

            case "trophy":

                _isMakingFlyingsTrophy = true;

                _goTrophy = toCopy;

                _goTrophyFrom = from;

                _goTrophyTo = to;

                _numsTrophy = n;

                break;

            default: DebugUtil.Info("not in 3 flying types");
                break;
        }


    }

    /// <summary>
    /// Update中调用
    /// </summary>
    void MakingFlyingS(string flyingType, GameObject _ToCopy,GameObject _Flyfrom,GameObject _Flyto,int _nums)
    {
        float _temptimer=0;

        int _tempCreated = 0;

        switch (flyingType)
        {
            case "exp":
                _tempTimerExp += Time.deltaTime;
                _temptimer = _tempTimerExp;
                break;
            case "gold":
                _tempTimerGold += Time.deltaTime;
                _temptimer = _tempTimerGold;
                break;
            case "trophy":
                _tempTimerTrophy += Time.deltaTime;
                _temptimer = _tempTimerTrophy;
                break;

            default:
				DebugUtil.Info("flyingType not in 3");
                break;
        }

        if (_temptimer >= 0.1f)
        {
            switch (flyingType)
            {
                case "exp":
                    _tempTimerExp =0;
                    break;
                case "gold":
                    _tempTimerGold = 0;
                    break;
                case "trophy":
                    _tempTimerTrophy = 0;
                    break;
                default:
                    break;
            }

            if (_ToCopy == null)
            {
                DebugUtil.Warning("_ToCopy empty");

                _isMakingFlyingsExp = false;

                _isMakingFlyingsGold = false;

                _isMakingFlyingsTrophy = false;
                return;
            }
           
            GameObject go = Instantiate(_ToCopy);

            go.transform.SetParent(_Flyfrom.transform);

            go.transform.localPosition = Vector3.zero;

            go.transform.localScale = Vector3.one;

            go.GetComponent<FlyingItems>()._flyingTo = _Flyto;

            go.transform.SetParent(_Flyto.transform);

            go.SetActive(true);

            switch (flyingType)
            {
                case "exp":
                    _expCreated++;
                    _tempCreated = _expCreated;
                    break;
                case "gold":
                    _goldCreated++;
                    _tempCreated = _goldCreated;
                    break;
                case "trophy":
                    _trophyCreated++;
                    _tempCreated = _trophyCreated;
                    break;
                default:
                    break;
            }
        }

        if (_tempCreated >= _nums)
        {
            switch (flyingType)
            {
                case "exp":
                    _isMakingFlyingsExp = false;
                    _expCreated = 0;
                    break;
                case "gold":
                    _isMakingFlyingsGold = false;
                    _goldCreated = 0;
                    break;
                case "trophy":
                    _isMakingFlyingsTrophy = false;
                    _trophyCreated = 0;
                    break;
                default:
                    break;
            }
        }
    }

    public void SendGameInfoToSdk(int lv, string userId, string nickName)
    {
        SDKEMAController.Instance.SendGameInfo(lv, userId, nickName);
        //Debug.Log("Testing: " + userId);
    }

    //根据string类参数，读取本地照片文件夹的对应照片
    public void ReadPhoto(string str)
    {
        int i = int.Parse(str);

        GameObject.Find("T_Photo").GetComponent<UITexture>().mainTexture=Resources.Load("Photos/" + i % 6) as UnityEngine.Texture;
        
    }

    /// <summary>
    /// 让Lua调用C#的PlayerPrefs.SetString方法
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_value"></param>
    public void PlayerPrefs_SetString_Lua(string _key,string _value)
    {
        PlayerPrefs.SetString(_key, _value);
    }

    /// <summary>
    /// //让Lua调用C#的PlayerPrefs.GetString方法
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_default_value"></param>
    public string PlayerPrefs_GetString_Lua(string _key, string _default_value)
    {
       return PlayerPrefs.GetString(_key, _default_value);
    }

    /// <summary>
    /// 第一次进游戏的时候打开卡牌界面
    /// </summary>
    public void ShowUICardPanel()
    {
        UIMgr.Instance.ShowUI(E_UIType.UICardPanel, typeof(UICard));
    }
}
    