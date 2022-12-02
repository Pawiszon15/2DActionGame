using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerInv = false;
    private float shortTimeAfterInvDuration = 0.2f;
    private bool isShortTimeAfterInv = false;
    private GameManger manger;

    private void Start()
    {
        manger = FindObjectOfType<GameManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Killable thing))
        {
            if (!isPlayerInv)
            {
                manger.ResetScene();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isShortTimeAfterInv)
        {
            if (collision.gameObject.TryGetComponent(out Killable thing))
            {
                manger.ResetScene();
            }
        }
    }
        

    public void StartInvincibility(float dashDuration)
    {

        StartCoroutine(InvTime(dashDuration));
    }

    private IEnumerator InvTime(float invTime)
    {
        isPlayerInv = true;
        yield return new WaitForSeconds(invTime);
        isPlayerInv = false;

        isShortTimeAfterInv = true;
        yield return new WaitForSeconds(shortTimeAfterInvDuration);
        isShortTimeAfterInv = false;
    }
}
