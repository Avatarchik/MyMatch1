/* 
    MyFrameWork Code By Jarjin lee
*/

using MyFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Facade {
    static GameObject m_GameManager;
    static GameObject m_TutorialManager;
    static Dictionary<string, object> m_Managers = new Dictionary<string, object>();

    GameObject AppGameManager
    {
        get
        {
            if (m_GameManager == null)
            {
                m_GameManager = GameObject.Find("GameManager");
            }
            return m_GameManager;
        }
    }

    GameObject AppTutorialManager
        {
            get {
                if (m_TutorialManager == null)
                {
                    m_TutorialManager = GameObject.Find("UI Root/TutorialManager");
                }
                return m_TutorialManager;
            }
        }

    protected Facade() {
        InitFramework();
    }
    protected virtual void InitFramework()
    {

    }

    /// <summary>
    /// 添加管理器
    /// </summary>
    public void AddManager(string typeName, object obj) {
        if (!m_Managers.ContainsKey(typeName)) {
            m_Managers.Add(typeName, obj);
        }
    }

    /// <summary>
    /// 添加Unity对象
    /// </summary>
    public T AddManager<T>(string typeName) where T : Component {
        object result = null;
        m_Managers.TryGetValue(typeName, out result);
        if (result != null) {
            return (T)result;
        }
        Component c = null;
        if (typeName == ManagerName.Tutorial)
            c = AppTutorialManager.GetComponent<T>();
        else
            c = AppGameManager.AddComponent<T>();

        m_Managers.Add(typeName, c);
        return default(T);
    }

    /// <summary>
    /// 获取系统管理器
    /// </summary>
    public T GetManager<T>(string typeName) where T : class {
        if (!m_Managers.ContainsKey(typeName)) {
            return default(T);
        }
        object manager = null;
        m_Managers.TryGetValue(typeName, out manager);
        return (T)manager;
    }

    /// <summary>
    /// 删除管理器
    /// </summary>
    public void RemoveManager(string typeName) {
        if (!m_Managers.ContainsKey(typeName)) {
            return;
        }
        object manager = null;
        m_Managers.TryGetValue(typeName, out manager);
        Type type = manager.GetType();
        if (type.IsSubclassOf(typeof(MonoBehaviour))) {
            GameObject.Destroy((Component)manager);
        }
        m_Managers.Remove(typeName);
    }
}
