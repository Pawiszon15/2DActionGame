using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy_Pistol : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;
    [SerializeField] bool isCarabine;

    [Header("references")]
    [SerializeField] GameObject pistolBullet;
    [SerializeField] Transform firePoint;
    
    private EnemyActivation enemyActivation;
    private EnemyAnimator enemyAnimator;

    private void Awake()
    {
        enemyActivation = GetComponent<EnemyActivation>();
        enemyAnimator = GetComponent<EnemyAnimator>();
    }

    //private void Start()
    //{
    //    StartCoroutine(RechargeAttackAbility());
    //}

    void Update()
    {
        if(enemyActivation.isThereLineOfSightAndInRange && enemyActivation.isEnemyReadyToShoot)
        {
            enemyActivation.isThereLineOfSightAndInRange = false;
            enemyActivation.isEnemyReadyToShoot = false;
            enemyAnimator.StartAttackAnimation();
        }
    }

    private void Shot()
    {
        Instantiate(pistolBullet, firePoint.position, firePoint.rotation);
        if (!isCarabine)
        {
            StartCoroutine(RechargeAttackAbility());
        }
    }

    private void StartRechargeAfterBurst()
    {
        StartCoroutine(RechargeAttackAbility());
    }

    IEnumerator RechargeAttackAbility()
    {
        enemyAnimator.StartIdling();
        yield return new WaitForSeconds(timeBetweenShots);
        enemyActivation.isEnemyReadyToShoot = true;
    }
}
