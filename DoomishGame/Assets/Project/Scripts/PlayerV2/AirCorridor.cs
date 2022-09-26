using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCorridor : MonoBehaviour
{
    [Header("Properties of air corridor")]
    [SerializeField] float airForce;

    [Header("References")]
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null)
        {
            Debug.Log("sth on trigger stay");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(gameObject.transform.up * airForce);
        }
    }
}
