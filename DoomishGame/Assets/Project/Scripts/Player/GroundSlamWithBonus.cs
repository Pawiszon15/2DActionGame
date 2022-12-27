using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSlamWithBonus : MonoBehaviour
{
    [Header("Charge properties")]
    [SerializeField] float chargeDuration;

    [Header("Ground slam properties")]
    [SerializeField] float groundSlamSpeed;


    [HideInInspector] public bool isGroundSlammingWithBonus = false;
    PlayerMovement playerMovement;
    Rigidbody2D rb;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && !playerMovement.isGrounded && !isGroundSlammingWithBonus)
        {
            StartCoroutine(ChargeAction());
        }

        //Additional conditions are needed, when we do some action to break ground slam action
        if(isGroundSlammingWithBonus && playerMovement.isGrounded)
        {
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
}
