using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RocketLuncher : MonoBehaviour
{
    [SerializeField] float timeBetweenShoots;
    [SerializeField] Transform rocketSpawnPoint;
    [SerializeField] GameObject rocket;
    private EnemyAnimator enemyAnimator;

    private GameObject spawnedRocket = null;
    void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    void StartAttackAnimation()
    {
        enemyAnimator.isAttacking = true;
        enemyAnimator.isIdling = false;
    }

    private void SpawnRocket()
    {
        spawnedRocket = Instantiate(rocket, rocketSpawnPoint.position, rocket.transform.rotation);
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    IEnumerator TimeBetweenRocketSpawn()
    {
        enemyAnimator.isIdling = true;
        enemyAnimator.isAttacking = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        if(spawnedRocket == null)
        {
            StartAttackAnimation();
        }

        else
        {
            StartCoroutine(TimeBetweenRocketSpawn());
            enemyAnimator.isIdling = true;
        }
    }
}
