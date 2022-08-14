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
    
    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject shootgunBullet;

    public Vector2 playerVelocity;
    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        bulletSpeed = 100f;
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
        GameObject bullet = Instantiate(shootgunBullet, firePoint.position, firePoint.rotation);
    }
/*
    IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }*/
}
