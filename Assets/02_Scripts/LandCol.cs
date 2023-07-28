using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCol : MonoBehaviour
{
    public static bool isJumpCheck = false;

    //콜라이더에 Player 닿는지, 점프 체크 위함
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
            isJumpCheck = true;
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Player")
            isJumpCheck = false;
    }

}
