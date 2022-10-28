using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Katana : MonoBehaviour
{
    [Header("Attack properties")]
    [SerializeField] float slashDuration;
    [SerializeField] float slashDurMultiplayer;
    [SerializeField] float cooldown;
    private float defSlashDuration;
    public bool isSlashing = false;

    [Header("Dash properties")]
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
    private AbilitiyCooldown abilityCooldown;
    private bool isTouchedGroundFromLastSlash;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        chMovement = GetComponentInParent<CharacterMovement>();
    }

    void Start()
    {
        abilityCooldown = GetComponent<AbilitiyCooldown>();
        ItemSwaper = GetComponentInParent<ItemSwaper>();
        deafaultGravityScale = rb2d.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isSlashing && abilityCooldown.numberOfUses > 0 && isTouchedGroundFromLastSlash)
        {
            if(chMovement.isPlayerBoosted)
            {
                Slash(slashDurMultiplayer * slashDuration);
                chMovement.isPlayerBoosted = false;
                Debug.Log("Enchanced katana slash has been done");
            }

            else
            {
                Slash(slashDuration);
            }
        }

        if(!isTouchedGroundFromLastSlash)
        {
            if(chMovement.isGrounded || chMovement.wallSliding)
            {
                isTouchedGroundFromLastSlash = true;
            }
        }
    }

    public void Slash(float customSlashDuration)
    {
        isSlashing = true;

        var newSlash = Instantiate(VFXEffect, firePoint.transform.position, VFXEffect.transform.localRotation, gameObject.transform);
        Destroy(newSlash, 0.3f);
        capsuleCollider2D.enabled = true;

        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0;
        Vector2 vector2 = gunPivot.transform.right;
        rb2d.AddForce(vector2 * dashSpeed, ForceMode2D.Impulse);
        StartCoroutine(SlashDuration(customSlashDuration));
    }

    IEnumerator SlashDuration(float customSlashDuration)
    {
        yield return new WaitForSeconds(customSlashDuration);
        isSlashing = false;
        capsuleCollider2D.enabled = false;
        rb2d.gravityScale = deafaultGravityScale;
        isTouchedGroundFromLastSlash = false;
        abilityCooldown.UseAbility();
    }

    public void BoostByGate()
    {
        Debug.Log("Slash through gate");
        StopCoroutine(SlashDuration(slashDuration));
        Slash(slashDuration * 3);
    }
}
    
