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
        //enemyAnimator.SetBool("isDying", isDying);
        //enemyAnimator.SetBool("isMoving", isMoving);
        //enemyAnimator.SetBool("isCharging", isCharging);
        //enemyAnimator.SetBool("isAttacking", isAttacking);
        //enemyAnimator.SetBool("isIdling", isIdling);

        //if (isAttacking)
        //{
        //    //CreateParticle(attackParticle);
        //    isAttacking = false;
        //}

        //if (isCharging)
        //{
        //    //CreateParticle(attackParticle);
        //}

        //if (isMoving)
        //{
        //    //CreateParticle(attackParticle);
        //    isMoving = false;
        //}

        //if (isDying)
        //{
        //    //CreateParticle(attackParticle);
        //    isDying = false;
        //}

        //if (isIdling)
        //{
        //    //CreateParticle(attackParticle);
        //    isDying = false;
        //}
    }

    public void StartDeathAnimation()
    {
        enemyAnimator.SetBool("isIdling", false);
        enemyAnimator.SetTrigger("isDaying");
    }

    public void StartAttackAnimation()
    {
        //enemyAnimator.SetBool("isIdling", false);
        enemyAnimator.SetTrigger("isAttacking");
    }

    private void CreateParticle(GameObject particleType)
    {
        GameObject obj = Instantiate(particleType , transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1);
    }
}
