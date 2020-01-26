using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;

public class ARInfo : MonoBehaviour
{

    public TMP_Text txtKinInfo;
    public TMP_Text txtKinName;
    public SpriteRenderer spriteRenderer;
    public KinData data;
    public ImageTargetBehaviour imageTargetBehaviour;
   
    void Start()
    {
        Debug.Log(imageTargetBehaviour);
        foreach (KinData.KinDataList kindata in data.kinDataList)
        {
            if (kindata.kinName == imageTargetBehaviour.ImageTarget.ImageTargetType.ToString())
            {
                txtKinInfo.text = kindata.info;
                txtKinName.text = kindata.kinName;
                spriteRenderer.sprite = Resources.Load<Sprite>("AR_UI/" + kindata.kinType);

            }

        }

    }
}
