using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopManager : MonoBehaviour
{
    public static PopManager Instance;

    [SerializeField] GameObject popHealth;
    [SerializeField] GameObject popSlow;
    [SerializeField] GameObject popTakes;
    [SerializeField] GameObject popTime;
    [SerializeField] GameObject popCombo;
    [SerializeField] GameObject LevelParent;
    [SerializeField] GameObject levelUp;
    [SerializeField] GameObject levelUpNum;
    // Start is called before the first frame update

    void Awake(){
        Instance = this;
    }

    public void PopActive()
    {
        popHealth.GetComponent<TMP_Text>().enabled = false;
        popSlow.GetComponent<TMP_Text>().enabled = false;
        popTakes.GetComponent<TMP_Text>().enabled = false;
        popTime.GetComponent<TMP_Text>().enabled = false;
        popCombo.GetComponent<TMP_Text>().enabled = false;
        levelUp.GetComponent<TMP_Text>().enabled = false;
        levelUpNum.GetComponent<TMP_Text>().enabled = false;
        LevelParent.SetActive(false);
    }

    public void showHealth()
    {
        popHealth.GetComponent<TMP_Text>().enabled = true;
        popHealth.GetComponent<FadeText>().PlayFade();
    }
    public void showSlow()
    {
        popSlow.GetComponent<TMP_Text>().enabled = true;
        popSlow.GetComponent<FadeText>().PlayFade();
    }
    public void showTakes()
    {
        popTakes.GetComponent<TMP_Text>().enabled = true;
        popTakes.GetComponent<FadeText>().PlayFade();
    }
    public void showTime()
    {
        popTime.GetComponent<TMP_Text>().enabled = true;
        popTime.GetComponent<FadeText>().PlayFade();
    }
    public void showCombo()
    {
        popCombo.GetComponent<TMP_Text>().enabled = true;
        popCombo.GetComponent<FadeText>().PlayFade();
    }
    public void ShowLevelUp()
    {
        LevelParent.SetActive(true);
        LevelParent.GetComponent<CustomAnimation>().PlayAnimation();
    }
}
