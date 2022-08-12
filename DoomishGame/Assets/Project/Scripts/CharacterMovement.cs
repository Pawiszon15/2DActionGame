using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody2D rb2D = null;

    private float movementSpeed = 3f;
    private float moveHorizontal;
    private float moveVertical;
    
    private float jumpForce = 50f;
    private bool isJumping;
    private int maxNumberOfJumps;
    private int jumpsAvailable;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        movementSpeed = 3f;
        jumpForce = 10f;
        isJumping = true;
/*        maxNumberOfJumps = 2;
        jumpsAvailable = 2;*/
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        Debug.Log(isJumping);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space was hit");
            if (isJumping == false)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }

    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < 0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * movementSpeed, 0), ForceMode2D.Impulse);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Debug.Log("hitting ground");
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

}
