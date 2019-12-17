using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleDebug : MonoBehaviour
{
    public Image dirtyGage;

    public Slider sliderGauge;

    public float maxDirtyPoint;
    public float currentDirtyPoint;

    //いじるな
    public UIManager uiManager;


    private void Start()
    {
        //100/100で始まるのでゲージがフルの状態で始まる
        currentDirtyPoint = maxDirtyPoint;
        //現在値を最大値で割ることで徐々にゲージを減らしていける
        dirtyGage.fillAmount = currentDirtyPoint / maxDirtyPoint;
        

        //UIシーンは別途ロードされるので、毎回、UIマネージャーを探して紐付けする
        uiManager = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<UIManager>();
        //uiManager.UpdateGage(); //まだ使わないけど追加

        StartCoroutine(AnimateDirtyGauge());


    }

    /// <summary>
    /// Dirtyゲージをアニメーションさせる
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimateDirtyGauge()
    {
        yield return new WaitForSeconds(0.3f);
        //Dirtyゲージの表示を更新
        dirtyGage.fillAmount = currentDirtyPoint / maxDirtyPoint;
        //DOValueはSliderの型限定で使えるので注意
        //DOValue(1...アニメーションさせる値、2...時間)　両方ともfloat型じゃないとダメ
        sliderGauge.DOValue(dirtyGage.fillAmount, 0.5f).SetEase(Ease.Linear);
    }

}
