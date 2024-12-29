using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHero : MonoBehaviour
{

    public void ChangeColor()
    {
        int r = Random.Range(0, Wall.Instance.ColorsBlock.Length);
        GetComponent<Renderer>().material = Wall.Instance.ColorsBlock[r];
        Debug.Log("Count " + Wall.Instance.ColorsBlock.Length);
    }

    public void ChangeColorRandom()
    {
        int r = Random.Range(0, Wall.Instance.ColorsBlock.Length - 1);
        Debug.Log("Length " + Wall.Instance.ColorsBlock.Length);
        GetComponent<Renderer>().material = Wall.Instance.ColorsBlock[r];
    }
}
