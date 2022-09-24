using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLuncher : MonoBehaviour
{
    [Header("Grenade Luncher Properties")]
    [SerializeField] float cooldown;

    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject grenade;

    private ItemSwaper itemSwaper;
    private bool canShot = true;

    private void Awake()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();  
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canShot)
        {
            ShootGrenade();
        }
    }

    private void ShootGrenade()
    {
        canShot = false;
        Instantiate(grenade, firePoint.transform.position, firePoint.rotation);
        canShot = true;
        itemSwaper.StartCooldown(cooldown);
    }
}
