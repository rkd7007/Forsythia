using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public Transform target;    //추적할 타겟
    public float dist;    //카메라와의 거리
    public float height;   //카메라의 높이

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        tr.position = Vector3.Lerp(tr.position, target.position - (target.forward * dist)
                                       + (Vector3.up * height), Time.deltaTime * 3.0f);
    }
}
