using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAbility : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float deflectRange;
    [SerializeField] float defectTime;
    [SerializeField] float deflectCooldown;
    [HideInInspector] public bool isDeflecting = false;
    private int numberOfDeflects = 1;
    private Player player;
    private PlayerAnimator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && numberOfDeflects > 0 && !isDeflecting)
        {
            Debug.Log("deflecting");
            isDeflecting = true;
            --numberOfDeflects;
            animator.ChangeDeflectAnimation();
            StartCoroutine(DeflectTime());
        }
        
        if(isDeflecting)
        {
            DeflectEnemiesBullet();
        }    

    }

    private void DeflectEnemiesBullet()
    {
        Collider2D[] overlapedBullets = Physics2D.OverlapCircleAll(transform.position, deflectRange, layer);
        Debug.Log(overlapedBullets.Length);
        foreach(Collider2D overlapedBullet in overlapedBullets)
        {
            if(overlapedBullet.gameObject.GetComponent<EnemyBullet>())
            {
                EnemyBullet enemyBullet = overlapedBullet.gameObject.GetComponent<EnemyBullet>();
                enemyBullet.DeflectBullet();
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
