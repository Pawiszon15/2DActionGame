using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WaveAttacker : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;

    [Header("references")]
    [SerializeField] GameObject pistolBullet;
    [SerializeField] Transform[] firePoint;

    private void Start()
    {
        StartCoroutine(WaitForAnotherShot());
    }

    private void Shot()
    {
        for(int i = 0; i < firePoint.Length; i++)
        {
            Instantiate(pistolBullet, firePoint[i].position, firePoint[i].rotation);
        }
        StartCoroutine(WaitForAnotherShot());
    }

    IEnumerator WaitForAnotherShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        Shot();
    }
}
