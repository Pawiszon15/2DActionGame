using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiyCooldown : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] KeyCode keyCode;
    [SerializeField] int maxNumberOfUses;
    [SerializeField] float cooldown;
    [HideInInspector] public int numberOfUses;
    private bool isCooldown = false;

    [Header("References")]
    [SerializeField] Image abilityImage = null;
    

    // Start is called before the first frame update
    void Start()
    {
        numberOfUses = maxNumberOfUses;        
    }

    private void Update()
    {
        if(isCooldown && abilityImage != null)
        {
            abilityImage.fillAmount += 1/cooldown * Time.deltaTime;
        }
    }

    public void UseAbility()
    {
        --numberOfUses;
        if(abilityImage != null)
        {
            abilityImage.fillAmount = 0;
            isCooldown = true;
        }
        //CheckAvaialibity();
        StartCoroutine(StartAbilityCooldown());
    }

    //private void CheckAvaialibity()
    //{
    //    if(numberOfUses <= 0)
    //    {
    //        abilityUI.PlaceHolderMakeUnavaialable();
    //    }
    //}

    private IEnumerator StartAbilityCooldown()
    {
        //abilityUI.PlaceHolderStartCooldown();
        yield return new WaitForSeconds(cooldown);
        isCooldown = true;

        if(abilityImage != null)
        {
            abilityImage.fillAmount = 1;
        }

        if(++numberOfUses !> maxNumberOfUses)
        {
            ++numberOfUses;
        }
    }
}
