using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyFrameWork;
using UnityEngine.SceneManagement;
using FightNew;

public class TutorialExector : MonoBehaviour
{
    public enum ExecuteStatus
    {
        None,
        Progressing,
        End
    }

    public string ExecrorName { get; set; }

    public int GuideID { get; set; }
    public ExecuteStatus Status { get; private set; }

    protected string[] _stepLines;

    protected TutorialManager _TutorialManager;

    protected bool _canNext = false;

    public delegate void GuideEvent(object sender, object args);

    public event GuideEvent OnComplete;

    public int _pc = -1;

    public string _currentLine;

    void Update()
    {
        if (_canNext)
            Next();
    }

    void Start()
    {
        _Variables = new Dictionary<string, object>();
        Status = ExecuteStatus.None;
        _pc = -1;
        _TutorialManager = AppFacade.Instance.GetManager<TutorialManager>(ManagerName.Tutorial);
        _currentLine = "";
        Next();
    }

    public TutorialExector()
    {
    }

    public void Execute(string script)
    {
        if (Status != ExecuteStatus.None) return;
        _stepLines = script.Replace("\r", "").Split('\n');
        Status = ExecuteStatus.Progressing;
        _canNext = true;
    }

    public void AddEnvironment(string name, object value)
    {
        _Variables[name] = value;
    }

    public void Abort()
    {
        //中止
        foreach (var v in _Variables.Values)
        {
            if (v is GameObject)
            {
                GameObject go = v as GameObject;
                ResetLayer(go);
            }
        }

        HideTips();
        HidePointer();

        _waitItemID = 0;
        _waitObjectPath = null;
        _waitPopupType = 0;
        _waitValueField = null;
        _waitValueTarget = null;

        if (_waitClickTarget)
        {
            WaitClickHandle(_waitClickTarget);
        }

        _pc = _stepLines.Length;
        _canNext = true;

        //记录剩余次数
        TutorialData data = _TutorialManager.GetDataByID(GuideID);
        if (data != null && data.bAfterExeSub)
        {
            string account = Util.CallMethod("UILoginCtrl", "GetAccount")[0].ToString();
            int timeleft = PlayerPrefs.GetInt(account + GuideID.ToString(), -1);
            if(timeleft != -1)
            {
                timeleft -= 1;
                if (timeleft < 0)
                    timeleft = 0;
                PlayerPrefs.SetInt(account + GuideID.ToString(), timeleft);
            }
            else
            {
                timeleft = data.ExeTimes - 1;
                if (timeleft < 0)
                    timeleft = 0;
                PlayerPrefs.SetInt(account + GuideID.ToString(), timeleft);
            }
        }
    }

    protected Dictionary<string, object> _Variables;

    internal object GetVariable(string name)
    {
        if (_Variables.ContainsKey(name)) return _Variables[name];
        return null;
    }

    internal void SetVariable(string name, object value)
    {
        _Variables[name] = value;
    }
    #region 解释器用的方法
    protected void GuideComplete()
    {
        if (OnComplete != null)
            OnComplete(this, null);
        Status = ExecuteStatus.End;
        //记录剩余次数
        TutorialData data = _TutorialManager.GetDataByID(GuideID);
        if (data != null && data.bAfterExeSub)
        {
            string account = Util.CallMethod("UILoginCtrl", "GetAccount")[0].ToString();
            int timeleft = PlayerPrefs.GetInt(account + GuideID.ToString(), -1);
            if (timeleft != -1)
            {
                timeleft -= 1;
                if (timeleft < 0)
                    timeleft = 0;
                PlayerPrefs.SetInt(account + GuideID.ToString(), timeleft);
            }
            else
            {
                timeleft = data.ExeTimes - 1;
                if (timeleft < 0)
                    timeleft = 0;
                PlayerPrefs.SetInt(account + GuideID.ToString(), timeleft);
            }
        } 
    }

