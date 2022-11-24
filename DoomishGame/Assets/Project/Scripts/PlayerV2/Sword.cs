using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] GameObject PickableComponent;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(transform.right * velocity, ForceMode2D.Impulse);
    }

    private void SwordStop()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
        PickableComponent.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "Player")
        {
            SwordStop();
        }
    }
}
