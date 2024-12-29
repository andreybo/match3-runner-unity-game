using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] MusicController mc;
    [SerializeField] GameObject videoButton;
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        videoButton.SetActive(false);
    }

    public void StartVideo(){
            Time.timeScale = 0;
            mc.maudio.volume = 0f;
            videoButton.SetActive(true);
            gameObject.SetActive(true);
            videoPlayer.enabled = true;
            videoPlayer.Play();
    }

    // The method to be called when the video finishes playing
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        mc.maudio.volume = 1f;
        videoPlayer.enabled = false;
        Time.timeScale = 1;
        videoButton.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        mc.maudio.volume = 1f;
        videoPlayer.enabled = false;
        Time.timeScale = 1;
        videoButton.SetActive(false);
        gameObject.SetActive(false);

    }
}
