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

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void TryToAcitvateBonus()
    {
        if(howManyEnemiesAreKilled >= howManyEnemiesNeedToKill)
        {
            StartCoroutine(CutdownBonus());
        }
    }

    public void AddEnemyToKillingManager()
    {
        CutdownPerKill();
    }

    void DecideToWhatActivate()
    {
        if(isBonusActive)
        {
            StopCoroutine(CutdownBonus());
            StartCoroutine(CutdownBonus());
        }

        else
        {
            TryToAcitvateBonus();
        }
    }

    IEnumerator CutdownPerKill()
    {
        howManyEnemiesAreKilled++;
        DecideToWhatActivate();
        yield return new WaitForSeconds(restTimePerKill);
        howManyEnemiesAreKilled--;
    }

    IEnumerator CutdownBonus()
    {
        playerMovement.ActivateBonusSpeed(additionalBonusSpeed);
        yield return new WaitForSeconds(bonusSpeedDuration);
        playerMovement.DeactivateBonusSpeed();
    }
}
