using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalerResponsive : MonoBehaviour
{

    float width;
    float height;
    public float resolution;

    // Start is called before the first frame update
    void Awake()
    {
        width = Screen.width;
        height = Screen.height;
        resolution = width / height;
        if (resolution > 0.6)
        {
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
        }
    }
}
