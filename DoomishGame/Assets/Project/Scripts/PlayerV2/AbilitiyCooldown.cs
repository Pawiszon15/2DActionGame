using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiyCooldown : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int maxNumberOfUses;
    [SerializeField] float cooldown;
    [HideInInspector] public int numberOfUses;

    [Header("References")]
    [SerializeField] AbilityCooldownUI abilityUI;

    // Start is called before the first frame update
    void Start()
    {
        numberOfUses = maxNumberOfUses;        
    }

    public void UseAbility()
    {
        --numberOfUses;
        //CheckAvaialibity();
        StartCoroutine(StartAbilityCooldown());
    }

    private void CheckAvaialibity()
    {
        if(numberOfUses <= 0)
        {
            abilityUI.PlaceHolderMakeUnavaialable();
        }
    }

    private IEnumerator StartAbilityCooldown()
    {
        //abilityUI.PlaceHolderStartCooldown();
        yield return new WaitForSeconds(cooldown);
        if(++numberOfUses !> maxNumberOfUses)
        {
            ++numberOfUses;
        }
    }
}
