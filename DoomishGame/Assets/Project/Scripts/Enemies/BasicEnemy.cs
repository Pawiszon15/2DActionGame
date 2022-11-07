using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //[SerializeField] bool hasShiled;
    public bool isProtector;
    private GameManger gameManger;
    private bool firstDMG;
    private Player player;
    private bool isFacingRight = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        gameManger = FindObjectOfType<GameManger>();
        firstDMG = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && firstDMG)
        {
            if(isProtector)
            {
                TryToKillProtector();
            }

            else
            {
                KillEnemy();
            }
        }
    }

    public void KillEnemy()
    {
        Debug.Log(gameObject.name);
        firstDMG = false;
        string objectName = this.gameObject.name;
        gameManger.KilledEnemy(objectName);
        Destroy(this.gameObject);
    }

    public void FlipToRight(bool isRight)
    {
        isFacingRight = isRight;
    }

    private void TryToKillProtector()
    {
        if (!isFacingRight && gameObject.transform.position.x <= player.transform.position.x)
        {
            KillEnemy();
        }

        else if(isFacingRight && gameObject.transform.position.x > player.transform.position.x)
        {
            KillEnemy();
        }
    }
}
