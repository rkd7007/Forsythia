using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    /*public GameObject Player;

    public float offsetX = 15f;
    public float offsetY = 10f;
    public float offsetZ = -200f; //객체와 카메라 사이의 거리

    public float ZoomSpeed = 10f;     //줌인아웃 속도


    Vector3 CameraPos;

    private Camera mainCamera;
    private float distance;

    private void Start()
    {

    }

    void Zoom()
    {

        distance = Input.GetAxis("Mouse ScrollWheel") * -1 * ZoomSpeed;

        if (distance != 0)
        {
            offsetZ += distance;
            Debug.Log(distance);
        }
    }

    private void Update()
    {
        Zoom();
    }

    private void LateUpdate()
    {
        CameraPos.x = Player.transform.position.x + offsetX;
        CameraPos.y = Player.transform.position.y + offsetY;
        CameraPos.z = Player.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, CameraPos, 10 * Time.deltaTime);
    }*/ // 졸졸 잘따라다니는 카메라 셋팅


    public float MoveSpeed;

    private Transform Target;
    private Vector3 Pos;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Pos = transform.position;
        transform.position += (Target.position - Pos) * MoveSpeed;
    }

}
