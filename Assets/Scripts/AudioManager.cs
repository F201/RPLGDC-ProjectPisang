using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;

    [SerializeField]
    private AudioSource bgm, select, jump,bgmFail, bgmScore, bgmWin, bgmLolos;

    private void Start()
    {
        if(audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void JumpSound()
    {
        jump.Play();
    }

    public void selectSound()
    {
        select.Play();
    }

    public void SetActiveBGM(bool active)
    {
        bgm.enabled = active;
    }

    public void PlayFailedBGM()
    {
        bgmFail.Play();
    }

    public void PlayBGMScore()
    {
        bgmScore.Play();
    }

    public void PlayBGMwin()
    {
        bgmScore.Play();
    }

    public void PlayBGMLose()
    {
        bgmScore.Play();
    }
}
