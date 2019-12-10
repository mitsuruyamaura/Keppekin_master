using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEditor;

public class ARManager : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab; //生成するオブジェクト
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    
    GameObject obj;
    public Camera arCamera;


    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Ended)
            {
                return;
            }

            if (obj == null)
            {

                if (raycastManager.Raycast(touch.position, raycastHitList, TrackableType.All))
                {
                    Debug.Log("RayCast成功");
                    obj = Instantiate(objectPrefab, raycastHitList[0].pose.position, raycastHitList[0].pose.rotation);
                }
                else
                {
                    Debug.Log("Raycast失敗");
                }

            }
        }

        //objが入ったら下の処理が動く
        if (obj != null)
        {
            obj.transform.LookAt(arCamera.transform);
        }

        
    }
}
