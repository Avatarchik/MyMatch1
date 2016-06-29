#define USE_TUTORIAL

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyFrameWork;
using UnityEngine.SceneManagement;
using LuaInterface;
using FightNew;

public enum TutorialCondition
{
    TC_NONE,
    TC_GAMELEVEL,
}

public class TutorialData
{
    public int ID;
    public string Name { get;  set; }
    public int Scene { get;  set; }
    public int Level { get; set; }
    public int Condition { get;  set; }  // 0  无  1 关卡
    public int Operator { get;  set; } //1 ==    2 >    3 <
    public int ConditionValue { get;  set; }
    public int ExeTimes { get;  set; }
    public bool bAfterExeSub { get; set; }
    public int NextStepID { get; set; }
    public string StartSound { get;  set; }

    public string _script;

    public string Script
    {
        get
        { return _script; }
        set
        {
            _script = value.Replace("\\\"", "\"");
        }
    }
}

public class TutorialManager : Manager
{
    private const int mTutorialLayer = 15;

    private const int mDefaultLayer = 0;

    /// <summary>
    /// 背景遮罩
    /// </summary>
    public GameObject Mask;
    /// <summary>
    /// 背景遮罩
    /// </summary>
    public UISprite MaskPanel;
    /// <summary>
    /// 引导容器
    /// </summary>
    public UIPanel TutorialPanel;
    /// <summary>
    /// 跳过按钮
    /// </summary>
    public GameObject SkipButton;
    //指针
    public GameObject[] PrefabPointers;
    //提示
    public GameObject[] PrefabTips;

    private Dictionary<string, TutorialExector> _guides;
    private Dictionary<int, TutorialData> _dataMap;

    private List<GameObject> _tips;

    private List<GameObject> _pointers;

    private Vector3[] _tipPositions;

    private int CurProcessID = -1;

    public GameObject _defaultObject;
    public E_CardType _defaultCardType;

    public TutorialData GetDataByID(int id)
    {
        return _dataMap[id];
    }
    void Start()
    {
#if USE_TUTORIAL
        _tips = new List<GameObject>();
        _pointers = new List<GameObject>();
        SkipButton.SetActive(false);

        _tipPositions = new Vector3[]{
             new Vector3(0, -360, 0),new Vector3(0, -360, 0) ,
            new Vector3(0, -360, 0),new Vector3(0, -360, 0) ,
            new Vector3(0, -360, 0),new Vector3(0, -360, 0) ,
        };

        var currentScene = SceneManager.GetActiveScene().buildIndex;

        EventDispatcher.AddListener(FightDefine.Event_GameOver, OnGameOverClean);
        EventDispatcher.AddListener(FightDefine.Event_LevelLoadOver, OnCheckCondition0);
        EventDispatcher.AddListener<Slot, E_CardType>(FightDefine.Event_HasNewBomb, OnCheckCondition1);
        EventDispatcher.AddListener<int,int>(FightDefine.Event_HasEnergy, OnCheckCondition2);
#endif
    }

