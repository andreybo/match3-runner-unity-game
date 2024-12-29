using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    public AudioClip[] soundtrackGame;
    public AudioClip[] soundtrackMenu;
    public AudioSource maudio;
    void Awake(){
        Instance = this;
    }

    private void Start()
    {
        maudio = GetComponent<AudioSource>();
        InMenu();
    }

    public void InGame()
    {
        maudio.clip = soundtrackGame[Random.Range(0, soundtrackGame.Length)];
        maudio.Play();
        maudio.volume = 0.3f;
    }

    public void InMenu()
    {
        maudio.clip = soundtrackMenu[Random.Range(0, soundtrackMenu.Length)];
        maudio.Play();
        maudio.volume = 0.3f;
    }
}
