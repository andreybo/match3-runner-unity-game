using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    // Start is called before the first frame update
    int zStart;

    public void Play(int z)
    {
        zStart = z;
        StartCoroutine(MoveObject());
        transform.position = new Vector3(transform.position.x, z, transform.position.z);
    }

    public IEnumerator MoveObject()
    {
        float speed = Random.Range(0.3f, 1f);
        float delay = Random.Range(0,0.8f);
        yield return new WaitForSeconds(delay);
        float StartTime = Time.time;
        float EndTime = StartTime + speed;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / speed;
            float z = Mathf.Lerp(zStart, 0, timeProgressed);
            transform.position = new Vector3(transform.position.x, z, transform.position.z);
            yield return null;
        }

        if (Time.time >= EndTime)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

}
