using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button returnHomeButton;


    void Start()
    {
        
        StartCoroutine(UIManager.instance.CheckChochikuStar());
  
        //ランクイメージの確認
        UIManager.instance.CheckRankImage();

        //StartCoroutine(SoundManager.instance.StopBGM());

        SoundManager.instance.PlayBgm(SoundManager.BGM_TYPE.HOME);

        returnHomeButton.onClick.AddListener(()=>OnclickMoveScene(SCENE_TYPE.HOME, returnHomeButton));
        
    }

    private void OnclickMoveScene(SCENE_TYPE sceneType, Button button)
    {
        button.interactable = false;
        StartCoroutine(SceneStateManager.instance.MoveScene(sceneType));
    }

}
