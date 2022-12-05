using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] bool shouldCreateAnotherBullet;

    [Header("References")]
    [SerializeField] GameObject bulletToCreate;
    [SerializeField] Transform firePoint;

    private Rigidbody2D rigidbody2d;
    private GameManger manger;
    private Player player;
    private bool wasDeflected = false;

    private void Awake()
    {
        manger = FindObjectOfType<GameManger>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.right * speed;
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
            if (shouldCreateAnotherBullet)
            {
                ExtraShot();
            }
        }
    }

    private void ExtraShot()
    {
        Instantiate(bulletToCreate, firePoint.position, firePoint.rotation);
    }

    public void DeflectBullet( Vector2 mouseDir)
    {
        if(!wasDeflected)
        {
            wasDeflected = true;
            Debug.Log("deflected bullet");
            gameObject.tag = "PlayerBullet";
            //rigidbody2d.velocity = new Vector2(-rigidbody2d.velocity.x, -rigidbody2d.velocity.y);
            rigidbody2d.velocity = mouseDir.normalized * speed;
        }
    }
}
