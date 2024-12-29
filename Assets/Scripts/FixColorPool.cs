using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixColorPool : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("crystal"))
        {
            gameObject.GetComponent<ColorHero>().ChangeColor();
        }
    }
}
