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
        enemyAnimator.SetBool("isIdling", true);
    }

    // Update is called once per frame

    public void StartDeathAnimation()
    {
        enemyAnimator.SetBool("isIdling", false);
        enemyAnimator.SetTrigger("isDaying");
    }

    public void StartAttackAnimation()
    {
        Debug.Log("start attack animation");
        enemyAnimator.SetBool("isIdling", false);
        enemyAnimator.SetTrigger("isAttacking");
    }

    public void StartIdling()
    {
        enemyAnimator.SetBool("isIdling", true);
    }

    private void CreateParticle(GameObject particleType)
    {
        GameObject obj = Instantiate(particleType , transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1);
    }
}
