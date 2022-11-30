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
    private bool isPerformingMelee;
    //private bool isPlayerHitable;
    public bool meleeAvaialable;

    [Header("References")]
    [SerializeField] GameObject shield;
    private BasicEnemy basicEnemy;
    private GameObject player;
    private PlayerMovement characterMovement;
    private float defaultWaitingTime;
    private EnemyAnimator enemyAnimator;

    private void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        player = FindObjectOfType<Player>().gameObject;
        characterMovement = player.GetComponent<PlayerMovement>();
        meleeAvaialable = true;
        isPerformingMelee = false;
        //isPlayerHitable = false;
        defaultWaitingTime = 0.1f;
        movementDirection = Vector2.right;
        StartCoroutine(WaitBeforeNextAction(defaultWaitingTime));
    }

    private void Update()
    {
        if(!isPerformingMelee)
        {
            gameObject.transform.position += movementDirection * Time.deltaTime * walkingSpeed;
        }
    }

    private void UpdateBehaviour()
    {
        CheckWhereIsPlayer();
        CheckWhetherPlayerIsHitable();
        StartCoroutine(WaitBeforeNextAction(defaultWaitingTime));
        if (basicEnemy == null)
        {
            Destroy(gameObject);
        }
    }
    private void CheckWhetherPlayerIsHitable()
    {
        bool isPlayerGrounded = false;
        isPlayerGrounded = characterMovement.isGrounded;
        
        if (isPlayerGrounded && Vector3.Distance(transform.position, player.transform.position) <= chargeDistanceTrigger)
        {
            //isPlayerHitable = true;

            if(meleeAvaialable)
            {
                ProtectorMeleeHit();
            }
        }

        else
        {
            //isPlayerHitable = false;
        }

    }

    private void CheckWhereIsPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            movementDirection = Vector2.right;
            if(!isPerformingMelee)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                basicEnemy.FlipToRight(true);
            }
        }

        else
        {
            movementDirection = -Vector2.right;
            if(!isPerformingMelee)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                basicEnemy.FlipToRight(false);
            }
        }
    }

    private void ProtectorMeleeHit()
    {
        isPerformingMelee = true;
        meleeAvaialable = false;
        enemyAnimator.isAttacking = true;
        enemyAnimator.isMoving = false;
        StartCoroutine(AbilityCooldown(chargeCooldown));
    }

    private void StopPerformingMelee()
    {
        isPerformingMelee = false;
        enemyAnimator.isMoving = true;
    }

    IEnumerator WaitBeforeNextAction(float waitingTime)
    {   
        yield return new WaitForSeconds(waitingTime);
        UpdateBehaviour();
    }

    //IEnumerator ChargeDuration()
    //{
    //    yield return new WaitForSeconds(chargeDurtaion);
    //    isCharging = false;
    //    StartCoroutine(AbilityCooldown(chargeCooldown));
    //}

    IEnumerator AbilityCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        meleeAvaialable = true;
    }
}