    protected void Next()
    {
        _pc++;

        if (_pc >= _stepLines.Length)
        {
            _currentLine = "";
            GuideComplete();
            return;
        }
        try
        {
            _currentLine = _stepLines[_pc];
            ParseLine();
        }
        catch (System.Exception ex)
        {
            Debug.Log("引导脚本错误：" + ex.Message + " 脚本：" + ExecrorName + " 行：" + _pc.ToString() + " " + _stepLines[_pc]);
        }
    }
    protected void ParseLine()
    {
        var line = _stepLines[_pc].Trim();
        if (line.Length == 0) return;

        if (line.StartsWith("//") || line.StartsWith("\\\\")) return;

        if (Regex.IsMatch(line, @"\$.+?=.+"))
        {
            ParseAssignment(line);
            return;
        }

        if (line.StartsWith("#"))
        {
            ParseControl(line);
            return;
        }

        ParseStatement(line);
    }

    protected void ParseAssignment(string line)
    {
        var match = Regex.Match(line, @"^(?<left>\$.+?)=(?<right>.+)$");
        var left = match.Groups["left"].Value.Trim();
        var right = match.Groups["right"].Value.Trim();
        SetVariable(left.ToLower(), ParseStatement(right));
    }

    protected void ParseControl(string line)
    {

    }

    protected object ParseStatement(string statement)
    {
        if (statement.StartsWith("$"))
            return GetVariable(statement.ToLower());

        if (statement.StartsWith("\"") && statement.EndsWith("\""))    //字符串   
        {
            if (statement.Length == 2) return "";
            return statement.Substring(1, statement.Length - 2).Replace("\\n", "\n").Replace("\\\"", "\"").Replace("@","\n").Replace("#"," ");
        }

        if (Regex.IsMatch(statement, @"^[\+\-0-9]+.*$")) //数字
        {
            if (statement.IndexOf('.') > -1)
                return float.Parse(statement);
            else
                return int.Parse(statement);
        }

        var match = Regex.Match(statement, @"^(?<name>\S+?)\((?<param>.*)\)$");
        if (match != null) //方法
        {
            var funcName = match.Groups["name"].Value.Trim();
            var paramStr = match.Groups["param"].Value.Trim();

            string[] parms = null;
            if (paramStr.Length > 0)
            {
                var paramMatch = Regex.Matches(paramStr, @"\"".*?\""|[^\,\(\)]+(\(.*\))?");
                parms = new string[paramMatch.Count];
                for (var i = 0; i < parms.Length; i++)
                {
                    parms[i] = paramMatch[i].Value;
                }
            }

            return CallFunction(funcName, parms);
        }

        //常量
        return GetVariable(statement.ToLower());
    }

