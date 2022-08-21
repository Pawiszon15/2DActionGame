using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private GameManger gameManger;

    private void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
    }

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
