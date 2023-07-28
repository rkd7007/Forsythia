using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeafPutScript : MonoBehaviour
{
    public GameObject Leaf;
    public GameObject SaveLeaf;

    public float fCreateSpeed; //= 1.5f;
    //public float fDeleteSpeed; // =10.0f;

    private int SaveCount;
    private int iCount;
    private float CurClock;
    // Start is called before the first frame update
    void Start()
    {
        SaveCount = 0;
        iCount = 0;
        CurClock = 0;

        FirstLeaf();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveCount >= 300)
        {
            CurClock += Time.deltaTime;
            if (CurClock > fCreateSpeed)
            {
                CreateLeaf();
                CreateLeafSecond();
                CreateLeafThird();
                CreateLeaf04();
                CreateLeaf05();
                CurClock = 0.0f;
            }
        }
    }
    void FirstLeaf()
    {
        for(int i = 0; i <= 1000; i++)
        {
            float RandomXPos = Random.Range(300, 800);
            float RandomZPos = Random.Range(-150, 300);
            float RandomQuat = Random.Range(0, 180);
            float RandomQuat2 = Random.Range(0, 180);
            float RandomQuat3 = Random.Range(0, 180);

            var m = Instantiate(SaveLeaf, new Vector3(RandomXPos, 1120, RandomZPos), Quaternion.Euler(RandomQuat3, RandomQuat, RandomQuat2));
            SaveCount++;
        }
    }

    void CreateLeaf()
    {

        float RandomXPos = Random.Range(300, 500);
        float RandomZPos = Random.Range(-150, 100);
        float RandomQuat = Random.Range(0, 180);
        float RandomQuat2 = Random.Range(0, 180);
        float RandomQuat3 = Random.Range(0, 180);

        if (iCount <= 150)
        {
            var o = Instantiate(Leaf, new Vector3(RandomXPos, 1412, RandomZPos), Quaternion.Euler(RandomQuat3, RandomQuat, RandomQuat2));
            iCount++;
        

            //Destroy(o, fDeleteSpeed);
            if (iCount >= 100) 
                iCount = 0;
        }
    }

    void CreateLeafSecond()
    {
        float RandomXPos = Random.Range(400, 700);
        float RandomZPos = Random.Range(0, 250);
        float RandomQuat = Random.Range(0, 90);
        float RandomQuat2 = Random.Range(0, 90);
        float RandomQuat3 = Random.Range(0, 90);

        if (iCount <= 150)
        {
            iCount++;
            var p = Instantiate(Leaf, new Vector3(RandomXPos, 1412, RandomZPos), Quaternion.Euler(RandomQuat2, RandomQuat, RandomQuat3));
            //Destroy(p, fDeleteSpeed);
            
        }
    }

    void CreateLeafThird()
    {
        float RandomXPos = Random.Range(400, 600);
        float RandomZPos = Random.Range(-100, 200);
        float RandomQuat = Random.Range(0, 90);
        float RandomQuat2 = Random.Range(0, 90);
        float RandomQuat3 = Random.Range(0, 90);

        if (iCount <= 150)
        {
            
            iCount++;
            var q = Instantiate(Leaf, new Vector3(RandomXPos, 1412, RandomZPos), Quaternion.Euler(RandomQuat2, RandomQuat, RandomQuat3));
            //Destroy(q, fDeleteSpeed);

        }
    }
    void CreateLeaf04()
    {
        float RandomXPos = Random.Range(600, 800);
        float RandomZPos = Random.Range(200, 300);
        float RandomQuat = Random.Range(0, 90);
        float RandomQuat2 = Random.Range(0, 90);
        float RandomQuat3 = Random.Range(0, 90);

        if (iCount <= 150)
        {

            iCount++;
            var q = Instantiate(Leaf, new Vector3(RandomXPos, 1412, RandomZPos), Quaternion.Euler(RandomQuat2, RandomQuat, RandomQuat3));
            //Destroy(q, fDeleteSpeed);

        }
    }
    void CreateLeaf05()
    {
        float RandomXPos = Random.Range(300, 500);
        float RandomZPos = Random.Range(-0, 150);
        float RandomQuat = Random.Range(0, 90);
        float RandomQuat2 = Random.Range(0, 90);
        float RandomQuat3 = Random.Range(0, 90);

        if (iCount <= 150)
        {

            iCount++;
            var q = Instantiate(Leaf, new Vector3(RandomXPos, 1412, RandomZPos), Quaternion.Euler(RandomQuat2, RandomQuat, RandomQuat3));
            //Destroy(q, fDeleteSpeed);

        }
    }
}


//일정시간 지나면 삭제 
//회전 Quaternion을 바꿔자
// 흩날리도록

