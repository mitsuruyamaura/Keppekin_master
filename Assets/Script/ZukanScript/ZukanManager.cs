using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZukanManager : MonoBehaviour
{
    //UI関連
    public Image kinImage;
    public TMP_Text kinName;
    public Image kinType;
    public TMP_Text removeCountText;

    [Header("キンのボタンのプレファブ")]
    public KinDetail kinDetailPrefab;
    [Header("キンのボタンの生成位置")]
    public Transform kinDetailTran;

    [Header("レアリティのプレファブ")]
    public GameObject rarelityPrefab;
    [Header("レアリティアイコンの生成位置")]
    public Transform rarelityTran;


    void Start()
    {
        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());

        //KinDataに登録されているキンの数だけボタンを生成
        for (int i = 0; i < GameData.instance.kindata.kinDataList.Count; i++)
        {
            KinDetail kinButton = Instantiate(kinDetailPrefab, kinDetailTran, false);
            kinButton.Init(GameData.instance.kindata.kinDataList[i], this);
            //KinInfoList[i] = kinButton;
        }
        //先頭のキンのデータをデフォルトとして画面上部に表示
        Display(GameData.instance.kindata.kinDataList[0]);
    }


    public void Display(KinData.KinDataList kinData)
    {
        kinImage.sprite = Resources.Load<Sprite>("Image/" + kinData.kinName);
        kinName.text = kinData.kinName;
        
        kinType.sprite = Resources.Load<Sprite>("Type/" + kinData.kinType);
        //removeCountText.text = kinData.removeCount.ToString();

    }


}
