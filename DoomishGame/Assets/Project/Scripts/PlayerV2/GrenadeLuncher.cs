using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLuncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject grenade;

    private AbilitiyCooldown abilityCooldown;
    private GameObject grenadeCreated;

    private void Awake()
    {
        abilityCooldown = GetComponent<AbilitiyCooldown>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && abilityCooldown.numberOfUses > 0)
        {
            ShootGrenade();
        }

        else if(Input.GetKeyDown(KeyCode.Mouse1) && abilityCooldown.numberOfUses == 0)
        {
            Grenade tempGrenade = grenadeCreated.GetComponent<Grenade>();
            tempGrenade.Explode();
        }
    }

    private void ShootGrenade()
    {
        abilityCooldown.UseAbility();  
        grenadeCreated = Instantiate(grenade, firePoint.transform.position, firePoint.rotation);
    }
}
