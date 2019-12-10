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
        currentDirtyPoint -= (float)totalExp / 2;

        //EXPを加算してEXpゲージの表示を更新
        //一旦倒した分はchoshikuに貯めておく
        GameData.chochiku += totalExp / 3;
        uiManager.UpdateGage();

        //Dirtyゲージが0以下になったらMaxに戻す(Debug用)
        if (currentDirtyPoint <= 0)
        {
            //貯蓄が一気に経験値に入るとともにリセットをかける
            GameData.exp += GameData.chochiku;
            GameData.chochiku = 0;
            currentDirtyPoint = 0;
            currentDirtyPoint = maxDirtyPoint;
        }

        //経験値が100以上になるとランクが1上がる
        if (GameData.exp >= 100)
        {
            //100を超えた分だけ繰り越すようにしてEXpゲージを更新する
            GameData.exp -= 100;
            uiManager.UpdateGage();
            GameData.rank += 1;

            Debug.Log(GameData.rank);

        }

        StartCoroutine(AnimateDirtyGauge());

        //先頭に勝つたびダーティが-30
        //最終的に0以下になると経験値が30増える
        currentDirtyPoint -= 30f;

        if (currentDirtyPoint <= 0)
        {
            currentDirtyPoint = 0;
            GameData.exp += 50;
            //SceneStateManager.instance.UpdateGage();

            currentDirtyPoint = maxDirtyPoint;

            //経験値が100以上になるとランクが１上がる
            if (GameData.exp >= 100)
            {
                GameData.exp = 0;
               // SceneStateManager.instance.UpdateGage();
                GameData.rank += 1;

                Debug.Log(GameData.rank);

            }
        }

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


    public void Lose()
    {
       
    }
   
}
