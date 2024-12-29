using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    [SerializeField] GameObject VFXDeath;

    public GameStatus gs;
    public SpawnRun SpawnRun;
    public GameObject VFXCollision;
    [SerializeField] AudioClip breakSound;
    //private float elapsedTime = 0.0f;

    void Update()
    {
        if (isWandering == false) {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true) {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            if (Physics.Raycast(transform.position, -transform.up, 2))
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }

    public void Hero()
    {
        gameObject.SetActive(false);
        PlaySFX();
        GameObject sparkles = Instantiate(VFXCollision, transform.position, transform.rotation);
        Invoke("Spawn",10);
    }


    private void DeathSparklesVFX()
    {
        GameObject sparkles = Instantiate(VFXDeath, transform.position, transform.rotation);
    }
    public void PlaySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }
}
