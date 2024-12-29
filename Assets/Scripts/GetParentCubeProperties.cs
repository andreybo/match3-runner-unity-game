using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParentCubeProperties : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;
    [SerializeField] Cube cube;

    // Update is called once per frame
    void Update()
    {
        if (cube.colorName != GetComponent<Cube>().colorName)
        {
            GetComponent<MeshRenderer>().material = mr.material;
            GetComponent<Cube>().colorName = cube.colorName;
        }
        
    }
}
