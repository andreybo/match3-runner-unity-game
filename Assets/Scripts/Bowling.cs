using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    int speed = 3;
    float z = 0;

    private void Update()
    {

        z = transform.position.z - speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        if (transform.position.y < -4f)
        {
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
        GetComponent<AudioSource>().Play();
    }
}
