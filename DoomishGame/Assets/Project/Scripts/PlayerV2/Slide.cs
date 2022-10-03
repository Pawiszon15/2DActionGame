using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("Slide properties")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float accelerationValue;

    [Header("Extra properties")]
    [SerializeField] float invTime;
    [SerializeField] float howMuchItChangePlayerSize;
    

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float playerMaxSpeed;

    private ItemSwaper itemSwaper;
    private ToolCooldown toolCooldown;
    private Vector2 maxSpeed;

    float playerInput = 0f;
    private bool invTimeActive = false;
    private bool dashTimeActive = false;

    CharacterMovement CharacterMovement;

    // Start is called before the first frame update
    void Start()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();
        toolCooldown = GetComponent<ToolCooldown>();
        CharacterMovement = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 slideVector = Vector2.zero;
        bool isGroundedLocally = CharacterMovement.isGrounded;         

        if (Input.GetKeyDown(KeyCode.Mouse1) && toolCooldown.rightMouseUse > 0 && isGroundedLocally)
        {
            StartCoroutine(SlideDash());
        }
    
        if(invTimeActive)
        {
            slideVector = new Vector2(playerInput * dashSpeed, playerRb.velocity.y);
            playerRb.velocity = Vector2.Lerp(playerRb.velocity, slideVector, accelerationValue);
        }

        if (dashTimeActive)
        {
            maxSpeed = new Vector2(playerInput * playerMaxSpeed, playerRb.velocity.y);
            playerRb.velocity = Vector2.Lerp(playerRb.velocity, maxSpeed, accelerationValue);
        }
    }

    IEnumerator SlideDash()
    {
        playerInput = Input.GetAxis("Horizontal");

        CharacterMovement.StartJumpBoost(2 * dashTime);
        player.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        invTimeActive = true;
        yield return new WaitForSeconds(invTime);
        invTimeActive = false;

        yield return new WaitForEndOfFrame();

        dashTimeActive = true;
        yield return new WaitForSeconds(dashTime - invTime);
        dashTimeActive = false;

        player.transform.localScale = new Vector3(1f, 1f, 1f);
        --toolCooldown.rightMouseUse;
        itemSwaper.TryToStartCooldown();
    }
}
