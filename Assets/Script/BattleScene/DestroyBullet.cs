using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キンの打ってくる弾をタップして破壊するクラス
/// </summary>
public class DestroyBullet : MonoBehaviour
{
    [Header("Rayを飛ばした際の、キンの弾の判定用")]
    public LayerMask bulletLayer; //インスペクターでBulletを指定する

    [Header("Rayを飛ばす距離")]
    public float distance;　//インスペクターで。100くらい

    [Header("赤いRayの表示時間(Debug用)")]　//インスペクターで5.0くらい。
    public float duration;

    [Header("Rayを飛ばすカメラ")]
    public Camera arCamera; //3DCameraオブジェクトをインスペクターで指定する


    public KinStates nakamaKin; //まだ使わない
    public int attackPower; //KinDataから取得するが、今は空にしておく

    public GameData.BattleKinData nakamas;




    public void SetAttackPower(int kinPower)
    {
        attackPower = kinPower;
    }

    ////private void Start()
    //{
    //    nakamas = GameData.instance.nakamaDates;
    //    attackPower = 5; 
    //}

    void Update()
    {

        //マウスで左クリックするか、画面をタップした際に判定を行う
        if (Input.GetMouseButtonDown(0))
        {
            //Rayを初期化する(使えるようにする)
            Ray ray = new Ray();

            //Rayが当たったオブジェクトを格納する型をRaycastHitという
            //その変数であるHitを用意する
            RaycastHit hit = new RaycastHit();

            //カメラをスタート地点とし、画面のタップ地点に向けてRayを発射する
            ray = arCamera.ScreenPointToRay(Input.mousePosition);

            //ゲームビューではRayは見えないので、シーンビューにRayを可視化して表示する
            //引数は(①Rayのスタート地点、②Rayの方向と距離、③Rayの色、④カメラの深度表示をするかどうか)
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);

            //Physics.Raycastメソッドを使って、Rayの当たり判定を行う
            //引数は、(①Rayのスタート地点、②Rayの方向と距離、③もしも当たり判定が取れた場合にオブジェクトを代入する変数hit
            //④Rayを飛ばす最大距離=(Mathf.Infinity)は無制限、⑤Rayと当たり判定を行うLayer)
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit, Mathf.Infinity, bulletLayer))
            {
                Debug.Log(hit.collider.gameObject);
                

                //打ち返す場合、弾のRigidBodyを取得して画面の奥(transform.forward)に向かって力を加える
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * distance);

                //hit変数に格納されたゲームオブジェクト(弾)を破壊する
                //hit変数の型はRaycasthit型なのでそのまま書いても破壊できないが、(Raycasthitの変数).collider.gameobjectの書式で指定するとゲームオブジェクトを取得して破壊できる
                //Destroy(hit.collider.gameObject);

                Damage(hit.collider.gameObject);
               

            }
        }
    }

    private void Damage(GameObject hitObj)
    {
        Debug.Log("タグの切り替わり通ってるよ");
        hitObj.tag = "ReturnBullet";
        
        hitObj.layer = LayerMask.NameToLayer("ReturnBullet");
        Debug.Log(hitObj.layer);

        hitObj.GetComponent<KinBullet>().damage = attackPower;
    }

}
