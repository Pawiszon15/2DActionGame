using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] GameObject gameObjectParent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SwordCarrier>())
        {
            collision.gameObject.GetComponent<SwordCarrier>().SwordPickup();
            Destroy(gameObjectParent);
        }
    }

}
