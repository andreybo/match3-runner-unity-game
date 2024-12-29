using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    public void NewScoreElement(string _username, int _score)
    {
        usernameText.text = _username;
        scoreText.text = _score.ToString();
    }
    public void NewScoreElement2(string _username, int _score)
    {
        usernameText.text = _username;
        scoreText.text = _score.ToString();
    }
}
