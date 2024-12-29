using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEventStep : MonoBehaviour
{
    [SerializeField] GameObject[] Add;
    [SerializeField] bool isEmpty;
    [SerializeField] bool isAdd;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hero") & isAdd != false)
        {
                gameObject.SetActive(false);
        }
    }

    public void SetActive()
    {
        if (isEmpty != true)
        {
            foreach (GameObject i in Add)
            {
                i.SetActive(true);
            }
        }
    }
}
