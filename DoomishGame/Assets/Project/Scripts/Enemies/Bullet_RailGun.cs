using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Bullet_RailGun : MonoBehaviour
{
    private GameManger manger;

    private void Start()
    {
        manger = FindObjectOfType<GameManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.WriteLine("Railgun hit");
        if (collision.gameObject.tag == "Player")
        {
            manger.ResetScene();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.WriteLine("sth");
    }
}
