using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KinZukanData", menuName = "ScritablaObjects/KinZukanData")]
public class KinZukanData : ScriptableObject
{
    public List<KinZukanDataList> kinZukanDataList = new List<KinZukanDataList>();

    [System.Serializable]
    public class KinZukanDataList
    {
        public int kinNum;
        public string kinName;
        public int rarerity;

        public string katakanaName;
        public KIN_TYPE kinType;


        [HideInInspector]
        public string info; //各キンの情報

    }
}