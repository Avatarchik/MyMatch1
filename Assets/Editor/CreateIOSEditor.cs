using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

partial class ProjectBuild : Editor
{
    //shell脚本直接调用这个静态方法
    static void BuildForIPhone()
    {//Function.DeleteFolder(Application.dataPath + "/Plugins/Android");
		Packager.BuildiPhoneResource();
        if (Function.projectName == "91")
        {
            //Function.CopyDirectory(Application.dataPath + "/91", Application.dataPath + "/Plugins/Android");
            //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子， 这里取到的就是 shell传进来的字符串 91
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), Function.projectName, BuildTarget.iOS, BuildOptions.None);
    }
}
