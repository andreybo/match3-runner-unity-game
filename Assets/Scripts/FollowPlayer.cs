using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [Header("Link")]
    [SerializeField] Transform target;
    Vector3 cameraOffset;
    [SerializeField] float smoothFactor = 0.5f;
    [SerializeField] bool lookAtTarget = true;
    Vector3 tempVec3 = new Vector3();
    //Vector3 newPosition = new Vector3();

    private void Start()
    {
        cameraOffset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        // Update the y position of the camera to 6f
        tempVec3 = new Vector3(target.position.x, 6f, target.position.z);

        // Offset the camera's position to the updated position
        Vector3 newPosition = tempVec3 + cameraOffset;

        // Smoothly move the camera to the updated position
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        transform.position = smoothedPosition;

        // Look at the target object if required
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
