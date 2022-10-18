using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] float speedMultiplayerByGate;

    private bool canPierceEnemies;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }

        else
        {
            if(!canPierceEnemies)
            {
                Destroy(gameObject);
            }
        }

    }

    public void BoostByGate()
    {
        rigidbody2d.velocity = rigidbody2d.velocity * speedMultiplayerByGate;
        canPierceEnemies = true;
    }
}
