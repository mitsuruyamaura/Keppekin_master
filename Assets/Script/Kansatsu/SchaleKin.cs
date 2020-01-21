using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchaleKin : MonoBehaviour
{
    [Header("自分のリジッドボディを入れる")]
    public Rigidbody2D rb;

    [Header("自分のtag")]
    public string myTag;

    [Header("相手のtag")]
    public string enemyTag;

    [Header("移動速度")]
    public float speed;

    [Header("キンのタイプ")]
    public KIN_TYPE kinType;

    [Header("キンの画像イメージ")]
    public Image imgKinImage;

    private bool isSetUp; //セット終了のフラグ
    private Vector2 direction; //移動する方向

    public enum State
    {
        //キンの状態用
        STOP,
        SET_UP,
        MOVE,
    }


    [Header("キンの現在の状態")]
    public State state = State.STOP; //初期値を設定しておく


    /// <summary>
    /// 初期設定を行う。このクラスとインスタンスと同時に呼び出してもらう
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="tag"></param>
    /// <param name="isMoveDir"></param>
    public void Init(string name, KIN_TYPE type, string tag, bool isMoveDir)
    {
        //イメージキンとキンタイプの表示設定
        imgKinImage.sprite = Resources.Load<Sprite>("Image/" + name);

        //用意していた変数に引数で受け取った情報を入れこむ
        kinType = type;

        //Tagをかえる
        myTag = tag;
        gameObject.tag = myTag;

        //Tagに合わせて敵となるTagを設定する
        if (gameObject.tag == "Enemy")
        {
            enemyTag = "Rival";
        }
        else
        {
            enemyTag = "Enemy";
        }

        //移動速度を生成位置(上下)に合わせてランダムに設定
        if (isMoveDir)
        {
            //上で生成なら下方向に移動
            speed = Random.Range(2.5f, 5);

        }
        else
        {
            //下で生成なら上方向に移動
            speed = Random.Range(2.5f, 5);
        }

        state = State.SET_UP;

    }


    void Update()
    {
        //StateがSTOPなら、キンの動きを止めておく
        if (state == State.STOP)
        {
            return;
        }

        //StateがSET_UPなら、キンの動きはじめる方向を決める
        if (state == State.SET_UP && !isSetUp)
        {
            //一回だけ入るようにフラグで制御する
            //Enumのみの場合、処理のタイミングによっては複数回入る可能性があるためフラグも作る
            isSetUp = true;

            //動き始める方向を設定
            SetDirection();

            //キンの移動を開始させる
            state = State.MOVE;
        }

        //StateがMOVEなら、キンを移動させる
        if (state == State.MOVE)
        {
            //移動
            Move();
        }
    }


    /// <summary>
    /// キンの動き始める方向を設定
    /// </summary>
    private void SetDirection()
    {
        //移動先を360度のいずれかの方向をランダムで設定
        direction = new Vector2(Random.Range(-2.5f, 2.5f), 1).normalized;
    }



    /// <summary>
    ///キンの移動 
    /// </summary>
    private void Move()
    {
        //設定した方向に移動速度分ずつ移動させる
        rb.velocity = speed * direction * transform.localScale.x;
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == myTag)
        {
            //シャーレの壁か味方にぶつかった時に跳ね返る
            SetDirection();
        }

        if (col.gameObject.tag == enemyTag)
        {
            //接触した相手が味方ではないなら破壊
            Destroy(gameObject);
        }
    }
}
