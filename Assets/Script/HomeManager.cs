using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [Header("Homeシーンに表示する菌のモデルのPrefabを登録する")]
    public GameObject[] homeKinPrefabs;

    [Header("モデルをスワイプで操作するクラス")]
    public ObjCtrl objCtrl;

    [Header("インスタンスする位置")]
    public Transform setPos;

	public Button zukanButton;

    public Button kansatsuButton;

    public Button battleStageButton;


    void Awake()
    {
        //配列からランダムに一体のキンを選択する
        int value = Random.Range(0, homeKinPrefabs.Length);
        Debug.Log(value);

        //一番目の配列しか生成しないようにする
        //選択されたキンのモデルのPrefabをインスタンスして、スワイプできるように設定する
        objCtrl.obj = Instantiate(homeKinPrefabs[value], setPos);
        //objCtrl.obj.GetComponent<ShotManager>().enabled = false; //ホームでキンが球を飛ばさないようにする
        
    }

    void Start()
    {
        StartCoroutine(TransitionManager.instance.FadeIn());

        //UIヘッダーを表示する
        UIManager.instance.SwitchDisplayCanvas(1);


        zukanButton.onClick.AddListener(() => OnClickMoveScene(SCENE_TYPE.ZUKAN, zukanButton));

        kansatsuButton.onClick.AddListener(() => OnClickMoveScene(SCENE_TYPE.KANSATSU, kansatsuButton));

        battleStageButton.onClick.AddListener(() => OnClickMoveScene(SCENE_TYPE.STAGE, battleStageButton));

        SoundManager.instance.PlayBgm(SoundManager.BGM_TYPE.HOME);
    }

    private void OnClickMoveScene(SCENE_TYPE sceneType, Button button)
    {
        //interactableでfalseにするとボタンを押せなくなる(スイッチ切られるイメージ)
        button.interactable = false;

        StartCoroutine(SceneStateManager.instance.MoveScene(sceneType));
    }

   

}
