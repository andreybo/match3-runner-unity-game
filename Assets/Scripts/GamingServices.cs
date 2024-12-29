using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using CloudOnce;

public class GamingServices : MonoBehaviour
{

    public static GamingServices instance;

    void Awake()
    {
        TestSingleton();
    }

    void TestSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitScoreToLeaderboard(int score)
    {
        Leaderboards.highScore.SubmitScore(score);
    }

}