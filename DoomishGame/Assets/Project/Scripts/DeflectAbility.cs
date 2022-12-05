using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class DeflectAbility : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float deflectRange;
    [SerializeField] Transform deflectPos;
    [SerializeField] float defectTime;
    [SerializeField] float deflectCooldown;
    [SerializeField] Transform mousePos;

    [HideInInspector] public bool isDeflecting = false;
    private int numberOfDeflects = 1;
    private Player player;
    private PlayerAnimator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && numberOfDeflects > 0 && !isDeflecting)
        {
            //player.StartDeflectInv(defectTime);
            //animator.ChangeDeflectAnimation();    
            --numberOfDeflects;      
            isDeflecting = true;
            StartCoroutine(DeflectTime());
        }  

        if(isDeflecting)
        {
            DeflectEnemiesBullet();
        }
    }

    private void DeflectEnemiesBullet()
    {
        Collider2D[] overlapedBullets = Physics2D.OverlapCircleAll(deflectPos.position, deflectRange, layer);
        Debug.Log(overlapedBullets.Length);
        foreach (Collider2D overlapedBullet in overlapedBullets)
        {
            if (overlapedBullet.gameObject.GetComponent<EnemyBullet>())
            {
                EnemyBullet enemyBullet = overlapedBullet.gameObject.GetComponent<EnemyBullet>();
                enemyBullet.DeflectBullet(mousePos.right);
            }
        }
    }

    IEnumerator DeflectTime()
    {
        yield return new WaitForSeconds(defectTime);
        isDeflecting = false;
        animator.ChangeDeflectAnimation();


        yield return new WaitForSeconds(deflectCooldown);
        ++numberOfDeflects;
    }
}
