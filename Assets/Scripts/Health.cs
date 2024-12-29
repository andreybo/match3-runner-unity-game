using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] AudioClip HitBreak;
    [SerializeField] GameObject blockSparklesVFX;
 
    public void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
    }

    public void DestroySFX()
    {
        AudioSource.PlayClipAtPoint(HitBreak, Camera.main.transform.position);
    }
}
