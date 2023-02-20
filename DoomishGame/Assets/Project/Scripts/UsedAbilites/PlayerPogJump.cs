using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPogJump : MonoBehaviour
{
    [Header("Pog jump")]
    [SerializeField] float pogJumpBufferTime;
    [SerializeField] float jumpPower;
    [SerializeField] float reloadTime;
    [SerializeField] LayerMask layers;
    [SerializeField] LayerMask forRaycast;
    [SerializeField] float rangeOfRaycast;
    [HideInInspector] public bool canPlayerPogJump;

    [Header("References")]
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Transform posOfPogJumpCollider;
    [SerializeField] Transform mousePos;
    [SerializeField] float rangeOfCircle;

    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    Rigidbody2D rb;

    [HideInInspector] public bool isPlayerTryingToPogJump = false;
    [HideInInspector] public bool makingPogJump = false;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && !playerMovement.IsDashing && playerMovement.LastOnGroundTime != 0
          && !playerMovement.isRolling && !playerMovement.isWallSliding && !playerMovement.IsWallJumping)
        {

            isPlayerTryingToPogJump = true;

            playerAnimator.StartPogJumpAnimation();
            StartCoroutine(PogCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PogableThings pogableThing))
        {
            if (isPlayerTryingToPogJump && canPlayerPogJump)
            {
                isPlayerTryingToPogJump = false;
                MakePogJump();
            }
        }
        
    }

    public void TurnOnCollision()
    {
        circleCollider2D.enabled = true;
    }

    public void TurnOffCollision()
    {
        circleCollider2D.enabled = false;
    }

    private void MakePogJump()
    {
        makingPogJump = true;
        TurnOffCollision();
    }

    IEnumerator PogCooldown()
    {
        yield return new WaitForSeconds(pogJumpBufferTime);
        canPlayerPogJump = true;
    }

}
