using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    public GameObject OptPanel;
    public GameObject OverPanel;
    public Transform Player;

    [SerializeField]
    private float patienceTime;

    public static bool IsLeftJump = false;       //왼쪽 도약 키를 눌렀는지
    public static bool IsRightJump = false;      //오른쪽 도약 키를 눌렀는지

    private float branchTime = 0.0f;             //가지 위에 얼마간 머물렀는지
    private bool branchTimeCheck = false;   //가지 위에 얼마간 머물렀는지 시간을 체크하기 위한 bool 변수

    private bool isStop = false;                 //옵션을 눌렀는지   
    private bool isBranchEnd = false;            //가지끝에 도달하였는지

    //가지의 포지션 index
    private static int branchIndex = 0;    

    private void Start()
    {
        branchIndex = 0;
        OptPanel.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(branchTime);

        //마지막 가지에 도착하면 strong과 weak 버튼 잠금
        if (branchIndex == Branch.Instance.MainbranchSize)
        {
            isBranchEnd = true;
            SceneManager.LoadScene("Fly");
        }

        if (branchTimeCheck)
        {
            branchTime += Time.deltaTime;
        }

        if (branchTime > patienceTime)
        {
            isBranchEnd = true;
            OverPanel.SetActive(true);
        }
    }

    public void LeftJump()
    {
       IsLeftJump = true;
    }

    public void RightJump()
    {
       IsRightJump = true;
    }

    public void TimeNotMove()
    {
        if (!isStop)
        {
            Time.timeScale = 0; //시간 멈춤
            OptPanel.SetActive(true); //옵션 판넬 보이게 함
            isStop = true;
        }
        else
        {
            Time.timeScale = 1; //시간 움직임
            OptPanel.SetActive(false); //옵션 판넬 안보이게 함
            isStop = false;
        }
    }

    public void Strong()
    {
        if(!isBranchEnd)
        {
            branchTime = 0.0f;
            branchTimeCheck = true;

            //플레이어의 위치를 mainBranchPosList[기존 index + 2] 위치로 이동
            Player.position = Branch.Instance.mainBranchPosList[branchIndex + 2].position;
            branchIndex = branchIndex + 2;
            ScoreManager.Score += 100;

            DropBranch(branchIndex);
        }
    }

    public void Weak()
    {
        if (!isBranchEnd)
        {
            branchTime = 0.0f;
            branchTimeCheck = true; 

            //플레이어의 위치를 mainBranchPosList[기존 index + 1] 위치로 이동
            Player.position = Branch.Instance.mainBranchPosList[branchIndex + 1].position;
            branchIndex = branchIndex + 1;
            ScoreManager.Score += 50;

            DropBranch(branchIndex);
        }
    }

    private void DropBranch(int index)
    {
        Branch.Instance.mainBranchPosList[index].parent.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Branch.Instance.mainBranchPosList[index].parent.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 3.0f, ForceMode.Impulse);
        DeleteBranch();
    }

    private void DeleteBranch()
    {
        //리스트를 전체 순회하면서 현재의 인덱스보다 낮은 branch는 삭제한다.
        for(int i = 0; i < Branch.Instance.MainbranchSize; ++i)
        {
            if(i < branchIndex)
            {
                Destroy(Branch.Instance.mainBranchPosList[i].parent.gameObject, 5.0f);
            }
        }
    }

    public void IsMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Again()
    {
        branchIndex = 0;
        OptPanel.SetActive(false);
        SceneManager.LoadScene("InGame");
    }
}
