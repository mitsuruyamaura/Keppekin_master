using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using FancyScrollView.Example02;

    public class Example03 : MonoBehaviour
    {
        public ScrollView[] scrollView = default;
        public KinData kindata;

        public List<KinData.KinDataList> nakamas = new List<KinData.KinDataList>();

        
        void Start()
        {
            foreach (KinData.KinDataList data in kindata.kinDataList)
            {
                nakamas.Add(data);
            }

        //Enumrable.Range(0, kindata.kinDataList.Count)
        //0から第二引数までの全ての値を使う
        //for文とほぼ一緒


        //using System.Linq; がある時 Selectメソッドは入った順番から並べてくれる
        ItemData[] items = Enumerable.Range(0, nakamas.Count).Select(i => new ItemData(nakamas[i])).ToArray();

            for (int i = 0; i < scrollView.Length; i++)
            {
                scrollView[i].UpdateData(items);
                scrollView[i].SelectCell(0);
            }

        }

    }

