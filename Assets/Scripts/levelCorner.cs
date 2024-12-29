using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCorner : MonoBehaviour
{
    public void Run(float e)
    {
        StartCoroutine(changePosition(e));
    }
    private IEnumerator changePosition(float i)
    {
        Vector3 startingPos = transform.localPosition;
        Vector3 finalPos = new Vector3(i, transform.localPosition.y, transform.localPosition.z);
        float elapsedTime = 0;

        while (elapsedTime < 0.5f)
        {
            transform.localPosition = Vector3.Lerp(startingPos, finalPos, (elapsedTime / 0.5f));
            elapsedTime += Time.deltaTime;
            yield return null;
            transform.localPosition = finalPos;
        }
    }

    public void RunBottom(int e, float x)
    {
        StartCoroutine(changeScale(e, x));
    }
    private IEnumerator changeScale(int i, float x)
    {
        Vector3 startingPos = transform.localScale;
        Vector3 finalPos = new Vector3(i, transform.localScale.y, transform.localScale.z);
        float elapsedTime = 0;
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);

        while (elapsedTime < 0.5f)
        {
            transform.localScale = Vector3.Lerp(startingPos, finalPos, (elapsedTime / 0.5f));
            elapsedTime += Time.deltaTime;
            yield return null;
            transform.localScale = finalPos;
        }
    }
}
