using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example02
{
    public class Cell : FancyScrollViewCell<ItemData, Context>
    {
        [SerializeField] Animator animator = default;
        [SerializeField] Text message = default;
        [SerializeField] Image image = default;
        [SerializeField] Button button = default;
        public KinData.KinDataList kindata;
        public Text cellKinName;
        public KinStates mochiKinStates;

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

            var selected = Context.SelectedIndex == Index;
            image.color = selected
                ? new Color32(255, 255, 255, 255) //true
                : new Color32(255, 255, 255, 100); //false
            image.sprite = Resources.Load<Sprite>("Image/" + kindata.kinName);

            //mochiKinStatesにkindata(スクリプタブルオブジェクトのデータ)を移植
            mochiKinStates.kinName = kindata.kinName;
            mochiKinStates.rarelity = kindata.rarerity;
            mochiKinStates.level = kindata.level;
            mochiKinStates.rundomNum = kindata.kinNum;
            mochiKinStates.inkColor = kindata.inkImage;
            mochiKinStates.type = kindata.kinType;
            mochiKinStates.nakamaKinNum = kindata.nakamaKinNum;
        

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

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;

        void OnEnable() => UpdatePosition(currentPosition);
    }
}
