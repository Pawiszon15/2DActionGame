using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent(out EnemyBullet enemyBullet))
        {
            Debug.Log("deflection");
        }
    }
}
