using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NamePop : MonoBehaviour
{
    public TMP_Text mainInputField;
    public TMP_Text mainInputField2;
    [SerializeField] LeaderboardController lc;
    string playerName;


    // Start is called before the first frame update
    public void SaveName()
    {
            playerName = mainInputField.text;

            playerName = playerName.Trim();
            if (playerName.Length < 2)
            {
                playerName = "Yolker" + Random.Range(0, 99999999);

            }
            else if (playerName.Length > 30)
            {
                playerName = playerName.Substring(0, 30);
            }

            PlayerPrefs.SetString("Username", playerName);
            PlayerPrefs.SetInt("IsFirstPopName", 1);
            lc.writeNewUser(playerName);
    }
    public void ChangeName()
    {
        playerName = mainInputField2.text;

        playerName = playerName.Trim();
        if (playerName.Length < 2)
        {
            playerName = "Yolker" + Random.Range(0, 99999999);

        }
        else if (playerName.Length > 30)
        {
            playerName = playerName.Substring(0, 30);
        }

        PlayerPrefs.SetString("Username", playerName);
        PlayerPrefs.SetInt("IsFirstPopName", 1);
        lc.writeNewUser(playerName);
    }
}
