using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEvent : MonoBehaviour
{
    Additionals Add;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            GameStatus.Instance.listOfAdds.Add(gameObject);
            PoolAdds.TakeAdds(gameObject);
        }
    }
}
