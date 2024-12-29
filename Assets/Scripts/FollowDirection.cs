using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDirection : MonoBehaviour
{

    [SerializeField] Transform target;
    Vector3 tempVec3 = new Vector3();

    void LateUpdate()
    {
        tempVec3.x = transform.position.x;
        tempVec3.y = 0f;
        tempVec3.z = target.position.z;

        transform.position = tempVec3;
    }
}
