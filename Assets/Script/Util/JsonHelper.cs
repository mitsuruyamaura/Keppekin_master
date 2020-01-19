using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class JsonHelper : MonoBehaviour
{

    /// <summary>
    /// Jsonファイルをstringで読み込む
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetJsonFile(string filePath, string fileName)
    {
        string fileText = "";

        //JsonFileを読み込む
        //filePath =  /
        //fileName = ZukanInfo.Json
        //FileInfo....初めからUnityが持ってるもので、一時的にJsonファイルを格納してくれるやつ
        FileInfo info = new FileInfo(Application.streamingAssetsPath + filePath + fileName);
        //この時点では作ったJsonファイルはそのまま（無加工）


       //ここで加工作業(JsonからStringに)
        try
        {
            //OpenRead...一行毎読み込みしてくれる
            //Encording...書式UTF8に変換してくれる(Unityが読み込める書式フォーマットがUTF8)
            using (StreamReader reader = new StreamReader(info.OpenRead(), Encoding.UTF8))
            {
                fileText = reader.ReadToEnd();
            }
        }
        //Exception...例外を示す型
        catch (Exception e)
        {
            //改行コード
            fileText += e + "\n";
        }

        Debug.Log(fileText);
        return fileText;
    }



}
