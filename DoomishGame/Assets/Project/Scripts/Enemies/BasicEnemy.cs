using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //[SerializeField] bool hasShiled;
    private GameManger gameManger;
    private bool firstDMG;

    private void Start()
    {
        gameManger = FindObjectOfType<GameManger>();
        firstDMG = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && firstDMG/* && !hasShiled*/)
        {
            KillEnemy();
        }

        //else if(hasShiled)
        //{
        //    SingleShot singleShot = GetComponent<SingleShot>();
        //    if(singleShot.isShiledBreaker)
        //    {
        //        KillEnemy();
        //    }
        //}
    }

    private void KillEnemy()
    {
        firstDMG = false;
        string objectName = gameObject.name;
        gameManger.KilledEnemy(objectName);
        Destroy(gameObject);
    }
}
