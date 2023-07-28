using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSun : MonoBehaviour
{
    public GameObject[] Randsun; //복제 될 햇살 세트
    public GameObject RandCloud; //복제 될 구름 세트

    GameObject obj; //클론 받아오는 오브젝트
    GameObject Cobj; //클론 받아오는 오브젝트

    List<GameObject> SunList = new List<GameObject>(); //오브젝트 리스트 생성
    List<GameObject> CloudList = new List<GameObject>(); //오브젝트 리스트 생성

    GameObject firstground; //햇살 생성 위치

    int ranSun; //햇살 위치 랜덤 지정
    int ranCloud;
    int count = 0;

    void Start()
    {
        firstground = GameObject.FindGameObjectWithTag("Sun"); //처음 발판의 위치 받아오기 위해 사용
        SunInIt(); //햇살 생성 시작
    }

    void Update()
    {

    }

    void SunInIt()
    {
        for (int i = 0; i < 10; i++)
        {
            ranSun = Random.Range(0, 5); //0~4까지 랜덤 돌림
            obj = (GameObject)Instantiate(Randsun[ranSun], firstground.transform.position, firstground.transform.rotation);
            obj.transform.localScale = new Vector3(4f, 4f, 4f);

            SunList.Add(obj);

            SunList[i].transform.position = new Vector3(firstground.transform.position.x,
                firstground.transform.position.y, firstground.transform.position.z + 1500 * i);  
        }

        while (count != 3)
        {
            ranCloud = Random.Range(0, 5);

            Cobj = (GameObject)Instantiate(RandCloud, firstground.transform.position, firstground.transform.rotation);
            Cobj.transform.localScale = new Vector3(30f, 30f, 10f);

            CloudList.Add(Cobj);

            CloudList[count].transform.position = new Vector3(SunList[ranCloud].transform.position.x,
    SunList[ranCloud].transform.position.y, SunList[ranCloud].transform.position.z - 100);

            count++;

        }
    }
}
