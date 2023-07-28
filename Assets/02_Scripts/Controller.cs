using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public Animator animator;

    public Transform Player;                    // 플레이어
    public Transform Stick;                     // 조이스틱

    public static Vector3 StickFirstPos;              // 조이스틱의 처음 위치.
    private Vector3 JoyVec;                     // 조이스틱의 벡터(방향)
    public float PlayerMoveSpeed = 50f;         // 플레이어 스피드
    private float Radius;                       // 조이스틱 배경의 반 지름.
    private bool MoveFlag;                      // 플레이어 움직임 스위치.
    private bool isJumping = false;             // 플레이어 점프 여부
    private int MoveDistance;

    Rigidbody rb;
    private RaycastHit hit;

    void Start()
    {
        MoveDistance = 0;
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;

        rb = Player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (MoveFlag)
        {
            Player.transform.Translate(Vector3.forward * Time.deltaTime * PlayerMoveSpeed);
            MoveDistance += (int)(Time.deltaTime * PlayerMoveSpeed);                       //플레이어가 이만큼 움직였따. 
        }
        else if (MoveDistance >= 2500) //얘도 수치화 시켜주도록 하자
        {
            //Event
            //Panel을 SetActive false 해놓고, Event발생 시 True 바꿔주기
        }

        if(!LandCol.isJumpCheck)
        {
            animator.SetBool("Jump", false);
        }
    }

    //점프
    public void OnClickJump()
    {
        //걷지 않고, 땅에 닿아있을때만 점프가능
        if(!MoveFlag && LandCol.isJumpCheck)
        {
            animator.SetBool("Jump", true);
            rb.velocity = new Vector3(0, 4.0f, 0);           
        }
    }

    // 드래그
    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        animator.SetBool("Walk", true);


        // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
        JoyVec = (Pos - StickFirstPos).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동. 
        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
    }

    // 드래그 끝.
    public void DragEnd()
    {
        animator.SetBool("Walk", false);

        Stick.position = StickFirstPos; // 스틱을 원래의 위치로.
        JoyVec = Vector3.zero;          // 방향을 0으로.

        MoveFlag = false;
    }
}