using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkingAbilityUI : MonoBehaviour
{
    [SerializeField] GameObject[] playerRes;

    public void SetCurrentResources(int currentRes)
    {
        if(currentRes == 0)
        {
            TurnOffAllResources();
        }

        TurnOnRightResources(currentRes);
    }

    public void TurnOffAllResources()
    {
        foreach (GameObject res in playerRes)
        {
            res.SetActive(false);
        }
    }

    private void TurnOnRightResources(int currentRes)
    {
        if(currentRes == 0)
            return;
        else
            playerRes[currentRes - 1].SetActive(true);

    }
}
