using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLuncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject grenade;

    private ItemSwaper itemSwaper;
    private ToolCooldown toolCooldown;
    private GameObject grenadeCreated;

    private void Awake()
    {
        toolCooldown = GetComponent<ToolCooldown>();
        itemSwaper = FindObjectOfType<ItemSwaper>();  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && toolCooldown.leftMouseUse > 0)
        {
            ShootGrenade();
        }

        else if(Input.GetKeyDown(KeyCode.Mouse1) && toolCooldown.leftMouseUse == 0)
        {
            Grenade tempGrenade = grenadeCreated.GetComponent<Grenade>();
            tempGrenade.Explode();
        }
    }

    private void ShootGrenade()
    {
        --toolCooldown.leftMouseUse;  
        grenadeCreated = Instantiate(grenade, firePoint.transform.position, firePoint.rotation);
        itemSwaper.TryToStartCooldown();
    }
}
