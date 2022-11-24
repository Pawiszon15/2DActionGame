using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCarrier : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject swordPrefab;
    [SerializeField] float waitTime;

    [SerializeField] Transform firePoint;
    [SerializeField] Transform weaponPivot;
    private bool canDashToSword;

    //private void Start()
    //{
    //    StartCoroutine(CheckWhetherYouCanDashToSword());
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && playerData.dashAmount > 0)
        {
            SwordThrow();
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDashToSword)
        {
            LongDashToSword();
        }

    }

    private void SwordThrow()
    {
        Instantiate(swordPrefab, firePoint.position, weaponPivot.rotation);
        --playerData.dashAmount;
    }

    public void SwordPickup()
    {
        ++playerData.dashAmount;
    }

    private void LongDashToSword()
    {
        //how to write it
        // 1. Turn off collision and rise up Alpha
        // 2. Setup Dir of Movment and speed
        // 3. Move in this dir
    }

    IEnumerator CheckWhetherYouCanDashToSword()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
