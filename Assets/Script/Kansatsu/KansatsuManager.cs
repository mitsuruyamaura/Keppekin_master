using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KansatsuManager : MonoBehaviour
{
    public Button returnButton;

    
    void Start()
    {
        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());

        //UIヘッダー隠す
        UIManager.instance.SwitchDisplayCanvas(0);


        returnButton.onClick.AddListener(() => StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.HOME)));
    }

}