    protected object CallFunction(string name, string[] parms)
    {
        switch (name.ToLower())
        {
            case "getgameobject":
                if (parms.Length == 1)
                {
                    return GetGameObject((string)ParseStatement(parms[0]));
                }
                if (parms.Length == 2)
                {
                    return GetGameObject((GameObject)ParseStatement(parms[0]), (string)ParseStatement(parms[1]));
                }
                break;
            case "getdefaultobject":
                return _TutorialManager._defaultObject;
            case "getvalue":
                return GetValue(ParseStatement(parms[0]), (string)ParseStatement(parms[1]));
            case "setvalue":
                SetValue(ParseStatement(parms[0]), (string)ParseStatement(parms[1]), ParseStatement(parms[2]));
                return null;
            case "showpointer":
                return ShowPointer((GameObject)ParseStatement(parms[0]), (int)ParseStatement(parms[1]));
            case "hidepointer":
                if (parms == null || parms.Length == 0)
                    HidePointer();
                else
                    HidePointer((GameObject)ParseStatement(parms[0]));
                break;
            case "showtip":
                return ShowTips((GameObject)ParseStatement(parms[0]), (int)ParseStatement(parms[1]), (string)ParseStatement(parms[2]));
            case "showtipex":
                return ShowTipsEx((GameObject)ParseStatement(parms[0]), (int)ParseStatement(parms[1]), (string)ParseStatement(parms[2]), (int)ParseStatement(parms[3]), (int)ParseStatement(parms[4]), (int)ParseStatement(parms[5]), (int)ParseStatement(parms[6]));
            case "hidetip":
                if (parms == null || parms.Length == 0)
                    HideTips();
                else
                    HideTips((GameObject)ParseStatement(parms[0]));
                break;
            case "bling":
                Bling((GameObject)ParseStatement(parms[0]));
                break;
            case "showmask":
                ShowMask();
                break;
            case "showmaska":
                ShowMaskA((float)ParseStatement(parms[0]));
                break;
            case "hidemask":
                HideMask();
                break;
            case "waitdragsuccess":
                WaitDragSuccess((GameObject)ParseStatement(parms[0]));
                break;
            case "waitclick":
                if (parms == null || parms.Length == 0)
                    WaitClick();
                else
                    WaitClick((GameObject)ParseStatement(parms[0]));
                break;
            case "waitclickandsetlayer":
                if (parms == null || parms.Length == 0)
                    WaitClick();
                else
                    WaitClick((GameObject)ParseStatement(parms[0]), true);
                break;
            case "click":
                Click((GameObject)ParseStatement(parms[0]));
                break;
            case "waitpress":
                if (parms == null || parms.Length == 0)
                    WaitPress();
                else
                    WaitPress((GameObject)ParseStatement(parms[0]));
                break;
            case "waitpopup":
                _waitPopupType = (int)ParseStatement(parms[0]);
                WaitPopup();
                break;
            case "waitclose":
                _waitPopupType = (int)ParseStatement(parms[0]);
                WaitClose();
                break;
            case "waitseconds":
                WaitSceonds((float)ParseStatement(parms[0]));
                break;
            case "waitvalue":
                _waitValueTarget = ParseStatement(parms[0]);
                _waitValueField = (string)ParseStatement(parms[1]);
                WaitValue();
                break;
            case "waitgameobject":
                _waitObjectPath = (string)ParseStatement(parms[0]);
                WaitGameObject();
                break;
            case "setlayer":
                SetLayer((GameObject)ParseStatement(parms[0]));
                break;
            case "resetlayer":
                ResetLayer((GameObject)ParseStatement(parms[0]));
                break;
            case "waititem":
                _waitItemID = System.Convert.ToUInt32(ParseStatement(parms[0]));
                WaitItem();
                break;
            case "waitwave":
                break;
            case "startguide":
                StartGuide(System.Convert.ToInt32(ParseStatement(parms[0])));
                break;
            case "pause":
                Pause();
                break;
            case "play":
                Play();
                break;
            case "show":
                Show((GameObject)ParseStatement(parms[0]));
                break;
            case "hide":
                Hide((GameObject)ParseStatement(parms[0]));
                break;
            case "open":
                Open(System.Convert.ToInt32(ParseStatement(parms[0])));
                break;
            case "close":
                Close();
                break;
            case "showskip":
                _TutorialManager.ShowSkip(this);
                break;
            case "hideskip":
                _TutorialManager.HideSkip();
                break;
            case "signend":
                _TutorialManager.SignEnd(this);
                break;
            case "enabledrag":
                if (parms.Length > 1)
                    EnableDrag(ParseStatement(parms[0]) as GameObject, System.Convert.ToInt32(ParseStatement(parms[1])) == 1);
                else
                    EnableDrag(ParseStatement(parms[0]) as GameObject, true);
                break;
            default:
                throw (new System.Exception("不能识别的方法名"));
        }

        return null;
    }

    #endregion

    #region 解释器用支持的方法
    protected void EnableDrag(GameObject go, bool enable)
    {
//         var dragPanelContents = go.GetComponent<UIDragPanelContents>();
//         if (dragPanelContents)
//         {
//             dragPanelContents.enabled = enable;
//         }
//         else
//         {
//             var dragPanel = go.GetComponent<UIDraggablePanel>();
//             if (dragPanel)
//             {
//                 dragPanel.enabled = enable;
//             }
//         }
    }

    protected void ShowSkip()
    {
        _TutorialManager.SkipButton.SetActive(true);
    }

    protected void HideSkip()
    {
        _TutorialManager.SkipButton.SetActive(false);
    }

    protected void Open(int popupType)
    {
        //_TutorialManager.ShowPopup(popupType);
    }

    protected void Close()
    {
        //_TutorialManager.ClosePopup();
    }

    protected GameObject GetGameObject(string path)
    {
        var go = GameObject.Find(path); //transform.Find(path).gameObject;
        return go;
    }

    protected GameObject GetGameObject(GameObject parent, string path)
    {
        if (path.StartsWith("/"))
            return GetGameObject(path);
        var trs = parent.transform.Find(path);
        return trs ? trs.gameObject : null;
    }

