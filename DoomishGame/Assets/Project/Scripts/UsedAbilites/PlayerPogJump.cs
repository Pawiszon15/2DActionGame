using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPogJump : MonoBehaviour
{
    [Header("Pog jump")]
    [SerializeField] float jumpPower;
    [SerializeField] float reloadTime;
    [SerializeField] LayerMask layers;
    [HideInInspector] public bool pogJumpColliderActive;
    private bool pogJumpAvaialable = true;

    [Header("References")]
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
        if (Input.GetKeyDown(KeyCode.X) && pogJumpAvaialable && !playerMovement.IsDashing && !playerMovement.isGrounded && !playerMovement.isRolling && !playerMovement.isWallSliding && !playerMovement.IsWallJumping)     
        {
            playerAnimator.StartPogJumpAnimation();
        }

        if (pogJumpColliderActive)
        {
            CheckForCollision();
        }
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
        pogJumpColliderActive = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
    }
}