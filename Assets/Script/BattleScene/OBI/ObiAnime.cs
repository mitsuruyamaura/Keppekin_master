using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObiAnime : MonoBehaviour
{

    public Image obiImage; //アニメさせるイメージをアサインする。ここでは帯のイメージ

    　int i = 0;

    private void Start()
    {
        StartCoroutine(AnimeObi());
    }


    public IEnumerator AnimeObi()
    {
        Sequence seq = DOTween.Sequence();
        //右下方向にゆっくり帯を移動させつつ表示

        seq.Append(obiImage.transform.DOMoveX((obiImage.transform.position.x + 10), 2.0f).SetEase(Ease.Linear));

        seq.Join(obiImage.transform.DOMoveY((obiImage.transform.position.y - 10), 2.0f).SetEase(Ease.Linear));
        seq.Join(obiImage.DOFade(1f, 1.0f));

        yield return new WaitForSeconds(1.5f);
        //ゆっくり表示
        seq.Append(obiImage.DOFade(0f, 1.0f));
        yield return new WaitForSeconds(1.0f);

        i++;

        //回数が3回以下だったら同じ処理を繰り返す
        if (i < 3)
        {
            StartCoroutine(AnimeObi());
        }

    }

}
