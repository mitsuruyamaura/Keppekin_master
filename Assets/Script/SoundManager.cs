using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public enum BGM_TYPE
    {
        HOME,
        BATTLE,
    }

    public AudioClip[] bgm;
    public AudioClip[] se;

    public AudioSource bgmSource;
    public AudioSource seSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayBgm(BGM_TYPE type)
    {
        int index = (int)type;
        bgmSource.clip = bgm[index];
        bgmSource.Play();
    }

}
