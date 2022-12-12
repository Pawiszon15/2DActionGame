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
    
    private EnemyActivation enemyActivation;
    private EnemyAnimator enemyAnimator;

    private void Start()
    {
        enemyActivation = GetComponent<EnemyActivation>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        StartCoroutine(RechargeAttackAbility());
    }

    private void Update()
    {
        if(enemyActivation.isThereLineOfSight && enemyActivation.isEnemyReadyToShoot)
        {
            enemyActivation.isThereLineOfSight = false;
            enemyActivation.isEnemyReadyToShoot = false;
            StartAttackAnimation();
        }
    }

    private void StartAttackAnimation()
    {
        enemyAnimator.isAttacking = true;
        enemyAnimator.isIdling = false;
    }

    private void Shot()
    {
        Instantiate(pistolBullet, firePoint.position, firePoint.rotation);
        StartCoroutine(RechargeAttackAbility());
    }

    IEnumerator RechargeAttackAbility()
    {
        enemyAnimator.isIdling = true;
        yield return new WaitForSeconds(timeBetweenShots);
        enemyActivation.isEnemyReadyToShoot = true;
    }
}
