using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;
    [SerializeField] float rotateSpeed;
    

    private Rigidbody2D rb;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 dir = (Vector2)target.position - rb.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * rocketSpeed;
    }

}
