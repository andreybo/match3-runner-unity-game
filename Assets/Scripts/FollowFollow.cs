using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FollowFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothX = 0.1f;
        public float smoothZ = 0.1f;

        private Vector3 velocity = Vector3.zero;

        private void LateUpdate()
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z - 4.5f);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Mathf.Max(smoothX, smoothZ));
        }
    }
