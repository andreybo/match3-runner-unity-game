using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using GooglePlayGames.BasicApi;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject Cubes;
    [SerializeField] GameObject Adds;
    [SerializeField] GameObject Din;
    [SerializeField] GameObject worldParent;
    [SerializeField] Camera Camera;
    [SerializeField] MenuGame mo;
    [SerializeField] TMP_Text scoreTextNext;
    [SerializeField] TMP_Text scoreTextFin;
    //[SerializeField] TMP_Text highText;
    [SerializeField] Cloud cloud;
    [SerializeField] SpawnRun sp;
    [SerializeField] FeaturesTop Features;
    [SerializeField] ChooseCharacter cc;
    [SerializeField] Vector3 startPosition = new Vector3(0,2,0);
    float lastScore;
    //float highScore;
    public List<Color> ColorCamera;
    //[SerializeField] SpriteRenderer fade;
    public float levelSpeed;
    //PopManager pop;
    public int level;
    int orderAd;



    [Header("Camera effects")]
    [SerializeField] GameObject effectGray;
    [SerializeField] GameObject effectColor;

    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("CharacterController").GetComponent<ChooseCharacter>();
        orderAd = PlayerPrefs.GetInt("Order");
        GetScore();
        Din.transform.position = new Vector3(0, 2, 0);
        cc.ModelCharacter(Din);
        StartGame();
        level = PlayerPrefs.HasKey("level") ? PlayerPrefs.GetInt("level") : 0;
        PopManager.Instance.ShowLevelUp();
    }

    public void StartGame()
    {
        Din.transform.position = startPosition;
        worldParent.transform.position = new Vector3(0, 0, 0);
        Ball.Instance.GetModel();
        GameStatus.Instance.Play();
        Time.timeScale = 1;
        effectGray.GetComponent<PostProcessVolume>().enabled = false;
        effectColor.GetComponent<PostProcessVolume>().enabled = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://twoj.io/");
    }

    public void OpenTable()
    {
        Application.OpenURL("https://twoj.io/");
    }


    void DestroyAdds()
    {
        while (Adds.transform.childCount > 0)
        {
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(Adds.transform.GetChild(0).gameObject);
        }
    }

    public void NextLevel()
    {
        mo.NextLevel();
        GetScore();
    }

    public void ExitGame(){
        GameStatus.Instance.SaveScore();
    }

    public void Lose()
    {
        mo.Score();
        Debug.Log("Lose");
        GetScore();
    }

    public IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(1f);
        Lose();
    }

    public IEnumerator NextDelay()
    {
        yield return new WaitForSeconds(1f);
        NextLevel();
    }

    public void GetScore()
    {
        lastScore = GameStatus.Instance.currentScore;
        scoreTextNext.text = lastScore.ToString();
        scoreTextFin.text = lastScore.ToString();
    }

}
