using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetRandom : MonoBehaviour
{
    public static int[] AddCount = { 15, 13, 12, 11, 13, 13, 8, 10, 2, 8};
    static int[] sizeBoard = { 0, 0, 1};
    public static int sizeNum = 2;
    public static int isSpawn;
    [SerializeField] GameObject[] AddsToggles = {};
    [SerializeField] GameObject SpawnToggle;
    [SerializeField] GameObject[] SizeToggle;
    [SerializeField] int billNum = 15;
    [SerializeField] int crystalNum = 13;
    [SerializeField] int batteryNum = 12;
    [SerializeField] int clockNum = 11;
    [SerializeField] int bombNum = 13;
    [SerializeField] int iceNum = 13;
    [SerializeField] int mushroomNum = 8;
    [SerializeField] int colorComboNum = 10;
    [SerializeField] int appleNum = 2;
    [SerializeField] int boolShip = 8;
    bool ifBill;
    bool ifCrystal;
    bool ifBattery;
    bool ifClock;
    bool ifBomb;
    bool ifIce;
    bool ifMushroom;
    bool ifShip;
    bool ifColor;
    bool ifApple;

    public void CheckToggles()
    {
        for (int i = 0; i < AddCount.Length; i++)
        {
            Debug.Log("Addss Lenght " + i);
            AddsToggles[i].GetComponent<Toggle>().isOn = AddCount[i] == 0 ? false : true;
        }

        SpawnToggle.GetComponent<Toggle>().isOn = isSpawn == 0 ? false : true;

        for (int i = 0; i < sizeBoard.Length; i++)
        {
            SizeToggle[i].GetComponent<Toggle>().isOn = sizeBoard[i] == 1 ? true : false;
        }
    }
    public void SpawnBlocksToggle()
    {
        isSpawn = SpawnToggle.GetComponent<Toggle>().isOn == true ? 1 : 0;
    }
    public void BlockSizeToggle()
    {
        for (int i = 0; i < sizeBoard.Length; i++)
        {
            sizeBoard[i] = 0;

            if (SizeToggle[i].GetComponent<Toggle>().isOn != false)
            {
                sizeBoard[i] = 1;
                sizeNum = i;
            }
        }
    }

    public void BillToggle()
    {
        AddCount[0] = AddsToggles[0].GetComponent<Toggle>().isOn == true ? billNum : 0;
        ifBill = AddsToggles[0].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void CrystalToggle()
    {
        AddCount[1] = AddsToggles[1].GetComponent<Toggle>().isOn == true ? crystalNum : 0;
        ifCrystal = AddsToggles[1].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void BatteryToggle()
    {
        AddCount[2] = AddsToggles[2].GetComponent<Toggle>().isOn == true ? batteryNum : 0;
        ifBattery = AddsToggles[2].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void ClockToggle()
    {
        AddCount[3] = AddsToggles[3].GetComponent<Toggle>().isOn == true ? clockNum : 0;
        ifClock = AddsToggles[3].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void BombToggle()
    {
        AddCount[4] = AddsToggles[4].GetComponent<Toggle>().isOn == true ? bombNum : 0;
        ifBomb = AddsToggles[4].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void IceToggle()
    {
        AddCount[5] = AddsToggles[5].GetComponent<Toggle>().isOn == true ? iceNum : 0;
        ifIce = AddsToggles[5].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void MushroomToggle()
    {
        AddCount[6] = AddsToggles[6].GetComponent<Toggle>().isOn == true ? mushroomNum : 0;
        ifMushroom = AddsToggles[6].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void ColorToggle()
    {
        AddCount[7] = AddsToggles[7].GetComponent<Toggle>().isOn == true ? colorComboNum : 0;
        ifColor = AddsToggles[7].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void AppleToggle()
    {
        AddCount[8] = AddsToggles[8].GetComponent<Toggle>().isOn == true ? appleNum : 0;
        ifApple = AddsToggles[8].GetComponent<Toggle>().isOn == true ? true : false;
    }
    public void ShipToggle()
    {
        //Всегда должен быть последним
        AddCount[9] = AddsToggles[9].GetComponent<Toggle>().isOn == true ? boolShip : 0;
        ifShip = AddsToggles[9].GetComponent<Toggle>().isOn == true ? true : false;
    }

    public void SaveAdd()
    {
        for (int i = 0; i < AddCount.Length; i++)
        {
            PlayerPrefs.SetInt("Add" + i, (int)AddCount[i]);
        }
        for (int i = 0; i < sizeBoard.Length; i++)
        {
            if (sizeBoard[i] == 1)
            {
                PlayerPrefs.SetInt("BoardSize", (int)i);
            }
        }
        PlayerPrefs.SetInt("isSpawn", (int)isSpawn);
        PlayerPrefs.SetInt("isSaved", (int)1);
        PlayerPrefs.Save();
    }

    public static void LoadAdd()
    {
        for (int i = 0; i < AddCount.Length; i++)
        {
            AddCount[i] = PlayerPrefs.GetInt("Add" + i);
        }
        isSpawn = PlayerPrefs.GetInt("isSpawn");

        int e = PlayerPrefs.GetInt("BoardSize");
        for (int i = 0; i < AddCount.Length; i++)
        {
            if (i == e)
            {
                sizeBoard[i] = 1;
                sizeNum = i;
            }
            else
            {
                sizeBoard[i] = 0;
            }
        }
    }

    public void LoadGame()
    {
        Destroy(gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Classic");
    }
}
