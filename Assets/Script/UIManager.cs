using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UI関係の制御をするクラス]
/// UISceneのUICanvasにつける
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("EXPゲージ表示用イメージ")]
    public Image expGage;

    [Header("EXPゲージアニメーション用のスライダー入れるとこ")]
    public Slider expSlider;

    // Start is called before the first frame update
    void Start()
    {
        //シーン遷移時、EXPゲージの紐付けがあるか確認し、ない場合にはタグでEXPゲージを探して紐付けを行う。
        if (expGage == null)
        {
            expGage = GameObject.FindGameObjectWithTag("EXPGage").GetComponent<Image>();
        }
        if (expSlider == null)
        {
            expSlider = GameObject.FindGameObjectWithTag("EXPSlider").GetComponent<Slider>();
        }
        UpdateGage();

    }



   /// <summary>
   /// EXPゲージの表示をEXPの値になるように更新する
   /// </summary>
    public void UpdateGage()
    {
        StartCoroutine(AnimateEXPGauge());
    }




    private IEnumerator AnimateEXPGauge()
    {
        yield return new WaitForSeconds(0.3f);

        expGage.fillAmount = (float)GameData.exp / 100;
        Debug.Log(GameData.rank);
        Debug.Log(GameData.exp);

        expSlider.DOValue(expGage.fillAmount, 0.5f).SetEase(Ease.Linear);
    }

}
