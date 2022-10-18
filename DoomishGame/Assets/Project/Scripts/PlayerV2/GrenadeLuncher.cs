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

    private void Awake()
    {
        toolCooldown = GetComponent<ToolCooldown>();
        itemSwaper = FindObjectOfType<ItemSwaper>();  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && toolCooldown.leftMouseUse > 0)
        {
            ShootGrenade();
        }
    }

    private void ShootGrenade()
    {
        --toolCooldown.leftMouseUse;  
        Instantiate(grenade, firePoint.transform.position, firePoint.rotation);
        itemSwaper.TryToStartCooldown();
    }
}
