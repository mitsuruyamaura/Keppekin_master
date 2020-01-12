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

    public List<GameObject> kinButtonList = new List<GameObject>(6);
    public KinDetail[] kinButtons;
    public Transform popupTran;


    void Start()
    {
        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());
        kinButtons = new KinDetail[GameData.instance.kindata.kinDataList.Count];


        //KinDataに登録されているキンの数だけボタンを生成
        for (int i = 0; i < GameData.instance.kindata.kinDataList.Count; i++)
        {
            Transform tran = kinButtonList[0].transform;
            if (i < 8)
            {
                tran = kinButtonList[0].transform;
            }
            else if (i >= 8 && i < 16)
            {
                tran = kinButtonList[1].transform;
            }

            KinDetail kinButton = Instantiate(kinDetailPrefab, tran, false);
            kinButton.Init(GameData.instance.kindata.kinDataList[i], this);
            kinButtons[i] = kinButton;

        }

        //for (int count = 0; count < kinButtons.Length; count++)
        //{
        //    if (count < 8)
        //    {
        //        kinButtons[count].transform.SetParent(kinButtonList[0].transform);
        //    }
        //    else if (count >= 8 && count < 16) 
        //    {
        //        kinButtons[count].transform.SetParent(kinButtonList[1].transform);
        //    }
        //}

        //先頭のキンのデータをデフォルトとして画面上部に表示
        Display(GameData.instance.kindata.kinDataList[0]);
    }


    public void Display(KinData.KinDataList kinData)
    {
        for (int i = 0; i < kinButtons.Length; i++)
        {
            if (kinButtons[i].kinData.kinNum == kinData.kinNum)
            {
                kinButtons[i].isSelect = true;
                kinButtons[i].kinImage.sprite = Resources.Load<Sprite>("Image/Icon_FaceImage/SelectIcon/s_" + kinData.kinName);
            }
            else
            {
                kinButtons[i].isSelect = false;
                kinButtons[i].kinImage.sprite = Resources.Load <Sprite>("Image/Icon_FaceImage/" + GameData.instance.kindata.kinDataList[i].kinName);
            }
        }

        kinImage.sprite = Resources.Load<Sprite>("Image/" + kinData.kinName);
        kinName.text = kinData.kinName;
        
        kinType.sprite = Resources.Load<Sprite>("Type/" + kinData.kinType);
        //removeCountText.text = kinData.removeCount.ToString();

        //キンのレアリティに応じてアイコンを生成する
        //すでに生成されている場合は生成されている数を比べて、違う場合は破棄する。
        if (rarelityTran.childCount != kinData.rarerity)
        {
            foreach (Transform child in rarelityTran)
            {
                Destroy(child.gameObject);
            }

            //レアリティの数だけアイコン生成
            for (int i = 0; i < kinData.rarerity; i++)
            {
                Instantiate(rarelityPrefab, rarelityTran, false);
            }

        }


    }


}
