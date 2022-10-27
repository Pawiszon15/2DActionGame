using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLauncher : MonoBehaviour
{
    [SerializeField] GameObject gate;
    [SerializeField] GameObject gatePos;

    private ItemSwaper itemSwaper;
    private AbilitiyCooldown abilityCooldown;

    // Start is called before the first frame update
    void Start()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();
        abilityCooldown = GetComponent<AbilitiyCooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && abilityCooldown.numberOfUses > 0)
        {
            Instantiate(gate, gatePos.transform.position, gatePos.transform.rotation);
            abilityCooldown.UseAbility();
        }    
    }
}