    bool CheckScene(int condition)
    {
        foreach (var data in _dataMap)
        {
            if (data.Value.Condition == condition)
            {
                if (data.Value.Scene == SceneManager.GetActiveScene().buildIndex)
                {
                    if (data.Value.Level == 0 || data.Value.Level == Fight.LevelProfile.NewCurrentLevelProfile.Level)
                    {
                        if (CheckCondition(data.Value.Condition, data.Value.Operator, data.Value.ConditionValue))
                        {
                            StartGuide(data.Key);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    bool CheckCondition(int condition, int oper, int value)
    {
        if (condition == 0) return true;
        int conditonValue = 0;
        switch (condition)
        {
            case 1://出现多消特效
                conditonValue = 1;
                break;
            case 2://升级
                return false;
            case 3://进阶
                return false;
        }

        switch (oper)
        {
            case 0:
                return true;
            case 1:
                return value == conditonValue;
            case 2:
                return value > conditonValue;
            case 3:
                return value < conditonValue;
        }

        return false;
    }

    public void OnCheckCondition0()
    {
        CheckScene(0);
    }

    public void OnCheckCondition1(Slot slot, E_CardType type)
    {
        if(slot != null)
        {
            switch (type)
            {
                case E_CardType.OneLine:
                    CheckScene(1);//全特效
                    CheckScene(4);//四消
                    break;
                case E_CardType.CrossLine:
                    CheckScene(1);//全特效
                    CheckScene(5);//直角消
                    break;
                case E_CardType.ThreeLine:
                    CheckScene(1);//全特效
                    CheckScene(6);//五消
                    break;
                case E_CardType.Stone:
                    CheckScene(1);//全特效
                    CheckScene(7);//能量块
                    break;
            }
            _defaultObject = slot.gameObject;
            _defaultCardType = type;
        }
    }

    public void OnCheckCondition2(int energy,int skillID)
    {
        CheckScene(8);
    }

    public void OnCheckCondition3()
    {

    }
    public void LoadTutorialTable()
    {
#if USE_TUTORIAL
        _guides = new Dictionary<string, TutorialExector>();
        _dataMap = new Dictionary<int, TutorialData>();
        object[] element = Util.CallMethod("TableTutorialCtrl", "GetDataCount");
        int count = int.Parse(element[0].ToString());
        for (int i = 1; i <= count; i++)
        {
            element = Util.CallMethod("TableTutorialCtrl", "GetDataByIndex", i);
            LuaTable MyData = element[0] as LuaTable;
            if (MyData != null)
            {
                TutorialData CData = new TutorialData();
                CData.ID = int.Parse(MyData.GetStringField("id"));
                CData.Name = MyData.GetStringField("name");
                CData.Scene = int.Parse(MyData.GetStringField("scene_index"));
                CData.Level = int.Parse(MyData.GetStringField("level_id"));
                CData.Condition = int.Parse(MyData.GetStringField("condition"));
                CData.Operator = int.Parse(MyData.GetStringField("condition_operator"));
                CData.ConditionValue = int.Parse(MyData.GetStringField("condition_value"));
                CData.ExeTimes = int.Parse(MyData.GetStringField("execute_times"));
                CData.bAfterExeSub = int.Parse(MyData.GetStringField("after_execute")) == 1 ? true : false;
                CData.NextStepID = int.Parse(MyData.GetStringField("next_step"));
                CData.StartSound = MyData.GetStringField("sound");
                CData.Script = MyData.GetStringField("script");
                _dataMap.Add(CData.ID, CData);
            }
        }
#endif    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGuide(string name, string script)
    {
        if (_guides.ContainsKey(name)) return;

        var go = new GameObject(name);
        go.transform.parent = transform;
        var executor = go.AddComponent<TutorialExector>();
        executor.ExecrorName = name;
        executor.Execute(script);
        executor.OnComplete += executor_OnComplete;
        _guides.Add(name, executor);
    }

    public void StartGuide(int guideID)
    {
        string account = Util.CallMethod("UILoginCtrl", "GetAccount")[0].ToString();
        int timeleft = PlayerPrefs.GetInt(account + guideID.ToString(), -1);
        if (timeleft != -1)
        {
            if (timeleft > 0)
            {
                timeleft -= 1;
                if(_dataMap[guideID].bAfterExeSub == false)
                    PlayerPrefs.SetInt(account + guideID.ToString(), timeleft);
                if (_dataMap.ContainsKey(guideID))
                {
                    var guide = _dataMap[guideID];
                    CurProcessID = guideID;
                    StartGuide(guide.Name, guide.Script);
                    _guides[guide.Name].GuideID = guideID;
                }
            }
        }
        else
        {
            timeleft = _dataMap[guideID].ExeTimes - 1;
            if(timeleft >= 0)
            {
                if (_dataMap[guideID].bAfterExeSub == false)
                    PlayerPrefs.SetInt(account + guideID.ToString(), timeleft);
                if (_dataMap.ContainsKey(guideID))
                {
                    var guide = _dataMap[guideID];
                    CurProcessID = guideID;
                    StartGuide(guide.Name, guide.Script);
                    _guides[guide.Name].GuideID = guideID;
                }
            }
        }
    }

    public void AbortGuide(string name)
    {
        if (_guides.ContainsKey(name))
        {
            Destroy(_guides[name]);
            _guides.Remove(name);
        }
    }

    public void AbortALLGuide()
    {
        foreach(KeyValuePair<string,TutorialExector> item in _guides)
        {
            item.Value.Abort();
        }
        _guides.Clear();
    }

    void executor_OnComplete(object sender, object args)
    {
        var executor = (TutorialExector)sender;
        _guides.Remove(executor.name);
        Destroy(executor.gameObject);

        if (executor.GuideID > 0)
        {
            var id = executor.GuideID;
            var guide = new TutorialData();
            if (guide.NextStepID > 0)
                StartGuide(guide.NextStepID);
        }
    }
    /// <summary>
    /// 标记结束
    /// </summary>
    /// <param name="executor"></param>
    public void SignEnd(TutorialExector executor)
    {
        //         GamePlay.Instance().GuideCompleted = executor.GuideID;
        //         NetworkManager.Instance.SetGuideStatus(SetGuideStatusResponse, null, GamePlay.Instance().GuideProgressing, GamePlay.Instance().GuideCompleted);
    }

    private TutorialExector _canSkipExecuter;

    public void ShowSkip(TutorialExector executer)
    {
        _canSkipExecuter = executer;
        SkipButton.SetActive(true);
    }

    public void HideSkip()
    {
        _canSkipExecuter = null;
        SkipButton.SetActive(false);
    }

    public GameObject ShowPointer(GameObject target, int type)
    {
        if (type < 1) type = 1;
        if (type > PrefabPointers.Length) type = PrefabPointers.Length;
        var pointer = NGUITools.AddChild(TutorialPanel.gameObject, PrefabPointers[type - 1]);
        pointer.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 0.1f);
        _pointers.Add(pointer);
        return pointer;
    }

    public void HidePointer()
    {
        foreach (var p in _pointers)
            NGUITools.Destroy(p);
        _pointers.Clear();
    }

    public void HidePointer(GameObject pointer)
    {
        _pointers.Remove(pointer);
        NGUITools.Destroy(pointer);
    }

    public GameObject ShowTips(GameObject target, int type, string tipStr)
    {
        if (type < 1) type = 1;
        if (type > PrefabTips.Length) type = PrefabTips.Length;
        var tip = NGUITools.AddChild(TutorialPanel.gameObject, PrefabTips[type - 1]);
        tip.GetComponent<TutorialTip>().Label.text = tipStr;
        tip.transform.localPosition = _tipPositions[type];
        //tip.transform.position = target.transform.position;
        _tips.Add(tip);
        return tip;
    }

    public GameObject ShowTipsEx(GameObject target, int type, string tipStr,int offsetX,int offsetY,int width,int height)
    {
        if (type < 1) type = 1;
        if (type > PrefabTips.Length) type = PrefabTips.Length;
        var tip = NGUITools.AddChild(TutorialPanel.gameObject, PrefabTips[type - 1]);
        TutorialTip script = tip.GetComponent<TutorialTip>();
        if(tip != null)
        {
            script.Label.text = tipStr;
            script.Label.width = width;
            script.Label.height = height;
            script.TextBox.width = width;
            script.TextBox.height = height;
            Vector3 pos = target.transform.position;
            pos.x += offsetX;
            pos.y += offsetY;
            tip.transform.localPosition = pos;
            _tips.Add(tip);
        }
        
        return tip;
    }

    public void HideTips()
    {
        foreach (var tip in _tips)
            NGUITools.Destroy(tip);
        _tips.Clear();
    }

    public void HideTips(GameObject tips)
    {
        _tips.Remove(tips);
        NGUITools.Destroy(tips);
    }

    public void ShowMask()
    {
        MaskPanel.alpha = 1;
        Mask.SetActive(true);
    }

    public void Bling(GameObject obj)
    {
        EventDispatcher.TriggerEvent<GameObject>(FightDefine.Event_ObjectsBling, obj);
    }
    public void ShowMaskA(float alpha)
    {
        MaskPanel.alpha = alpha;
        Mask.SetActive(true);
    }

    public void HideMask()
    {
        Mask.SetActive(false);
    }

    void OnSkipClick()
    {
        if (_canSkipExecuter)
        {
            _canSkipExecuter.Abort();
            HideSkip();
            HideMask();
        }
    }

    void OnGameOverClean()
    {
        HideMask();
        AbortALLGuide();
    }
}