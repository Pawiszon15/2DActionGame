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
    [SerializeField] float explosionRangeMulti;

    [Header("References")]
    [SerializeField] CircleCollider2D explosionTrigger;
    private bool stickToSth = false;
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
        if(collision.gameObject.tag == "Platform")
        {
            Debug.Log("enter collision");
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
            stickToSth = true;
            StartCoroutine(ExplodeAfterTime());
        }

        else if(collision.gameObject.tag == "Player" && stickToSth)
        {
            StopAllCoroutines();
            Explode();
        }
    }



    public void Explode()
    {
        explosionTrigger.radius = explosionRadius + 1;
        explosionTrigger.enabled = true;
        StartCoroutine(WaitForMomement());
    }

    IEnumerator ExplodeAfterTime()
    {
        yield return new WaitForSeconds(timeToExplode);
        explosionTrigger.radius = explosionRadius + 1;
        explosionTrigger.enabled = true;
        yield return new WaitForSeconds(0.05f);
        var inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider2d in inExplosionRadius)
        {
            Rigidbody2D rigidbody2D = collider2d.GetComponent<Rigidbody2D>();

            if (rigidbody2D != null)
            {
                //other effect for the player, and other effect for the enemies
                Vector2 distanceVector = collider2d.transform.position - transform.position;
                if(distanceVector.magnitude > 0)
                {
                    //rigidbody2D.velocity = Vector2.zero;
                    float explosionPower = explosionForce; /*/distanceVector.magnitude;*/
                    rigidbody2D.AddForce(explosionPower * distanceVector.normalized, ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject);
    }

    IEnumerator WaitForMomement()
    {
        yield return new WaitForSeconds(0.05f);
        var inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider2d in inExplosionRadius)
        {
            Rigidbody2D rigidbody2D = collider2d.GetComponent<Rigidbody2D>();

            if (rigidbody2D != null)
            {
                //other effect for the player, and other effect for the enemies
                Vector2 distanceVector = collider2d.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    //rigidbody2D.velocity = Vector2.zero;
                    float explosionPower = explosionForce; /*/distanceVector.magnitude;*/
                    rigidbody2D.AddForce(explosionPower * distanceVector.normalized, ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject);
    }

    public void ThrowInDirection(Vector2 slashDir)
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.AddForce(slashDir * 2 * velocity, ForceMode2D.Impulse);
    }

    public void BoostByGate()
    {
        explosionRadius = explosionRadius * explosionRangeMulti;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

}
