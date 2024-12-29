using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using Unity.VisualScripting;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance;
    string shareMessage;

    [Header("UI")]
    [SerializeField] int pointsPerBlockDestroyed = 1;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text ruleText;
    [SerializeField] TMP_Text scoreTime;
    [SerializeField] GameObject HP1, HP2, HP3, HP4;
    [SerializeField] GameObject level4;
    //[SerializeField] int r = 50;

    [Header("Count")]
    public float currentScore;
    [SerializeField] float currentTime = 120;
    public int livesRule = 3;

    [Header("Rules")]
    public float rules;
    public int livesScore;
    AudioSource AS;
    [SerializeField] AudioClip TimeSound;

    [Header("High Score")]
    public float highScore;
    public float levelScore;

    [Header("Other")]
    public Slider mSlider;
    [SerializeField] LevelController level;
    public List<GameObject> listOfAdds = new List<GameObject>();
    [SerializeField] CustomAnimation ca;
    public int IsFirst;
    //public bool ifComboColor;
    Scene m_Scene;
    string sceneName;
    [SerializeField] GameObject LoadingScreen;
    public Slider slider;

    CoinsManager coinsManager;
    public bool isLevelNew;

    
    void Awake()
    {
        Instance = this;
        int[] rulesArray = { 40, 60, 80, 100, 120 };
        if(PlayerPrefs.HasKey("isLevelNew")){
            if(PlayerPrefs.GetInt("isLevelNew") == 1){
                isLevelNew = true;
                rules = rulesArray[Random.Range(0, rulesArray.Length)];
                levelScore = 0;
                PlayerPrefs.SetFloat("levelRules", rules);
                PlayerPrefs.SetInt("isLevelNew", 0);
                PlayerPrefs.SetFloat("levelScore", 0);
                PlayerPrefs.Save();
            }
            else{
                isLevelNew = false;
                if(PlayerPrefs.HasKey("levelRules")){
                    rules = PlayerPrefs.GetFloat("levelRules");
                    levelScore = PlayerPrefs.GetFloat("levelScore");
                }
                else{
                    rules= 60;
                    levelScore = PlayerPrefs.GetFloat("levelScore");
                    PlayerPrefs.SetFloat("levelRules", rules);
                    PlayerPrefs.Save();
                }
            }
        }
        else{
            isLevelNew = true;
            levelScore = 0;
            PlayerPrefs.SetInt("isLevelNew", 0);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        AS = GetComponent<AudioSource>();
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        highScore = PlayerPrefs.GetFloat("HighScore");
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        coinsManager = FindObjectOfType<CoinsManager>();
        setRule();
    }

    public void SaveScore()
    {
        if (currentScore > levelScore)
        {
            PlayerPrefs.SetFloat("levelScore", currentScore);
            float newScore = (highScore - levelScore) + currentScore;
            PlayerPrefs.SetFloat("HighScore", newScore);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                DisplayTime(currentTime);
            }
            else
            {
                StartCoroutine(level.LoseDelay());
                Debug.Log("Time has run out!");
                currentTime = 0;
            }

            if (currentScore >= rules)
            {
                SaveData();
                StartCoroutine(level.NextDelay());
            }
    }

    public void coinsPlay(Vector3 other) {
            coinsManager.AddCoins(other, pointsPerBlockDestroyed);
    }

    public void TimePlaySound()
    {
        AS.clip = TimeSound;
        AS.Play();
    }
    public void Play()
    {
        livesRule = PlayerPrefs.HasKey("health") ? PlayerPrefs.GetInt("health") : 3;
        if (livesRule == 4)
        {
            level4.SetActive(true);
        }
        else
        {
            level4.SetActive(false);
        }
        AS.Stop();
        scoreTime.color = Color.white;
        Slider();
        livesScore = livesRule;
        LivesCheck();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        scoreTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timeToDisplay < 30f) {
            scoreTime.color = Color.red;
            TimePlaySound();
        }
        else{
            AS.Stop();
        }
    }


    public void TimerAdd()
    {
        currentTime += 30;
    }



    public void AddToScore(int kill)
    {
        currentScore += kill * pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
        Slider();
        BounceIcon();
    }

    void BounceIcon()
    {
        ca.PlayAnimation();
    }

    public void Slider()
    {
        mSlider.value = currentScore / rules;
        Debug.Log("Slider" + mSlider.value);
    }

    public void LivesRemove() {
        livesScore -= 1;
        if (livesScore < 1) {
            StartCoroutine(level.LoseDelay());
        }
        LivesCheck();
    }

    public void levelNext()
    {
        MusicController.Instance.enabled = false;
        string sname = PlayerPrefs.GetString("SceneName");
        LoadLevel(sname);
    }

    public void setRule(){
        ruleText.text = rules.ToString();
    }

    public void levelRestart()
    {
        MusicController.Instance.enabled = false;
        LoadLevel(sceneName);
    }

    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(string sceneIndex)
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


    public void LivesAdd()
    {
        if (livesScore < livesRule)
        {
            livesScore += 1;
            LivesCheck();
        }
    }

    public void SaveData()
    {
            int[] numbers = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            int randomIndex = Random.Range(0, numbers.Length);
            int randomNumber = numbers[randomIndex];
            string sceneName = "Scene" + randomNumber;

            PlayerPrefs.SetInt("level", level.level + 1);
            PlayerPrefs.SetInt("isLevelNew", 1);
            PlayerPrefs.SetFloat("HighScore", highScore + currentScore);
            PlayerPrefs.SetString("SceneName", sceneName);
            PlayerPrefs.Save();
    }

    public void SkipAndSave(){
            int[] numbers = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            int randomIndex = Random.Range(0, numbers.Length);
            int randomNumber = numbers[randomIndex];
            string sceneName = "Scene" + randomNumber;

            PlayerPrefs.SetInt("level", level.level + 1);
            PlayerPrefs.SetInt("isLevelNew", 1);
            PlayerPrefs.SetString("SceneName", sceneName);
            PlayerPrefs.Save();
    }

    public void LivesCheck()
    {
        if (livesScore == 4)
        {
            HP4.SetActive(true);
            HP3.SetActive(true);
            HP2.SetActive(true);
            HP1.SetActive(true);
        }
        else if (livesScore == 3)
        {
            HP4.SetActive(false);
            HP3.SetActive(true);
            HP2.SetActive(true);
            HP1.SetActive(true);
        }
        else if (livesScore == 2)
        {
            HP4.SetActive(false);
            HP3.SetActive(false);
            HP2.SetActive(true);
            HP1.SetActive(true);
        }
        else if (livesScore == 1)
        {
            HP4.SetActive(false);
            HP3.SetActive(false);
            HP2.SetActive(false);
            HP1.SetActive(true);
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
    }



    public void ClickShareButton()
    {
        shareMessage = "I can't believe I scored " + currentScore + " candies in Yolker Run 3D";

        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Yoler Run 3D").SetText(shareMessage).Share();
    }
}