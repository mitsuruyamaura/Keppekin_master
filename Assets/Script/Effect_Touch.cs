using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Touch : MonoBehaviour
{
    //基本ここいじらない
    public ParticleSystem effectTouch; //タップエフェクト
    public Camera effectCamera; //カメラの座標

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //タップ地点のワールド座標までパーティクルを移動して、パーティクルエフェクトを1つ生成する
            Vector3 pos = effectCamera.ScreenToWorldPoint(Input.mousePosition + effectCamera.transform.forward * 10);
            effectTouch.transform.position = pos;
            effectTouch.Emit(1);

        }
        
    }
}
