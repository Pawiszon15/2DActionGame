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

    private float leftArrowLastTimePressed;
    private float rightArrowLastTimePressed;
    private bool isPlayerTryingToPogJump = false;

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
        //rightArrowLastTimePressed -= Time.deltaTime;
        //leftArrowLastTimePressed -= Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    leftArrowLastTimePressed = pogJumpBufferTime;
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    rightArrowLastTimePressed = pogJumpBufferTime;
        //}

        //if (leftArrowLastTimePressed > 0f && rightArrowLastTimePressed > 0f)
        //{
        //    isPlayerTryingToPogJump = true;
        //}

        //else
        //{
        //    isPlayerTryingToPogJump = false;
        //}

        if (Input.GetKeyDown(KeyCode.UpArrow) && pogJumpAvaialable && !playerMovement.IsDashing && playerMovement.LastOnGroundTime != 0
          && !playerMovement.isRolling && !playerMovement.isWallSliding && !playerMovement.IsWallJumping && !playerMovement.recentlyReallyColseToWalls)
        {
            pogJumpAvaialable = false;
            playerAnimator.StartPogJumpAnimation();
            StartCoroutine(PogCooldown());
        }


        if (pogJumpColliderActive)
        {
            CheckForCollision();
        }

        Debug.Log(isPlayerTryingToPogJump);
    }

    private void CheckForCollision()
    {
        Collider2D[] OverlapThings = Physics2D.OverlapCircleAll(posOfPogJumpCollider.position, rangeOfCircle, layers);

        foreach (Collider2D overlapThing in OverlapThings)
        {
            if (overlapThing.TryGetComponent(out EnemyBullet enemyBullet))
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

    IEnumerator PogCooldown()
    {
        yield return new WaitForSeconds(pogJumpBufferTime);
        pogJumpAvaialable = true;
    }

}
