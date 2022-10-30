using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject explosion;

    private Rigidbody2D rb;
    private Transform target;
    private GameManger gameManger;
    private SpriteRenderer spriteRender;
    private HomingRocket homingRocket;

    // Start is called before the first frame update
    void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>().transform;
        spriteRender = GetComponent<SpriteRenderer>();
        homingRocket = GetComponent<HomingRocket>();
    }

    void FixedUpdate()
    {
        Vector2 dir = (Vector2)target.position - rb.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * rocketSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision);
            Destroy(gameObject);
            StartCoroutine(WaitBeforeResetingTheLevel());
            gameManger.ResetScene();             
        }

        else if(collision.gameObject.tag == "Platform")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject, 0.2f);
        }
    }

    public void CreateExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    IEnumerator WaitBeforeResetingTheLevel()
    {
        yield return new WaitForSeconds(1f);
    }
}
