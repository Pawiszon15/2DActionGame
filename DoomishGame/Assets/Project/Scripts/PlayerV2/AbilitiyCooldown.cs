using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiyCooldown : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int maxNumberOfUses;
    [SerializeField] int cooldown;
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
        CheckAvaialibity();
        StartCoroutine(startAbilityCooldown());
    }

    private void CheckAvaialibity()
    {
        if(numberOfUses <= 0)
        {
            abilityUI.PlaceHolderMakeUnavaialable();
        }
    }

    private IEnumerator startAbilityCooldown()
    {
        abilityUI.PlaceHolderStartCooldown();
        yield return new WaitForSeconds(cooldown);
        if(++numberOfUses !> maxNumberOfUses)
        {
            ++numberOfUses;
        }
    }
}
