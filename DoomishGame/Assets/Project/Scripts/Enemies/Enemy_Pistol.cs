using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy_Pistol : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;
 
    [Header("references")]
    [SerializeField] GameObject pistolBullet;
    [SerializeField] Transform firePoint;
    private EnemyAnimator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        StartCoroutine(WaitForAnotherShot());
    }

    private void StartAttackAnimation()
    {
        enemyAnimator.isAttacking = true;
        enemyAnimator.isIdling = false;
    }

    private void Shot()
    {
        Instantiate(pistolBullet, firePoint.position, firePoint.rotation);
        StartCoroutine(WaitForAnotherShot());
    }

    IEnumerator WaitForAnotherShot()
    {
        enemyAnimator.isIdling = true;
        yield return new WaitForSeconds(timeBetweenShots);
        StartAttackAnimation();
    }
}
