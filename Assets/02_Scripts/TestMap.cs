using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void CheckGroundNormal()
    {
        RaycastHit downHit;
        if (Physics.Raycast(transform.position + transform.TransformVector(controller.center), -transform.up, out downHit))
        {
            Vector3 new_forward = Vector3.Cross(transform.right, downHit.normal);
            transform.rotation = Quaternion.LookRotation(new_forward, downHit.normal);
        }
    }
}
