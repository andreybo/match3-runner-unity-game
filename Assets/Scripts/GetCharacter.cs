using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetCharacter : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] string nameCharacter;
    [SerializeField] TMP_Text costString;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] GameObject[] Active;
    [SerializeField] GameObject[] Disabled;
    float sumScore;
    //bool active = false;
    
    bool c_robot = false;
    void Start()
    {
        nameTMP.text = nameCharacter;
        costString.text = cost.ToString();
        sumScore = PlayerPrefs.GetFloat("HighScore");
        c_robot = PlayerPrefs.HasKey("c_robot");
        if (sumScore >= cost || c_robot)
        {
            foreach(GameObject item in Disabled)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in Active)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject item in Disabled)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in Active)
            {
                item.SetActive(false);
            }
            //gameObject.GetComponent<Button>().interactable = false;
        }
    }

    void Update()
    {
        if (c_robot)
        {
            foreach (GameObject item in Disabled)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in Active)
            {
                item.SetActive(true);
            }
        }
    }
}
