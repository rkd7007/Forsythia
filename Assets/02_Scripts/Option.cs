using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject optoinPanel;

    void Start()
    {

    }
    
    void Update()
    {
        if (ButtonMa.OptionClick) //옵션을 눌러 켜졌으면
        {
            optoinPanel.SetActive(true); //옵션 판넬 보이게 함
            Time.timeScale = 0; //시간 멈춤
        }
        if (!ButtonMa.OptionClick) //옵션을 눌러 꺼졌으면
        {
            optoinPanel.SetActive(false); //옵션 판넬 안보이게 함
            Time.timeScale = 1;
        }
    }
}
