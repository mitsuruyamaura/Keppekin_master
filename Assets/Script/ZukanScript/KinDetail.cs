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

    [Header("キン詳細ポップアップのプレファブ")]
    public KinInfoPu kinInfoPrefab;

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
        else　//選択中の場合
        {
            if (!isOpenPU)
            {
                StartCoroutine(CreateKinInfoPu());
            }
    
        }
    }

    //public void DisplayKin()
    //{
    //    zukanManager.Display(kinData);
    //}

    public IEnumerator CreateKinInfoPu()
    {
        //TODO
        //PUインスタンス
        //名前、属性、説明文、イメージ(2D)

        //重複防止用フラグを立てる
        isOpenPU = true;

        //ポップアップをインスタンスし、kinDataを渡して設定用メソッドを呼び出す
        KinInfoPu kinPop = Instantiate(kinInfoPrefab, zukanManager.kinPopupTran, false);

        kinPop.SetupPopUp(kinData);

        //1秒経ったら重複防止フラグを下す
        yield return new WaitForSeconds(1.0f);
        isOpenPU = false;

    }

}
