using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KinAnimation : MonoBehaviour
{
    int frameCnt = 0; //フレームカウント
    [Header("振り幅。ここを変えると上下の揺れ幅が変わる")]
    public float amplitude = 0.001f;


    //FixedUpdateはフレームレートの端末差を無くしてくれる。1フレームに一回の保証付
    void FixedUpdate()
    {
        FuwaFuwa(); 
    }


    /// <summary>
    /// ふわふわ動かす
    /// </summary>
    void FuwaFuwa()
    {
        frameCnt += 1;

        if (10000 <= frameCnt)
        {
            frameCnt = 0;
        }

        if (0 == frameCnt % 2)
        {
            //上下に振動させる（ふわふわ表現）
            float posYSin = Mathf.Sin(2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f));
            transform.DOMove(new Vector3(0, amplitude * posYSin, 0), 0.0f);

        }
    }

}
