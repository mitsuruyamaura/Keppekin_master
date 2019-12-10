using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleTimer : MonoBehaviour
{
    
    public BattleManager battleManager;
    private int currentTime; //現在の残り時間
    private float timer; //時間計測用

    [Header("バトル時間の設定値")]
    public int battleTime; //インスペクターで設定する

    [Header("残り時間の文字が大きくなる時間")]
    public int limitTime;

    [Header("バトル時間の表示")]
    public TMP_Text countText;

    public BattleUIManager battleUIManager;

    void Start()
    {
        //currentTimeにbattleTimeを設定する
        currentTime = battleTime;
    }


    void Update()
    {
        //if (battleUIManager.isStop)
        //{
        //    return;
        //}

        //バトルが終了していないなら
        if (!battleManager.isGameUp)
        {
            //timerを利用して経過時間を計測
            timer += Time.deltaTime;

            //１秒経過ごとにtimerを0に戻し、currentTimeを減算する
            if (timer >= 1)
            {

                timer = 0;
                currentTime--;

                //時間表示を更新する(ToString("F0"))を使って、小数点は表示しない
                countText.text = currentTime.ToString("F0");

                //残り時間がlimitTimeになった文字を一瞬大きくする
                if (currentTime <= limitTime)
                {
                    Sequence seq = DOTween.Sequence();
                    seq.Append(countText.transform.DOScale(1.5f, 0.25f));
                    seq.Append(countText.transform.DOScale(1.0f, 0.25f));
                }

                if (currentTime <= 0)
                {
                    //バトル終了のフラグを立てる
                    currentTime = 0;
                    countText.text = currentTime.ToString("F0");
                    battleManager.isGameUp = true;
                }
            }
        }

    }
}
