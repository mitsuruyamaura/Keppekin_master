using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayKinInfo : MonoBehaviour
{
    public Image kinImage;
    public TMP_Text kinName;
    public TMP_Text rarerityText;
    public Image kinType;
    public TMP_Text removeCountText;

    public void Display(KinData.KinDataList kinData)
    {
        kinImage.sprite = Resources.Load<Sprite>("Image/" + kinData.kinName);
        kinName.text = kinData.kinName;
        rarerityText.text = kinData.rarerity.ToString();
        kinType.sprite = Resources.Load<Sprite>("Type/" + kinData.kinType);
        removeCountText.text = kinData.removeCount.ToString();
    }


}
