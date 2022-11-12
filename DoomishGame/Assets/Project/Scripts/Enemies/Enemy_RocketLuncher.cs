using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RocketLuncher : MonoBehaviour
{
    [SerializeField] float timeBetweenShoots;
    [SerializeField] Transform rocketSpawnPoint;
    [SerializeField] GameObject rocket;

    private GameObject spawnedRocket = null;
    void Start()
    {
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    void SpawnRocket()
    {
       spawnedRocket = Instantiate(rocket, rocketSpawnPoint.position, rocket.transform.rotation);
    }

    IEnumerator TimeBetweenRocketSpawn()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        if(spawnedRocket == null)
        {
            SpawnRocket();
        }
        StartCoroutine(TimeBetweenRocketSpawn());
    }
}
