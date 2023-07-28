using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public float moveSpeed = 15;
   

    private Vector3 moveDir;
    private Vector3 PrevPos;

    private float SwipeLength;
    

    private Vector2 StartPos;
    private Vector2 LastPos;    

    public GameObject Target;
    //public GameObject MainCam;
    // SP와 LP 사이의 거리가 일정 이상일 경우 인게임 화면 

    void Start()
    {
        PrevPos = this.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //캐릭터의 콜라이더 박스안에 있는지 
        {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);


            //if (Ray.Equals(Target.transform.position, Input.mousePosition))
            //{
            //    this.StartPos = Input.mousePosition;
            //}
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.transform.tag.Equals("Player"))
                {
                    this.StartPos = Input.mousePosition;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && StartPos.x != 0)
        {
            this.LastPos = Input.mousePosition;
            SwipeLength = LastPos.x - StartPos.x;
        }
        Debug.Log(SwipeLength);
        if (SwipeLength >= 5 || SwipeLength <= -5)
        {
            SceneManager.LoadScene("TestInGame");
        }

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //회전 매개변수 축, 각도, 기준점
        //    transform.Rotate(0, Time.deltaTime * -80, 0, Space.World);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0, Time.deltaTime * 80, 0, Space.World);
        //}
        //moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void DistanceFunc()
    {

    }

    void FixedUpdate()
    {
        // rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}
