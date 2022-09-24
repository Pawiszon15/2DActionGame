using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion properties")]
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float timeToExplode;

    [Header("Grenade properties")]
    [SerializeField] float velocity;

    [Header("References")]
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(transform.right * velocity, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Debug.Log("enter collision");
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToExplode);
        var inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D collider2d in inExplosionRadius)
        {
            Rigidbody2D rigidbody2D = collider2d.GetComponent<Rigidbody2D>();
            if(rigidbody2D != null)
            {
                //other effect for the player, and other effect for the enemies
                Vector2 distanceVector = collider2d.transform.position - transform.position;
                if(distanceVector.magnitude > 0)
                {
                    float explosionPower = explosionForce/distanceVector.magnitude;
                    rigidbody2D.AddForce(explosionPower * distanceVector.normalized, ForceMode2D.Impulse);
                }

            }
        }

        Destroy(gameObject);
    }
}
