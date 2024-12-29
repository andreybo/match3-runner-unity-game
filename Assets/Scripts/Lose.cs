using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    //[SerializeField] SpawnRun SpawnRun;
    public bool ifStarted = false;

    private void Update()
    {
        ifStarted = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject din = collision.gameObject;
        if (din.CompareTag("Hero") & ifStarted != true)
        {
            StartCoroutine(Back(din));
        }
    }

    private IEnumerator Back(GameObject din)
    {
        float speed = GameControllerButton.Instance.levelSpeed;
        GameStatus.Instance.LivesRemove();
        ifStarted = true;
        PopManager.Instance.showTakes();
        Ball.Instance.VFXHealthCol();
        din.GetComponent<Rigidbody>().isKinematic = true;
        GameControllerButton.Instance.speed = 0;
        //din.GetComponent<Rigidbody>().velocity = new Vector3(0, 4, 0);
        din.transform.position = new Vector3(din.transform.position.x, 4f, din.transform.position.z);
        yield return new WaitForSeconds(0.2f);
        din.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        din.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        din.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        din.SetActive(true);
        din.GetComponent<Rigidbody>().isKinematic = false;
        GameControllerButton.Instance.speed = speed;
    }
}
