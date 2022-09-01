using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Enemy_Pistol : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;
 
    [Header("references")]
    [SerializeField] GameObject pistolBullet;
    [SerializeField] Transform firePoint;

    private void Start()
    {
        StartCoroutine(WaitForAnotherShot());
    }

    private void Shot()
    {
        Instantiate(pistolBullet, firePoint.position, firePoint.rotation);
        StartCoroutine(WaitForAnotherShot());
    }

    IEnumerator WaitForAnotherShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        Shot();
    }
}
