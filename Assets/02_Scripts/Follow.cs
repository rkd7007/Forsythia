using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Quaternion targetRote; 
    public float fSpeed = 2.0f;

    private float fHeight = 0.0f;

    private Vector3 v;
    private Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { //target이 멈췄을 때 이 객체의 로테이션 값을 고정시키고 위아래로 포지션값만 조정 
        

        //v = new Vector3(270, 45, -270);
        //this.transform.Rotate(v*Time.deltaTime); //이새끼 왜 로테이션이 쳐안먹히는거야

        //Debug.Log(this.transform.rotation);
        fHeight = Random.Range(0,0); //펄럭펄럭 

        Vector3 Pos = target.position + new Vector3(0.0f, fHeight, 0.0f); //거리

        transform.position = Vector3.Lerp(transform.position, Pos, Time.deltaTime * fSpeed);//거리가 생기면 따라오도록

        //Vector3 dirTarget = target.transform.position - this.transform.position;
        //this.transform.forward = dirTarget.normalized;
        
        
        //transform.position += (target.position - Pos) * fSpeed;
        //transform.position -= new Vector3(3.0f, 0, 3.0f);
        transform.LookAt(target); // 뒤집혀지는 원인
        //this.transform.rotation = Quaternion.LookRotation(target.position);
        //r.MoveRotation(r.rotation * Time.deltaTime);

        //transform.rotation = Quaternion.LookRotation(Pos);
        //this.transform.rotation = Quaternion.Euler(new Vector3(270, 45, 0));
    }
}
