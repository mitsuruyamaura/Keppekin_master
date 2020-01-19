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

    public Button btnRightArrow;
    public Button btnLeftArrow;

    public Button btnHome;　//ホームへのシーン遷移
    public Button btnPreview; //previewシーンへの遷移

    public bool isPreview; //重複防止用のフラグ

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

    [Header("キン詳細ポップアップの生成位置")]
    //ヒエラルキー上のCanvasをアサインし、そこを生成位置とする
    public Transform kinPopupTran;

    //インスペクター上でIconNumberのイメージを6個入れる。
    //０が左端。
    public Image[] iconNumbers;



    //重複タップ防止用フラグ
    private bool isMoveButtonList;
    //現在のボタンリストの番号
    private int currentButtonListNo = 0;

    public PageScrollRect page;

    private int displayKinNo; //Previewシーンで参照するキンの番号を保存する


    void Start()
    {
        UIManager.instance.SwitchDisplayCanvas(1);

        //シーン遷移時のフェイドイン処理
        StartCoroutine(TransitionManager.instance.FadeIn());
        kinButtons = new KinDetail[GameData.instance.kinSO.kinMasterData.kinZukanData.Count];


        //KinDataに登録されているキンの数だけボタンを生成
        for (int i = 0; i < GameData.instance.kinSO.kinMasterData.kinZukanData.Count; i++)
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
            else if (i >=  16 && i < 24)
            {
                tran = kinButtonList[2].transform;
            }
            else if (i >= 24 && i < 32)
            {
                tran = kinButtonList[3].transform;
            }
            else if (i >= 32 && i < 40)
            {
                tran = kinButtonList[4].transform;
            }
            else if (i >= 40 && i < 42)
            {
                tran = kinButtonList[5].transform;
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
        btnRightArrow.onClick.AddListener(() => OnClickNextButtonList());

            //左矢印ボタンにメソッドを登録
            btnLeftArrow.onClick.AddListener(() => OnClickPrevButtonList());

        btnHome.onClick.AddListener(()=>StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.HOME)));

            //先頭なので左には戻れないようにする
            btnLeftArrow.gameObject.SetActive(false);

        //プレヴューシーンへの遷移
        btnPreview.onClick.AddListener(OnClickChooseKin);


        //先頭のキンのデータをデフォルトとして画面上部に表示
        Display(GameData.instance.kindata.kinDataList[0]);

        //初期位置の色を変える
        iconNumbers[0].color = new Color(1.0f, 0.259f, 0.471f);
        Debug.Log(iconNumbers[0].color);

    }


    public void Display(KinData.KinDataList kinData)
    {
        for (int i = 0; i < kinButtons.Length; i++)
        {
            if (kinButtons[i].kinData.kinNum == kinData.kinNum)
            {
                kinButtons[i].isSelect = true;
                kinButtons[i].kinImage.sprite = Resources.Load<Sprite>("Image/Icon_FaceImage/SelectIcon/s_" + kinData.kinName);

                //3Dモデルのあるキンか確認
                if (kinData.is3DModel)
                {
                    //あるならプレヴューボタンを押せるようにする
                    btnPreview.interactable = true;
                }
                else
                {
                    //ないなら灰色にして押せないようにする
                    btnPreview.interactable = false;
                }

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

        //現在表示されているキンの番号をkinDataからdisplayKinNoに入れておく
        displayKinNo = kinData.kinNum;

      

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
    private void OnClickNextButtonList(bool isSwipe = false)
    {
        if (!isMoveButtonList)
        {
            //ボタンの重複防止用のフラグを立てる
            isMoveButtonList = true;

            //左矢印ボタンを表示する
            btnLeftArrow.gameObject.SetActive(true);

            //ボタンリストの番号を１つ(ページ進める)
            currentButtonListNo++;

            //ボタンリストが最終ページなら右矢印ボタンを非表示にする
            if (currentButtonListNo >= kinButtonList.Count - 1)
            {
                btnRightArrow.gameObject.SetActive(false);
            }

            //スワイプでボタンリストを移動させていたら無視
            if (!isSwipe)
            {
                //スワイプしていない（ボタンを押していない）なら、1ページ進めたボタンリストを表示
                float destX = -currentButtonListNo * page.pageWidth;
                page.content.anchoredPosition = new Vector2(destX, page.content.anchoredPosition.y);
                page.tempIndex = -currentButtonListNo;

            }

            //ナンバーアイコンの色変更
            ChangeIconNumberImage();

           
            //再度ボタンを押せるようにする
            isMoveButtonList = false;
        }
    }


    /// <summary>
    /// ボタンリストを1つもどす
    /// </summary>
    private void OnClickPrevButtonList(bool isSwipe = false)
    {
        if (!isMoveButtonList)
        {
            //ボタンの重複防止用のフラグを立てる
            isMoveButtonList = true;

            //右矢印ボタンを表示する
            btnRightArrow.gameObject.SetActive(true);

   

            //ボタンリストの番号を一つページを戻す
            currentButtonListNo--;
            Debug.Log(currentButtonListNo);


            //すでにリストが最初のページなら左矢印ボタンを非表示にする
            if (currentButtonListNo <= 0)
            {
                btnLeftArrow.gameObject.SetActive(false);
            }

            //スワイプでボタンリストを移動させていたら無視
            if (!isSwipe)
            {
                //スワイプしていない（ボタンを押した）なら、1ページ戻したボタンリストを表示
                float destX = currentButtonListNo * page.pageWidth;
                page.content.anchoredPosition = new Vector2(destX, page.content.anchoredPosition.y);
                page.tempIndex = currentButtonListNo;
            }

            //ナンバーボタンの色変更
            ChangeIconNumberImage();

            //再度ボタンを押せるようにする
            isMoveButtonList = false;
        }
    }

    //スワイプに合わせてボタンの左右矢印ボタンの表示/非表示を切り替え
   public void OnClickArrowButton(int prevPageIndex)
    {
        //比較用にcurrentArrowButtonListNoを使うが、currentButtonListNoはこの後のメソッドで変更するので
        //ここではcurrentButtonListNoをいったん別の変数に入れて利用する
        int temp = currentButtonListNo;

        //prevPageIndexの値をみて矢印ボタンの表示状態を非表示に変更する
        if (prevPageIndex == 0)
        {
            btnLeftArrow.gameObject.SetActive(false);
        }

        if (prevPageIndex == kinButtonList.Count - 1)
        {
            btnRightArrow.gameObject.SetActive(false);
        }

        //比較してどちらの矢印ボタンのメソッドにするか決定する
        if (temp > prevPageIndex)
        {
            OnClickPrevButtonList(true);
        }
        else
        {
            OnClickNextButtonList(true);
        }

    }


    /// <summary>
    /// ボタンリストの位置にあるアイコンナンバーの色を変える
    /// </summary>
    private void ChangeIconNumberImage()
    {

        for (int i = 0; i < iconNumbers.Length; i++)
        {
            if (currentButtonListNo == i)
            {
                //ボタンリストの位置にあるアイコンの色を変更
                //特色は使う色のRGB値を255で割る。
                iconNumbers[i].color = new Color(1.0f, 0.259f, 0.471f);
            }
            else
            {
                //そのほかの位置にあるアイコンの色を白にする
                iconNumbers[i].color = new Color(1.0f, 1.0f, 1.0f);
            }

        }

    }

    private void OnClickChooseKin()
    {
        if (!isPreview)
        {
            isPreview = true;

            //GameDataにdisplayKinNo(現在表示されているキンの番号を渡す)
            GameData.instance.previewKinNo = displayKinNo;

            //シーン遷移
            StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.ZUKAN_PREVIEW));

        }

    }
}
