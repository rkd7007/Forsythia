using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    public GameObject Green;
    public GameObject Yellow;
    public GameObject joyStick;
    public GameObject joyStickBack;
    public GameObject jumpBnt;

    public void ChangeBtton()
    {
        Vector3 Temp = new Vector3(0,0,0);

        //색상 변경
        Temp = Green.transform.position;
        Green.transform.position = Yellow.transform.position;
        Yellow.transform.position = Temp;

        //실제로 조이스틱 위치와 점프 버튼 위치 변경
        Temp = jumpBnt.transform.position;
        jumpBnt.transform.position = joyStick.transform.position;
        joyStick.transform.position = Temp;
        joyStickBack.transform.position = Temp;

        //조이스틱 컨트롤 위해서 위치 변경된 값을 전달.
        Controller.StickFirstPos = joyStick.transform.position;
    }
}
