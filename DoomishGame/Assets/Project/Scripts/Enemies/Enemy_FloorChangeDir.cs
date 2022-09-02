using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FloorChangeDir : MonoBehaviour
{
    [SerializeField] Enemuy_FloorDeneyer floorDeneyer;
    [SerializeField] bool isRightChecker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            floorDeneyer.ChangeDirectionToOpposite();
        }
    }
}
