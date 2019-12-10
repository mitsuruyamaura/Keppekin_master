using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成される弾のクラス
/// </summary>
public class KinBullet : MonoBehaviour
{
    [Header("生成された弾の生存時間")]
    public float duration;

    private float timer; //生存時間のカウント用

    public float damage;

    public string inkImageName;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            //プレイヤーに当たらない場合、生存時間経過で破壊
            Destroy(gameObject);
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
