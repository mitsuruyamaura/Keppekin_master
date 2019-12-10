using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    public void MoveHome()
    {
        //SceneManager.LoadScene("Home");
        StartCoroutine(SceneStateManager.instance.MoveScene(SCENE_TYPE.HOME));
    }

}
