using UnityEngine;
using System.Collections;
using LuaInterface;
using MyFrameWork;

public class test0422 : MonoBehaviour
{
    public UISprite mUISprite;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //UIMgr.Instance.ShowUI(E_UIType.UIArenaPanel, typeof(UIArena));

            // UIMgr.Instance.ShowMessageBox("用户名输入不能为空", "确定", null);

            //ReadAllSkills();

            AddCardPanel();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //UIMgr.Instance.ShowUI(E_UIType.UIArenaPanel, typeof(UIArena));

            // UIMgr.Instance.ShowMessageBox("用户名输入不能为空", "确定", null);

            ChangeToGray();
        }

    }
    public void ReadSkillInLua()
    {
        object[]obj= Util.CallMethod("TableSkillCtrl", "Get_1_Skill", 1002);

        LuaTable skill = obj[0] as LuaTable;

        Debug.Log("skill length: " + skill.Length);

        string ski_name = skill.GetStringField("name");

        string ski_desc = skill.GetStringField("desc");

        Debug.Log(ski_name+"  "+ ski_desc);

    }

    public void ReadAllSkills()
    {
        object[] obj = Util.CallMethod("TableSkillCtrl", "Get_All_Skills");

        LuaTable skills = obj[0] as LuaTable;

        Debug.Log("skills.Length: " + skills.Length);

        for (int i = 1; i <= skills.Length; i++)
        {
            LuaTable skill = skills[i] as LuaTable;

            if (skill!=null)
            {
                Debug.Log(skill.GetStringField("id"));

                Debug.Log(skill.GetStringField("name"));

                Debug.Log(skill.GetStringField("desc"));

                Debug.Log(skill.GetStringField("skill_icon"));

                Debug.Log("------------------------");
            }
        }
    }

    bool flag;
    public void ChangeToGray()
    {
        flag = !flag;
        if (flag)
        {
            gameObject.GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored Gray");
        }
        else
        {
            gameObject.GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");
        }
    }

    public void AddCardPanel()
    {
        UIMgr.Instance.ShowUI(E_UIType.UICardPanel, typeof(UICard));
    }

}