    protected object GetValue(object obj, string proName)
    {
        var field = obj.GetType().GetField(proName);
        if (field != null)
        {
            return field.GetValue(obj);
        }
        var pro = obj.GetType().GetProperty(proName);
        if (pro != null)
            return pro.GetValue(obj, null);
        return null;
    }

    protected void SetValue(object obj, string proName, object value)
    {
        var field = obj.GetType().GetField(proName);
        if (field != null)
        {
            field.SetValue(obj, value);
            return;
        }
        var property = obj.GetType().GetProperty(proName);
        if (property != null)
        {
            property.SetValue(obj, value, null);
            return;
        }
    }

    protected string GetGameObjectPath(GameObject go)
    {
        string path = "/" + go.name;
        while (go.transform.parent)
        {
            go = go.transform.parent.gameObject;
            path = "/" + go.name + path;
        }
        return path;
    }

    protected void WaitSceonds(float secs)
    {
        _canNext = false;
        Invoke("WaitSecondsHandle", secs);
    }

    protected void WaitSecondsHandle()
    {
        _canNext = true;
    }

    private GameObject _waitDragSuccessSlot;
    
    protected void WaitDragSuccess(GameObject target)
    {
        _canNext = false;
        _waitDragSuccessSlot = target;
        TouchMgr.Instance.AfterMoveHandler = WaitDragSuccessHandle;
    }

    protected void WaitDragSuccessHandle(Slot target)
    {
        TouchMgr.Instance.AfterMoveHandler = null;
        _waitDragSuccessSlot = null;
        _canNext = true;
    }

    private GameObject _waitClickTarget;

    protected void WaitClick()
    {
        if (!_TutorialManager.Mask.activeSelf) return;
        WaitClick(_TutorialManager.Mask);
    }

    protected void Click(GameObject target)
    {
        target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
    }

    protected void WaitClick(GameObject target)
    {
        _canNext = false;
        _waitClickTarget = target;
        target.AddComponent<TutorialClickComponent>();
        target.GetComponent<TutorialClickComponent>().Click += WaitClickHandle;
    }

    protected void WaitClick(GameObject target, bool setLayer)
    {
        WaitClick(target);
        if (setLayer)
            if (target.GetComponent<TutorialLayerComponent>() == null)
                target.AddComponent<TutorialLayerComponent>();
    }

    protected void WaitPress()
    {
        if (!_TutorialManager.Mask.activeSelf) return;
        WaitPress(_TutorialManager.Mask);
    }

    protected void WaitPress(GameObject target)
    {
        _canNext = false;
        _waitClickTarget = target;
        target.AddComponent<TutorialClickComponent>();
        target.GetComponent<TutorialClickComponent>().Press += WaitClickHandle;
    }

    protected void WaitPressHandle(GameObject target)
    {
        target.GetComponent<TutorialClickComponent>().Press -= WaitPressHandle;
        Destroy(target.GetComponent<TutorialClickComponent>());
        _waitClickTarget = null;
        _canNext = true;
    }

    protected void WaitClickHandle(GameObject target)
    {
        target.GetComponent<TutorialClickComponent>().Click -= WaitClickHandle;
        Destroy(target.GetComponent<TutorialClickComponent>());
        _waitClickTarget = null;
        _canNext = true;
    }

    protected GameObject ShowPointer(GameObject target, int pointerType)
    {
        return _TutorialManager.ShowPointer(target, pointerType);
    }

    protected void HidePointer()
    {
        _TutorialManager.HidePointer();
    }

    protected void HidePointer(GameObject pointer)
    {
        _TutorialManager.HidePointer(pointer);
    }

    protected GameObject ShowTips(GameObject target, int type, string tipStr)
    {
        return _TutorialManager.ShowTips(target, type, tipStr);
    }

    protected GameObject ShowTipsEx(GameObject target, int type, string tipStr, int offsetX, int offsetY, int width, int height)
    {
        return _TutorialManager.ShowTipsEx(target, type, tipStr, offsetX, offsetY, width, height);
    }

    protected void HideTips()
    {
        _TutorialManager.HideTips();
    }

    protected void HideTips(GameObject tips)
    {
        _TutorialManager.HideTips(tips);
    }

