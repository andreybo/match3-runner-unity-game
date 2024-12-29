using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject blockSparklesVFX;

    [Header("Count")]
    public int collisionCount = 0;

    [Header("Colors")]
    Renderer rend;
    //public Vector2 colorStart;
    public bool isChanged = false;
    public string colorName;

    //[SerializeField] GameObject dot;
    //[SerializeField] bool isRay = true;

    //[Header("Bools")]
    //public bool isFreeze;

    void Start()
    {
        rend = GetComponent<Renderer>();
        //isFreeze = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            PlayBlockDestroy();
        }
        else if (collision.gameObject.CompareTag("Ice"))
        {
            Paint(collision.gameObject.GetComponent<Renderer>().material);
        }
    }


    private void Freeze()
    {        
        rend.material.mainTextureOffset =  new Vector2(0.2f, 0.8f);
    }

    private void Paint(Material m)
    {
        rend.material = m;
        colorName = m.name;
    }

    public void PlayBlockDestroy()
    {
        //PoolCubes.TakeCube(gameObject);
        gameObject.SetActive(false);
    }


    private void applyPowerUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
    }

    public void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
    }

 }
