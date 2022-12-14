using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody2D rb2D = null;
    [Header("")]

    [Header("Grounded movement")]
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float maxPlayerVelocity;
    private float moveHorizontal;
    private float moveVertical;
    private float maxDefaultVelocity;

    [Header("Aerial movement")]
    [SerializeField] private float aerialMovMulti;
    [SerializeField] private float maxAerialMovementThroughInput;
    [SerializeField] private int maxNumberOfJumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private float temJumpForceAfterSlide;
    private float defGravityScale;
    private float defJumpForce;
    private float defAerialMovMulti;
    public bool isGrounded;
    private int jumpsAvailable;
    private bool isGroundSlaming;

    [Header("Wall slide")]
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] Transform frontChecker;
    private bool isFrontTouchingWall;
    [HideInInspector] public bool wallSliding;
    private bool facingRight = true;

    [Header("Sliding")]
    [SerializeField] float slideDecressSpeed;
    [SerializeField] float playerHeightDuringSlide;
    private bool isSliding;

    [Header("Is Grounded Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    [Header("Wall jumping")]
    [SerializeField] float wallJumpTime;
    [SerializeField] float xWallForce;
    [SerializeField] float yWallForce;
    private bool walljumping;

    [Header("References")]
    [SerializeField] GameObject playerFrontTarget;
    [SerializeField] float timeOfGateBoost;
    [HideInInspector] public bool isPlayerBoosted;
    [SerializeField] CapsuleCollider2D groundSlamCollider;
    [SerializeField] GameObject player;
    private CapsuleCollider2D capsule2D;

    // Start is called before the first frame update
    void Start()
    {       
        capsule2D = GetComponent<CapsuleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        isGrounded = true;
        isGroundSlaming = false;
        jumpsAvailable = maxNumberOfJumps;
        defJumpForce = jumpForce;
        defAerialMovMulti = aerialMovMulti;
        maxDefaultVelocity = maxPlayerVelocity;
        defGravityScale = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        moveVertical = Input.GetAxisRaw("Vertical");
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpsAvailable = jumpsAvailable - 1;
            }
        }

        if (moveHorizontal > 0f && facingRight == false)
        {
            Flip();
        }
        else if (moveHorizontal < 0f && facingRight == true)
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isFrontTouchingWall = Physics2D.OverlapCircle(frontChecker.position, checkRadius, whatIsGround);

        if (isFrontTouchingWall == true && isGrounded == false && moveHorizontal != 0)
        {
            wallSliding = true;
        }
        
        else
        {
            wallSliding = false;
        }

        if (wallSliding == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (wallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            walljumping = true;
            Invoke("setWallJumpingToFalse", wallJumpTime);
        }

        if (walljumping)
        {
            rb2D.velocity = new Vector2(-moveHorizontal * xWallForce, yWallForce);

        }

        if(isGroundSlaming && isGrounded)
        {
            IsGroundSlaming();
        }

        if(Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            if(!isSliding)
            {
                player.transform.localScale = new Vector3(1f, 0.5f, 1f);
                //capsule2D.radius = 0.25f;
                rb2D.AddForce(new Vector2(0, -5f));
            }

            isSliding = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isSliding)
        {
            if(isSliding)
            {
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                //capsule2D.radius = 0.5f;
            }

            isSliding = false;
        }


        playerFrontTarget.transform.localPosition = new Vector3(Mathf.Clamp(rb2D.velocity.x, -8f, 8f), Mathf.Clamp(rb2D.velocity.y, 0f, 8f), playerFrontTarget.transform.localPosition.z);

    }

    void FixedUpdate()
    {   if(isSliding)
        {
            rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0, slideDecressSpeed), rb2D.velocity.y);
        }

        if (isGrounded && !isSliding)
        {
            Vector2 movement = new Vector2(moveHorizontal * maxMovementSpeed, rb2D.velocity.y);
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, movement, accelerationSpeed);
        }

        if (!isGrounded && moveHorizontal != 0 && Mathf.Abs(rb2D.velocity.x) < Mathf.Abs(maxAerialMovementThroughInput))
        {
            //Make it work so more velocity player has less it will change that
            Vector2 movement = new Vector2(moveHorizontal * maxMovementSpeed * aerialMovMulti, 0);
            //            if (max < rb2D.velocity.x + movement.x)
            rb2D.AddForce(movement);
        }

        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, maxPlayerVelocity);
    }

    private void Flip()
    {
        frontChecker.transform.localPosition = new Vector3(-frontChecker.transform.localPosition.x, frontChecker.transform.localPosition.y, frontChecker.transform.localPosition.z);
        facingRight = !facingRight;
    }

    void setWallJumpingToFalse()
    {
        walljumping = false;
    }

    public void IsGroundSlaming()
    {
        if(!isGrounded)
        {
            isGroundSlaming = true;
            groundSlamCollider.enabled = true;
            rb2D.gravityScale = 0;
            aerialMovMulti = 0;
        }

        else
        {
            isGroundSlaming = false;
            groundSlamCollider.enabled = false;
            rb2D.gravityScale = defGravityScale;
            aerialMovMulti = defAerialMovMulti;
        }
    }

    public void BoostByGate()
    {
        StartCoroutine(TempBoostByGate());
    }

    IEnumerator TempBoostByGate()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
        isPlayerBoosted = true;
        yield return new WaitForSeconds(timeOfGateBoost);
        isPlayerBoosted = false;
        spriteRenderer.color = Color.white;
    }

     IEnumerator TempHigherVelocity(float maxTempVelocity, float timeOfBoost)
    {
        maxPlayerVelocity = maxTempVelocity;
        yield return new WaitForSeconds(timeOfBoost);
        maxPlayerVelocity = maxDefaultVelocity;
    }
    
    //public void StartVelocityBoost(float maxTempVelocity, float timeOfBost)
    //{
    //    StartCoroutine(TempHigherVelocity(maxTempVelocity, timeOfBost));
    //}

    //public void StartJumpBoost(float timeOfBoost)
    //{
    //    StartCoroutine(TempHigherJump(timeOfBoost));
    //}

    //IEnumerator TempHigherJump(float timeOfBoost)
    //{
    //    jumpForce = temJumpForceAfterSlide;
    //    yield return new WaitForSeconds(timeOfBoost);
    //    jumpForce = defJumpForce;
    //}
}
