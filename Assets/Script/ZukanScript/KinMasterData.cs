using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class kinMasterData
{
    public List<KinZukanData> ZukanInfo = new List<KinZukanData>();

    [System.Serializable]
    public class KinZukanData
    {
        public int kinNum;
        public string kinName;
        public int rarerity;

        public string katakanaName;
        public KIN_TYPE kinType;


        //[HideInInspector]
        public string info; //各キンの情報

        public bool is3DModel;

    }
}