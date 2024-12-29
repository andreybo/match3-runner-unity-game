using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelControllerMenu : MonoBehaviour
{

    [Header("Characters")]
    string adUnitId;

    public void OpenWebsite()
    {
        Application.OpenURL("https://twoj.io/#contact");
    }
    public void OpenWaitlist()
    {
        Application.OpenURL("https://y.twoj.io/");
    }

    public void OpenTable()
    {
        Application.OpenURL("https://twoj.io/");
    }

    void eventPurchased()
    {
    }

}
