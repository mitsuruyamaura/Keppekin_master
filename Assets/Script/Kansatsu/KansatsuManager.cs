using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KansatsuManager : MonoBehaviour
{

    [Header("シャーレ用キンのプレファブ")]
    public SchaleKin schaleKinPrefab;

    [Header("キンの生成位置")]
    public Transform[] shaleKinTrans;

    [Header("キンの属性")]
    public KIN_TYPE[] kinTypes;

    [Header("キンの名前")]
    public string[] kinNames;

    [Header("キンの名前表示用")]
    public TMP_Text[] txtKinNames;

    [Header("キンのイメージ表示用")]
    public Image[] imgKinImages;

    [Header("キンの属性イメージ表示用")]
    public Image[] imgKinTypes;

    [Header("キンのタグ")]
    public string[] kinTags;

    [Header("一回あたりのキンの最小生成数")]
    public int minNumber;

    [Header("一回あたりのキンの最大生成数")]
    public int maxNumber;

    [Header("待機時間の最小値")]
    public int minWaitTime;

    [Header("待機時間の最大値")]
    public int maxWaitTime;

    [Header("待機時間")]
    public int[] waitTimes;

    [Header("待機時間のカウント用")]
    public float[] timers;

    [Header("観察スタートのフラグ")]
    public bool isKansatsuStart;

    [Header("上側の生成されたキンの総数")]
    public int enemyCount;

    [Header("下側の生成されたキンの総数")]
    public int rivalCount;


    //画面用UI
    public TMP_Text txtTime;
    public TMP_Text txtEnemyCount;
    public TMP_Text txtRivalCount;
    public Button returnButton;

    
    void Start()
    {
        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());

        //UIヘッダー隠す
        UIManager.instance.SwitchDisplayCanvas(0);

        //ボタンの登録
        returnButton.onClick.AddListener(() => StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.HOME)));

        //キンの生成数の表示を更新
        UpdateDisplayKinCount();
    }



    /// <summary>
    /// ポップアップで設定したキンの情報を受け取り、変数に登録する
    /// </summary>
    /// <param name="names"></param>
    /// <param name="types"></param>
    public void SetUpShaleKinInfo(string[] names, KIN_TYPE[] types, int maxValue, string[]katakanas)
    {
        for (int i = 0; i < names.Length; i++)
        {
            //キンの名前と属性登録
            kinNames[i] = names[i];
            kinTypes[i] = types[i];

            //生成までにかかる時間を設定
            waitTimes[i] = Random.Range(minWaitTime, maxWaitTime);

            //キンの名前(カタカナ)を画面に表示する
            txtKinNames[i].text = katakanas[i];
            Debug.Log(names[i]);

            //キンのイメージと属性のイメージを画面に表示
            imgKinImages[i].sprite = Resources.Load<Sprite>("Image/" + kinNames[i]);
            imgKinTypes[i].sprite = Resources.Load<Sprite>("Type/" + kinTypes[i]);

            timers[i] = 0;
        }

        //一回あたりの生成数の最大値を受け取る
        maxNumber = maxValue;

        //設定が終わったので観察を開始する
        isKansatsuStart = true;
    }




    void Update()
    {
        if (!isKansatsuStart)
        {
            //設定するまでは動かない
            return;
        }
        else
        {
            //キンその1の生成までのカウントを行う
            timers[0] += Time.deltaTime;

            if (timers[0] >= waitTimes[0])
            {
                timers[0] = 0;

                //シャーレキンの生成をする
                CreateShaleKins(kinNames[0], kinTypes[0], kinTags[0], shaleKinTrans[0], true);
                //次回の生成までの待機時間を設定
                waitTimes[0] = Random.Range(minWaitTime, maxWaitTime);
            }

            //キンその２の生成までのカウントを行う
            timers[1] += Time.deltaTime;
            if (timers[1] >= waitTimes[1])
            {
                timers[1] = 0;
                CreateShaleKins(kinNames[1], kinTypes[1], kinTags[1], shaleKinTrans[1], false);
                waitTimes[1] = Random.Range(minWaitTime, maxWaitTime);
            }
        }
    }



    /// <summary>
    /// シャーレキンを生成する
    /// </summary>
    public void CreateShaleKins(string name, KIN_TYPE type, string tag, Transform tran, bool isMoveDir)
    {
        //今回生成するキンの数をランダムに設定
        int spawnCount = Random.Range(minNumber, maxNumber);


        int value = 0;
        for (int i = 0; i < spawnCount; i++)
        {
            //生成回数だけシャーレキンを生成
            SchaleKin schaleKin = Instantiate(schaleKinPrefab, tran, false);
            schaleKin.Init(name, type, tag, isMoveDir);
            value++;
        }

        //生成したキンを合計する
        if (isMoveDir)
        {
            enemyCount += value;
        }
        else
        {
            rivalCount += value;
        }

        //キンの生成数の表示を更新
        UpdateDisplayKinCount();

    }

    /// <summary>
    /// 生成されたキンのカウントを更新して表示
    /// </summary>
    private void UpdateDisplayKinCount()
    {
        txtEnemyCount.text = enemyCount.ToString();
        txtRivalCount.text = rivalCount.ToString();

    }
}



