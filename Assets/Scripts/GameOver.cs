using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text currentScoreText;
    float currentScore;

    void Start()
    {
        currentScoreText.text = currentScore.ToString();
    }

}
