using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoBullet : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    [SerializeField] LayerMask layer;
    [SerializeField] float StartSpeed, launchPhase, HoldPhase, rotateSpeed, rocketSpeed;
    private bool shouldFollowPlayer = false;
    private GameObject target;
    DeflectAbility deflectAbility;

    private void Awake()
    {
        target = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        deflectAbility = FindObjectOfType<DeflectAbility>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(deflectAbility.mousePos.right * StartSpeed, ForceMode2D.Impulse);
        StartCoroutine(LaunchPhase());
    }

    void FixedUpdate()
    {
        if (shouldFollowPlayer)
        {
            Vector2 dir = (Vector2)target.transform.position - rb.position;
            dir.Normalize();

            float rotateAmount = Vector3.Cross(dir, transform.right).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.right * rocketSpeed;

        if(Physics2D.Raycast(transform.position, dir, 4f, layer))
        {
            Destroy(gameObject);
        }
        }

    }

    IEnumerator LaunchPhase()
    {
        yield return new WaitForSeconds(launchPhase);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(HoldPhase);
        circleCollider.isTrigger = true;
        shouldFollowPlayer = true;

    }
}