using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionbreak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) // 박스 영역에 들어오면 호출 //ㅠ.ㅠ 함수명.....
    {
        if (col.gameObject.tag == "break")
        {
            Debug.Log("Check322222");
#if UNITY_EDITOR


#elif UNITY_WEBPLAYER

               Application.OpenURL("http://google.com");

#else

                 Application.Quit();

#endif
        }
    }

}
