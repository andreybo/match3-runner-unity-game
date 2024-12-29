using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] GameObject boomSparklesVFX;
    AudioSource AudioSource;
    public bool isColided = false;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter()
    {
        if (isColided == false)
        {
            TriggerSparklesVFX();
            PlaySFX();
            isColided = true;
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(gameObject);
        }
    }

    public void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(boomSparklesVFX, transform.position, transform.rotation);
    }

    public void PlaySFX()
    {
        AudioSource.PlayClipAtPoint(AudioSource.clip, Camera.main.transform.position);
    }

    void Update(){
        if (transform.position.y < -4f)
        {
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(gameObject);
        }
    }

}
