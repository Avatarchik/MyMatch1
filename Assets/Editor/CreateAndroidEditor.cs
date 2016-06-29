using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

partial class ProjectBuild : Editor
{

    //�������ҳ��㵱ǰ�������еĳ����ļ���������ֻ���Ѳ��ֵ�scene�ļ����� ��ô��������д���������ж� ��֮����һ���ַ������顣
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();
        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }

    static void BuildForAndroid()
    {
        //Function.DeleteFolder(Application.dataPath + "/Plugins/Android");
        Packager.BuildAndroidResource();
        if (Function.projectName == "91")
        {
            //Function.CopyDirectory(Application.dataPath + "/91", Application.dataPath + "/Plugins/Android");
            //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        }
        string path = Application.dataPath + "/../" + Function.projectName + ".apk";
        BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);
    }
}
