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

            Win(enemyData.kinLebel, enemyData.kinRarelity);

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
        GameData.currentDirtyPoint -= (float)totalExp / 2;

        //EXPを加算してEXpゲージの表示を更新
        //一旦倒した分はchoshikuに貯めておく
        GameData.chochiku += totalExp / 3;


        //Dirtyゲージが0以下になったらMaxに戻す(Debug用)
        if (GameData.currentDirtyPoint <= 0)
        {
            //貯蓄が一気に経験値に入るとともにリセットをかける
            GameData.exp += GameData.chochiku;
            GameData.chochiku = 0;
            GameData.currentDirtyPoint = 0;
            GameData.currentDirtyPoint = 100;
        }

        //経験値が100以上になるとランクが1上がる
        if (GameData.exp >= 100)
        {
            //100を超えた分だけ繰り越すようにしてEXpゲージを更新する
            GameData.exp -= 100;
            GameData.rank += 1;

            Debug.Log(GameData.rank);

        }


        //先頭に勝つたびダーティが-30
        //最終的に0以下になると経験値が30増える
        GameData.currentDirtyPoint -= 30f;

        if (GameData.currentDirtyPoint <= 0)
        {
            GameData.currentDirtyPoint = 0;
            GameData.exp += 50;
            //SceneStateManager.instance.UpdateGage();

            GameData.currentDirtyPoint = 100;

            //経験値が100以上になるとランクが１上がる
            if (GameData.exp >= 100)
            {
                GameData.exp = 0;
                // SceneStateManager.instance.UpdateGage();
                GameData.rank += 1;

                Debug.Log(GameData.rank);

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

}
