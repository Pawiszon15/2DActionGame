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

    private float firstTap;
    private float secondTap;
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
        //firstTap -= Time.deltaTime;
        //secondTap -= Time.deltaTime;

        //if(Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    if(firstTap < 0.01f)
        //    {
        //        firstTap = pogJumpBufferTime;
        //    }

        //    else
        //    {
        //        secondTap = pogJumpBufferTime;
        //    }
                
        //    if(firstTap > 0f && secondTap > 0f)
        //    {
        //        //isPlayerTryingToPogJump = true;
        //    }
            
        //}
        if (Input.GetKeyDown(KeyCode.UpArrow) && pogJumpAvaialable && !playerMovement.IsDashing && playerMovement.LastOnGroundTime != 0
          && !playerMovement.isRolling && !playerMovement.isWallSliding && !playerMovement.IsWallJumping)
        {
            firstTap = 0f;
            secondTap = 0f;
            isPlayerTryingToPogJump = true;

            pogJumpAvaialable = false;
            playerAnimator.StartPogJumpAnimation();
            StartCoroutine(PogCooldown());
        }


        if (pogJumpColliderActive)
        {
            CheckForCollision();
        }
    }

    private void CheckForCollision()
    {
        Collider2D[] OverlapThings = Physics2D.OverlapCircleAll(posOfPogJumpCollider.position, rangeOfCircle, layers);

        foreach (Collider2D overlapThing in OverlapThings)
        {
            if (overlapThing.TryGetComponent(out EnemyBullet enemyBullet))
            {
                enemyBullet.DeflectBullet(mousePos.right);
                if (isPlayerTryingToPogJump)
                {
                    isPlayerTryingToPogJump = false;
                    MakePogJump();
                }
            }

            if (overlapThing.TryGetComponent(out PogableThings pogableThing))
            {
                if (isPlayerTryingToPogJump)
                {
                    isPlayerTryingToPogJump=false;
                    MakePogJump();
                }
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
        makingPogJump = true;
        Debug.Log("pogging");
        ////Time.timeScale = 0.1f;
        //pogJumpColliderActive = false;
        //rb.velocity = new Vector2(rb.velocity.x, 0f);
        //rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        ////playerMovement.Jump();
    }

    IEnumerator PogCooldown()
    {
        yield return new WaitForSeconds(pogJumpBufferTime);
        pogJumpAvaialable = true;
    }

}
