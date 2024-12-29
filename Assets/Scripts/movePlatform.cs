using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    [SerializeField] Transform Platform;
    public float speed;
    
    void Update()
    {
            Platform.transform.Translate(Platform.transform.forward * Time.deltaTime * speed, Space.World);
    }
}
