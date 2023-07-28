using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endGame : MonoBehaviour
{
    Text text;
    Text text2;

    public GameObject EndPanel = null;
    public float speed = 2.0f;

    private void Start()
    {
        EndPanel.SetActive(false);
        text = EndPanel.transform.GetChild(0).GetComponent<Text>();
        text2 = EndPanel.transform.GetChild(1).GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EndPanel.SetActive(true);
            StartCoroutine(FadeTextToFullAlpha());
        }
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);

        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / speed));
            yield return null;
        }

        while (text2.color.a < 1.0f)
        {
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, text2.color.a + (Time.deltaTime / speed));
            yield return null;
        }
    }
}
