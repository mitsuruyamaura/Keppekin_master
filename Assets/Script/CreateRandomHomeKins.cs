using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomHomeKins : MonoBehaviour
{
    [Header("Homeシーンに表示する菌のモデルのPrefabを登録する")]
    public GameObject[] homeKinPrefabs;

    [Header("モデルをスワイプで操作するクラス")]
    public ObjCtrl objCtrl;

    [Header("インスタンスする位置")]
    public Transform setPos;



    void Awake()
    {
        //配列からランダムに一体のキンを選択する
        int value = Random.Range(0, homeKinPrefabs.Length);
        Debug.Log(value);

        //選択されたキンのモデルのPrefabをインスタンスして、スワイプできるように設定する
        objCtrl.obj = Instantiate(homeKinPrefabs[value], setPos);
        objCtrl.obj.GetComponent<ShotManager>().enabled = false; //ホームでキンが球を飛ばさないようにする
        
    }

    void Start()
    {
        StartCoroutine(TransitionManager.instance.FadeIn());
    }

}
