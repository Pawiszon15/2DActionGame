using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaverBullet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] float expadingSpeed;
    private float bulletScale = 0f;

    private Rigidbody2D rigidbody2d;
    private GameManger manger;

    private void Awake()
    {
        manger = FindObjectOfType<GameManger>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody2d.velocity = transform.right * speed;
    }

    private void Update()
    {
        bulletScale += Time.deltaTime * expadingSpeed;
        transform.localScale = new Vector2(transform.localScale.x, bulletScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manger.ResetScene();
        }

        else if (collision.gameObject.tag == "Platform")
        {
            Destroy(this.gameObject);
        }
    }
}
