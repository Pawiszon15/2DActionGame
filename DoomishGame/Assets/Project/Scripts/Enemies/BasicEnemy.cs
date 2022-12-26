using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool itHasShield = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    private GameManger gameManger;
    private bool firstDMG;
    private EnemyAnimator enemyAnimator;
    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        gameManger = FindObjectOfType<GameManger>();

        firstDMG = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && firstDMG)
        {
            if(!itHasShield)
            {
                StartDeathAnimation();
            }
        }
    }

    public void StartDeathAnimation()
    {
        enemyAnimator.StartDeathAnimation();
        //StartCoroutine(StopTimeForMomemnt());
        //player.InstaDashRefill();
 }

    public void KillEnemy()
    {
        firstDMG = false;
        gameManger.KilledEnemy();
        Destroy(this.gameObject);
    }


    IEnumerator StopTimeForMomemnt()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }

    public void TurnOnShield()
    {
        spriteRenderer.enabled = true;
        itHasShield = true;
        Debug.Log("Turn on shield on enemy");
    }

    public void TurnOffShield()
    {
        spriteRenderer.enabled = false;
        itHasShield = false;
    }
}
