using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace FancyScrollView.Example02
{
    public class Example02 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
        [SerializeField] Text selectedItemInfo = default;
        public KinData kindata;

        public List<KinData.KinDataList> nakamas = new List<KinData.KinDataList>();

        void Start()
        {
            scrollView.OnSelectionChanged(OnSelectionChanged);

            foreach (KinData.KinDataList data in kindata.kinDataList)
            {
                if (data.nakamaKinNum > 0)
                {
                    nakamas.Add(data);
                }
            }


            //Enumerable.Range(0, kindata.kinDataList.Count)
            //0から第二引数までの値を全て使う
            //forぶんとほぼ一緒
            var items = Enumerable.Range(0, nakamas.Count)
                .Select(i => new ItemData(nakamas[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }

        void OnSelectionChanged(int index)
        {
            selectedItemInfo.text = $"Selected item info: index {index}";
        }
    }
}
