using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGParallax : MonoBehaviour
{
    public float parallaxFactor;
    private Vector2 offset;
    [SerializeField] Transform Platform;
    private RawImage m_material;
    public Texture[] images;

    private void Start()
    {
        m_material = GetComponent<RawImage>();
        m_material.texture = images[Random.Range(0, images.Length)];
    }

    private void Update()
    {
        float z = Platform.position.z;
        m_material.uvRect = new Rect(0, z * parallaxFactor, 1, 2);
    }
}
