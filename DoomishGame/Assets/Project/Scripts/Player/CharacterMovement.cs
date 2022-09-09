using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody2D rb2D = null;

    [Header("Grounded movement")]
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float maxPlayerVelocity;

    [Header("Aerial movement")]
    [SerializeField] private float aerialMovMulti;
    [SerializeField] private float maxAerialMovementThroughInput;
    [SerializeField] private int maxNumberOfJumps;
    [SerializeField] private float jumpForce;


    private float moveHorizontal;
    private float moveVertical;

    
    private bool isGrounded;
    private int jumpsAvailable;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        isGrounded = true;
        jumpsAvailable = maxNumberOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        moveVertical = Input.GetAxisRaw("Vertical");
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsAvailable > 0)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpsAvailable = jumpsAvailable - 1;
            }
        }
    }

    void FixedUpdate()
    {
        if(isGrounded)
        {
            Vector2 movement = new Vector2(moveHorizontal * maxMovementSpeed, rb2D.velocity.y);
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, movement, accelerationSpeed);
        }

        if (!isGrounded && moveHorizontal != 0 && Mathf.Abs(rb2D.velocity.x) < Mathf.Abs(maxAerialMovementThroughInput))
        {
            //Make it work so more velocity player has less it will change that
            Vector2 movement = new Vector2(moveHorizontal * maxMovementSpeed * aerialMovMulti , 0);
//            if (max < rb2D.velocity.x + movement.x)
                rb2D.AddForce(movement); 
        }

        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, maxPlayerVelocity);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
            jumpsAvailable = maxNumberOfJumps;
            Debug.Log(isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
            Debug.Log(isGrounded);
        }
    }

}
