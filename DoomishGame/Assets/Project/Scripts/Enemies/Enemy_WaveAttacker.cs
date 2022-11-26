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
        for(int i = 0; i < firePoint.Length; i++)
        {
            Instantiate(pistolBullet, firePoint[i].position, firePoint[i].rotation);
        }

        StartCoroutine(WaitForAnotherShot());
    }

    IEnumerator WaitForAnotherShot()
    {
        enemyAnimator.isIdling = true;
        yield return new WaitForSeconds(timeBetweenShots);
        StartAttackAnimation();
    }
}
