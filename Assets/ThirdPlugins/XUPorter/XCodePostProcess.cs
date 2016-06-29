using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.Xml;
#endif
using System.IO;

public static class XCodePostProcess
{
    #if UNITY_EDITOR
    [PostProcessBuild (100)]
    public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS) {
            Debug.LogWarning ("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        string path = Path.GetFullPath (pathToBuiltProject);

        // Create a new project object from build target
        XCProject project = new XCProject (pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        string[] files = Directory.GetFiles (Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        foreach (string file in files) {
            project.ApplyMod (file);
        }

        //project.AddOtherLinkerFlags("-licucore");

		project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", "iPhone Developer: Yiming Li (329XT79CBQ)", "Release");
		project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", "iPhone Developer: Yiming Li (329XT79CBQ)", "Debug");

        // 缂栬緫plist 鏂囦欢
        EditorPlist(path);

        //缂栬緫浠ｇ爜鏂囦欢
        //EditorCode(path);

        // Finally save the xcode project
        project.Save();

		Debug.Log(projectName);

        if(projectName== "91")
        {
        }

    }

	public static string projectName
	{
		get
		{
			foreach(string arg in System.Environment.GetCommandLineArgs()) {
				if(arg.StartsWith("project"))
				{
					return arg.Split("-"[0])[1];
				}
			}
			return "test";
		}
	}
    private static void EditorPlist(string filePath)
    {
     
        XCPlist list =new XCPlist(filePath);
		string bundle = "com.ema.SSWWIOS";

        string PlistAdd = @"  
            <key>CFBundleURLTypes</key>
            <array>
            <dict>
            <key>CFBundleTypeRole</key>
            <string>Editor</string>
            <key>CFBundleURLIconFile</key>
            <string>Icon@2x</string>
            <key>CFBundleURLName</key>
            <string>"+bundle+@"</string>
            <key>CFBundleURLSchemes</key>
            <array>
            <string>ww123456</string>
            </array>
            </dict>
            </array>";
        
        list.AddKey(PlistAdd);
        list.ReplaceKey("<string>com.koramgame.${PRODUCT_NAME}</string>","<string>"+bundle+"</string>");
        //淇濆瓨
        list.Save();

    }

    private static void EditorCode(string filePath)
    {
		//璇诲彇UnityAppController.mm鏂囦欢
        XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        //鍦ㄦ寚瀹氫唬鐮佸悗闈㈠?鍔犱竴琛屼唬鐮?        UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"","#import <ShareSDK/ShareSDK.h>");

        //鍦ㄦ寚瀹氫唬鐮佷腑鏇挎崲涓琛嬌        UnityAppController.Replace("return YES;","return [ShareSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation wxDelegate:nil];");

        //鍦ㄦ寚瀹氫唬鐮佸悗闈㈠?鍔犱竴琛嬌        UnityAppController.WriteBelow("UnityCleanup();\n}","- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url\r{\r    return [ShareSDK handleOpenURL:url wxDelegate:nil];\r}");


    }

    #endif
}
