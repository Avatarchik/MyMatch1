using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

partial class ProjectBuild : Editor
{
    //shell�ű�ֱ�ӵ��������̬����
    static void BuildForIPhone()
    {//Function.DeleteFolder(Application.dataPath + "/Plugins/Android");
		Packager.BuildiPhoneResource();
        if (Function.projectName == "91")
        {
            //Function.CopyDirectory(Application.dataPath + "/91", Application.dataPath + "/Plugins/Android");
            //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
        //������ǹ���xcode���̵ĺ��ķ����ˣ� 
        //����1 ��Ҫ��������г���
        //����2 ��Ҫ��������ӣ� ����ȡ���ľ��� shell���������ַ��� 91
        //����3 ���ƽ̨
        BuildPipeline.BuildPlayer(GetBuildScenes(), Function.projectName, BuildTarget.iOS, BuildOptions.None);
    }
}
