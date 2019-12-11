using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ResultPopUp : MonoBehaviour
{
    [Header("除菌回数を表示するテキスト")]
    public TMP_Text removeCountTxt;

    [Header("バトルしたキンのアイコン")]
    public Image removeKinIcon;

    [Header("バトルしたキンのなまえ")]
    public string removeKinName;

    [Header("バトルしたキンの属性")]
    public KIN_TYPE removeKinType;

    [Header("勝利イメージ")]
    public Image winImage;

    [Header("敗北イメージ")]
    public Image loseImage;

    [Header("閉じるボタン")]
    public Button closeBut;

    [Header("染領完了")]
    public Image[] winMesImage;

    [Header("染領失敗")]
    public Image[] loseMesImage;




    /// <summary>
    /// BattleManager.CSから呼ばれる
    /// リザルトポップアップに表示する情報を設定する
    /// </summary>
    public void SetUp(KinStateManager kinState, bool isResult)
    {
        //除菌回数やキンのアイコンなどの情報をKinStateManagerのloadEnemyDataから取得し表示する
        removeCountTxt.text = kinState.loadEnemyData.removeCount.ToString();
        removeKinIcon.sprite = Resources.Load<Sprite>("Image/" + kinState.loadEnemyData.kinName);
        removeKinName = kinState.loadEnemyData.kinName;
        removeKinType = kinState.loadEnemyData.kinType;

        //勝敗によって分岐
        if (isResult)
        {
            //勝利　勝利イメージを表示
            winImage.enabled = true;

            for (int i = 0; i < winMesImage.Length; i++)
            {
                winMesImage[i].enabled = true;
            }

            //除菌回数をアニメ付きで加算する
            DOTween.To(() => kinState.loadEnemyData.removeCount, (x) =>
            kinState.loadEnemyData.removeCount = x,
            kinState.loadEnemyData.removeCount++, 1.5f).SetRelative();
        }
        else
        {
            //敗北　敗北イメージ表示
            loseImage.enabled = true;

            for (int i = 0; i < loseMesImage.Length; i++)
            {
                loseMesImage[i].enabled = true;
            }

        }
        }


    public void OnClickCloseButton()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.Linear));

        sequence.Append(transform.DOScale(new Vector3(0.8f,  0.8f, 0.8f), 0.25f).SetEase(Ease.Linear));

        sequence.Join(GetComponent<CanvasGroup>().DOFade(1, 0.4f).SetEase(Ease.Linear));

        StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.STAGE));
    }

}
