using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgm, select, jump,bgmFail;

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
}
