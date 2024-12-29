using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSprite : MonoBehaviour
{
    [Header("Main")]
    public int idPool;

    /*
     * 0 - coins
     * 1 - diamonds
     * 2 - tickets
     * */

    public int poolCount = 15;
    public Transform from;
    public Transform to;
    public bool toHeader = true;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject Top;


    [Header("Additional")]
    bool ifPlay;
    [SerializeField] float timeAnimation = 0.3f;

    public void Play()
    {
        if (ifPlay!=true & from != null & to != null)
        {
            if (toHeader != false)
            {
                Top.SetActive(true);
            }
            StartCoroutine(StartMove());
            ifPlay = true;
            particles.transform.position = to.position;
        }
    }

    IEnumerator StartMove()
    {
        for ( int i = 0; i < poolCount; i++ )
        {
            bool isEnd = false;
            if (i == poolCount - 1)
            {
                isEnd = true;
            }
            StartCoroutine(moveObject(Pool.Instance.Activate(idPool, from.position), isEnd));
            float delay = Random.Range(0, 0.2f);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator moveObject(GameObject tempObject, bool isEnd)
    {
        var a = tempObject.transform.position;
        float rand = Random.Range(-1f, 1f);
        float rand2 = Random.Range(-1f, 1f);
        var b = new Vector3(rand, rand2, 0);
        var c = to.transform.position;

        float elapsedTime = 0;
        CustomAnimation custom = tempObject.GetComponent<CustomAnimation>();
        if (custom!=null)
        {
            custom.PlayAnimation();
        }

        while (elapsedTime < timeAnimation)
        {
            var ab = Vector2.Lerp(a, b, (elapsedTime / timeAnimation));
            var bc = Vector2.Lerp(b, c, (elapsedTime / timeAnimation));
            var result = Vector2.Lerp(ab, bc, (elapsedTime / timeAnimation));
            tempObject.transform.position = result;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (elapsedTime > timeAnimation)
        {
            Pool.Instance.Deactivate(tempObject);
            if (to.GetComponent<CustomAnimation>() != null)
            {
                to.GetComponent<CustomAnimation>().PlayAnimation();
            }
            particles.GetComponent<ParticleSystem>().Play();
            if (isEnd != false)
            {
                ifPlay = false;

                if (toHeader != false)
                {
                    Top.SetActive(false);
                }
            }
        }

    }

}
