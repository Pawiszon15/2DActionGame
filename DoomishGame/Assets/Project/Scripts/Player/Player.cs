using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isPlayerInvForDeflactableBullets = false;
    [HideInInspector] public bool isPlayerInv = false;
    private float shortTimeAfterInvDuration = 0.2f;
    private bool isShortTimeAfterInv = false;
    private GameManger manger;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        manger = FindObjectOfType<GameManger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Killable thing))
        {
            if (isPlayerInv)
            {

            }

            else if (isPlayerInvForDeflactableBullets && collision.gameObject.TryGetComponent(out EnemyBullet deflectableBullet))
            {
                Debug.Log("it is working DEFLECT");
                //deflectableBullet.DeflectBullet();
            }

            else
            {
                manger.ResetScene();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isShortTimeAfterInv)
        {
            if (collision.gameObject.TryGetComponent(out Killable thing))
            {
                manger.ResetScene();
            }
        }
    }

    public void TryToKillPlayer()
    {
        if(!isPlayerInv)
        {
            manger.ResetScene();
        }
    }

    #region INV TIMERS
    public void StartInvincibility(float dashDuration)
    {

        StartCoroutine(InvTime(dashDuration));
    }

    public void StartDeflectInv(float deflectInv)
    {
        StartCoroutine(defInvTime(deflectInv));
    }

    private IEnumerator defInvTime(float defTime)
    {
        isPlayerInvForDeflactableBullets = true;
        yield return new WaitForSeconds(defTime);
        isPlayerInvForDeflactableBullets = false;
    }

    private IEnumerator InvTime(float invTime)
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);

        isPlayerInv = true;
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(invTime);
        isPlayerInv = false;
        boxCollider2D.enabled = true;

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        isShortTimeAfterInv = true;
        yield return new WaitForSeconds(shortTimeAfterInvDuration);
        isShortTimeAfterInv = false;
    }
    #endregion
}
