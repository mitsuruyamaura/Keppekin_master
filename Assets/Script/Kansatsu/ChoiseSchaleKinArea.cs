using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロールヴューの真ん中にいるキンの情報を取得するクラス
/// </summary>
public class ChoiseSchaleKinArea : MonoBehaviour
{
    public SelectSchaleKinPopup selectSchaleKinPopup;

    [Header("KansatsuManagerのキンの名前と属性の配列用番号")]
    public int arrayNum;


    private void OnTriggerEnter2D(Collider2D col)
    {
        //キンの情報を取得してKansatsuManagerのクラスに渡す
        Cell_SchaleKin cell = col.gameObject.transform.parent.gameObject.GetComponent<Cell_SchaleKin>();

        if (cell != null)
        {
            selectSchaleKinPopup.kinNames[arrayNum] = cell.kindata.kinName;
            selectSchaleKinPopup.kinTypes[arrayNum] = cell.kindata.kinType;
            selectSchaleKinPopup.katakanaNames[arrayNum] = cell.kindata.katakanaName;

            Debug.Log(cell.kindata.kinName);
            Debug.Log(cell.kindata.kinType);

        }
    }
}
