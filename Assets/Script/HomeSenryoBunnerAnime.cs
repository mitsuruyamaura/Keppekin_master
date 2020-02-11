using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomeSenryoBunnerAnime : MonoBehaviour
{
    public Image bunner;

    void Start()
    {
		//bunner.transform.DOShakePosition(1f, 50f, 3, 0, false, true).SetLoops(-1, LoopType.Yoyo);
		//bunner.transform.DOPunchPosition(Vector3.one, 0.3f, 40, 5f, false).SetLoops(-1, LoopType.Yoyo);

		bunner.transform.DOMoveY(0.01f, 0.5f).SetLoops(-1, LoopType.Yoyo);

	}

    
}
