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
    public static UIManager instance;

    [Header("EXPゲージ表示用イメージ")]
    public Image expGage;

    [Header("EXPゲージアニメーション用のスライダー入れるとこ")]
    public Slider expSlider;

    public Image rankImage;
    public Image[] starIcons;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

        CheckRankImage();

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

        expGage.fillAmount = (float)GameData.instance.exp / 100;
        Debug.Log(GameData.instance.rank);
        Debug.Log(GameData.instance.exp);

        expSlider.DOValue(expGage.fillAmount, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// ランクイメージをランクに合わせて表示する
    /// </summary>
    public void CheckRankImage()
    {
        rankImage.sprite = Resources.Load<Sprite>("UI/Image_RANK/Icon_RANK" + GameData.instance.rank);
    }

    public IEnumerator CheckChochikuStar()
    {
        //遷移を待つ
        yield return new WaitForSeconds(1.0f);

        for (int i = 1; i < 4; i++)
        {
            if (i <= GameData.instance.chochikuStar)
            {
                starIcons[i - 1].enabled = true;


                Sequence seq = DOTween.Sequence();
                seq.Append(starIcons[i - 1].transform.DOScale(1.5f, 0.5f));
                seq.Append(starIcons[i - 1].transform.DOScale(1.1f, 0.25f));

                yield return new WaitForSeconds(0.75f);

            }
        }

    }

    public void SwitchDisplayCanvas(float alpha)
    {
        //CanvasGroupのアルファ値を引数で受け取った値(0か1)にする
        canvasGroup.alpha = alpha;
    }

}
