using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Katana : MonoBehaviour
{
    [Header("Attack properties")]
    [SerializeField] float slashDuration;
    [SerializeField] float cooldown;
    private bool canSlash = true;

    [Header("Dash properties")]
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    private float deafaultGravityScale;

    [Header("Visual effect")]
    [SerializeField] GameObject VFXEffect;
    [SerializeField] Transform firePoint;

    [Header("References")]
    [SerializeField] CapsuleCollider2D capsuleCollider2D;
    [SerializeField] CharacterMovement chMovement;
    [SerializeField] Transform gunPivot;

    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private ItemSwaper ItemSwaper;
    private ToolCooldown toolCooldown;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        chMovement = GetComponentInParent<CharacterMovement>();
    }

    void Start()
    {
        toolCooldown = GetComponent<ToolCooldown>();
        ItemSwaper = GetComponentInParent<ItemSwaper>();
        deafaultGravityScale = rb2d.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canSlash && toolCooldown.leftMouseUse > 0)
        {
            Slash();
        }
    }

    public void Slash()
    {
        canSlash = false;

        var newSlash = Instantiate(VFXEffect, firePoint.transform.position, VFXEffect.transform.localRotation, gameObject.transform);
        Destroy(newSlash, 0.3f);
        capsuleCollider2D.enabled = true;

        rb2d.velocity = Vector2.zero;
        Vector2 vector2 = gunPivot.transform.right;
        rb2d.AddForce(vector2 * dashSpeed, ForceMode2D.Impulse);
        StartCoroutine(SlashDuration());
    }

    IEnumerator SlashDuration()
    {
        yield return new WaitForSeconds(slashDuration);
        canSlash = true;
        capsuleCollider2D.enabled = false;
        rb2d.gravityScale = deafaultGravityScale;
        ItemSwaper.TryToStartCooldown();
        --toolCooldown.leftMouseUse;
    }

}
    
