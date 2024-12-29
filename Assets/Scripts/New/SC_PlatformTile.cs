using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform inner;
    public GameObject[] obstacles; //Objects that contains different obstacle types which will be randomly activated

    public void ActivateRandomObstacle()
    {
        DeactivateAllObstacles();

        System.Random random = new System.Random();
        int randomNumber = random.Next(0, obstacles.Length);
        obstacles[randomNumber].SetActive(true);
    }

    public void MoveToBack(){
        ActivateRandomObstacle();
        SetActive();
    }

    public void SetActive(){
        foreach (Transform child in inner)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<Renderer>().material = Wall.Instance.ColorsBlock[Random.Range(0, Wall.Instance.ColorsBlock.Length - 1)];
        }
    }

    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
}