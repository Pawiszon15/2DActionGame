using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] LayerMask layersToCheck;

    private Rigidbody2D rb;
    private GameObject playerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        ProximityToGround();
    }

    private void ProximityToGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -Vector2.up, 100f, layersToCheck);

        if(hit2D.distance > 3f)
        {
           rb.velocity = bulletSpeed * -Vector2.up;
        }

        else
        {
            rb.velocity = bulletSpeed * Vector2.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform") 
            Destroy(gameObject);
    }
}
