using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float slamSpeed;

    [Header("Properties")]
    [SerializeField] Rigidbody2D rb2d;

    [SerializeField] 
    private ToolCooldown toolCooldown;
    private ItemSwaper itemSwaper;
    private CharacterMovement chMovement;

    // Start is called before the first frame update
    void Awake()
    {
        chMovement = FindObjectOfType<CharacterMovement>();
        toolCooldown = GetComponent<ToolCooldown>();
        itemSwaper = FindObjectOfType<ItemSwaper>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && toolCooldown.rightMouseUse > 0)
        {
            SlamGetDown();
        }
    }

    private void SlamGetDown()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.velocity = new Vector2(0, -slamSpeed);
        chMovement.IsGroundSlaming();
        --toolCooldown.rightMouseUse;
        itemSwaper.TryToStartCooldown();
    }
}
