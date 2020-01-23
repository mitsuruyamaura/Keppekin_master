using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// 観察するキンと生成数を選択するポップアップ用クラス
/// </summary>
public class SelectSchaleKinPopup : MonoBehaviour
{

    public KansatsuManager kansatsuManager;

    [Header("生成したいキン１〜２の名前")]
    public string[] kinNames;

    [Header("生成したいキン１〜２のタイプ")]
    public KIN_TYPE[] kinTypes;

    [Header("一回に生成するキンの最大値")]
    public int maxKinNum;


    //UI関連
    public Button btnReturn;
    public Button btnKansatsu;
    public CanvasGroup canvasGroup;


    [Header("一回に生成するキンの最大値を入力する場所")]
    public TMP_InputField inputField;

    [Header("一回に生成するキンの最大値の表示")]
    public TMP_Text txtMaxKinNum;


    void Start()
    {
        //アニメ付きでPU表示
        gameObject.transform.DOScale(1f, 0.25f);
        canvasGroup.DOFade(1, 0.25f);

        //各ボタンにメソッドを登録
        btnReturn.onClick.AddListener(() => StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.HOME)));
        btnKansatsu.onClick.AddListener(ClosePopUp);

        //最大生成数の初期数字を表示
        txtMaxKinNum.text = maxKinNum.ToString();
        
    }


    /// <summary>
    /// InputFieldで生成するキンの最大数を設定する
    /// InputFieldのOnEndEditイベントにこのメソッドを登録しておく
    /// </summary>
    public void OnClickSubmit()
    {
        //InputFieldに入力された数字はstring型なので、stringからintにパース(分析、置き換え)してmaxKinNumに入れる
        //int.TryParse(第一引数で置き換えたいstringを指定、 out 第二引数で置き換えしてintになった値を折れる変数を指定)
        //Try,Catchとおんなじ感じ
        //空白でも大丈夫
        int.TryParse(inputField.text, out maxKinNum);


        //最大数が5以下なら5にする
        if (maxKinNum <= 5)
        {
            maxKinNum = 5;
        }
        //画面の表示を更新し、InputFieldを空白にする
        txtMaxKinNum.text = maxKinNum.ToString();
        inputField.text = "";
    }


    /// <summary>
    /// PUを閉じてキンのデータを残す
    /// </summary>
    public void ClosePopUp()
    {
        //KansatsuManagerのSetupKinInfoに設定した2体分のキンのデータと最大生成数を渡す
        kansatsuManager.SetUpShaleKinInfo(kinNames, kinTypes, maxKinNum);

        //PUをアニメ付きで破壊する
        gameObject.transform.DOScale(1.3f, 0.25f);
        //アルファを０にする
        canvasGroup.DOFade(0, 0.5f);
        Destroy(gameObject, 0.5f);
    }
}
