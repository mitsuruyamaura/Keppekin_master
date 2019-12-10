using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIに表示されいる"汚れ"をタップで破壊するクラス
/// </summary>
public class Stain : MonoBehaviour
{
    public void DestroyStains()
    {
        Destroy(gameObject);
    }
}
