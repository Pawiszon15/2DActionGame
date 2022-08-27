using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    private PolygonCollider2D collider2d;
    private Rigidbody2D rb;

    private void Awake()
    {
        collider2d = GetComponent<PolygonCollider2D>();
        rb = collider2d.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

}
