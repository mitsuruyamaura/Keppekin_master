using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZukanManager : MonoBehaviour
{
    //UI関連
    public Image kinImage;
    public TMP_Text kinName;
    public Image kinType;
    public TMP_Text removeCountText;

    public Button btnRightArrow;
    public Button btnLeftArrow;

    [Header("キンのボタンのプレファブ")]
    public KinDetail kinDetailPrefab;
    [Header("キンのボタンの生成位置")]
    public Transform kinDetailTran;

    [Header("レアリティのプレファブ")]
    public GameObject rarelityPrefab;
    [Header("レアリティアイコンの生成位置")]
    public Transform rarelityTran;

    public List<GameObject> kinButtonList = new List<GameObject>(6);
    public KinDetail[] kinButtons;
    public Transform popupTran;


    //重複タップ防止用フラグ
    private bool isMoveButtonList;
    //現在のボタンリストの番号
    private int currentButtonListNo = 0; 


    void Start()
    {
        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());
        kinButtons = new KinDetail[GameData.instance.kindata.kinDataList.Count];


        //KinDataに登録されているキンの数だけボタンを生成
        for (int i = 0; i < GameData.instance.kindata.kinDataList.Count; i++)
        {
            Transform tran = kinButtonList[0].transform;
            if (i < 8)
            {
                tran = kinButtonList[0].transform;
            }
            else if (i >= 8 && i < 16)
            {
                tran = kinButtonList[1].transform;
            }


            KinDetail kinButton = Instantiate(kinDetailPrefab, tran, false);
            kinButton.Init(GameData.instance.kindata.kinDataList[i], this);
            kinButtons[i] = kinButton;
        }
            
            //最初のボタンリストを表示し、他のボタンリストは非表示にする。
            //for (int i = 0; i < kinButtonList.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        kinButtonList[i].SetActive(true);
            //    }
            //    else
            //    {
            //        kinButtonList[i].SetActive(false);
            //    }
            //}

            //右矢印ボタンにメソッドを登録
            btnRightArrow.onClick.AddListener(OnClickNextButtonList);

            //左矢印ボタンにメソッドを登録
            btnLeftArrow.onClick.AddListener(OnClickPrevButtonList);

            //先頭なので左には戻れないようにする
            btnLeftArrow.gameObject.SetActive(false);

        //先頭のキンのデータをデフォルトとして画面上部に表示
        Display(GameData.instance.kindata.kinDataList[0]);
    }


    public void Display(KinData.KinDataList kinData)
    {
        for (int i = 0; i < kinButtons.Length; i++)
        {
            if (kinButtons[i].kinData.kinNum == kinData.kinNum)
            {
                kinButtons[i].isSelect = true;
                kinButtons[i].kinImage.sprite = Resources.Load<Sprite>("Image/Icon_FaceImage/SelectIcon/s_" + kinData.kinName);
            }
            else
            {
                kinButtons[i].isSelect = false;
                kinButtons[i].kinImage.sprite = Resources.Load <Sprite>("Image/Icon_FaceImage/" + GameData.instance.kindata.kinDataList[i].kinName);
            }
        }

        kinImage.sprite = Resources.Load<Sprite>("Image/" + kinData.kinName);
        kinName.text = kinData.kinName;
        
        kinType.sprite = Resources.Load<Sprite>("Type/" + kinData.kinType);
        //removeCountText.text = kinData.removeCount.ToString();

        //キンのレアリティに応じてアイコンを生成する
        //すでに生成されている場合は生成されている数を比べて、違う場合は破棄する。
        if (rarelityTran.childCount != kinData.rarerity)
        {
            foreach (Transform child in rarelityTran)
            {
                Destroy(child.gameObject);
            }

            //レアリティの数だけアイコン生成
            for (int i = 0; i < kinData.rarerity; i++)
            {
                Instantiate(rarelityPrefab, rarelityTran, false);
            }

        }


    }


    /// <summary>
    /// ボタンリストを1ページ進める
    /// </summary>
    private void OnClickNextButtonList()
    {
        if (!isMoveButtonList)
        {
            //ボタンの重複防止用のフラグを立てる
            isMoveButtonList = true;
            btnLeftArrow.gameObject.SetActive(true);

            //現在表示中のボタンリストを非表示にする
            kinButtonList[currentButtonListNo].SetActive(false);

            //ボタンリストの番号を1つ(ページ)進める
            currentButtonListNo++;

            //すでにリストが最終ページなら最初のページ番号にする
            if (currentButtonListNo >= kinButtonList.Count - 1)
            {
                btnRightArrow.gameObject.SetActive(false);
            }

            //1ページ進めたボタンリストを表示
            kinButtonList[currentButtonListNo].SetActive(true);

            //再度ボタンを押せるようにする
            isMoveButtonList = false;



        }
    }

    private void OnClickPrevButtonList()
    {
        if (!isMoveButtonList)
        {
            //ボタンの重複防止用のフラグを立てる
            isMoveButtonList = true;

            btnRightArrow.gameObject.SetActive(true);

            //現在表示中のボタンリストを非表示にする
            kinButtonList[currentButtonListNo].SetActive(false);

            //ボタンリストの番号を一つページを戻す
            currentButtonListNo--;

            //すでにリストが最初のページなら最後のページ番号にする
            if (currentButtonListNo <= 0)
            {
                btnLeftArrow.gameObject.SetActive(false);
            }

            //1ページ戻したボタンリストを表示
            kinButtonList[currentButtonListNo].SetActive(true);

            //再度ボタンを押せるようにする
            isMoveButtonList = false;
        }
        
        
    }

    //スワイプに合わせてボタンの表示/非表示を切り替え
    void Update()
    {
        if (page.prevPageIndex == 0)
        {
            btnRightArrow.gameObject.SetActive(false);
            btnLeftArrow.gameObject.SetActive(true);
        }
        Debug.Log(page.prevPageIndex);
    }

}
