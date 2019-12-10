using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///画面に被弾用の”汚れ”を生成するクラス 
/// </summary>
public class DamaedInk : MonoBehaviour
{
    [Header("生成されるインクのプレファブ")]
    public GameObject stainPrefab;

    [Header("生成する位置情報")]
    public Transform canvasTran;

    [Header("生成されたインクを管理するリスト")]
    public List<GameObject> stainList = new List<GameObject>();

    [Header("インクの大きさ")]
    public float mimScale;
    public float maxScale;

    private Sprite ink;



    //タグBulletじゃなければこの処理は実行されない
    private void OnCollisionEnter(Collision col) //col == 弾(当たったオブジェクトの情報が入る) KinBulletクラスを持っている弾
    {
        if (col.gameObject.tag == "Bullet")
        {

            KinBullet kinBullet = col.gameObject.GetComponent<KinBullet>();

            //inkの中身はからなので必ずif文に入り、inkImageフォルダの画像を読み込む
            if (ink == null)
            {

                ink = Resources.Load<Sprite>("InkImage/" + kinBullet.inkImageName);
            }

            //Vector3 pos = Camera.main.WorldToScreenPoint(col.gameObject.transform.position);
            GameObject stain = Instantiate(stainPrefab, canvasTran, false);

            stain.transform.localPosition = new Vector3(Random.Range(-300, 300), Random.Range(-600, 600), 0);
            float value = Random.Range(mimScale, maxScale);
            stain.transform.localScale = new Vector3(value, value, 1);

            Image stainImage = stain.GetComponent<Image>();

            stainImage.sprite = ink;

            stainList.Add(stain);
            Destroy(col.gameObject, 0.5f);

            if (stainList.Count >= 10)
            {
                //TODO ゲームオーバー処理を書く
                //画面をインクだらけにするエフェクト？
                //SE BGM
                //シーン遷移
                //持っていく情報

            }

        }

    }
}
