using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOpen : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject Info;
    [SerializeField] GameObject Characters;
    [SerializeField] GameObject Story;
    [SerializeField] GameObject Games;
    [SerializeField] LeaderboardController lc;

    [SerializeField] GameObject[] ButtonsPurchase;

    
    int IsFirstPop;
    List<GameObject> Views = new List<GameObject>();
    public string version = "1.0.0";
    public bool isShowWhatNew = true;
    string currentVersion;
    int level;
    
    public Slider slider;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] CoinsManager coinsManager;
    [SerializeField] VideoIntro videoIntro;


    public TMP_Text candies;
    public TMP_Text levelText;
    //string playerName;

    bool IsFirst;

    private void SortMenu()
    {
        MainMenu.SetActive(true);
        Setting.SetActive(false);
        Info.SetActive(false);
        LoadingScreen.SetActive(false);
        Characters.SetActive(false);
        Story.SetActive(false);
        Games.SetActive(false);
        Views.Add(MainMenu);
        Views.Add(Setting);
        Views.Add(Info);
        Views.Add(LoadingScreen);
        Views.Add(Characters);
        Views.Add(Story);
        Views.Add(Games);
    }

    public void Purchase(){
        PlayerPrefs.SetInt("Purchase", 1);
        SettingsBack();
        hideButoons();
    }

    public void hideButoons()
    {
        foreach (GameObject item in ButtonsPurchase)
        {
            item.SetActive(false);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        SortMenu();
        MainMenu.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        MainMenu.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        Zero();

        GetCandies();

        level = PlayerPrefs.GetInt("level");
        levelText.text = level.ToString();


        if(PlayerPrefs.HasKey("Purchase")){
            hideButoons();
        }

        IsFirst = PlayerPrefs.HasKey("IsFirst") ? true : false;
    }


    public void SkipLevel(){
        if(PlayerPrefs.HasKey("Purchase")){
            GameStatus.Instance.SkipAndSave();
            StartGame();
        }
        else{
           SettingsFromGame();
        }
    }

    void GetCandies()
    {
        float n1 = PlayerPrefs.GetFloat("HighScore");
        int sum = (int)n1;
        GamingServices.instance.SubmitScoreToLeaderboard(sum);
        candies.text = sum.ToString();
    }

    public void GetRewarded(){
        float n2 = PlayerPrefs.GetFloat("HighScore");
        int sum = (int)n2 + 30;
        GamingServices.instance.SubmitScoreToLeaderboard(sum);
        candies.text = sum.ToString();
    }

    void Zero()
    {
        foreach (GameObject item in Views)
        {
            item.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            item.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        }
    }

    public void CharactersMenu()
    {
        MainMenu.SetActive(false);
        Characters.SetActive(true);
        GetComponent<ChooseCharacterMenu>().ActiveCharacter();
    }
    public void BackCharacters()
    {
        Characters.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void StoryMenu()
    {
        MainMenu.SetActive(false);
        Story.SetActive(true);
        videoIntro.StartVideo();
        Debug.Log("Story");
    }
    public void BackStory()
    {
        Story.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void GamesMenu()
    {
        MainMenu.SetActive(false);
        Games.SetActive(true);
        if(!IsFirst){
            MainMenu.SetActive(false);
            Games.SetActive(true);
            IsFirst = true;
            PlayerPrefs.SetInt("IsFirst", 1);
            PlayerPrefs.Save();
            Debug.Log("First");
        }
        else{
            MainMenu.SetActive(false);
            Games.SetActive(true);
            if(!PlayerPrefs.HasKey("Purchase")){
                gameObject.GetComponent<ShowInterstitialScript>().ShowInterstitialButtonClicked();
            }
            Debug.Log("First No");
        }
    }
    public void BackGames()
    {
        Games.SetActive(false);
        MainMenu.SetActive(true);
    }


    public void Settings()
    {
        string newname = PlayerPrefs.GetString("Username");
        MainMenu.SetActive(false);
        Setting.SetActive(true);
    }

    public void SettingsFromGame()
    {
        string newname = PlayerPrefs.GetString("Username");
        Games.SetActive(false);
        Setting.SetActive(true);
    }

    public void SettingsBack()
    {
        MainMenu.SetActive(true);
        Setting.SetActive(false);
    }

    public void Back()
    {
        MainMenu.SetActive(true);
    }
    public void InfoOpen()
    {
        MainMenu.SetActive(false);
        Info.SetActive(true);
    }
    public void BackInfo()
    {
        Info.SetActive(false);
        MainMenu.SetActive(true);
    }


    public void StartGame()
    {
        MainMenu.SetActive(false);
        string scenename = PlayerPrefs.HasKey("SceneName") ? PlayerPrefs.GetString("SceneName") : "Scene1";
        LoadLevel(scenename);
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
            float progress = Mathf.Clamp01(operation.progress / 2f);

            slider.value = progress;
            //progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
