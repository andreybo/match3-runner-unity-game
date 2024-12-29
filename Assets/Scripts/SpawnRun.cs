using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRun : MonoBehaviour
{
    float z;
    float x;
    float y;
    Vector3 SpawnPoint;
    float[] randomRotate = { 90, -90, 180, 0, 30, 45 };

    GameObject objTemp;
    [Header("Pooling")]
    public float secondsBetweenSpawn = 1;
    [SerializeField] float elapsedTime = 0.0f;

    [Header("Link")]
    [SerializeField] Transform ParentAds;
    [SerializeField] Cloud Cloud;

    void Update()
    {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > secondsBetweenSpawn & Cloud.isCube != false)
            {
                elapsedTime = 0;
                Pull();
            }
    }

    private void Pull()
    {

        if (GameStatus.Instance.listOfAdds.Count != 0)
        {
            int r = Random.Range(0, GameStatus.Instance.listOfAdds.Count);
            objTemp = GameStatus.Instance.listOfAdds[r];
            GameStatus.Instance.listOfAdds.Remove(objTemp);
        }
        if (objTemp != null)
        {
            Spawn(objTemp);
        }
    }

    public void Spawn(GameObject obj)
    {
            SpawnPoint = Cloud.whereCube;
            obj.transform.position = new Vector3(Mathf.Round(SpawnPoint.x), SpawnPoint.y, Mathf.Round(SpawnPoint.z));
            obj.transform.parent = ParentAds;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            obj.transform.Rotate(0f, randomRotate[Random.Range(0, 4)], 0f, Space.Self);
            if (obj.CompareTag("crystal_parent"))
            {
                obj.GetComponent<ColorHero>().ChangeColor();
            }
            if (obj.CompareTag("Ice"))
            {
                obj.GetComponent<ColorHero>().ChangeColorRandom();
                obj.transform.Rotate(0f, randomRotate[Random.Range(3, 6)], 0f, Space.Self);
            }
            if (obj.CompareTag("Bomb") || obj.CompareTag("Ice"))
            {
                obj.GetComponent<Bomb>().isColided = false;
                obj.transform.Rotate(0f, randomRotate[Random.Range(3, 6)], 0f, Space.Self);
            }
                obj.SetActive(true);
    }

}
