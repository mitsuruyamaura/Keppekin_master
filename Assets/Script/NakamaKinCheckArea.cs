using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView.Example02;

/// <summary>
/// StagePopUpに見えない当たり判定をつけるクラス
/// </summary>
public class NakamaKinCheckArea : MonoBehaviour
{

    public StagePopUp stagePopUp;

    //Trigger型だと見えない当たり判定がつく(オブジェクト同士すり抜けるけど、当たり判定だけは取れる)
    private void OnTriggerEnter2D(Collider2D col)
    {
        //mochiKinStatesが入る
        //GameDataにも入ることになるので保存してくれるようになる
        stagePopUp.nakamaKinStates = col.gameObject.transform.parent.gameObject.GetComponent<Cell>().mochiKinStates;
        Debug.Log(col.gameObject.transform.parent.gameObject.GetComponent<Cell>().mochiKinStates.kinName);
    }

}
