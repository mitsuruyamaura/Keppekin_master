using UnityEngine;

public class ObjCtrl : MonoBehaviour {

    [Header("回転させるオブジェクト")]
    public GameObject obj;

    //回転用
    Vector2 sPos;   // タッチした座標
    Quaternion sRot;// タッチしたときの回転
    float wid, hei, diag;  // wid、heiには端末のスクリーンサイズをとり、diagに比率を入れる
    float tx, ty;    // 回転時のposition.xとposition.yにかけるための変数を入れる

    //ピンチイン ピンチアウト用
    float vMin = 0.5f, vMax = 2.0f;  // 倍率制限
    float sDist = 0.0f, nDist = 0.0f; // 距離変数
    Vector3 initScale; // 回転させるオブジェクトの最初の大きさ
    float v = 1.0f; // 現在倍率。これを更新してスケールにかけることで大きさをかえる

    void Start() {
        // 端末ごとのスクリーンの高さと幅を記録する
        wid = Screen.width;
        hei = Screen.height;

        // Mathf.Sqrtを使い、平方根(ルート)を算出し、画面比率を計算する
        // Mathf.Pow(float_1,float_2)で引数設定。float_1を(float_2)乗(ここでは2乗)する
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));

        // 回転させるオブジェクトのスケールを記録
        initScale = obj.transform.localScale;
    }

    void Update() {
        if (Input.touchCount == 1) {
            // １つの指で画面をタップ、あるいはスワイプしているなら回転させる計算を行う
            Touch t1 = Input.GetTouch(0);
            if (t1.phase == TouchPhase.Began) {
                sPos = t1.position;
                sRot = obj.transform.rotation;
            } else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary) {
                // 指が画面上を移動している、あるいは指は画面に触れてはいるが動いていないとき
                tx = (t1.position.x - sPos.x) / wid; // 横移動量(-1<tx<1)
                ty = (t1.position.y - sPos.y) / hei; // 縦移動量(-1<ty<1)
                obj.transform.rotation = sRot;
                obj.transform.Rotate(new Vector3(90 * ty, -90 * tx, 0), Space.World);
            }
        } else if (Input.touchCount >= 2) {
            // ２つの指で画面をタップ、あるいはスワイプしていたらピンチイン ピンチアウト
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);
            if (t2.phase == TouchPhase.Began) {
                sDist = Vector2.Distance(t1.position, t2.position);   // タッチした指の2点間の距離を算出しスタート地点にする
            } else if ((t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary) &&
                       (t2.phase == TouchPhase.Moved || t2.phase == TouchPhase.Stationary)) {
                nDist = Vector2.Distance(t1.position, t2.position);   // タッチした指の2点間の距離を算出し移動後の地点にする 
                v = v + (nDist - sDist) / diag;  // 動いた指の方向からタッチした時の方向を引いて画面比率で割る
                sDist = nDist;   // スタート地点を移動後地点に更新
                if (v > vMax) {  // 拡大する場合、最大値を超えないようにし、超える場合には最大値に抑える
                    v = vMax; 
                }
                if (v < vMin) {  // 縮小する場合、最小値を超えないようにし、超える場合には最小値に抑える
                    v = vMin; 
                }
                obj.transform.localScale = initScale * v;  // スケールに比率をかけて新しいスケールに更新する
            }
        }
    }
}
