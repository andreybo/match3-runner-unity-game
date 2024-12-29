using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject Sound;
    [SerializeField] GameObject Combo;
    //[SerializeField] GameObject Vibrate;
    //[SerializeField] GameObject Touch;
    bool ifOnSound;
    bool ifOnVibrate;
    bool ifAutoCombo;
    //bool ifTouch;

    private void Start()
    {
        ifOnSound = PlayerPrefs.GetInt("sound") == 0 ? true : false;
        ifAutoCombo = PlayerPrefs.GetInt("combo") == 0 ? true : false;
        //ifOnVibrate = PlayerPrefs.GetInt("vibrate") == 0 ? true : false;
        //ifTouch = PlayerPrefs.GetInt("touch") == 1 ? true : false;
        Debug.Log("setSound" + ifOnSound);
        //Debug.Log("setVibr" + ifOnVibrate);
        //Debug.Log("setTouch" + ifTouch);

        /*if (ifOnVibrate)
        {
            Vibrate.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            Vibrate.GetComponent<Toggle>().isOn = false;
        }*/


        if (ifOnSound)
        {
            Sound.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            Sound.GetComponent<Toggle>().isOn = false;
        }
        if (ifAutoCombo)
        {
            Combo.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            Combo.GetComponent<Toggle>().isOn = false;
        }


        /*if (ifTouch)
        {
            Touch.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            Touch.GetComponent<Toggle>().isOn = false;
        }*/
    }

    public void SoundSelect()
    {
        bool ifOnSound2 = Sound.GetComponent<Toggle>().isOn;
        if (ifOnSound2)
        {
            PlayerPrefs.SetInt("sound", 0);
            PlayerPrefs.Save();
            AudioListener.volume = 1;
            Debug.Log("setSound2" + PlayerPrefs.GetInt("sound"));
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            PlayerPrefs.Save();
            AudioListener.volume = 0;
            Debug.Log("setSound2" + PlayerPrefs.GetInt("sound"));
        }
    }

    public void ComboSelect()
    {
        bool ifAutoCombo2 = Combo.GetComponent<Toggle>().isOn;
        if (ifAutoCombo2)
        {
            PlayerPrefs.SetInt("combo", 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("combo", 1);
            PlayerPrefs.Save();
        }
    }

    /*public void TouchSelect()
    {
        bool ifTouch2 = Touch.GetComponent<Toggle>().isOn;
        if (ifTouch2)
        {
            PlayerPrefs.SetInt("touch", 1);
            PlayerPrefs.Save();
            int check = PlayerPrefs.GetInt("touch");
            Debug.Log("setTouch2" + check);
        }
        else
        {
            PlayerPrefs.SetInt("touch", 0);
            PlayerPrefs.Save();
            int check = PlayerPrefs.GetInt("touch");
            Debug.Log("setTouch2" + check);
        }
    }*/
}
