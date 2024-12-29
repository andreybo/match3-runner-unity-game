using UnityEngine;
using System.Collections.Generic;

public class PoolCubes : MonoBehaviour
{

    public List<CubesList> CubesList = new List<CubesList>();


    static Transform thisTransform;
    static int[] numberCubes;
    static GameObject[][] stCubes;
    static public int numCubesList;


    void Start()
    {
        thisTransform = transform;
        numCubesList = CubesList.Count;
        AddObjectsToPool();
    }

    void AddObjectsToPool()
    {
        numberCubes = new int[numCubesList];
        stCubes = new GameObject[numCubesList][];

        for (int num = 0; num < numCubesList; num++)
        {
            numberCubes[num] = CubesList[num].numberObjects;
            stCubes[num] = new GameObject[numberCubes[num]];
            InstanInPool(CubesList[num].Cube, stCubes[num]);
        }

    }

    void InstanInPool(GameObject obj, GameObject[] objs)
    {

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(obj);
            objs[i].SetActive(false);
            objs[i].transform.parent = thisTransform;
            //GameStatus.Instance.listOfPools.Add(objs[i]);
        }

    }


    static public GameObject GiveCube(int numElement)
    {

        for (int i = 0; i < numberCubes[numElement]; i++) if (!stCubes[numElement][i].activeSelf) return stCubes[numElement][i];

        return null;

    }


    static public void TakeCube(GameObject obj)
    {

        if (obj.activeSelf) obj.SetActive(false);
        if (obj.transform.parent != thisTransform) obj.transform.parent = thisTransform;
    }

}


[System.Serializable]

public class CubesList
{
    public GameObject Cube;
    public int numberObjects = 1;

}