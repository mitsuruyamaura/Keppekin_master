using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleDebug : MonoBehaviour
{
    public Image dirtyGage;

    public Slider sliderGauge;

    private void Start()
    {
        //現在値を最大値で割ることで徐々にゲージを減らしていける
        dirtyGage.fillAmount = GameData.instance.currentDirtyPoint / 100;
        
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
        dirtyGage.fillAmount = GameData.instance.currentDirtyPoint / 100;
        //DOValueはSliderの型限定で使えるので注意
        //DOValue(1...アニメーションさせる値、2...時間)　両方ともfloat型じゃないとダメ
        sliderGauge.DOValue(dirtyGage.fillAmount, 0.5f).SetEase(Ease.Linear);
    }

}
