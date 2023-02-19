using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] bool shouldCreateAnotherBullet;
    [SerializeField] bool shouldCreatExplosion;

    [Header("Delay properties")]
    [SerializeField] bool shouldaHaveDelay;
    [SerializeField] float delayDuration;

    [Header("References")]
    [SerializeField] LayerMask layers;
    [SerializeField] GameObject bulletToCreate;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject counterParticle;
    [SerializeField] GameObject destrBulletParticle;

    private Rigidbody2D rigidbody2d;
    private GameManger manger;
    private Player player;
    private bool wasDeflected = false;
    private CinemaShakes cinemaShakes;
    private float timeAfterSpawn;

    private void Awake()
    {
        manger = FindObjectOfType<GameManger>();
        player = FindObjectOfType<Player>();
        cinemaShakes = FindObjectOfType<CinemaShakes>();
    }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.right * speed;
    }

    private void Update()
    {
        timeAfterSpawn += Time.deltaTime;    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bullet hit sth");
        if (!shouldCreateAnotherBullet && !shouldaHaveDelay && collision.gameObject.tag == "Platform")
        {
            if (destrBulletParticle != null)
            {
                Instantiate(destrBulletParticle, transform.position, Quaternion.identity);
            }

            cinemaShakes.CameraShakeStart(0.8f, 0.2f);
            Destroy(this.gameObject);
        }

        if (shouldCreateAnotherBullet)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, firePoint.right, 100f, layers);

            if (hit.collider.gameObject.TryGetComponent(out PlayerMovement isthereplayer) && timeAfterSpawn > 0.2f)
            {
                ExtraShot();
                Destroy(this.gameObject);
            }
        }

        else if (shouldaHaveDelay)
        {
            StartCoroutine(DelayBeforeExplosion());
        }

        else if (shouldCreatExplosion)
        {
            cinemaShakes.CameraShakeStart(2f, 0.15f);
            ExtraShot();
        }

    }

    private void ExtraShot()
    {
        Instantiate(bulletToCreate, firePoint.position, firePoint.rotation);
    }

    public void DeflectBullet(Vector2 playerInput)
    {
        if (!wasDeflected)
        {
            wasDeflected = true;
            GameObject particleObject = Instantiate(counterParticle, transform.position, Quaternion.identity);
            Destroy(particleObject,0.25f);
            //cinemaShakes.CameraShakeStart(1f, 0.1f);
            gameObject.tag = "PlayerBullet";
            gameObject.layer = 0;
            //rigidbody2d.velocity = new Vector2(-rigidbody2d.velocity.x, -rigidbody2d.velocity.y);
            rigidbody2d.velocity = playerInput * (2 * speed);
        }
    }

    private IEnumerator DelayBeforeExplosion()
    {
        rigidbody2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(delayDuration);
        ExtraShot();
        Destroy(gameObject);
    }    
}
