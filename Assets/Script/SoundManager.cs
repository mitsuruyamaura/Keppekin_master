using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [Header("BGMのフェードイン、フェードアウトの時間")]
    public float fadeTime = 2.0f;

    public int index = -1; //現在鳴っているBGMの番号（配列の番号と連動）

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

    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="type"></param>
    public void PlayBgm(BGM_TYPE type)
    {
        if(index == (int)type)
        {
            return;
            //再生されている音源と同じ場合には何もしないで終了
        }

        //BGM_TYPEをint型にキャストして配列の番号に対応させる
        index = (int)type;

        //BGM用再生プレイヤーの音源をセットする（CDプレイヤーにセットするイメージ）
        bgmSource.clip = bgm[index];

        //BGMを再生
        bgmSource.Play();

        //Volumeを0にしておいて徐々に大きくして1(MAX)にする
        //MAXになるまで時間はfadeTimeで指定する
        bgmSource.DOFade(1, fadeTime);

    }

    /// <summary>
    /// BGMを止める
    /// </summary>
    /// <returns></returns>
    public IEnumerator StopBGM()
    {
        //BGMのVolumeを徐々に下げて0(min)にする
        bgmSource.DOFade(0, fadeTime);

        //下げている時間だけ待機する
        yield return new WaitForSeconds(fadeTime);

        //BGMを止める
        bgmSource.clip = null;

        //再生が終了したので配列用の番号を初期化する
        index = -1;
    }

}
