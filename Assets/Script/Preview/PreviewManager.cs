using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour
{
    [Header("Previewシーンに表示するキンのモデルのPrefabを登録するための配列")]
    public GameObject[] homeKinPrefabs;

    [Header("3Dモデルをスワイプで操作するクラス")]
    public ObjCtrl objCtrl;

    [Header("3Dモデルをインスタンスする位置")]
    public Transform setPos;

    [Header("Zukanシーンに戻るボタン")]
    public Button btnReturn;


    void Awake()
    {
        //Zukanシーンで選択されたキンのモデルのPrefabをインスタンスして、スワイプできるように設定する
        objCtrl.obj = Instantiate(homeKinPrefabs[GameData.instance.previewKinNo] , setPos);

        ////キンが球をとばさないようにする
        //objCtrl.obj.GetComponent<ShotManager>().enabled = false;
    }

    void Start()
    {
        UIManager.instance.SwitchDisplayCanvas(0);

        //フェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());

        //ボタンシーン遷移用のメソッドを登録

        btnReturn.onClick.AddListener(() => OnclickMoveScene(SCENE_TYPE.ZUKAN, btnReturn));
        
    }

    private void OnclickMoveScene(SCENE_TYPE sceneType, Button button)
    {
        button.interactable = false;
        StartCoroutine(SceneStateManager.instance.MoveScene(sceneType));
    }
}
