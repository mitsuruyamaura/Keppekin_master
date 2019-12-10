using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KinData",menuName = "ScritablaObjects/CreateKindata")]
public class KinData : ScriptableObject
{
    public List<KinDataList> kinDataList = new List<KinDataList>();

    [System.Serializable]
    public class KinDataList
    {
        public int kinNum;
        public string kinName;
        public int rarerity;
        public int level;
        public KIN_TYPE kinType;

        public float bulletSpeed; //KinBulletクラスへ渡す
        public string inkImage; //イメージの設定のないインク用のイメージをインスタンスし、名前からデータを参照して付着するイメージを変更する。
        public int removeCount;

        public int nakamaKinNum; //バトルシーンでインスタンスする用のきんにつける番号

    }
}