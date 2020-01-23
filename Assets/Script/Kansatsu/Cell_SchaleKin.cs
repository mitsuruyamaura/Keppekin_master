using FancyScrollView;
using FancyScrollView.Example02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cell_SchaleKin : FancyScrollViewCell<ItemData, Context>
{

    public Animator animator = default;
    public Text message = default;
    public Image image = default;
    public Button button = default;
    public KinData.KinDataList kindata;
    public Text cellKinName;

    public Image typeImage;


    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }


    void Start()
    {
        button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
    }


    public override void UpdateContent(ItemData itemData)
    {
        kindata = itemData.Kindata;
        cellKinName.text = kindata.kinName;
        typeImage.sprite = Resources.Load<Sprite>("Type/" + kindata.kinType);



        //Context.SelectedIndexとIndexが一緒ですかって聞いてる
        //一行で下のif文すませてる
        //if (Context.SelectedIndex == Index)
        //{
        //    selected = true;
        //}
        //else
        //{
        //    selected = false;
        //}
        bool selected = Context.SelectedIndex == Index;



        image.color = selected
            ? new Color32(255, 255, 255, 255) //true
            : new Color32(255, 255, 255, 100); //false
        image.sprite = Resources.Load<Sprite>("Image/" + kindata.kinName);
    }


    public override void UpdatePosition(float position)
    {
        currentPosition = position;

        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }

        animator.speed = 0;
    }

    //GameObjectが非アクティブになると Animator　がリセットされてしまうため
    //現在位置を保持しておいて、OnEnable のタイミングで現在位置を再設定します
    float currentPosition = 0;

    void OnEnable()
    {
        UpdatePosition(currentPosition);
    }


}
