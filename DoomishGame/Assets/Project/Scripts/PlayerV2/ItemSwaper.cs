using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwaper : MonoBehaviour
{
    [SerializeField] GameObject[] allTools;
    [SerializeField] float[] allToolsCooldown;
    [SerializeField] UI_WeaponDisplayer weaponDisplayer;
    [SerializeField] bool[] weaponAvaiability;
    private int currentTool;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTool != 2)
            {
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

    public void StartCooldown(float cooldownTime)
    {
        StartCoroutine(CooldownCutdown(cooldownTime));
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
        weaponDisplayer.makeToolAvaiable(usedWeapon);
        if(usedWeapon == currentTool)
        {
            allTools[usedWeapon].SetActive(true);
        }
    }
}
