using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterMenu : MonoBehaviour
{
    //public GameObject DIN;
    public int characterNumber;
    public string characterName;
    [SerializeField] GameObject[] Characters;
    [SerializeField] GameObject[] CharactersMenuItem;
    [SerializeField] GameObject Next;
    [SerializeField] GameObject Prev;

    private void Start()
    {
        characterNumber = PlayerPrefs.GetInt("Character");
        characterName = PlayerPrefs.GetString("CharacterName");
        if (!PlayerPrefs.HasKey("Character"))
        {
            ch0();
        }
        else
        {
            ModelCharacter();
        }
        CheckPosition();
    }

    public void ActiveCharacter()
    {
        SwapCh(characterNumber);
    }

    public void ModelCharacter()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            if (i != characterNumber)
            {
                Characters[i].SetActive(false);
            }
            else
            {
                Characters[i].SetActive(true);
                PlayerPrefs.SetString("CharacterName", Characters[i].name);
                PlayerPrefs.SetInt("Character", characterNumber);
                PlayerPrefs.Save();
            }
        }
    }

    public void ch0()
    {
        //Din
        characterNumber = 0;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 4f);
        PlayerPrefs.SetFloat("height", 3f);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch1()
    {
        //D7N - v kepke
        characterNumber = 1;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 4f);
        PlayerPrefs.SetFloat("height", 3f);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch2()
    {
        //DON - Lady
        characterNumber = 2;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 4f);
        PlayerPrefs.SetFloat("height", 3f);
        PlayerPrefs.SetInt("health", 4);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch3()
    {
        //D2N - Pilot
        characterNumber = 3;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 6f);
        PlayerPrefs.SetFloat("height", 3f);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch4()
    {
        //DJN - Gop
        characterNumber = 4;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 3f);
        PlayerPrefs.SetFloat("height", 3f);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch5()
    {
        //DCN - Pruzhina
        characterNumber = 5;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 4f);
        PlayerPrefs.SetFloat("height", 5f);
        PlayerPrefs.SetInt("health", 3);
        PlayerPrefs.SetInt("ship", 0);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }
    public void ch6()
    {
        //DzzZN - Robot
        characterNumber = 6;
        ModelCharacter();
        PlayerPrefs.SetFloat("speed", 4f);
        PlayerPrefs.SetFloat("height", 4f);
        PlayerPrefs.SetInt("health", 4);
        PlayerPrefs.SetInt("ship", 1);
        PlayerPrefs.SetInt("Character", characterNumber);
        PlayerPrefs.Save();
    }

    void CheckPosition()
    {
        if (characterNumber == Characters.Length - 1)
        {
            Next.SetActive(false);
        }
        else if (characterNumber == 0)
        {
            Prev.SetActive(false);
        }
        else
        {
            Next.SetActive(true);
            Prev.SetActive(true);
        }
    }

    public void SwapAllByArrayNext()
    {
        characterNumber += 1;
        CheckPosition();
        SwapCh(characterNumber);
    }
    public void SwapAllByArrayPrev()
    {
        characterNumber -= 1;
        CheckPosition();
        SwapCh(characterNumber);
    }

    void SwapCh(int i)
    {
        for (int go = 0; go < CharactersMenuItem.Length; go++)
        {
            if (go == i)
            {
                CharactersMenuItem[go].SetActive(true);
                CharactersMenuItem[go].GetComponent<CustomAnimation>().PlayAnimation();
            }
            else
            {
                CharactersMenuItem[go].SetActive(false);
            }
        }
    }

}
