using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//가지의 속성...
public class Branch : MonoBehaviour
{
    public GameObject BranchFirst;

    private static Branch instance;
    public static Branch Instance => instance;

    public List<Transform> mainBranchList = new List<Transform>();
    public List<Transform> mainBranchLeftList = new List<Transform>();
    public List<Transform> mainBranchRightList = new List<Transform>();
    public List<Transform> mainBranchPosList = new List<Transform>();

    public int MainbranchSize = 119;

    public Branch()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    private void Start()
    {
        mainBranchPosList.Add(BranchFirst.transform);
    }
}
