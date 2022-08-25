using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody2D rb2D = null;

    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float accelerationSpeed;

    private float moveHorizontal;
    private float moveVertical;

    [SerializeField] private int maxNumberOfJumps;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private int jumpsAvailable;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        isGrounded = true;

        maxNumberOfJumps = 2;
        jumpsAvailable = maxNumberOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        moveVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {            
            if(jumpsAvailable>0)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpsAvailable = jumpsAvailable - 1;
                Debug.Log(jumpsAvailable);
            }  
        }
    }

    void FixedUpdate()
    {
        /*//Physical movement
        if (moveHorizontal > 0.1f || moveHorizontal < 0.1f)
        {
            if (rb2D.velocity.x < maxMovementSpeed && rb2D.velocity.x > -maxMovementSpeed)
            {
                rb2D.AddForce(new Vector2(moveHorizontal * movementSpeed, 0), ForceMode2D.Force);
            }
        }*/

        if(isGrounded)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal * maxMovementSpeed, rb2D.velocity.y);
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, movement, accelerationSpeed);
        }
       
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Debug.Log("hitting ground");
            isGrounded = true;
            jumpsAvailable = maxNumberOfJumps;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }

}
