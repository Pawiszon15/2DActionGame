using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanic : MonoBehaviour
{
    [Header("Shotgun")]
    [SerializeField] private float recoilForce;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float speedOfBullets;
    [SerializeField] private float timeOnScreenBullets;

    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    [SerializeField] Transform gunPivot;

    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject shootgunBullet;
    [SerializeField] Canvas canvas;

    [Header("MeleeAttack")]
    [SerializeField] float DashForce;
    [SerializeField] float durationOfDash;
    [SerializeField] float timeBetweenDashes;

    //Shotgun private variable
    private bool canShoot;
    private bool usingShotgun;

    //Dash private variable
    private bool canMelee;
    private CircleCollider2D circleCollider;

    //General variables
    private Rigidbody2D rb2d;
    
    
    void Start()
    {
        
        usingShotgun = true;
        canShoot = true;
        canMelee = true;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeActiveWeapon();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && canShoot && usingShotgun)
        {
            ShootShotgun();       
        }         
        
        if(Input.GetKeyDown(KeyCode.Mouse1) && canMelee && !usingShotgun)
        {
            MeleeAttack();
        }    
    }



    //Main functionalites
    void ShootShotgun()
    {
        GameObject bullet1 = Instantiate(shootgunBullet, firePoint1.position, firePoint1.rotation);
        GameObject bullet2 = Instantiate(shootgunBullet, firePoint2.position, firePoint1.rotation);
        GameObject bullet3 = Instantiate(shootgunBullet, firePoint3.position, firePoint1.rotation);
        ShotgunRecoil();
        canShoot = false;

        StartCoroutine(ShootingCooldown());
    }

    void MeleeAttack()
    {
        rb2d.AddForce(gunPivot.transform.right * DashForce, ForceMode2D.Impulse);
        circleCollider.isTrigger = true;
        canMelee = false;

        StartCoroutine(DashCooldown());
    }



    //Supporting function to shotting and dash
    void ShotgunRecoil()
    {
        //Add impulse in opposite direction of shoot
        rb2d.AddForce(-gunPivot.transform.right * recoilForce, ForceMode2D.Impulse);
    }

    void BulletSpread()
    {
        //LATER
    }

    void ChangeActiveWeapon()
    {
        if (usingShotgun)
        {
            usingShotgun = false;
            canvas.enabled = false;
            Debug.Log("isShotgunActive" + usingShotgun);
        }

        else
        {
            usingShotgun = true;
            canvas.enabled = true;
            Debug.Log("isShotgunActive" + usingShotgun);
        }
    }



    //Couroutines
    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(timeBetweenDashes);
        canMelee = true;
    }    
}
