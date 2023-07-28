using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static float Score;

    void Update()
    {
        scoreText.text = "점수 : " + Mathf.Round(Score);
    }
}
