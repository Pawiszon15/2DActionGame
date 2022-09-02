using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FloorDeneyer_GuradedGround : MonoBehaviour
{

    [SerializeField] GameObject floorGuard;

    private Enemuy_FloorDeneyer floorGuardScript;
    private bool canCheckPlayerPos;
    private float timeBetweenCheckPlayerPos;

    private void Start()
    {
        canCheckPlayerPos = true;
        timeBetweenCheckPlayerPos = 0.1f;
        floorGuardScript = floorGuard.GetComponent<Enemuy_FloorDeneyer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && canCheckPlayerPos)
        {
            canCheckPlayerPos = false;
            floorGuardScript.StartChase(collision.gameObject.transform.position);
            StartCoroutine(WaitBeforeCheckingPlayerPos());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canCheckPlayerPos)
        {
            canCheckPlayerPos = false;
            floorGuardScript.StartChase(collision.gameObject.transform.position);
            StartCoroutine(WaitBeforeCheckingPlayerPos());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            floorGuardScript.StopChase();
        }
    }

    IEnumerator WaitBeforeCheckingPlayerPos()
    {
        yield return new WaitForSeconds(timeBetweenCheckPlayerPos);
        canCheckPlayerPos = true;
    }

}
