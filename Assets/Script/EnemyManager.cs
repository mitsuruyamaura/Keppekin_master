using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyManager : MonoBehaviour
{

    [Header("レアリティごとの提供割合")]
    public int[] weights;

    [Header("生成するキンの数")]
    public int enemys; //３に指定する

    [Header("キンのデータベース(スクリプタブルオブジェクト)")]
    public KinData kindata;

    [Header("生成したキンを管理する(入れておく)配列")]
    public KinStates[] silhouettesImageBox;

    [Header("生成するキンのプレファブ。クラスから生成")]
    public KinStates silhouettePrefab;

    [Header("生成するキンの初期位置")]
    public Transform mainStageTransform;


    [Header("生成する位置の幅の最小値")]
    public float minX;
    [Header("生成する位置の幅の最大値")]
    public float maxX;

    [Header("生成する位置の高さの最小値")]
    public float minY;
    [Header("生成する位置の高さの最大値")]
    public float maxY;

    private Vector3 initTran; //生成したキンの位置の調整用


    void Awake()
    {
        StartCoroutine(TransitionManager.instance.FadeIn());

        StartCoroutine(CreateKinMaskImage());

    }


    private IEnumerator CreateKinMaskImage()
    {
        //SilhouetteImageBox(キンを入れる配列のパーテーション)をいくつ作るか決めないといけないのでまず
        //生成するenemyの数とSilhouetteImageboxの数を一致させる
        //new 初期化
        silhouettesImageBox = new KinStates[enemys];

        //ランダムな値を代入しておくListを作成し、初期化
        List<int> randomNumbers_1 = new List<int>();
        List<int> randomNumbers_2 = new List<int>();
        List<int> randomNumbers_3 = new List<int>();


        //kindata.KindataList.Countはスクリプタブルオブジェクトの要素数(Size)をとってきてくれる

        for (int i = 0; i < kindata.kinDataList.Count; i++)
        {
            //レアリティごとに新しいキンのリストを作る
            if (kindata.kinDataList[i].rarerity == 1)
            {
                randomNumbers_1.Add(i);
            }
            else if (kindata.kinDataList[i].rarerity == 2)
            {
                randomNumbers_2.Add(i);
            }
            else
            {
                randomNumbers_3.Add(i);
            }

            //iの値が入っているint型のリストクをi個分作っておく
            //List<int>の中身は順番に{0,1,2,3,4,5,6,7,8,9}になっている

        }

        //ランダムな値を取得し保存しておくListを作成し、初期化
        List<int> results = new List<int>();

        //while分は(条件)になるまで処理を繰り返す　＝ここでは、enemyの値が0になるまでループする
        while (enemys > 0)
        {

            //レアリティを設定する
            int randomRarelity = ChooseKinRarelity();
            Debug.Log(randomRarelity);
            int enemyNum = 0;

            //レアリティに合わせたリストからランダムな菌を選択する
            if (randomRarelity == 1)
            {
                int random = Random.Range(0, randomNumbers_1.Count);
                enemyNum = randomNumbers_1[random];
                results.Add(enemyNum); //ランダムな値を追加
                randomNumbers_1.Remove(enemyNum); //重複回避用に引数を修正
            }
            else if (randomRarelity == 2)
            {
                int random = Random.Range(0, randomNumbers_2.Count);
                enemyNum = randomNumbers_2[random];
                results.Add(enemyNum);　//ランダムな値を追加
                randomNumbers_2.Remove(enemyNum); //重複回避用に引数を修正
            }
            else if (randomRarelity == 3)
            {
                int random = Random.Range(0, randomNumbers_3.Count);
                enemyNum = randomNumbers_3[random];
                results.Add(enemyNum); //ランダムな値を追加
                randomNumbers_3.Remove(enemyNum); //重複回避用に引数を修正
            }

            //重複してランダムの値を取らないように使った値はListからのぞいておく
            Debug.Log(enemyNum);

            //ランダムな値を取り終わったので、enemysをデクリメントする
            enemys--;
        }



        //lengthは要素数の最大値をとってきてくれる、countのようなもの
        //配列...length, リスト...countが最大値
        for (int i = 0; i < results.Count; i++)
        {
            //クラスを使ってキンのオブジェクト(シルエット)をインスタンシエイトする。
            //生成位置はCanvas(mainStageTransform)内にする。
            KinStates silhouetteObj = Instantiate(silhouettePrefab, mainStageTransform, false);

            //全て同じ位置に生成されてしまうので、ランダムな位置に生成するため調整を加える
            initTran.x = Random.Range(minX, maxX);
            initTran.y = (-400 + (300 * i) + Random.Range(minY, maxY));

            //最終的な位置を決める
            silhouetteObj.transform.localPosition = initTran;


            //inの右側は型とかクラスの変数。探したいものがあるリスト。10こあるやつ
            //inの左側は探し物を一つずつ確認する変数。右と左の型が一緒じゃないとダメ。
            //右と左が合致したら中の処理に入ってくれる、入らないうちは0から一つずつ確認していってくれる
            foreach (KinData.KinDataList data in kindata.kinDataList)
            {
                if (data.kinNum == results[i])
                {
                    //合致したデータの持つ値(名前、属性、イメージ)を生成したキンのデータに入れる
                    silhouetteObj.rundomNum = data.kinNum;
                    silhouetteObj.kinName = data.kinName;
                    silhouetteObj.type = data.kinType;
                    silhouetteObj.kinMaskImage.sprite = Resources.Load<Sprite>("Image/" + data.kinName);
                    silhouetteObj.level = data.level;
                    silhouetteObj.rarelity = data.rarerity;
                    silhouetteObj.inkColor = data.inkImage;
                    silhouetteObj.nakamaKinNum = data.nakamaKinNum;


                    //キンをリストに追加する
                    silhouettesImageBox[i] = silhouetteObj;

                    //生成したキンに拡大+縮小のアニメを再生させる
                    Sequence seq = DOTween.Sequence();

                    seq.Append(silhouetteObj.transform.DOScale(new Vector3(1.5f, 1.5f), 0.7f));
                    seq.Append(silhouetteObj.transform.DOScale(new Vector3(1.0f, 1.0f), 0.3f));

                    //1秒待つ
                    yield return new WaitForSeconds(1.0f);

                    //生成したキンのSiihouetteクラスの持つ、IdleAnimeImageメソッドを呼び出す
                    StartCoroutine(silhouetteObj.IdleAnimeImage()); //コルーチン呼び出し



                }
            }

        }
    }

    /// <summary>
    /// キンのレアリティを設定する
    /// </summary>
    private int ChooseKinRarelity()
    {
        //合計用
        int total = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //weightを合計する
            total += weights[i];
        }
        Debug.Log(total);

        //ランダムな値をとる（int型なので最後の値を取れるように+1する）
        int value = Random.Range(0, total + 1);
        Debug.Log(value);

        //ランダムな値がweights[i]の値以下かどうか確認する
        //値以下の時には、iをrarelityとして戻す
        for (int i = 0; i < weights.Length; i++)
        {
            if (value < weights[i]) {
                return i + 1;
            }
            else
            {
                value -= weights[i];
            }
            
        }

        return 1;


    }
}

