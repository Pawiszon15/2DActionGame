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

        if (!shouldCreateAnotherBullet && collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }

        if (shouldCreateAnotherBullet)
        {
            Destroy(this.gameObject, delayDuration + 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, firePoint.right, 100f, layers);
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.gameObject.TryGetComponent(out PlayerMovement isthereplayer))
            {
                ExtraShot();
            }
        }

        else if (shouldaHaveDelay)
        {
            StartCoroutine(DelayBeforeExplosion());
        }

        else if (shouldCreatExplosion)
        {
            ExtraShot();
        }

    }

    private void ExtraShot()
    {
        Instantiate(bulletToCreate, firePoint.position, firePoint.rotation);
    }

    public void DeflectBullet(Vector2 mouseDir)
    {
        if (!wasDeflected)
        {
            wasDeflected = true;
            gameObject.tag = "PlayerBullet";
            //rigidbody2d.velocity = new Vector2(-rigidbody2d.velocity.x, -rigidbody2d.velocity.y);
            rigidbody2d.velocity = mouseDir.normalized * (2 * speed);
        }
    }

    private IEnumerator DelayBeforeExplosion()
    {
        rigidbody2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(delayDuration);
        ExtraShot();
    }    
}
