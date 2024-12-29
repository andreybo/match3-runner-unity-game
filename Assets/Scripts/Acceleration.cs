using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    float x;
    float ax;
    float y;
    float ay;
    public float z = 0;
    public float marginX = 0;
    public float marginY = 0;
    Vector3 newpos;

    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    void Update()
    {

        ax = x + Input.acceleration.x * marginX;
        ay = y + Input.acceleration.x * marginY;
        newpos = new Vector3(ax, ay, z);
        transform.position = Vector3.Lerp(transform.position, newpos, Time.deltaTime * 0.8f);
    }
}