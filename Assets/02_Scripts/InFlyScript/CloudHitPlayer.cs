using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHitPlayer : MonoBehaviour
{
    float MoreSpeedTime;
    bool IsHit;
    public static bool Onparicle;

    float CloudScore = 300;

    void Update()
    {
        if(IsHit == true)
        {
            MoreSpeedTime += Time.deltaTime; //일정한 시간에 따라 증가함
        }
        //구름링을 통과한 뒤 1.5초가 지나면
        if (MoreSpeedTime >= 1.5f)
        {
            TestFly.BirdSpeed = 400; //속도 낮추기
            InFlyCamera.speed = 400;
            MoreSpeedTime = 0f; //시간 초기화
            IsHit = false;
            Onparicle = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") //구름링이 Player 태그와 충돌하면
        {
            TestFly.BirdSpeed = 550; //속도 증가
            InFlyCamera.speed = 550;
            IsHit = true;
            Onparicle = true;

            ScoreManager.Score += CloudScore; //300점 증가
        }
    }
}
