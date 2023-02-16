using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_WaveAttacker : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;

    [Header("references")]
    [SerializeField] GameObject pistolBullet;
    [SerializeField] Transform[] firePoint;
    private EnemyAnimator enemyAnimator;
    private EnemyActivation enemyActivation;

    private void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyActivation = GetComponent<EnemyActivation>();
        enemyActivation.isEnemyReadyToShoot = true;
    }

    private void Update()
    {
        if (enemyActivation.isThereLineOfSightAndInRange && enemyActivation.isEnemyReadyToShoot)
        {
            enemyActivation.isThereLineOfSightAndInRange = false;
            enemyActivation.isEnemyReadyToShoot = false;
            enemyAnimator.StartAttackAnimation();
        }
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
        enemyActivation.isEnemyReadyToShoot = true;
    }
}
