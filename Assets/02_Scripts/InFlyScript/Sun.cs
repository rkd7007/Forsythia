using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public GameObject Sphere; //복제 될 구름링
    public GameObject[] firstground; //구름링 생성 위치

    GameObject obj; //클론 받아오는 오브젝트
    List<GameObject> SunList = new List<GameObject>(); //오브젝트 리스트 생성

    int ranCloud; //구름링 위치 랜덤 지정

    void Start()
    {
        // firstground = GameObject.FindGameObjectWithTag("Sun"); //처음 발판의 위치 받아오기 위해 사용
        SunInIt(); //햇살 생성 시작
    }

    void Update()
    {
        //만약 비행 게이지가 0이 되어 비행이 끝나면
        if (TestFly.IsFlyEnd == true)
        {
            Destroy(obj.gameObject); //클론을 지운다
        }
    }

    //햇살 생성 시키는 함수
    void SunInIt()
    {
        //세 개만 생성
        for (int i = 0; i < 3; i++)
        {
            ranCloud = Random.Range(0, 8);
            //Debug.Log(ranCloud);
            //obj = (GameObject)Instantiate(Sphere[ranSph], firstground.transform.position, firstground.transform.rotation);
            obj = (GameObject)Instantiate(Sphere, firstground[ranCloud].transform.position, firstground[ranCloud].transform.rotation);
            obj.transform.localScale = new Vector3(35f, 35f, 5f); //구름링 클론 크기 변경

            SunList.Add(obj);

            SunList[i].transform.position = new Vector3(firstground[ranCloud].transform.position.x,
                firstground[ranCloud].transform.position.y, firstground[ranCloud].transform.position.z);

            //if (ranSph == 0)
            //    obj.transform.localScale = new Vector3(13f, 13f, 13f); //햇살 클론 크기 변경
            //else
            //{
            //    CloudSun++; //구름링 개수 증가
            //    obj.transform.localScale = new Vector3(35f, 35f, 5f); //구름링 클론 크기 변경
            //}
            //SunList.Add(obj);

            //if (ranSph == 1) 
            //{
            //    SunList[i].transform.position = new Vector3(ranX, firstground.transform.position.y, transform.position.z + 100 * i);
            //}
            //else
            //    SunList[i].transform.position = new Vector3(ranX, firstground.transform.position.y, transform.position.z + 200 * i);

            //if (CloudSun >= 3) //구름링 3 (또는 4개) 생성 했으면
            //    ranSph = 0; //햇살만 생성시킨다 

        }
    }
}
