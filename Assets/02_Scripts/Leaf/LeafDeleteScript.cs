using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDeleteScript : MonoBehaviour
{
    private float fCheck;
    private float CurClock;
    private float RotateClock;
    private bool bCheck;
    // Start is called before the first frame update
    private GameObject Planet;
    void Start()
    {
        bCheck = true;
        CurClock = 0;
        RotateClock = 0.08f;
        fCheck = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bCheck == true)
        {
            CurClock += Time.deltaTime;
            if (CurClock >= RotateClock)
            {
                CurClock = 0.0f;
                RoateFunc();
            }
        }
    }

    void RoateFunc()
    {
        float RandomX = Random.Range(0, 180);
        float RandomY = Random.Range(0, 180);
        float RandomZ = Random.Range(0, 180);

        this.transform.rotation = Quaternion.Euler(RandomX, RandomY, RandomZ);
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.transform.tag == "Planet")
        {
            bCheck = false;
            Destroy(gameObject, 3.0f);
        }
        else if(other.transform.tag  == "Player")
        {
            bCheck = false;
        }
    } 
}
