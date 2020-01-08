using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    void Start()
    {
        GameData.instance.chochikuStar++;
        StartCoroutine(UIManager.instance.CheckChochikuStar());
        //GameData.instance.rank++;
        //ランクイメージの確認
        UIManager.instance.CheckRankImage();
    }

}
