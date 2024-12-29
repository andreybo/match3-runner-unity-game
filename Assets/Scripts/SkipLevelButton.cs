using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject UIBuy;
    void Start()
    {
        
        if(PlayerPrefs.HasKey("Purchase")){
            UIBuy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
