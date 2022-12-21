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
    private Transform target;
    Vector2 _moveInput;
    DeflectAbility deflectAbility;

    private void Awake()
    {
        target = FindObjectOfType<Player>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        deflectAbility = FindObjectOfType<DeflectAbility>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        Debug.Log(_moveInput);
        rb.AddForce(_moveInput * StartSpeed, ForceMode2D.Impulse);
        StartCoroutine(LaunchPhase());
    }

    void FixedUpdate()
    {
        if (shouldFollowPlayer)
        {
            Vector2 dir = (Vector2)target.position - rb.position;
            dir.Normalize();

            float rotateAmount = Vector3.Cross(dir, transform.right).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.right * rocketSpeed;


            if (Physics2D.Raycast(transform.position, dir, 1f, layer))
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