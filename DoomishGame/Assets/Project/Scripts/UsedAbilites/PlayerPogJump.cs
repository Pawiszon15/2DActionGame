using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPogJump : MonoBehaviour
{
    [Header("Pog jump")]
    [SerializeField] float jumpPower;
    [SerializeField] float reloadTime;
    [SerializeField] LayerMask layers;
    [SerializeField] LayerMask forRaycast;
    [SerializeField] float rangeOfRaycast;
    [HideInInspector] public bool pogJumpColliderActive;
    private bool pogJumpAvaialable = true;

    [Header("References")]
    [SerializeField] Transform PlayersFeet;
    [SerializeField] Transform posOfPogJumpCollider;
    [SerializeField] Transform mousePos;
    [SerializeField] float rangeOfCircle;

    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    Rigidbody2D rb;

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
        //if (playerMovement.isGrounded)
        //{
        //    pogJumpAvaialable = false;
        //}

        //else
        //{
        //    pogJumpAvaialable = true;
        //}

        if (Input.GetKeyDown(KeyCode.Space) && pogJumpAvaialable && !playerMovement.IsDashing && playerMovement.LastOnGroundTime != 0
          && !playerMovement.isRolling && !playerMovement.isWallSliding && !playerMovement.IsWallJumping && !playerMovement.recentlyReallyColseToWalls)     
        {
            if(!Physics2D.Raycast(new Vector2(PlayersFeet.position.x, PlayersFeet.position.y), -PlayersFeet.up, rangeOfRaycast, forRaycast))
            {

                Debug.Log(" is player grounded" + playerMovement.isGrounded);
                playerAnimator.StartPogJumpAnimation();
            }    

            else
            {
                Debug.Log("Toclose to the ground");
            }

        }

        if (pogJumpColliderActive)
        {
            CheckForCollision();
        }

        Debug.Log(playerMovement.recentlyReallyColseToWalls);
    }

    private void CheckForCollision()
    {
        Collider2D[] OverlapThings = Physics2D.OverlapCircleAll(posOfPogJumpCollider.position, rangeOfCircle, layers);

        foreach(Collider2D overlapThing in OverlapThings)
        {
            if(overlapThing.TryGetComponent(out EnemyBullet enemyBullet))
            {
                enemyBullet.DeflectBullet(mousePos.right);
                MakePogJump();
            }

            if (overlapThing.TryGetComponent(out PogableThings pogableThing))
            {
                MakePogJump();
            }

            // I do not like it. I feel like it is more interesting if you can only kill enemies with dash
            //if(overlapThing.TryGetComponent(out BasicEnemy enemy))
            //{
            //    //enemy.KillEnemy();
            //    MakePogJump();
            //}
        }

    }

    private void MakePogJump()
    {
        //Time.timeScale = 0.1f;
        pogJumpColliderActive = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
    }
}
