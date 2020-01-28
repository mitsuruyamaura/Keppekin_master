using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KinStates : MonoBehaviour
{
    [Header("キンのID番号 = データから参照するので始めは空")]
　　public int rundomNum;

    [Header("キンの名前 = データから参照するので始めは空")]
    public string kinName;

    [Header("キンの属性 = データから参照するので始めは空")]
    public KIN_TYPE type;

    [Header("キンのイメージ = データから参照するので始めは空")]
    public Image kinMaskImage;

    [Header("キンの強さ")]
    public int level;

    [Header("キンの珍しさ")]
    public int rarelity;

    [Header("インクのファイル名")]
    public string inkColor;

    [Header("連れてく仲間キン用の番号")]
    public int nakamaKinNum;

    [Header("キンの攻撃力")]
    public int kinPower; //キンの攻撃力

    [Header("キンのカタカナ名前")]
    public string katakanaName;

    private JyunbiPopUp jyunbi; //StagePopUpを開くための紐付け

    public void Start()
    {
        //JyunbiPopUpを取得しておく
        jyunbi = GameObject.FindGameObjectWithTag("PopUpManager").GetComponent<JyunbiPopUp>();
    }


    /// <summary>
    /// StagePopUpを開く。キンのイメージをタップすると呼ばれる
    /// </summary>
    public void YobidashiCreatePop()
    {
        jyunbi.CreatePopUp(this);

    }




    /// <summary>
    /// キンをアニメさせる
    /// </summary>
    /// <returns></returns>
    public IEnumerator IdleAnimeImage()
    {

        //もぞもぞするアニメーション
        float randomValue = Random.Range(1.5f, 2.5f);
        transform.DOLocalPath(
            new Vector3[]
            {    //1
                new Vector3 (transform.localPosition.x + Random.Range(-30f, 30f),
                transform.localPosition.y + Random.Range(-30f,30f)),

                new Vector3(transform.localPosition.x - Random.Range(-30f, 30f),
                transform.localPosition.y - Random.Range(-30f,30f)),

                new Vector3(transform.localPosition.x, transform.localPosition.y)
            },
            randomValue, //2
            PathType.CatmullRom);  //3


        yield return new WaitForSeconds(randomValue);


        StartCoroutine(IdleAnimeImage());





        //うろうろさせる
        //newVector3の配列を作る
        //３個の位置を2秒かけて移動する
        //transform.DOLocalPath(new Vector3[] {
            //new Vector3( //1
            //transform.position.x + 30f,
            //transform.position.y + 30f),

            //Vector3.zero, //2

            //new Vector3(transform.position.x - 30f, //3
            //transform.position.y + 20f)},

            //2f,//1-3を2秒かけてやれという命令

            //CatMullRom...滑らかに曲線で移動する
            //SetLoops(-1...永遠にループ, LoopType.Yoyo...1-3⇨3-1...)
            //PathType.CatmullRom).SetLoops(-1, LoopType.Yoyo);
    }

   
}
