// **********************************************************************
// 
// 文件名(File Name)：             SpriteTitleChange.cs
// 
// 作者(Author)：                  #AuthorName1#
//								  #AuthorName2# 
//                                #AuthorName3#
//
// 创建时间(CreateTime):           #CreateTime#
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.IO;

public class SpriteTitleChange : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string allText = File.ReadAllText(path);
			allText = allText.Replace("#AuthorName1#", "http://www.youkexueyuan.com").Replace("#AuthorName2#", "XiaoHong").Replace("#AuthorName3#", "Yangzj")
				.Replace("#CreateTime#", System.DateTime.Now.Year + "/" + System.DateTime.Now.Month.ToString("00")
				         + "/" + System.DateTime.Now.Day.ToString("00") + " " + System.DateTime.Now.Hour.ToString("00") + ":"
				         + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00"));

            File.WriteAllText(path, allText);
        }

    }
}
