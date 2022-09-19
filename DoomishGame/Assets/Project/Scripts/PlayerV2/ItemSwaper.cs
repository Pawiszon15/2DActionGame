using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwaper : MonoBehaviour
{
    [SerializeField] GameObject[] allTools;
    [SerializeField] float[] toolCooldowns;
    [SerializeField] UI_WeaponDisplayer weaponDisplayer;

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
                allTools[currentTool].SetActive(true);
                weaponDisplayer.moveMarker(currentTool);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentTool != 0)
            {
                allTools[currentTool].SetActive(false);
                currentTool--;
                allTools[currentTool].SetActive(true);
                weaponDisplayer.moveMarker(currentTool);
            }
        }
    }


    public void StartCooldownOnTool(float cooldownTime)
    {

    }

    public void MakeToolAvaiableAgain()
    {

    }

    public void DeactivateUsedSkill()
    {

    }

    private void ChangeWeapon()
    {

    }
}
