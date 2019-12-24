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
    public int rank;
    public int exp;
    public int chochiku;
    public float currentDirtyPoint;

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

    //セーブ用のenum
    public enum DATA_TYPE
    {
        RANK,
        EXP,
        CHOCHIKU,
        DIRTY_POINT
    }


//初期化...普段は省略してかける
//int x = new int();
//x = 5;
//int x = 5;
//けど、上で作ったクラスはUnityが知らないからいちいちnewで初期化してあげないと使えない


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
        Load(); //Awakeの後に保存されているデータがあれば呼びに行く
    }

    /// <summary>
    /// データセーブ用
    /// </summary>
    public void Save()
    {
        //SetFloatでデータをセットしてからSaveメソッドでデータを保存する
        PlayerPrefs.SetFloat(DATA_TYPE.RANK.ToString(), rank);
        PlayerPrefs.SetFloat(DATA_TYPE.EXP.ToString(), exp);
        PlayerPrefs.SetFloat(DATA_TYPE.CHOCHIKU.ToString(), chochiku);
        PlayerPrefs.SetFloat(DATA_TYPE.DIRTY_POINT.ToString(), currentDirtyPoint);

        PlayerPrefs.Save();
    }


    /// <summary>
    /// データロード用、呼びだし補助機能、とりあえず
    /// </summary>
    public void Load()
    {

        //引数なしで各値にデータを入れ込む
        rank = (int)PlayerPrefs.GetFloat(DATA_TYPE.RANK.ToString(), 1);

        exp = (int)PlayerPrefs.GetFloat(DATA_TYPE.EXP.ToString(), 0);

        chochiku = (int)PlayerPrefs.GetFloat(DATA_TYPE.CHOCHIKU.ToString(), 0);

        currentDirtyPoint = PlayerPrefs.GetFloat(DATA_TYPE.DIRTY_POINT.ToString(), 100);


        Debug.Log(rank);
        Debug.Log(exp);
        Debug.Log(chochiku);
        Debug.Log(currentDirtyPoint);

    }


    /// <summary>
    /// データ一括削除用、アンインストールしなくても消せるように
    /// </summary>
    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }

}
