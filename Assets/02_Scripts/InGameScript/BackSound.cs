using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSound : MonoBehaviour
{
    bool IsOff;
    bool IsOn;
    public GameObject SoundOff;
    public GameObject SoundOn;
    public AudioSource BackAudio; //효과음 source

    void Start()
    {
        //배경음은 켜져 있는 것으로 시작됨
        IsOff = false;
        IsOn = true;
    }

    void Update()
    {
        if (IsOn) //켜져있으면
        {
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            BackAudio.mute = false;
        }
        if (IsOff) //꺼져있으면
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            BackAudio.mute = true;
        }
    }

    public void IsBackSound()
    {
        if (IsOn)
        {
            IsOn = false;
            IsOff = true;
        }
        else if (IsOff)
        {
            IsOff = false;
            IsOn = true;
        }
    }
}
