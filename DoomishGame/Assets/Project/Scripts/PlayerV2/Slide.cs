using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("Slide properties")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float invTime;
    [SerializeField] float howMuchItChangePlayerSize;

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRb;

    private ItemSwaper itemSwaper;
    private ToolCooldown toolCooldown;

    // Start is called before the first frame update
    void Start()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();
        toolCooldown = GetComponent<ToolCooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1) && toolCooldown.rightMouseUse > 0)
        {
            //set spped greater than player movement with larp
            //gradually decresse it 
            StartCoroutine(SlideDash());
        }
    }

    IEnumerator SlideDash()
    {
        yield return new WaitForSeconds(invTime);

        yield return new WaitForSeconds(dashTime - invTime);
    }
}
