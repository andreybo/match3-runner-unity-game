using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Wall : MonoBehaviour
{

    

    [Header("Colors")]
    public Material[] ColorsBlock;
    
    public static Wall Instance;
    
    void Awake(){
        Instance = this;
    }

}