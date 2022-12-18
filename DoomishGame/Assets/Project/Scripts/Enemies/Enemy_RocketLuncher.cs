using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RocketLuncher : MonoBehaviour
{
    [SerializeField] float timeBetweenShoots;
    [SerializeField] Transform rocketSpawnPoint;
    [SerializeField] GameObject rocket;

    private EnemyActivation enemyActivation;
    private EnemyAnimator enemyAnimator;
    private GameObject spawnedRocket = null;
    void Start()
    {
        enemyActivation = GetComponent<EnemyActivation>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyActivation.isEnemyReadyToShoot = true;
    }
    private void Update()
    {
        if(enemyActivation.isThereLineOfSightAndInRange && enemyActivation.isEnemyReadyToShoot)
        {
            enemyActivation.isThereLineOfSightAndInRange = false;
            enemyActivation.isEnemyReadyToShoot = false;
            enemyAnimator.StartAttackAnimation();
        }
    }

    private void SpawnRocket()
    {
        spawnedRocket = Instantiate(rocket, rocketSpawnPoint.position, rocket.transform.rotation);
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    IEnumerator TimeBetweenRocketSpawn()
    {
        enemyAnimator.StartIdling();
        yield return new WaitForSeconds(timeBetweenShoots);
        if(spawnedRocket == null)
        {
            enemyActivation.isEnemyReadyToShoot = true;
        }

        else
        {
            StartCoroutine(TimeBetweenRocketSpawn());
            enemyAnimator.isIdling = true;
        }
    }
}
