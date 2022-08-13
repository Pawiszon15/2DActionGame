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

    private Rigidbody2D rigidbody2d;

    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip shootSound;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        canShoot = true;
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
        
    }

    IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }

}
