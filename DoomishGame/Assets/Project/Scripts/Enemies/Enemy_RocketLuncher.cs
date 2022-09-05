using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RocketLuncher : MonoBehaviour
{
    [SerializeField] float timeBetweenShoots;
    [SerializeField] Transform rocketSpawnPoint;
    [SerializeField] GameObject rocket;

    void Start()
    {
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    void SpawnRocket()
    {
        Instantiate(rocket, rocketSpawnPoint.position, Quaternion.identity);
        StartCoroutine(TimeBetweenRocketSpawn());
    }

    IEnumerator TimeBetweenRocketSpawn()
    {
        yield return new WaitForSeconds(timeBetweenShoots);
        SpawnRocket();
    }
}
