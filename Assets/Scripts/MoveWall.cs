using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    // set the minimum and maximum speed of the movement
    public float minSpeed = 1.0f;
    public float maxSpeed = 5.0f;

    // set the minimum and maximum duration of the movement
    public float minDuration = 1.0f;
    public float maxDuration = 5.0f;

    void Start()
    {
        // call the Move() coroutine
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            // randomly select the direction of the movement (-1 for left, 1 for right)
            int direction = Random.Range(0, 2) == 0 ? -1 : 1;

            // randomly select the speed and duration of the movement
            float speed = Random.Range(minSpeed, maxSpeed);
            float duration = Random.Range(minDuration, maxDuration);

            // move the object in the specified direction for the specified duration
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                transform.Translate(direction * speed * Time.deltaTime, 0, 0);
                elapsedTime += Time.deltaTime;
                if (transform.position.x < -5.0f || transform.position.x > 5.0f)
                {
                    direction *= -1;
                }
                yield return null;
            }

            // wait for a short delay before starting the next movement
            float delay = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(delay);
        }
    }
}
