using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.GetComponent<Grenade>())
        {
            Grenade grenade = collision.gameObject.GetComponent<Grenade>();
            //grenade.ThrowInDirection(gameObject.transform.right);
        }
    }
}
