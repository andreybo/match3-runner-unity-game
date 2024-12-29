using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tStart;
    [SerializeField] GameObject tLeft;
    [SerializeField] GameObject tRight;
    [SerializeField] GameObject tCombo;
    [SerializeField] GameObject tInfo;
    GameObject ActiveObject;

    //[SerializeField] float secondsBetweenSpawn = 5;
    //[SerializeField] float elapsedTime = 0.0f;

    public int step = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TutorialStep(int i)
    {
        if (i == 1)
        {
            Time.timeScale = 0;
            tStart.SetActive(true);
            ActiveObject = tStart;
        }
        if (i == 2)
        {
            Time.timeScale = 0;
            tLeft.SetActive(true);
            ActiveObject = tLeft;
        }
        if (i == 3)
        {
            Time.timeScale = 0;
            tRight.SetActive(true);
            ActiveObject = tRight;
        }
        if (i == 4)
        {
            Time.timeScale = 0;
            tCombo.SetActive(true);
            ActiveObject = tCombo;
        }
        if (i == 5)
        {
            Time.timeScale = 0;
            tInfo.SetActive(true);
            ActiveObject = tInfo;
        }
        if (i == 6)
        {
            gameObject.SetActive(false);
        }
    }

    public void Unpaused()
    {
        Time.timeScale = 1;
        ActiveObject.SetActive(false);
    }

}
