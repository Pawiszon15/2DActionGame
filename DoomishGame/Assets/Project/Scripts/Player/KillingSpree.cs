using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class KillingSpree : MonoBehaviour
{
    [Header("Bonus speed properties")]
    [SerializeField] float additionalBonusSpeed;
    [SerializeField] int howManyEnemiesNeedToKill;
    [SerializeField] float restTimePerKill;
    [SerializeField] float bonusSpeedDuration;

    [Header("Hidden information")]
    public int howManyEnemiesAreKilled;
    public bool isBonusActive;
    private PlayerMovement playerMovement;
    private IEnumerator corutinePerKill;
    private float timeLeftInBonus;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        corutinePerKill = CutdownPerKill();
    }

    private void Update()
    {
        if(isBonusActive)
        {
            timeLeftInBonus -= Time.deltaTime;

            if(timeLeftInBonus < 0 && isBonusActive)
            {
                //turn of bonus
                isBonusActive = false;
                timeLeftInBonus = 0;
                howManyEnemiesAreKilled = 0;
                playerMovement.DeactivateBonusSpeed();
            }
        }


    }

    public void AddEnemyToKillingManager()
    {
        howManyEnemiesAreKilled++;

        //Prolong the bonus
        if (isBonusActive)
        {
            timeLeftInBonus = bonusSpeedDuration;
            Debug.Log("what to do if bonus activate");

        }

        else
        {
            //Try to activate bonus
            if (howManyEnemiesAreKilled >= howManyEnemiesNeedToKill)
            {
                isBonusActive = true;
                timeLeftInBonus = bonusSpeedDuration;
                playerMovement.ActivateBonusSpeed(additionalBonusSpeed);
                StopCoroutine(corutinePerKill);
            }

            else
            {
                StartCoroutine(corutinePerKill);
            }
        }
    }
    IEnumerator CutdownPerKill()
    {
        yield return new WaitForSeconds(restTimePerKill);
        howManyEnemiesAreKilled--;
    }

 
}
