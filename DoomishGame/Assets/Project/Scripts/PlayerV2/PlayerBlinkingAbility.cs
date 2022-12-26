using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkingAbility : MonoBehaviour
{
    [Header("Ability properties")]
    [SerializeField] float blinkingDistance;
    [SerializeField][Range(0, 1)] float slowmoEffect;
    [SerializeField] float maxTimeOfSlowmo;

    [Header("Resources")]
    [SerializeField] int blinkCost;
    [SerializeField] int maxResourcesAmount;

    private int currentReousrceAmount;

    // Update is called once per frame
    void Update()
    {

    }
    
    private void HandleInputs()
    {
        //idea diffrent ability for no resources?
        if (blinkCost <= currentReousrceAmount)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(StartAbility());
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                StopCoroutine(StartAbility());
                EndAbility();
            }
        }

    }

    IEnumerator StartAbility()
    {
        yield return null;
        EndAbility();
    }

    private void EndAbility()
    {

    }

    public void AddResources()
    {

    }
}
