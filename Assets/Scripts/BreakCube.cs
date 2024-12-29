using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<Cube>().PlayBlockDestroy();
    }
}
