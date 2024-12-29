using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturesTop : MonoBehaviour
{
    [SerializeField] GameObject[] Features;
    Vector3 startScale = new Vector3(0.7f, 0.7f, 0.7f);

    /*
     * 0 - Battery
     * 1 - Clock
     * 2 - Diamond
     * 3 - Grib
     * 4 - Health +
     * 5 - Kakashka
     * 6 - Kamen
     * 7 - Lowspeed
     * 8 - Ship
     */

    public void Show(int i)
    {
        int sum = transform.childCount;
        if (sum > 2)
        {
            transform.GetChild(1).localScale = startScale;
            transform.GetChild(2).localScale = startScale;
            Destroy(transform.GetChild(0).gameObject);
        }
        else if(sum > 1)
        {
            transform.GetChild(0).localScale = startScale;
            transform.GetChild(1).localScale = startScale;
        }
        else if (sum > 0)
        {
            transform.GetChild(0).localScale = startScale;
        }

        GameObject action = Instantiate(Features[i], transform.position, transform.rotation);
        action.transform.SetParent(gameObject.transform);
        action.transform.localScale = new Vector3(1,1,1);
    }

    public void Destroy()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
}
