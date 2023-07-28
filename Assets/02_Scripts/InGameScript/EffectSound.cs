using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    bool IsOff;
    bool IsOn;
    public GameObject SoundOff;
    public GameObject SoundOn;
    public GameObject EffecSound; //효과음 source 오브젝트를 가져온다

    void Start()
    {
        //효과음은 켜져 있는 것으로 시작됨
        IsOff = false;
        IsOn = true;
    }

    void Update()
    {
        if (IsOn) //켜져있으면
        {
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            EffecSound.SetActive(true); //오브젝트가 생성되어있어 소리가 난다
        }
        if (IsOff) //꺼져있으면
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            EffecSound.SetActive(false); //오브젝트가 없어져 소리가 안 난다
        }
    }

    public void IsEffectSound()
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
