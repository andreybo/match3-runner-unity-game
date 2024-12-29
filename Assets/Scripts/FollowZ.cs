using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZ : MonoBehaviour
{
    [SerializeField] GameObject Follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Follow.transform.position.x, transform.position.y, Follow.transform.position.z);
    }
}
