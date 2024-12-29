using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Additionals : MonoBehaviour
{
    [SerializeField] GameObject blockSparklesVFX;
    public float timer;
    private void Update()
    {
        if (transform.position.y < -4f)
        {
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(gameObject);
        }
    }
    public void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
    }
}
