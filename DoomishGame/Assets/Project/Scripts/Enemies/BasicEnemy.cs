using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private GameManger gameManger;
    private bool firstDMG;

    private void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
        firstDMG = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && firstDMG)
        {
            firstDMG = false;
            string objectName = gameObject.name;
            gameManger.KilledEnemy(objectName);
            Destroy(gameObject);
        }
    }
}
