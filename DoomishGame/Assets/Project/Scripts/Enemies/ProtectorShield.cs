using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorShield : MonoBehaviour
{
    GameManger manger;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        manger = FindObjectOfType<GameManger>();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Debug.Log("player death" + time);
            manger.ResetScene();
        }

        if(collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("playerbullet" + time);
        }
    }
}
