using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    bool ifStart = false;
    bool ifEnd = false;
    [SerializeField] TMP_Text textmeshPro;
    [SerializeField] float smooth = 0.5f;
    float go;

    public void Zero()
    {
        ifEnd = false;
        textmeshPro.color = new Color(0, 0, 0, 0);
    }

    private void Start()
    {
        Zero();
        ifStart = false;
        ifEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifStart!= false & ifEnd != true) {
            go = textmeshPro.color.a + Time.deltaTime * smooth;
            gameObject.transform.localScale = new Vector3(go, go, go);
            textmeshPro.color = new Color(0, 0, 0, go);
            if (textmeshPro.color.a > 0.9)
            {
                ifEnd = true;
            }
        }
        else if(ifEnd != false)
        {
            go = textmeshPro.color.a - Time.deltaTime * smooth;
            textmeshPro.color = new Color(0, 0, 0, go);
            if (textmeshPro.color.a < 0)
            {
                ifEnd = false;
                ifStart = false;
                textmeshPro.enabled = false;
            }
        }

        
    }

    public void PlayFade()
    {
        Zero();
        float r = Random.Range(-20, 20);
        gameObject.transform.localEulerAngles = new Vector3(0, 0, r);
        ifStart = true;
    }
}
