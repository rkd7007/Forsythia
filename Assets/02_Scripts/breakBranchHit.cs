using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBranchHit : MonoBehaviour
{
    public GameObject OverPanel;

    private void Start()
    {
        OverPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "breakBranch")
        {
            //게임오버
            OverPanel.SetActive(true);
        }
    }
}
