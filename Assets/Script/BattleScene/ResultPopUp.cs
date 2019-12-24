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


    public enum RESULT_TYPE
    {
        WIN,
        LOSE
    }

    public RESULT_TYPE resultType;

    int num = 0;

    public int debugRarelity;
    public int debugLevel;

    private void Start()
    {
        removeCountTxt.text = num.ToString();
        resultType = RESULT_TYPE.WIN;
        StartCoroutine(AnimeText());
    }




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
            resultType = RESULT_TYPE.WIN;
            StartCoroutine(AnimeText());
     
            yield return new WaitForSeconds(1.0f);

            Win(enemyData.kinLebel, enemyData.kinRarelity);

        }
        else
        {
            resultType = RESULT_TYPE.LOSE;
            StartCoroutine(AnimeText());

        }
    }

    /// <summary>
    /// 退治成功時の処理
    /// </summary>
    public void Win(int level, int rarelity)
    {
        Debug.Log(level);
        Debug.Log(rarelity);

        //levelとrarelityから獲得EXPを計算し、合計する
        int levelExp = level * 10;
        //Mathf.Pow(1,2)...第一引数に入れた値を第二引数乗する
        //(float型の計算式なのでまたintにキャストしている)
        // 1 = 1 * (1 * 10) = 10, 2 = 2 * (2 * 10) = 40, 3 = 3 * (3 * 10) = 90
        int rareExp = (int)Mathf.Pow(rarelity, 2) * 10;

        Debug.Log(rareExp);
        int totalExp = levelExp + rareExp;
        Debug.Log(totalExp);

        //DirtyPointの現在値からEXPの半分を引く
        GameData.instance.currentDirtyPoint -= (float)totalExp / 2;

        //EXPを加算してEXpゲージの表示を更新
        //一旦倒した分はchoshikuに貯めておく
        GameData.instance.chochiku += totalExp / 3;


        //Dirtyゲージが0以下になったらMaxに戻す(Debug用)
        if (GameData.instance.currentDirtyPoint <= 0)
        {
            //貯蓄が一気に経験値に入るとともにリセットをかける
            GameData.instance.exp += GameData.instance.chochiku;
            GameData.instance.chochiku = 0;
            GameData.instance.currentDirtyPoint = 0;
            GameData.instance.currentDirtyPoint = 100;
        }

        //経験値が100以上になるとランクが1上がる
        if (GameData.instance.exp >= 100)
        {
            //100を超えた分だけ繰り越すようにしてEXpゲージを更新する
            GameData.instance.exp -= 100;
            GameData.instance.rank += 1;

            Debug.Log(GameData.instance.rank);

        }


        //先頭に勝つたびダーティが-30
        //最終的に0以下になると経験値が30増える
        GameData.instance.currentDirtyPoint -= 30f;

        if (GameData.instance.currentDirtyPoint <= 0)
        {
            GameData.instance.currentDirtyPoint = 0;
            GameData.instance.exp += 50;
            //SceneStateManager.instance.UpdateGage();

            GameData.instance.currentDirtyPoint = 100;

            //経験値が100以上になるとランクが１上がる
            if (GameData.instance.exp >= 100)
            {
                GameData.instance.exp = 0;
                // SceneStateManager.instance.UpdateGage();
                GameData.instance.rank += 1;

                Debug.Log(GameData.instance.rank);

            }
        }

        GameData.instance.Save();

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


    private IEnumerator AnimeText()
    {
        //アニメの中で待つとAppendが順番ではなく、まとまってしまうので先に一旦待つ。
        yield return new WaitForSeconds(0.5f);

        Sequence seq = DOTween.Sequence();
        switch (resultType)
        {
            case RESULT_TYPE.WIN:
                //勝利　勝利イメージを表示
                winImage.enabled = true;

                //菌イメージ
                seq.Append(winImage.transform.DOScale(1.5f, 0.25f));
                seq.Append(winImage.transform.DOScale(1.0f, 0.25f));

                //染
                //DOShakeScale()...ランダムに大きさが変わる
                seq.Append(winMesImage[0].transform.DOLocalMoveY(2, 0.25f)).SetEase(Ease.Linear);
                seq.Append(winMesImage[0].transform.DOShakeScale(0.25f));

                //領
                seq.Append(winMesImage[1].transform.DOLocalMoveY(-9, 0.25f)).SetEase(Ease.Linear);
                seq.Append(winMesImage[1].transform.DOShakeScale(0.25f)).SetEase(Ease.Linear);

                //完
                seq.Append(winMesImage[2].transform.DOLocalMoveY(6.5f, 0.25f)).SetEase(Ease.Linear);
                seq.Append(winMesImage[2].transform.DOShakeScale(0.25f));

                //了
                seq.Append(winMesImage[3].transform.DOLocalMoveY(-13.5f, 0.25f)).SetEase(Ease.Linear);
                seq.Append(winMesImage[3].transform.DOShakeScale(0.25f));

                break;



            case RESULT_TYPE.LOSE:
                //敗北 敗北イメージを表示
                loseImage.enabled = true;

                //菌イメージ
                seq.Append(loseImage.transform.DOScale(1.5f, 0.25f));
                seq.Append(loseImage.transform.DOScale(1.0f, 0.25f));

                //染
                seq.Append(loseMesImage[0].transform.DOLocalMoveY(3, 0.25f)).SetEase(Ease.Linear);
                seq.Append(loseMesImage[0].transform.DOShakeScale(0.25f));

                //領
                seq.Append(loseMesImage[1].transform.DOLocalMoveY(-8, 0.25f)).SetEase(Ease.Linear);
                seq.Append(loseMesImage[1].transform.DOShakeScale(0.25f));

                //失
                seq.Append(loseMesImage[2].transform.DOLocalMoveY(7, 0.25f)).SetEase(Ease.Linear);
                seq.Append(loseMesImage[2].transform.DOShakeScale(0.25f));

                //敗
                seq.Append(loseMesImage[3].transform.DOLocalMoveY(-15.5f, 0.25f)).SetEase(Ease.Linear);
                seq.Append(loseMesImage[3].transform.DOShakeScale(0.25f));

                break;

        }

        yield return new WaitForSeconds(1.0f);

        //除菌回数をアニメさせてカウント
        int value = 9;
        int remove = value + 1;

        //DOTween.To()と書くとDOTweenに登録されていないオブジェクトも動かせる
        //DOTween.To(①...何に入れるか、②...何を入れるか、③どこまで値を変化させるか、④変化させるのにかける時間)
        DOTween.To(
            () => num,
            (x) =>
            {
                num = x;
                removeCountTxt.text = x.ToString();
            },
            remove,
            3f);

        //勝利メソッドのデバッグ
        Win(debugLevel, debugRarelity);

    }

}
