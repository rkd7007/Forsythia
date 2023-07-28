using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public float MoveSpeed; //카메라맨 스피드

    private Transform Target; //플레이어 Pos
    private Vector3 Pos;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.Rotate(2.2f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Pos = transform.position;
        Pos.y -= 25;
        Pos.x += 10;
        transform.position += (Target.position - Pos) * MoveSpeed * Time.deltaTime;

    }
}
