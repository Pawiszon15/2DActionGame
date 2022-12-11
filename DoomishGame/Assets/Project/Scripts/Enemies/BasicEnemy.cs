using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //For animation purpose

    //[SerializeField] bool hasShiled;
    public bool isProtector;
    private GameManger gameManger;
    private bool firstDMG;
    private PlayerMovement player;
    private bool isFacingRight = false;
    private EnemyAnimator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        player = FindObjectOfType<PlayerMovement>();
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
                StartDeathAnimation();
            }
        }
    }

    public void StartDeathAnimation()
    {
        enemyAnimator.isDying = true;
        //StartCoroutine(StopTimeForMomemnt());
        //player.InstaDashRefill();
 }

    public void KillEnemy()
    {
        firstDMG = false;
        gameManger.KilledEnemy();
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
            StartDeathAnimation();
        }

        else if(isFacingRight && gameObject.transform.position.x > player.transform.position.x)
        {
            StartDeathAnimation();
        }
    }

    IEnumerator StopTimeForMomemnt()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }
}
