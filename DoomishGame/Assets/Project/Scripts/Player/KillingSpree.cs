using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private IEnumerator corutineBonus;
    private IEnumerator corutinePerKill;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        corutineBonus = CutdownBonus();
        corutinePerKill = CutdownPerKill();
    }

    public void AddEnemyToKillingManager()
    {
        howManyEnemiesAreKilled++;

        if (isBonusActive)
        {
            Debug.Log("what to do if bonus activate");
            StartCoroutine(WaitForLittleMoment());
        }

        else
        {
            if (howManyEnemiesAreKilled >= howManyEnemiesNeedToKill)
            {
                isBonusActive = true;
                StopCoroutine(corutinePerKill);
                StartCoroutine(corutineBonus);
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

    IEnumerator CutdownBonus()
    {
        Debug.Log("Begin");
        //playerMovement.ActivateBonusSpeed(additionalBonusSpeed);
        yield return new WaitForSeconds(bonusSpeedDuration);
        Debug.Log("End");
        isBonusActive = false;
        //playerMovement.DeactivateBonusSpeed();
    }

    IEnumerator WaitForLittleMoment()
    {
        //Why this doesn't work???
        StopCoroutine(corutineBonus);
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(corutineBonus);
    }
}
