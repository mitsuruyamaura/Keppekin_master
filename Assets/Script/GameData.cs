using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームデータを保存・管理するクラス
/// </summary>
public class GameData : MonoBehaviour
{
    //シングルトン...一つのゲームシーンに一個しか存在できないゲームオブジェクト(空のものにつける)
    public static GameData instance;

    //TODO ここにバトルシーンなどに引き継ぎたいデータを保存する変数を用意する
    //まずSceneManagerクラスから変数を移す
    public static int rank;
    public static int exp;
    public static int chochiku;

    [Header("キンのデータベース(スクリプタブルオブジェクト)")]
    public KinData kindata;

    //クラスの中に作ったクラスがインスペクターで見れる
    [System.Serializable]
    public class BattleKinData
    {
        public int kinNum;
        public string kinName;
        public int kinRarelity;
        public int kinLebel;
        public int kinPower;
        public KIN_TYPE kinType;

        public float bulletSpeed;
        public string inkImage;
        public int removeCount;
        public int nakamaKinNum;

    }

    public BattleKinData enemyDatas = new BattleKinData(); //BattleKinDataで作った変数が引数としていれられる。初期化してすぐに引数として使える状態にしている

    public BattleKinData nakamaDates = new BattleKinData();

//初期化...普段は省略してかける
//int x = new int();
//x = 5;
//int x = 5;
//けど、上で作ったクラスはUnityが知らないからいちいちnewで初期化してあげないと使えない




    public static KinStates battleKinStates; //バトルするキンのデータ(その都度変更される)
    public static KinStates nakamaKinStates; //バトルに連れていく仲間のキンのデータ(その都度変更される)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


}
