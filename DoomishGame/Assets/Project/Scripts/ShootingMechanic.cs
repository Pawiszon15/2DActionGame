using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanic : MonoBehaviour
{
    [SerializeField] private float recoilForce;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float speedOfBullets;
    [SerializeField] private float timeOnScreenBullets;

    private bool canShoot;
    
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    [SerializeField] Transform gunPivot;


    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject shootgunBullet;

    public Vector2 playerVelocity;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && canShoot)
        {
            ShootShotgun();       
        }         
    }

    void ShootShotgun()
    {
        GameObject bullet1 = Instantiate(shootgunBullet, firePoint1.position, firePoint1.rotation);
        GameObject bullet2 = Instantiate(shootgunBullet, firePoint2.position, firePoint1.rotation);
        GameObject bullet3 = Instantiate(shootgunBullet, firePoint3.position, firePoint1.rotation);
        Recoil();
        canShoot = false;
        StartCoroutine(ShootingCooldown());
    }

    void Recoil()
    {
        //need direction
        Vector2 recoilDir = new Vector2(gunPivot.position.x, gunPivot.position.y);

        //need to add impluse force
        rb2d.AddForce(-gunPivot.transform.right * recoilForce, ForceMode2D.Impulse);
       
    }

    void BulletSpread()
    {
        //LATER
    }


    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }
}
