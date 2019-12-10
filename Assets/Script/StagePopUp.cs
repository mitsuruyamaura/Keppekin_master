using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePopUp : MonoBehaviour
{
    public GameObject backGround;
    public Image kinImage;
    public Image typeImage;
    public GameObject kinName;
    //public GameObject levelImagePrefab;
    //public GameObject rarelityImagePrefab;

    public Transform levelPlace;
    public Transform rarelityPlace;


    public GameObject debugPanel;
    public BattleDebug battleDebug;

    public Button winButton;
    public Button loseButton;

    public bool isDebugBattleOn;

    public KinStates battlekinStates;
    public KinStates nakamaKinStates;


    //初めからイメージ3個置いておく
    //レアリティの数だけ表示する
    public Image[] levelImages;
    public Image[] rarelityImages;


    /// <summary>
    /// 強さとレアリティの値に応じてイメージを生成する
    /// </summary>
    public void SetUp(KinStates states) 
    {
        //Stageで選択されたバトルするキンの情報を取得
        battlekinStates = states;

        //JyunbiPopUpで処理していた画像イメージの取得をここで行う
        kinImage.sprite = Resources.Load<Sprite>("Image/" + battlekinStates.kinName);
        kinName.GetComponent<Text>().text = battlekinStates.kinName;
        typeImage.sprite = Resources.Load<Sprite>("Type/" + battlekinStates.type);

        //強さとレアリティの値に合わせてイメージを生成する
        for (int i = 0; i<battlekinStates.level; i++)
        {
            //コンポーネントのチェックがtrueにすると入る
            levelImages[i].enabled = true;

        }

        for (int i =0; i<battlekinStates.rarelity; i++)
        {
            //コンポーネントのチェックがtrueにすると入る
            rarelityImages[i].enabled = true;
           
        }

        //BattleDebugを探して紐付けする
        //battleDebug = GameObject.FindGameObjectWithTag("Stage").GetComponent<BattleDebug>();

        //ボタンに外からメソッドを登録できるonClickのスクリプト版
        //AddListenerに登録できるメソッドは引数を持ってないメソッドだけ
        //登録したい場合は　AddListener(() => battleDebug.Win(level, rarelity))のようにする(ラムダ式)
        //private void Test () {    //  () => の部分
        //battleDebug.Win(level, rarelity);
        //}
    //winButton.onClick.AddListener(() => battleDebug.Win(battlekinStates.level, battlekinStates.rarelity));
        //loseButton.onClick.AddListener(battleDebug.Lose);
    }





    public void ClosePopUp()

    {
        //シーケンス宣言して、Appendで順番に処理を書いていく、秒数の合計はDestroyの処理時間に合わせる
        Sequence sequence = DOTween.Sequence();
        sequence.Append(backGround.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetEase(Ease.Linear));
        sequence.Append(backGround.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.25f).SetEase(Ease.Linear));
                sequence.Join(backGround.GetComponent<CanvasGroup>().DOFade(0, 0.25f).SetEase(Ease.Linear));


        //backGround.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1.0f);
        //第二引数にfloat型で破壊されるまでの待ち時間を指定できる
        Destroy(gameObject, 0.35f);
    }

    public void OnClickBattleButton()
    {
        if (isDebugBattleOn)
        {
            //SetUpメソッドで渡された変数statesをbattlekinstatesにいれ、
            //バトルボタンが押されるとbattlekinstateの各データがGameDataクラスに渡される
            //Gamedataクラスはシングルトンでシーンを遷移しても破壊されないので、バトルシーンにタップしたきんのデータが持っていける

            //バトルするキンのデータをGameDataに渡す
            GameData.BattleKinData saveEnemyKindata = new GameData.BattleKinData();
            saveEnemyKindata.kinNum = battlekinStates.rundomNum;
            saveEnemyKindata.kinName = battlekinStates.kinName;
            saveEnemyKindata.kinRarelity = battlekinStates.rarelity;
            saveEnemyKindata.kinLebel = battlekinStates.level;
            saveEnemyKindata.kinType = battlekinStates.type;
            saveEnemyKindata.inkImage = battlekinStates.inkColor;

            //仲間菌のデータをGameDataに渡す
            //スクロールされて出てきた仲間(真ん中の子)の情報
            GameData.BattleKinData saveNakamaKindata = new GameData.BattleKinData();
            saveNakamaKindata.kinNum = nakamaKinStates.rundomNum;
            saveNakamaKindata.kinName = nakamaKinStates.kinName;
            saveNakamaKindata.kinRarelity = nakamaKinStates.rarelity;
            saveNakamaKindata.kinLebel = nakamaKinStates.level;
            saveNakamaKindata.kinType = nakamaKinStates.type;
            saveNakamaKindata.inkImage = nakamaKinStates.inkColor;
            saveNakamaKindata.nakamaKinNum = nakamaKinStates.nakamaKinNum;



            GameData.instance.enemyDatas = saveEnemyKindata; //stagepopupで宣言した変数たちをenemydataというGameDataクラスで新しく作った変数に移植した
            GameData.instance.nakamaDates = saveNakamaKindata; 
            
            Debug.Log("通ってる");
            StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.BATTLE));
        }
        else
        {
            Debug.Log("通ってない");
            debugPanel.SetActive(true);
        }
        

    } 

    public void CloseDebugPanel()
    {
        debugPanel.SetActive(false);
        ClosePopUp();
    }


}
