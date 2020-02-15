using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{



    void Start()
    {
        
        StartCoroutine(UIManager.instance.CheckChochikuStar());
  
        //ランクイメージの確認
        UIManager.instance.CheckRankImage();

        //StartCoroutine(SoundManager.instance.StopBGM());

        SoundManager.instance.PlayBgm(SoundManager.BGM_TYPE.HOME);
    }

}
