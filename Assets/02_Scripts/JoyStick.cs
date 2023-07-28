using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
    public Image Stick;
    private Vector3 orignPos = Vector3.zero;

    void Start()
    {
        orignPos = Stick.transform.position; //현재 조이스틱의 위치가 처음 위치이다
    }

    public void OnDrag()
    {
        Touch touch = Input.GetTouch(0); //Touch는 모바일 장치에서만 작동하므로 pc에서 오작동 일어날 수 있음

        if (Stick != null)
        {
            Stick.rectTransform.position = touch.position; //터치한 위치로 조이스틱이 움직인다
        }
        //터치한 곳과 조이스틱의 처음 위치를 기준으로 이동한 방향 구한 뒤 캐릭터 이동
        Vector3 dir = (orignPos - new Vector3(touch.position.x, touch.position.y, orignPos.z)).normalized;

        float touchAreaRadius = Vector3.Distance(orignPos, new Vector3(touch.position.x, touch.position.y, orignPos.z));
    }

    public void OnEndDrag()
    {
        //드래그(터치)가 끝나면 조이스틱을 처음 위치(원위치)로 이동시킨다
        if (Stick != null)
        {
            Stick.rectTransform.position = orignPos;
        }
    }
}