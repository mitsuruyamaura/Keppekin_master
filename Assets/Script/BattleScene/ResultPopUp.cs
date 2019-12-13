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
    public TMP_Text removeKinName;

    [Header("バトルしたキンの属性")]
    public TMP_Text removeKinType;

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
    public IEnumerator SetUp(GameData.BattleKinData enemyData, bool isResult)
    {
        //除菌回数やキンのアイコンなどの情報をKinStateManagerのloadEnemyDataから取得し表示する
        removeCountTxt.text = enemyData.removeCount.ToString();
        removeKinIcon.sprite = Resources.Load<Sprite>("Image/" + enemyData.kinName);
        removeKinName.text = enemyData.kinName;
        removeKinType.text = enemyData.kinType.ToString();

        //勝敗によって分岐
        if (isResult)
        {
            //勝利　勝利イメージを表示
            winImage.enabled = true;

            for (int i = 0; i < winMesImage.Length; i++)
            {
                winMesImage[i].enabled = true;
            }

            yield return new WaitForSeconds(1.0f);


            //除菌回数をアニメ付きで加算する
            DOTween.To(() => enemyData.removeCount, (x) =>
            enemyData.removeCount = x,
            enemyData.removeCount++, 1.5f).SetRelative();
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
        StartCoroutine(ClosePopUp());
    }



    public IEnumerator ClosePopUp()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).SetEase(Ease.Linear));

        sequence.Append(transform.DOScale(new Vector3(0f,  0f, 0f), 0.7f).SetEase(Ease.Linear));

        sequence.Join(GetComponent<CanvasGroup>().DOFade(0, 0.7f).SetEase(Ease.Linear));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.STAGE));
    }

}
