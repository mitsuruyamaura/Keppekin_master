using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinInfoList : MonoBehaviour
{
    public static string[] infoList;

    void Start()
    {
        string tempText = "";

        //TextAsset...テキストファイルをUnityが読み込むための型
        TextAsset textAsset = new TextAsset();


        //textAsset = Resources.Load("①", typeof(TextAsset)) as TextAsset;
        //①テキストファイル名, ②TextAssetの形で読み込むようにtypeOfメソッドで指定している
        //as TextAsset;...読み込んだものをTextAsset型に変換する
        textAsset = Resources.Load("KinInfo", typeof(TextAsset)) as TextAsset;

        //String型で一致させやっと目に見えるテキストになる
        tempText = textAsset.text;

        //Split(',')...引数に指定した文字で(シングルクオーテーションのなか)分割
        infoList = tempText.Split(',');

        //GameDataの持っているKinDataの各Infoに、Textファイルから取得した各Infoを入れ込む
        for (int i = 0; i < infoList.Length; i++)
        {
            GameData.instance.kindata.kinDataList[i].info = infoList[i];
        }

    }

}
