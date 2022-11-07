using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorShield : MonoBehaviour
{
    private GameManger manger;
    private float time;
    [SerializeField] BasicEnemy basicEnemy;

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
            manger.ResetScene();
        }

        if(collision.gameObject.tag == "PlayerBullet")
        {
            if(collision.gameObject.GetComponent<SingleShot>())
            {
                Debug.Log("bullet decteded");
            }

            if(collision.gameObject.GetComponent<Slash>())
            {
                Debug.Log("slash detected");
            }
        }


    }
}
