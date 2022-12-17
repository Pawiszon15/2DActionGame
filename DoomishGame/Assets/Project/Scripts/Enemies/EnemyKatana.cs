using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyKatana : MonoBehaviour
{
    [Header("Slash properties")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] Vector2 boxCenter;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float slashCooldown;
    [SerializeField] float[] PositionDuringSlash;

    private bool isVurnable = false;

    private EnemyActivation enemyActivation;
    private EnemyAnimator animator;

    private GameObject player;
    private float directionOfDash;
    private bool shouldCheckForCollision = false;
    private bool playerParry = false;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        enemyActivation = GetComponent<EnemyActivation>();
        animator = GetComponent<EnemyAnimator>();
        enemyActivation.isEnemyReadyToShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyActivation.isThereLineOfSightAndInRange && enemyActivation.isEnemyReadyToShoot)
        {
            animator.StartAttackAnimation();
            enemyActivation.isThereLineOfSightAndInRange = false;
            enemyActivation.isEnemyReadyToShoot = false;
        }

        if(shouldCheckForCollision)
        {
            SlashAttackByEnemy();
        }
    }

    private void ReflectPlayerAttack()
    {
        Debug.Log("reflect");
    }

    private void TryToApplayDamage()
    {
        if (!isVurnable)
        {
            Debug.Log("Enemy isn't vurnable");

            //ReflectPlayerAttack();
        }

        else
        {
            animator.StartDeathAnimation();
        }
    }

    public void SlashAttackByEnemy()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, boxSize,0f ,layerMask);
        Debug.Log(collider.Length);
        


        foreach (Collider2D collider2d in collider)
        {
            Debug.Log(collider2d.gameObject.name);
            if (collider2d.TryGetComponent(out PlayerSlash playerAttack))
            {
                isVurnable = true;
                animator.StartStagerdAnimation();
                //Time.timeScale = 0f;
                playerParry = true;

                return;
            }

            else if (collider2d.TryGetComponent(out Player player))
            {
                if(!playerParry)
                {
                    player.TryToKillPlayer();
                }
            }
        }

        StartCoroutine(DashCooldown());
    }

    private void CloseDistanceToThePlayer()
    {
    }

    private void SetPositionOne()
    {
        directionOfDash = enemyActivation.isFacingRight;
        transform.position = transform.position + new Vector3(PositionDuringSlash[0] * directionOfDash, 0, 0);
    }

    private void SetPostionTwo()
    {
        transform.position = transform.position + new Vector3(PositionDuringSlash[1] * directionOfDash, 0, 0);
    }

    private void SetPositionThree()
    {
        transform.position = transform.position + new Vector3(PositionDuringSlash[2] * directionOfDash, 0, 0);
    }

    private void SetPositionFourth()
    {
        transform.position = transform.position + new Vector3(PositionDuringSlash[3] * directionOfDash, 0, 0);
    }

    private void EndVurnability()
    {
        isVurnable = false;
    }

    
    private void StartCollisionChecks()
    {
        shouldCheckForCollision = true;
    }

    private void EndCollisionChecks()
    {
        shouldCheckForCollision = false;
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, boxSize);
    }



    private IEnumerator DashCooldown()
    {
        animator.isIdling = true;
        yield return new WaitForSeconds(slashCooldown);
        enemyActivation.isEnemyReadyToShoot = true;
    }
}
