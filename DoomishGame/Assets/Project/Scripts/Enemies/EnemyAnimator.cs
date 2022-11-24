using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    private Animator enemyAnimator;

    [SerializeField] public GameObject attackParticle, chargeParticle, moveParticle, dyingParticle, idleParticle;

    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isCharging = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isDying = false;
    [HideInInspector] public bool isIdling = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (isAttacking)
        {
            enemyAnimator.SetBool("isAttacking", isAttacking);
            CreateParticle(attackParticle);
            isAttacking = false;
        }

        if (isCharging)
        {
            enemyAnimator.SetBool("isCharging", isCharging);
            CreateParticle(attackParticle);
            isAttacking = false;
        }

        if (isMoving)
        {
            enemyAnimator.SetBool("isMoving", isMoving);
            CreateParticle(attackParticle);
            isAttacking = false;
        }

        if (isDying)
        {
            enemyAnimator.SetBool("isDying", isDying);
            CreateParticle(attackParticle);
            isAttacking = false;
        }

        if (isIdling)
        {
            enemyAnimator.SetBool("isIdling", isIdling);
            CreateParticle(attackParticle);
            isAttacking = false;
        }
    }

    private void CreateParticle(GameObject particleType)
    {
        GameObject obj = Instantiate(particleType , transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1);
    }
}
