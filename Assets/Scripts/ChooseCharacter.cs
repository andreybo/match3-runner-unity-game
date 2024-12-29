using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    public GameObject[] Characters;
    public int characterNumber;
    public static ChooseCharacter Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadNumber();
    }

    private void LoadNumber()
    {
        characterNumber = PlayerPrefs.GetInt("Character");
    }

    public void ModelCharacter(GameObject Din)
    {
        LoadNumber();
        GameObject oldDIN = Din.transform.GetChild(0).gameObject;
        GameObject newDIN = Characters[characterNumber];

        Quaternion rotation = oldDIN.transform.rotation;
        Vector3 position = oldDIN.transform.position;
        Vector3 scale = oldDIN.transform.localScale;

        GameObject newGameObject = Instantiate(newDIN, position, rotation);

        if (oldDIN.transform.parent != null)
        {
            newGameObject.transform.SetParent(oldDIN.transform.parent);
            newGameObject.transform.SetAsFirstSibling();
            newGameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        DestroyImmediate(oldDIN);
    }
}
