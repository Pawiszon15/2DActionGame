using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAbility : MonoBehaviour
{
    [SerializeField] float deflectRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeflectEnemiesBullet()
    {
        var overlapedBullets = Physics2D.OverlapCircleAll(transform.position, deflectRange);

        foreach(var overlapedBullet in overlapedBullets)
        {

            EnemyBullet enemyBullet = overlapedBullet.GetComponent<EnemyBullet>();
        }
    }
}
