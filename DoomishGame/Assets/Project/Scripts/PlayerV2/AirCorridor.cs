using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCorridor : MonoBehaviour
{
    [Header("Properties of air corridor")]
    [SerializeField] float airForce;
    [SerializeField] float liveTime;

    [Header("References")]
    [SerializeField] GameObject airCorridor;
    private BoxCollider2D boxCollider;
    

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(LiveTime());
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

    IEnumerator LiveTime()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(airCorridor);
    }
}
