using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] float speedMultiplayerByGate;
    [HideInInspector] public bool isShiledBreaker = false;

    private bool canPierceEnemies = false;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canPierceEnemies = false;
        rigidbody2d.velocity = transform.right * speed;
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "EnemyShield")
        {
            Destroy(gameObject);
        }

        else if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyKilled");
            if (!canPierceEnemies)
            {
                Destroy(gameObject);
            }
        }

        else if(collision.gameObject.GetComponent<HomingRocket>())
        {
            Destroy(collision.gameObject);
            if(!canPierceEnemies)
            {
                Destroy(gameObject);
            }
        }

        else if(collision.gameObject.GetComponent<Grenade>())
        {
            BoostByGrenade();
            Destroy(collision.gameObject);
        }
    }

    public void BoostByGate()
    {
        rigidbody2d.velocity = rigidbody2d.velocity * speedMultiplayerByGate;
        canPierceEnemies = true;
    }

    private void BoostByGrenade()
    {
        isShiledBreaker = true;
        Debug.Log("extra propertiy for bullet thanks to grenade");
    }
}
