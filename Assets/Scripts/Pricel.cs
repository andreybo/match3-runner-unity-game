using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pricel : MonoBehaviour
{
    [SerializeField] Ball Ball;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            MeshRenderer m = gameObject.GetComponent<MeshRenderer>();
            m.enabled = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            MeshRenderer m = gameObject.GetComponent<MeshRenderer>();
            m.enabled = false;
        }
    }
}
