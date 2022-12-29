using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlinkingAbilityUI : MonoBehaviour
{
    [SerializeField] GameObject[] playerRes;
    [SerializeField] Slider slider;

    private float maxTimeOfDash;
    private float currentTimeOfDash;
    private PlayerBlinkingAbility blinkingAbility;

    private void Awake()
    {
        blinkingAbility = FindObjectOfType<PlayerBlinkingAbility>();
    }

    private void Update()
    {
        if(blinkingAbility.ongoingBlinkDuration > 0 && blinkingAbility.ongoingBlink)
        {
            slider.value = blinkingAbility.ongoingBlinkDuration/2;
        }
    }

    public void TurnBlinkSlider(bool value)
    {
        slider.gameObject.SetActive(value);
    }

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
