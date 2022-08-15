using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] GameManger gameManger;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("tigger");
        if (collision.gameObject.tag == "Bullet")
        {
            string objectName = gameObject.name;
            gameManger.KilledEnemy(objectName);
            Destroy(gameObject);
        }
    }


}
