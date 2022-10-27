using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float slamSpeed;

    [Header("Properties")]
    [SerializeField] Rigidbody2D rb2d;

    private AbilitiyCooldown abilityCooldown;
    private CharacterMovement chMovement;

    // Start is called before the first frame update
    private void Start()
    {
        chMovement = GetComponentInParent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SlamGetDown();
        }
    }

    private void SlamGetDown()
    {
        rb2d.velocity = Vector2.zero;
        if(chMovement.isPlayerBoosted)
        {
            rb2d.velocity = new Vector2(0, 2 * -slamSpeed);
            Debug.Log("Enchanced ground slam has been done");
            chMovement.isPlayerBoosted = false;
        }

        else
        {
            rb2d.velocity = new Vector2(0, -slamSpeed);
        }

        chMovement.IsGroundSlaming();
        abilityCooldown.UseAbility();
    }

}
