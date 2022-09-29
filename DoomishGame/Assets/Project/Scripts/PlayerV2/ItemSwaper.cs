using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwaper : MonoBehaviour
{
    [SerializeField] GameObject[] allTools;
    [SerializeField] bool[] weaponAvaiability;
    [SerializeField] int[] howManyTimeUsed;
    [SerializeField] float[] toolCooldown;

    private UI_WeaponDisplayer weaponDisplayer;
    private int currentTool;

    private void Awake()
    {
        weaponDisplayer = FindObjectOfType<UI_WeaponDisplayer>();

        for (int i = 0; i < allTools.Length; i++)
        {
            toolCooldown[i] = allTools[i].GetComponent<ToolCooldown>().cooldown;
        }
    }

    void Start()
    {
        foreach(var tool in allTools)
        {
            tool.SetActive(false);
        }

        allTools[1].SetActive(true);
        currentTool = 1;
        weaponDisplayer.moveMarker(currentTool);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTool != 2)
            {
                IfSthUsedStartCooldown(currentTool);
                allTools[currentTool].SetActive(false);
                currentTool++;
                weaponDisplayer.moveMarker(currentTool);

                if (weaponAvaiability[currentTool])
                {
                    allTools[currentTool].SetActive(true);
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentTool != 0)
            {
                IfSthUsedStartCooldown(currentTool);
                allTools[currentTool].SetActive(false);
                currentTool--;
                weaponDisplayer.moveMarker(currentTool);

                if (weaponAvaiability[currentTool])
                {
                    allTools[currentTool].SetActive(true);
                }
            }
        }
    }


    public void TryToStartCooldown()
    {
        if (howManyTimeUsed[currentTool] > 0)
        {
            StartCoroutine(CooldownCutdown(toolCooldown[currentTool]));
        }
        ++howManyTimeUsed[currentTool];
    }

    private void IfSthUsedStartCooldown(int usedTool)
    {
        if(howManyTimeUsed[currentTool] > 0)
        {
            StartCoroutine(CooldownCutdown(toolCooldown[usedTool]));
        }
        howManyTimeUsed[usedTool] = 0;
    }

    public IEnumerator CooldownCutdown(float cooldownTime)
    {
        Debug.Log("sth");
        int usedWeapon = currentTool;
        weaponAvaiability[usedWeapon] = false;
        weaponDisplayer.makeToolUnavaiable(usedWeapon);
        allTools[usedWeapon].SetActive(false);

        yield return new WaitForSeconds(cooldownTime);

        weaponAvaiability[usedWeapon] = true;
        howManyTimeUsed[usedWeapon] = 0;
        weaponDisplayer.makeToolAvaiable(usedWeapon);
        if(usedWeapon == currentTool)
        {
            allTools[usedWeapon].SetActive(true);
        }
    }
}
