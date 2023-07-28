using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public Transform branchPos;

    //게임이 시작되면 최초 1번만 가지를 스폰.
    private bool isSpawnMainBranch = false;
    private bool isSpawnOtherBranch = false;

    //썩은가지가 바로 이전에 나왔는지 체크
    private bool isBreakBranch = false;

    private int randDir = 0;
    private int randType = 0;
    private string branchType = "leafBranch";

    private void Start()
    {
        objectPooler = ObjectPooler.Instance; 
    }

    private void Update()
    {
        if (!isSpawnMainBranch)
        {
            MainBranchSpawner();
            MainBranchSpawner02();
            isSpawnMainBranch = true;
        }

        if (!isSpawnOtherBranch)
        {
            OtherBranchSpawner();
            isSpawnOtherBranch = true;
        }
    }

    private void MainBranchSpawner()
    {
        ObjectPooler.Instance.spawnFromPool("mainBranch", branchPos.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
    }

    private void MainBranchSpawner02()
    {
        for (int i = 0; i < Branch.Instance.MainbranchSize; ++i)
        {
            ObjectPooler.Instance.spawnFromPool("mainBranch"
                , new Vector3(Branch.Instance.mainBranchList[i].position.x, Branch.Instance.mainBranchList[i].position.y, Branch.Instance.mainBranchList[i].position.z)
                , Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
    }


    private void OtherBranchSpawner()
    {
        for (int i = 0; i < Branch.Instance.MainbranchSize; ++i)
        {
            //랜덤값을 지정한다. 
            randDir = Random.RandomRange(1, 101);   //(만약 짝수가 나오면 왼쪽, 홀수가 나오면 오른쪽을 지정)   

            //왼쪽에 곁가지를 생성한다.
            if (randDir % 2 == 0)
            {
                if(!isBreakBranch)
                {
                    //마지막 가지일 경우
                    if (i == Branch.Instance.MainbranchSize - 1)
                        randType = Random.RandomRange(1, 4);

                    else
                        randType = Random.RandomRange(1, 5);
                }
                else
                {
                    randType = Random.RandomRange(1, 4);
                }

                branchType = SelectBranchType(randType);

                //썩은 가지이면 회전값이 달라야 함
                if (randType == 4)
                {
                    ObjectPooler.Instance.spawnFromPool(branchType
                        , new Vector3(Branch.Instance.mainBranchLeftList[i].position.x, Branch.Instance.mainBranchLeftList[i].position.y, Branch.Instance.mainBranchLeftList[i].position.z)
                        , Quaternion.Euler(-243.565f, -27.76099f, 151));

                    isBreakBranch = true;
                }

                else
                {
                    ObjectPooler.Instance.spawnFromPool(branchType
                       , new Vector3(Branch.Instance.mainBranchLeftList[i].position.x, Branch.Instance.mainBranchLeftList[i].position.y, Branch.Instance.mainBranchLeftList[i].position.z)
                       , Quaternion.Euler(-180.0f, -270.0f, -270.0f));

                    isBreakBranch = false;
                }
            }

            if (randDir % 2 == 1)
            {
                if (!isBreakBranch)
                    randType = Random.RandomRange(1, 5);
                else
                    randType = Random.RandomRange(1, 4);

                branchType = SelectBranchType(randType);

                //썩은 가지이면 회전값이 달라야 함
                if(randType == 4)
                {
                    ObjectPooler.Instance.spawnFromPool(branchType
                        , new Vector3(Branch.Instance.mainBranchRightList[i].position.x, Branch.Instance.mainBranchRightList[i].position.y, Branch.Instance.mainBranchRightList[i].position.z)
                        , Quaternion.Euler(-99.88799f, -325.486f, 317.172f));

                    isBreakBranch = true;
                }

                //오른쪽에 곁가지를 생성한다.
                else
                {
                    ObjectPooler.Instance.spawnFromPool(branchType
                        , new Vector3(Branch.Instance.mainBranchRightList[i].position.x, Branch.Instance.mainBranchRightList[i].position.y, Branch.Instance.mainBranchRightList[i].position.z)
                        , Quaternion.Euler(0.0f, -270.0f, -90.0f));

                    isBreakBranch = false;
                }
            }
        }
    }

    private string SelectBranchType(int _randType)
    {

        if(_randType == 1)
        {
            return "normal";
        }

        if (_randType == 2)
        {
            return "forsy";
        }

        if (_randType == 3)
        {
            return "leafBranch";
        }

        if (_randType == 4)
        {
            return "breakBranch";
        }

        return "0";
    }
}