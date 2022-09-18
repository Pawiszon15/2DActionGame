using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Katana : MonoBehaviour
{
    [Header("Attack properties")]
    [SerializeField] float slashDuration;
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
    [SerializeField] Transform gunPivot;

    private Rigidbody2D rb2d;
    private Transform playerTransform;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    void Start()
    {
        deafaultGravityScale = rb2d.gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) &&canSlash)
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

        //dash
        rb2d.velocity = Vector2.zero;
        //rb2d.gravityScale = 0f;
        Vector2 vector2 = gunPivot.transform.right;
        //rb2d.velocity = vector2 * dashSpeed;
        rb2d.AddForce(vector2 * dashSpeed, ForceMode2D.Impulse);

        StartCoroutine(SlashDuration());
    }

    IEnumerator SlashDuration()
    {
        yield return new WaitForSeconds(slashDuration);
        canSlash = true;
        capsuleCollider2D.enabled = false;
        rb2d.gravityScale = deafaultGravityScale;
    }

}
    
