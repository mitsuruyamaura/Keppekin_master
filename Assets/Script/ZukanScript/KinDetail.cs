using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinDetail : MonoBehaviour
{
    public Button buttonKinInfo;
    public bool isSelect;
    public KinData.KinDataList kinData;
    public DisplayKinInfo displayKinInfo;

    void Start()
    {
        buttonKinInfo.onClick.AddListener(OnclickSelect);
    }

    public void Init(KinData.KinDataList data, DisplayKinInfo display)
    {
        kinData = data;
        displayKinInfo = display;
    }

    public void OnclickSelect()
    {
        if (!isSelect)
        {

            isSelect = true;
            DisplayKin();

        }
        else
        {
            CreateKinInfoPu();
        }
    }

    public void DisplayKin()
    {
        displayKinInfo.Display(kinData);
    }

    public void CreateKinInfoPu()
    {
        //TODO
        //PUインスタンス
        //名前、属性、説明文、イメージ(2D)
    }

}
