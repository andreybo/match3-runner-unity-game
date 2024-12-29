using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class LeaderboardController : MonoBehaviour
{

    //User Data variables
    [Header("UserData")]
    public TMP_Text usernameField;
    public TMP_Text scoreField;
    public GameObject scoreElement;
    public Transform scoreboardContent;
    string username;
    float score;
    float score2;
    public TMP_Text scoreBest;


    private void Start()
    {
        score = PlayerPrefs.GetFloat("HighScore");
        scoreBest.text = score.ToString();
    }

    public void SaveName()
    {

    }

    public void StartScreen()
    {

    }

    public void writeNewUser(string playerName)
    {

    }

}
