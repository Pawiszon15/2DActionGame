using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shielder : MonoBehaviour
{
    [Header("Shield ability")]
    [SerializeField] GameObject shield;
    [SerializeField] float shiledCooldown;
    private bool isShieldAvaialable;

    [Header("Close attack")]
    [SerializeField] GameObject meleeBullet;
    [SerializeField] float meleeRangeTrigger;
    [SerializeField] float meleeCooldown;
    private bool isMeleeAttackAvaiable;
    private bool isPlayerInMeleeRange;

    [Header("References")]
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject playerTrackingPoint;
    [SerializeField] float defaultWaitTime;
    private bool bullshitBool;


 

    // Start is called before the first frame update
    void Start()
    {
        isShieldAvaialable = true;
        isMeleeAttackAvaiable = true;
        StartCoroutine(AbilityCooldown(defaultWaitTime, bullshitBool));
    }


    private void SpawnShield(Vector2 shieldPosition, Vector3 shieldRotation)
    {
        Instantiate(shield, shieldPosition, Quaternion.Euler(shieldRotation));
        StartCoroutine(AbilityCooldown(shiledCooldown, isShieldAvaialable));
    }

    private void CaluclateShieldSpawnProperties()
    {
        Vector2 forwardPosOfPlayer = Vector2.zero;
        Vector3 forwardRotOfPlayer = new Vector3(0, 0, 90f);

        forwardPosOfPlayer = playerTrackingPoint.transform.position;
        //if(playerTrackingPoint.transform.position.y > 2)
        //{
        //    forwardRotOfPlayer = new Vector3(0, 0, 0f);
        //}

        SpawnShield(forwardPosOfPlayer, forwardRotOfPlayer);
    }

    private void MeleeAttack()
    {
        Instantiate(meleeBullet, firePoint.transform.position, firePoint.transform.rotation);
        StartCoroutine(AbilityCooldown(meleeCooldown, isMeleeAttackAvaiable));
    }

    private void TryToDoSth()
    {
        float lineToPlayer;
        lineToPlayer = Vector3.Distance(gameObject.transform.position, playerTrackingPoint.transform.position);
        
        if (lineToPlayer <= meleeRangeTrigger)
        {
            isPlayerInMeleeRange = true;
        }

        if(isShieldAvaialable && !isPlayerInMeleeRange)
        {
            CaluclateShieldSpawnProperties();
        }

        else if(isMeleeAttackAvaiable && isPlayerInMeleeRange) //ut;s needed to check line of sight
        {
            MeleeAttack();
        }

        else
        {
            StartCoroutine(AbilityCooldown(defaultWaitTime, bullshitBool));
        }
    }

    IEnumerator AbilityCooldown(float cooldownTime, bool isAbilitiyAvailable)
    {
        isPlayerInMeleeRange = false;
        isAbilitiyAvailable = false;
        yield return new WaitForSeconds(cooldownTime);
        isAbilitiyAvailable = true;
        TryToDoSth();
    }
}
