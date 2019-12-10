using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    public BattleMenuPopUp battleMenuPuPrefab;
    public Transform canvasTransform;

    public bool isStop;

    public void CreateBattleMenuPopUp()
    {

        BattleMenuPopUp battleMenuPu = Instantiate(battleMenuPuPrefab, canvasTransform, false);
        //battleMenuPu.battleUIManager = this;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(battleMenuPu.battleBg.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f)).SetEase(Ease.InCirc);
        sequence.Append(battleMenuPu.battleBg.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f)).SetEase(Ease.InCirc);
        sequence.Join(battleMenuPu.battleBg.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.InCirc));

        //isStop = true;
        ////timeScale = ０は時間が止まる
        //Time.timeScale = 0;

    }
}
