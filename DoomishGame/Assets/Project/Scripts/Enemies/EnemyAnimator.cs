using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    private Animator animator;

    [SerializeField] public GameObject attackParticle, chargeParticle, moveParticle, dyingParticle, idleParticle;
    [SerializeField] Transform posOfAttackParticle;

    [Header("Sounds")]
    [SerializeField] AudioClip attackSound, chargeSound, idleSound;
    [HideInInspector] AudioSource audioSource;

    [Header("State of animations")]
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isCharging = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isDying = false;
    [HideInInspector] public bool isIdling = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isIdling", true);
    }

    // Update is called once per frame

    public void StartDeathAnimation()
    {
        animator.SetBool("isIdling", false);
        animator.SetTrigger("isDaying");
    }

    public void StartAttackAnimation()
    {
        animator.SetBool("isIdling", false);
        animator.SetTrigger("isAttacking");
    }

    public void StartIdling()
    {
        animator.SetBool("isIdling", true);
    }

    private void CreateParticle(GameObject particleType)
    {
        GameObject obj = Instantiate(particleType , transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1);
    }

    private void CreatAttackParticle()
    {
        GameObject obj = Instantiate(attackParticle, posOfAttackParticle.position, Quaternion.identity, posOfAttackParticle.transform);
        Destroy(obj, 1);
    }

    public void PlayEnemySound(int whatSound)
    {
        switch (whatSound)
        {
            case 0:
                audioSource.clip = chargeSound;
                break;

            case 1:
                audioSource.clip = attackSound;
                break;

            case 2:
                audioSource.loop = true;
                audioSource.clip = idleSound;
                break;
        }
        
        audioSource.Play();
    }
}
