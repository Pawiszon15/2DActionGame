using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSlamWithBonus : MonoBehaviour
{
    [Header("Charge properties")]
    [SerializeField] float chargeDuration;

    [Header("Ground slam properties")]
    [SerializeField] float groundSlamSpeed;

    [Header("References")]
    [SerializeField] Transform pivotPoint;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask layersToCheck;

    [HideInInspector] public bool isGroundSlammingWithBonus = false;
    PlayerMovement playerMovement;
    PlayerBlinkingAbility playerBlinkingAbility;
    Rigidbody2D rb;

    private void Awake()
    {
        playerBlinkingAbility = GetComponent<PlayerBlinkingAbility>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && !playerMovement.isGrounded && !isGroundSlammingWithBonus)
        {
            StartCoroutine(ChargeAction());
        }

        if (playerMovement.IsDashing)
        {
            isGroundSlammingWithBonus = false;
        }

        else if(playerMovement.IsJumping)
        {
            isGroundSlammingWithBonus = false;
        }

        else if(playerBlinkingAbility.afterBlink)
        {
            isGroundSlammingWithBonus = false;
        }

        //Additional conditions are needed, when we do some action to break ground slam action
        else if (isGroundSlammingWithBonus && playerMovement.isGrounded)
        {
            KillNerbayEnemies();
            isGroundSlammingWithBonus = false;
        }
    }

    IEnumerator ChargeAction()
    {
        isGroundSlammingWithBonus = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(chargeDuration);
        GSAction();
    }

    void GSAction()
    {
        rb.velocity = new Vector2(0, -groundSlamSpeed);
    }

    private void KillNerbayEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(pivotPoint.transform.position, boxSize, 0f, layersToCheck);

        foreach(Collider2D enemy in enemies)
        {
            if(enemy.gameObject.TryGetComponent(out BasicEnemy basicEnemy))
            {
                basicEnemy.StartDeathAnimation();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pivotPoint.transform.position, boxSize);
    }
}
