using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class KinInfoPu : MonoBehaviour
{
    //UI関連
    public TMP_Text txtKinName;
    public TMP_Text txtInfo;
    public Image imgKinImage;
    public Image imgKinType;
    public Button btnClose;
    public CanvasGroup canvasGroup;

    /// <summary>
    /// ポップアップに表示するキンの詳細を設定
    /// </summary>
    public void SetupPopUp(kinMasterData.KinZukanData kinData)
    {
        //アルファを0にしておいて、フェイドインさせて表示する
        canvasGroup.DOFade(1.0f, 0.5f);

        //キンの名前表示
        txtKinName.text = kinData.katakanaName;

        //キン情報を表示
        //キンデータの文字列をnの部分で区切り、それを順番にstring配列に入れる
        //例：人の手に常にいる。n汚れがつきやすそうなところを見つけると手に持っているペンでマーキングしてしまうらしい。n
        //１つ目の配列 "人の手に常にいる。n"
        //2つ目の配列　"汚れがつきやすそうなところを見つけると手に持っているペンでマーキングしてしまうらしい。n"
        //Split('①')...' 'で囲まれた①で分ける
        string[] info = kinData.info.Split('n');

        //配列の要素数の最大値数だけ処理を繰り返す
        for (int i = 0; i < info.Length; i++)
        {
            //不要なnを削除する
            //Replace(①、②)
            //①を②に変えるメソッド
            info[i].Replace("n", "");

            //配列の順番に表示内容を追加する(+= なので前のテキストに次のテキストが追加される)
            txtInfo.text += info[i] + "\n";
            //改行する
            //txtInfo.text += "¥n";
        }

        //キンのイメージ表示
        imgKinImage.sprite = Resources.Load<Sprite>("Image/" + kinData.kinName);

        //属性アイコン表示
        imgKinType.sprite = Resources.Load<Sprite>("Type/" + kinData.kinType);

        //閉じるボタンアイコンにメソッドを登録
        btnClose.onClick.AddListener(OnClickClosePopUp);

    }

    public void OnClickClosePopUp()
    {
        //いきなり消さずにアルファを0にしてPUをフェイドアウトさせる
        canvasGroup.DOFade(0f, 0.5f);
        Destroy(gameObject, 0.5f);
    }



}
