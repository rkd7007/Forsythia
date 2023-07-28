using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    public Quaternion TargetRotation;
    public Transform CameraVector;

    public float RotationSpeed;
    public float ZoomSpeed;         // 줌 스피드.
    public float Distance;          // 카메라와의 거리.

    private Vector3 AxisVec;        // 축의 벡터.
    private Vector3 Gap;

    private Transform MainCamera;   // 카메라 컴포넌트.

    void Start()
    {
        MainCamera = UnityEngine.Camera.main.transform;
    }

    void Update()
    {
        Zoom();
        CameraRotation();
    }

    // 카메라 줌.
    void Zoom()
    {
        Distance += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * -5;
        Distance = Mathf.Clamp(Distance, 100f, 300f);

        AxisVec = transform.forward * -1;
        AxisVec *= Distance;
        MainCamera.position = transform.position + AxisVec;
    }

    void CameraRotation()
    {
        if (transform.rotation != TargetRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, RotationSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            Gap.x += Input.GetAxis("Mouse Y") * RotationSpeed * -1;
            Gap.y += Input.GetAxis("Mouse X") * RotationSpeed;


            Gap.x = Mathf.Clamp(Gap.x, -5f, 80f);
            TargetRotation = Quaternion.Euler(Gap);

            //Quaternion q = TargetRotation;
            //q.x = q.z = 0;
            //CameraVector.transform.rotation = q;

        }

    }
}
