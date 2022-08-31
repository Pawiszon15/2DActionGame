using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunner : MonoBehaviour
{
    [Header("Weapon properites")]
    [SerializeField] float timeBetweenShoots;
    [SerializeField] float timeOfRailgunShoot;
    [SerializeField] float timeOfShootPreparation;
    [SerializeField] float timeJustBeforeShot;

    [Header("References")]
    [SerializeField] GameObject railLine;
    [SerializeField] RotateEnemyWeapon rotatingWeapon;
    [SerializeField] SpriteRenderer LineSpriteRenderer;
    [SerializeField] Transform firePoint;

    private Transform playerPos;
    private bool preaperingForShoot;
    private bool ongoingRailShoot;


    void Start()
    {
        preaperingForShoot = false;
        StartCoroutine(PreaperingForTheShot());
    }

    // Update is called once per frame
    void Update()
    {
        if(preaperingForShoot)
        {

        }

        if(!ongoingRailShoot)
        {
            /*RotateGun();*/
        }

        else if(ongoingRailShoot)
        {

        }

    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        GameObject RailBullet = Instantiate(railLine, firePoint.position, firePoint.rotation);
        Destroy(RailBullet, timeOfRailgunShoot);
        StartCoroutine(TimeBetweenShoots());
    }

    IEnumerator PreaperingForTheShot()
    {
        rotatingWeapon.enabled = true;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeOfShootPreparation)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
            LineSpriteRenderer.material.color = newColor;
            yield return null;
        }

        rotatingWeapon.enabled = false; 
        yield return new WaitForSeconds(timeJustBeforeShot);
        Shoot();
    }

    IEnumerator TimeBetweenShoots()
    {
        yield return new WaitForSeconds(timeOfRailgunShoot);
        LineSpriteRenderer.material.color = new Color(1, 1, 1, 0f);
        yield return new WaitForSeconds(timeBetweenShoots);
        StartCoroutine(PreaperingForTheShot());
    }

}
