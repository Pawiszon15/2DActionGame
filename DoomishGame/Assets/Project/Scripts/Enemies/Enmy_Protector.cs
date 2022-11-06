using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enmy_Protector : MonoBehaviour
{
    [Header("Protector properties")]
    [SerializeField] float walkingSpeed;
    [SerializeField] float activationRange;
    private Vector3 movementDirection;

    [Header("Charge properties")]
    [SerializeField] float chargeSpeed;
    [SerializeField] float chargeDurtaion;
    [SerializeField] float chargeCooldown;
    [SerializeField] float chargeDistanceTrigger;
    private Vector3 chargeDireciton;
    private bool isCharging;
    private bool isPlayerChargable;
    public bool isChargeAvaialable;

    [Header("References")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject player;
    private CharacterMovement characterMovement;
    private float defaultWaitingTime;

    private void Start()
    {
        characterMovement = player.GetComponent<CharacterMovement>();
        isChargeAvaialable = true;
        isCharging = false;
        isPlayerChargable = false;
        defaultWaitingTime = 0.25f;
        movementDirection = Vector2.right;
        StartCoroutine(WaitBeforeNextAction(defaultWaitingTime));
    }

    private void Update()
    {
        if(!isCharging)
        {
            gameObject.transform.position += movementDirection * Time.deltaTime * walkingSpeed;
        }

        else if(isCharging)
        {
            gameObject.transform.position += chargeDireciton * Time.deltaTime * chargeSpeed;
        }

        if(isChargeAvaialable)
        {
            CheckWhetherPlayerIsChargable();
            if(isPlayerChargable)
            {
                ProtectorCharge();
            }
        }
    }

    private void UpdateBehaviour()
    {
        CheckWhereIsPlayer();
        StartCoroutine(WaitBeforeNextAction(defaultWaitingTime));
    }
    private void CheckWhetherPlayerIsChargable()
    {
        bool isPlayerGrounded = false;
        isPlayerGrounded = characterMovement.isGrounded;
        
        if (isPlayerGrounded && Vector3.Distance(transform.position, player.transform.position) <= chargeDistanceTrigger)
        {
            isPlayerChargable = true;
        }
        
        else
        {
            isPlayerChargable = false;
        }

    }

    private void CheckWhereIsPlayer()
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            movementDirection = Vector2.right;
            if(!isCharging)
            {
                shield.transform.localPosition = new Vector3(Mathf.Abs(shield.transform.localPosition.x), shield.transform.localPosition.y, shield.transform.localPosition.z);
            }
        }

        else
        {
            movementDirection = -Vector2.right;
            if(!isCharging)
            {
                shield.transform.localPosition = new Vector3(-(Mathf.Abs(shield.transform.localPosition.x)), shield.transform.localPosition.y, shield.transform.localPosition.z);
            }
        }
    }

    private void ProtectorCharge()
    {
        isChargeAvaialable = false;
        CheckWhereIsPlayer();
        chargeDireciton = movementDirection;
        isCharging = true;
        StartCoroutine(ChargeDuration());
    }

    IEnumerator WaitBeforeNextAction(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        UpdateBehaviour();
    }

    IEnumerator ChargeDuration()
    {
        yield return new WaitForSeconds(chargeDurtaion);
        isCharging = false;
        StartCoroutine(AbilityCooldown(chargeCooldown));
    }

    IEnumerator AbilityCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        isChargeAvaialable = true;
    }
}
