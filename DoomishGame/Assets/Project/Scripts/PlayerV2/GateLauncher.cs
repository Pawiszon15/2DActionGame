using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLauncher : MonoBehaviour
{
    [SerializeField] GameObject gate;
    [SerializeField] GameObject gatePos;

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
        if(Input.GetKeyDown(KeyCode.Q) && toolCooldown.rightMouseUse > 0)
        {
            Instantiate(gate, gatePos.transform.position, gatePos.transform.rotation);
            --toolCooldown.rightMouseUse;
            itemSwaper.TryToStartCooldown();
        }    
    }
}
