using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class JyunbiPopUp : MonoBehaviour
{
    
    public StagePopUp stagePopUpPrefab;
    //public GameObject stagePopUpPrefab;
    public Transform canvasTransform;


    public void CreatePopUp(KinStates kinStates)
    {
        //ゲームオブジェクト型でインスタンシエイトすると欲しい情報をわざわざGetcomponentしないといけなくなるが
        //StagePopUp stagePopUp =
        //GameObject test = Instantiate(stagePopUpPrefab, canvasTransform, false);
        //StagePopUp stagePopUp = test.GetComponent<StagePopUp>();
        //stagePopUp.kinImage = ;

        //クラスでクローンするとそのままクラスに入れられる、欲しい情報を直でもらえる
        //第一引数には欲しいクラスがついているプレファブを指定する。prefabの型と欲しいクラスを合わせる。
        //クラスをprefabに指定してもちゃんとゲームオブジェクトができる。
        StagePopUp stagePopUp = Instantiate(stagePopUpPrefab, canvasTransform, false);

        //kinImageはpulicで宣言されているのでいじれる
        //stagePopUp.kinImage.sprite = Resources.Load<Sprite>("Image/" + kinName);
        //stagePopUp.kinName.GetComponent<Text>().text = kinName;
        //stagePopUp.typeImage.sprite = Resources.Load<Sprite>("Type/" + kinType);
        //強さと珍しさのイメージを作る

        stagePopUp.SetUp(kinStates);

        //ステージポップアップ表示時のアニメ処理
        Sequence sequence = DOTween.Sequence();
        sequence.Append(stagePopUp.backGround.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f)).SetEase(Ease.InCirc);
        sequence.Append(stagePopUp.backGround.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f)).SetEase(Ease.InCirc);
        sequence.Join(stagePopUp.backGround.GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.InCirc));

        //DOFade(アルファ値、秒数)　CanvasGroupにしか使えない
        //CanvasGroupはUIの親的なやつ
        //ここでいうCanvasGroupはBG_PopUpについているもの(BG_PopUpはStagePopUpのPrefabの中にある)



    }
}
