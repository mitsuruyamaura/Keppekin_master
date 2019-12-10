using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// シーン遷移前後のフェイド処理クラス
/// </summary>
public class TransitionManager : MonoBehaviour
{
    //シングルトン
    public static TransitionManager instance;

    [Header("フェイド用マスクイメージ")]
    public Image fadeMaskImage;

    [Header("フェイド処理の時間")]
    public float duration;

    private void Awake()
    {
        //nullは初めてゲームが実行された場合のこと
        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject); >>シーン遷移してもgameobjectを壊すなという命令
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //すでに１個あったら二個目は作らない、破棄する
            Destroy(gameObject);
        }
    }


    public IEnumerator FadeIn()
    {
        //fadeMaskImage.enabled = true;
        //マスクイメージをアニメで縮小する
        fadeMaskImage.transform.DOScale(new Vector3(0.1f, 0.1f), duration).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(duration);
        fadeMaskImage.enabled = false;

    }




    /// <summary>
    /// フェイドアウト処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        fadeMaskImage.enabled = true;
        //マスクイメージをアニメで拡大する
        fadeMaskImage.transform.DOScale(new Vector3(20f, 20f), duration).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(duration);
    }


}
