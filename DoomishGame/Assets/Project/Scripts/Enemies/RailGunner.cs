using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunner : MonoBehaviour
{
    [Header("Weapon properites")]
    [SerializeField] float timeBetweenShoots;
    [SerializeField] float timeOfRailgunShoot;
    [SerializeField] float timeOfShootPreparation;

    [Header("References")]
    [SerializeField] GameObject railLine;
    [SerializeField] GameObject player;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform weaponHolder;

    private Transform playerPos;
    private bool preaperingForShoot;
    private bool ongoingRailShoot;

   

    void Start()
    {
        preaperingForShoot = false;     
    }

    // Update is called once per frame
    void Update()
    {
        if(preaperingForShoot)
        {

        }

        if(!ongoingRailShoot)
        {
            RotateGun();
        }

        else if(ongoingRailShoot)
        {

        }

    }

    private void Shoot()
    {

    }

    private void RotateGun() 
    {
    
    }

    IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
    }

}
