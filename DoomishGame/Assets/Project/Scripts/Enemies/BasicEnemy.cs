using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool itHasShield = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool explosionGuy;
    [SerializeField] Doors door;
    [SerializeField] GameObject particle;

    private GameManger gameManger;
    private bool firstDMG;
    private EnemyAnimator enemyAnimator;
    private KillingSpree killingSpree;
    private PlayerBlinkingAbility blinkingAbility;
    private CinemaShakes cinemaShakes;

    private void Awake()
    {
        cinemaShakes = FindObjectOfType<CinemaShakes>();
        blinkingAbility = FindObjectOfType<PlayerBlinkingAbility>();
        killingSpree = FindObjectOfType<KillingSpree>();
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

        //else if(collision.gameObject.tag == "Explosion" && firstDMG)
        //{
        //    if (!explosionGuy)
        //    {
        //        StartDeathAnimation();
        //    }
        //}
    }

    public void StartDeathAnimation()
    {
        cinemaShakes.CameraShakeStart(0);
        door.enemyKilled();
        firstDMG = false;
        blinkingAbility.AddResources();
        killingSpree.AddEnemyToKillingManager();
        enemyAnimator.StartDeathAnimation();

        //StartCoroutine(StopTimeForMomemnt());
        //player.InstaDashRefill();
    }

    public void KillEnemy()
    {
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
    }

    public void TurnOffShield()
    {
        spriteRenderer.enabled = false;
        itHasShield = false;
    }

    private void SpawnGhost()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
}
