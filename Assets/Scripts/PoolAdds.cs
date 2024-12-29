using UnityEngine;
using System.Collections.Generic;

public class PoolAdds : MonoBehaviour
{

    [SerializeField] bool isRandom = false;
    public List<AddsList> AddsList = new List<AddsList>();


    static Transform thisTransform;
    static int[] numberAdds;
    static GameObject[][] stAdds;
    static public int numAddsList;


    void Start()
    {
        thisTransform = transform;
        numAddsList = AddsList.Count;
        AddObjectsToPool();
    }

    void AddObjectsToPool()
    {
        numberAdds = new int[numAddsList];
        stAdds = new GameObject[numAddsList][];

        for (int num = 0; num < numAddsList; num++)
        {
            if (isRandom != false)
            {
                numberAdds[num] = SetRandom.AddCount[num];
            }
            else
            {
                numberAdds[num] = AddsList[num].numberObjects;
            }
            stAdds[num] = new GameObject[numberAdds[num]];
            InstanInPool(AddsList[num].Add, stAdds[num]);
        }

    }


    void InstanInPool(GameObject obj, GameObject[] objs)
    {

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(obj);
            objs[i].SetActive(false);
            objs[i].transform.parent = thisTransform;
            GameStatus.Instance.listOfAdds.Add(objs[i]);
        }

    }


    static public GameObject GiveAdds(int numElement)
    {

        for (int i = 0; i < numberAdds[numElement]; i++) if (!stAdds[numElement][i].activeSelf) return stAdds[numElement][i];

        return null;

    }

    static public void TakeAdds(GameObject obj)
    {

        if (obj.activeSelf) obj.SetActive(false);
        if (obj.transform.parent != thisTransform) obj.transform.parent = thisTransform;
    }

}


[System.Serializable]

public class AddsList
{
    public GameObject Add;
    public int numberObjects = 100;
}