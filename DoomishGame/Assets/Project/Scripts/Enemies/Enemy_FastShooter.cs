using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FastShooter : MonoBehaviour
{
    [Header("Shooting properties")]
    [SerializeField] float timeBetweenShots;
    [SerializeField] float cooldownBetweenSeries;

    [Header("references")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForAnotherShot());
    }

    private void Shot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    IEnumerator ConseciutiveShots()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(timeBetweenShots);
        Shot();
        yield return new WaitForSeconds(timeBetweenShots);
        Shot();
        yield return new WaitForSeconds(timeBetweenShots);
        Shot();
        StartCoroutine(WaitForAnotherShot());
    }

    IEnumerator WaitForAnotherShot()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(cooldownBetweenSeries);
        StartCoroutine(ConseciutiveShots());
    }
}
