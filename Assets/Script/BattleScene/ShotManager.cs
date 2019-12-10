using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// キンから弾を生成するクラス
/// </summary>
public class ShotManager : MonoBehaviour
{
    [Header("生成する弾のオブジェクトのクラス")]
    public KinBullet kinBulletPrefab;

    [Header("弾の速度")]
    public float speed;

    [Header("弾を生成するまでの待機時間")]
    public float waitTime;

    public bool debugSwitch;

    private int count = 0;

    public KinStateManager kinStateManager; //スクリプトで取得するのでアサインはしない

    public string inkImageName;


    /// <summary>
    /// 
    /// </summary>
    public void SetUp(KinStateManager kinState)
    {
        kinStateManager = kinState;
        inkImageName = kinStateManager.loadEnemyData.inkImage;

    }




    void Update()
    {
        if (!debugSwitch)
        {



            count += 1;
            //waittTimeフレームごとにショットする（小さいほど早く打ってくる）
            if (count % waitTime == 0)
            {
                //キンをランダムに回転させる
                //float value_x = Random.Range(-40, 40);
                //float value_y = Random.Range(-140, -180);
                //transform.DORotate(new Vector3(value_x, value_y, 0), 0.5f);
                Kinshot();
            }

        }
    }

    public void Kinshot()
    {
        KinBullet kinBullet = Instantiate(kinBulletPrefab, transform.position, transform.rotation);
        //KinBulletクラスで作ったinkImageName変数にこのクラスで作ったinkImageNameを入れる
        kinBullet.inkImageName = inkImageName;
        kinBullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    //戻ってきた弾に当たったらHPを減少させる
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ReturnBullet")
        {
            kinStateManager.ProcDamage(col.gameObject.GetComponent<KinBullet>().damage);
            Debug.Log("ダメージ通ってるよ");
            Destroy(col.gameObject, 0.2f);
        }
    }

}
