using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanic : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int maxEnergy;
    public static int currentEnergy;

    [Header("Shotgun")]
    [SerializeField] private float recoilForce;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float speedOfBullets;
    [SerializeField] private float timeOnScreenBullets;
    [SerializeField] private int shotgunEnergyCost;

    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    [SerializeField] Transform gunPivot;

    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject shootgunBullet;

    [Header("MeleeAttack")]
    [SerializeField] float DashForce;
    [SerializeField] float durationOfDash;
    [SerializeField] float timeBetweenDashes;

    //Shotgun private variable
    private bool canShoot;
    private bool usingShotgun;

    //Dash private variable
    private bool canMelee;

    [Header("References")]
    [SerializeField] ResourceDisplayer energyDisplayer;
    [SerializeField] ResourceDisplayer weaponNameDisplayer;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rb2d;
    private float rotateOverTime;
    private float rotationSpeed;

    void Start()
    {
        currentEnergy = maxEnergy;

        usingShotgun = true;
        canShoot = true;
        canMelee = true;

        rb2d = GetComponent<Rigidbody2D>();
        energyDisplayer.ChangeResourceAmount(currentEnergy);
        weaponNameDisplayer.ChangeResourceName("Shotgun", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeActiveWeapon();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && canShoot && usingShotgun && currentEnergy >= shotgunEnergyCost)
        {
            ShootShotgun();       
        }         
        
        if(Input.GetKeyDown(KeyCode.Mouse1) && canMelee && !usingShotgun)
        {
            MeleeAttack();
        }

        //Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //RotateGun(mousePos);
    }

    public void RenewEnergy(int amountOfEnergy)
    {
        if(currentEnergy < maxEnergy)
        {
            currentEnergy += amountOfEnergy;
            energyDisplayer.ChangeResourceAmount(currentEnergy);
        }

    }

    //Main functionalites
    void ShootShotgun()
    {
        currentEnergy -= shotgunEnergyCost;
        energyDisplayer.ChangeResourceAmount(currentEnergy);
        GameObject bullet1 = Instantiate(shootgunBullet, firePoint1.position, firePoint1.rotation);
        GameObject bullet2 = Instantiate(shootgunBullet, firePoint2.position, firePoint1.rotation);
        GameObject bullet3 = Instantiate(shootgunBullet, firePoint3.position, firePoint1.rotation);
        ShotgunRecoil();
        canShoot = false;

        StartCoroutine(ShootingCooldown());
    }

    void MeleeAttack()
    {
        spriteRenderer.color = Color.red;
        gameObject.tag = "Bullet";
        capsuleCollider.enabled = true;
        rb2d.AddForce(gunPivot.transform.right * DashForce, ForceMode2D.Impulse);
        canMelee = false;

        StartCoroutine(DashCooldown());
        StartCoroutine(DashDuration());
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
            Debug.Log("isShotgunActive" + usingShotgun);
            weaponNameDisplayer.ChangeResourceName("Sword", Color.red);
        }

        else
        {
            usingShotgun = true;
            Debug.Log("isShotgunActive" + usingShotgun);
            weaponNameDisplayer.ChangeResourceName("Shotgun", Color.green);
        }
    }

    //void RotateGun(Vector3 lookPoint/*, bool allowRotationOverTime*/)
    //{
    //    Vector3 distanceVector = lookPoint - gunPivot.position;

    //    float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
    //    //if (rotateOverTime && allowRotationOverTime)
    //    {
    //        gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    //    }
    //    //else
    //    //{
    //    //    gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    //}
    //}

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

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(durationOfDash);
        capsuleCollider.enabled = false;
        gameObject.tag = "Player";
        spriteRenderer.color = Color.white;
    }
}
