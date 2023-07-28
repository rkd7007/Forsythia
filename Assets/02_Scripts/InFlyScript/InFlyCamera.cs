using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFlyCamera : MonoBehaviour
{
    public static float speed = 400; //카메라 스피드

    void Start()
    {
        
    }
    
    void Update()
    {
        //새와 함께 움직여야 함으로 새가 앞으로 나아가는 스피드와 같아야 한다. Fly.BirdSpeed
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
