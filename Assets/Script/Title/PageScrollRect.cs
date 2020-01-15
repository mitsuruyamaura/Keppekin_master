using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// ボタンリストをスワイプで変更するためのクラス
/// </summary>
public class PageScrollRect : ScrollRect
{
    public float pageWidth;
    public int prevPageIndex = 0;
    public ZukanManager zukanManager;
    public int tempIndex;

    protected override void Awake() {
        base.Awake();
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    protected override void Start()
    {
        base.Start();
        zukanManager = GameObject.FindGameObjectWithTag("ZukanManager").GetComponent<ZukanManager>();
    }



    public override void OnBeginDrag(PointerEventData eventData) {
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData) {
        base.OnEndDrag(eventData);
        Debug.Log(eventData);
        Debug.Log(eventData.delta.x);
        
        StopMovement();


        //RoundToint　四捨五入
        //元の位置に戻らずに移動しているか確認用
        bool isMove = false;
        int pageIndex = Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);

        if (tempIndex != pageIndex)
        {
            isMove = true;
            tempIndex = pageIndex;
        }

        //Abs絶対値
        //-の数値が出たら-がとった整数にする処理
        //例えば　-3が3になる　3はそのまま3
        if (pageIndex == prevPageIndex && Mathf.Abs(eventData.delta .x) >= 5)
        {
            pageIndex += (int)Mathf.Sign(eventData.delta.x);
        }

        //スワイプ距離計算
        float destX = pageIndex * pageWidth;


        // content.anchoredPosition 箱ごと滑らかに動く
        content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);


        //元の位置に戻らずに移動しているなら
        if (isMove)
        {
            //矢印の表示を連動させる
            prevPageIndex = Mathf.Abs(pageIndex);
            zukanManager.OnClickArrowButton(prevPageIndex);
        }

        prevPageIndex = pageIndex;
    }
}
