using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKatana : MonoBehaviour
{
    [Header("Slash properties")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] Vector2 boxCenter;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float slashCooldown;

    private bool isVurnable;

    private EnemyActivation enemyActivation;
    private EnemyAnimator animator;

    private GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        enemyActivation = GetComponent<EnemyActivation>();
        animator = GetComponent<EnemyAnimator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void SlashAttack()
    {
        //kind of like player dash
    }

    private void ReflectPlayerAttack()
    {

    }

    private void TryToApplayDamage()
    {
        if (!isVurnable)
        {
            ReflectPlayerAttack();
        }

        else
        {
            animator.StartDeathAnimation();
        }
    }

    public void SlashAttackByEnemy()
    {
        Collider2D collider = Physics2D.OverlapBox(boxCenter, boxSize, layerMask);

        if(collider.TryGetComponent(out PlayerSlash playerAttack))
        {
            //inv time
        }

        else if(collider.TryGetComponent(out Player player))
        {
            player.TryToKillPlayer();
        }
    }

    private void CloseDistanceToThePlayer()
    {

    }
}
