using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinDetail : MonoBehaviour
{
    public Button buttonKinInfo;
    public bool isSelect;
    public KinData.KinDataList kinData;
    public ZukanManager zukanManager;
    public Image kinImage;

    private bool isOpenPU;

    public KinInfoPu kinInfoPuPrefab;

    public string folderName;


    void Start()
    {
        buttonKinInfo.onClick.AddListener(OnclickSelect);
    }

    public void Init(KinData.KinDataList data, ZukanManager display)
    {
        kinData = data;
        zukanManager = display;

        //TODO IconFaceが出来上がったら""変更する
        kinImage.sprite = Resources.Load<Sprite>("Image/Icon_FaceImage/" + kinData.kinName);
    }

    public void OnclickSelect()
    {
        if (!isSelect)
        {
            zukanManager.Display(kinData);

            //isSelect = true;
            //DisplayKin();

        }
        else
        {
            CreateKinInfoPu();
        }
    }

    //public void DisplayKin()
    //{
    //    zukanManager.Display(kinData);
    //}

    public void CreateKinInfoPu()
    {
        //TODO
        //PUインスタンス
        //名前、属性、説明文、イメージ(2D)
    }

}
