using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Fly : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player; //움직일 오브젝트
    public Rigidbody _player;

    bool right, left;

    float _Time;

    Quaternion StartRotate; //처음 시작 회전 값

    void Start()
    {
        _player.GetComponent<Rigidbody>();
        right = false;
        left = false;
        StartRotate = player.transform.rotation;
    }

    void Update()
    {
        //왼쪽 화면으로 나가려고 할 경우
        if (player.transform.position.x <= 460)
            player.transform.position = new Vector3(460, player.transform.position.y, player.transform.position.z);
        //오른쪽 화면으로 나가려고 할 경우
        if (player.transform.position.x >= 600)
            player.transform.position = new Vector3(600, player.transform.position.y, player.transform.position.z);

        if (right)
        {
            _player.AddForce(Vector3.right * TestFly.MoveSpeed);
            _player.transform.Rotate(new Vector3(0, 0, Time.deltaTime * -20));
        }

        if (left)
        {
            _player.AddForce(Vector3.left * TestFly.MoveSpeed);
            _player.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 20));
        }
    }
    //오른쪽으로 버튼을 누르면
    public void RightFly()
    {
        right = true;
    }
    //왼쪽으로 버튼을 누르면
    public void LeftFly()
    {
        left = true;
    }
    public void OffRightFly()
    {
        right = false;
    }
    public void OffLeftFly()
    {
        left = false;
    }
    //버튼 누르는 동안
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    //버튼에서 손을 떼면
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