    protected void Bling(GameObject obj)
    {
        _TutorialManager.Bling(obj);
    }

    protected void ShowMask()
    {
        _TutorialManager.ShowMask();
    }

    protected void ShowMaskA(float alpha)
    {
        _TutorialManager.ShowMaskA(alpha);
    }

    protected void HideMask()
    {
        _TutorialManager.HideMask();
    }

    protected void Pause()
    {
        //GameOption.IsAppPause = true;
        Time.timeScale = 0;
    }

    protected void Play()
    {
        //GameOption.IsAppPause = false;
        Time.timeScale = 1;
    }

    protected void SetLayer(GameObject go)
    {
        if (go.GetComponent<TutorialLayerComponent>() == null)
            go.AddComponent<TutorialLayerComponent>();
        Next();
    }

    protected void ResetLayer(GameObject go)
    {
        if (go.GetComponent<TutorialLayerComponent>())
        {
            go.GetComponent<TutorialLayerComponent>().Reset();
            Destroy(go.GetComponent<TutorialLayerComponent>());
        }
    }

    protected void StartGuide(int guideID)
    {
        _TutorialManager.StartGuide(guideID);
    }
    private uint _waitItemID;

    protected void WaitItem()
    {
//         if (_waitItemID > 0)
//             if (GameInven.Instance.GetItemCount(_waitItemID) > 0)
//             {
//                 _waitItemID = 0;
//                 _canNext = true;
//             }
//             else
//             {
//                 _canNext = false;
//                 Invoke("WaitItem", 0.1f);
//             }
    }

    private int _waitPopupType;

    protected void WaitPopup()
    {
//         if (_waitPopupType > 0)
//             if (_TutorialManager.GetPopup(_waitPopupType).activeSelf)
//             {
//                 _waitPopupType = 0;
//                 _canNext = true;
//             }
//             else
//             {
//                 _canNext = false;
//                 Invoke("WaitPopup", 0.1f);
//             }
    }

    protected void WaitClose()
    {
//         if (_waitPopupType > 0)
//             if (!_TutorialManager.GetPopup(_waitPopupType).activeSelf)
//             {
//                 _waitPopupType = 0;
//                 _canNext = true;
//             }
//             else
//             {
//                 _canNext = false;
//                 Invoke("WaitClose", 0.1f);
//             }
    }

    private string _waitObjectPath;

    protected void WaitGameObject()
    {
        if (!string.IsNullOrEmpty(_waitObjectPath))
            if (GameObject.Find(_waitObjectPath))
            {
                _waitObjectPath = null;
                _canNext = true;
            }
            else
            {
                _canNext = false;
                Invoke("WaitGameObject", 0.1f);
            }
    }

    protected object _waitValueTarget;
    protected string _waitValueField;

    protected void WaitValue()
    {
        if (_waitValueTarget != null && !string.IsNullOrEmpty(_waitValueField))
            if (GetValue(_waitValueTarget, _waitValueField) != null)
            {
                _waitValueTarget = null;
                _waitValueField = null;
                _canNext = true;
            }
            else
            {
                _canNext = false;
                Invoke("WaitValue", 0.1f);
            }
    }

//     protected GameEntity _waitHPTarget;
//     protected float _waitHPValue;
// 
//     protected void WaitHP()
//     {
//         if (_waitHPTarget)
//             if (_waitHPTarget.HP <= _waitHPValue)
//             {
//                 _waitHPValue = 0;
//                 _waitHPTarget = null;
//                 _canNext = true;
//             }
//             else
//             {
//                 _canNext = false;
//                 Invoke("WaitHP", 0.1f);
//             }
//     }
// 
//     protected void WaitHPPercent()
//     {
//         if (_waitHPTarget)
//             if (_waitHPTarget.HP / _waitHPTarget.MaxHP <= _waitHPValue)
//             {
//                 _waitHPValue = 0;
//                 _waitHPTarget = null;
//                 _canNext = true;
//             }
//             else
//             {
//                 _canNext = false;
//                 Invoke("WaitHPPercent", 0.1f);
//             }
//     }

    protected void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected void Show(GameObject obj)
    {
        obj.SetActive(true);
    }
    #endregion
}
