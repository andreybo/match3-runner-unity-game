using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    [SerializeField] GameObject InGame;
    [SerializeField] GameObject Pause;
    [SerializeField] GameObject GameEnd;
    [SerializeField] GameObject GameNext;
    [SerializeField] GameObject LoadingScreen;
    List<GameObject> Views = new List<GameObject>();
    public Slider slider;
    [SerializeField] CoinsManager coinsManager;

    private void Awake()
    {
        InGame.SetActive(true);
        Pause.SetActive(false);
        GameEnd.SetActive(false);
        GameNext.SetActive(false);
        LoadingScreen.SetActive(false);
        Views.Add(InGame);
        Views.Add(Pause);
        Views.Add(GameEnd);
        Views.Add(GameNext);
        Views.Add(LoadingScreen);
        Zero();
    }

    private void Start()
    {
        coinsManager.getTarget();
    }

    void Zero()
    {
        foreach (GameObject item in Views)
        {
            item.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            item.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        InGame.SetActive(false);
        Pause.SetActive(true);
        MusicController.Instance.maudio.volume = 0.1f;
        //Pause.GetComponent<CustomAnimation>().PlayAnimation();
    }
    public void Continue()
    {
        Time.timeScale = 0f;
        Pause.SetActive(false);
        InGame.SetActive(true);
        MusicController.Instance.maudio.volume = 0.5f;
        //InGame.GetComponent<CustomAnimation>().PlayAnimation();
    }
    public void Exit()
    {
        Pause.SetActive(false);
        LoadLevel(0);
    }

    public void Repeat()
    {
        Time.timeScale = 1;
        GameEnd.SetActive(false);
        InGame.SetActive(true);
        MusicController.Instance.maudio.volume = 0.5f;
        //InGame.GetComponent<CustomAnimation>().PlayAnimation();
    }

    public void RepeatFromPause()
    {
        Time.timeScale = 1f;
        Pause.SetActive(false);
        InGame.SetActive(true);
        MusicController.Instance.maudio.volume = 0.5f;
        //InGame.GetComponent<CustomAnimation>().PlayAnimation();
    }

    public void NextLevel()
    {
        GameNext.SetActive(true);
        InGame.SetActive(false);
        GameNext.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        MusicController.Instance.maudio.volume = 0f;
        // GameEnd.GetComponent<CustomAnimation>().PlayAnimation();
    }

    public void Score()
    {
        GameEnd.SetActive(true);
        InGame.SetActive(false);
        GameEnd.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        MusicController.Instance.maudio.volume = 0f;
        // GameEnd.GetComponent<CustomAnimation>().PlayAnimation();
    }

    public void ExitEnd()
    {
        //GameEnd.SetActive(false);
        LoadLevel(0);
        MusicController.Instance.maudio.volume = 0.5f;
        MusicController.Instance.InMenu();
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void ReloadLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }



    // Start is called before the first frame update
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            //progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
