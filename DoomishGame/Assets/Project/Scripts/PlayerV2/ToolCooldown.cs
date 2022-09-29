
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolCooldown : MonoBehaviour
{
    public float cooldown;
    public int leftMouseUse;
    public int rightMouseUse;

    private int leftMouseUseDefault;
    private int rightMouseUseDefault;
    private bool doOnce = true;

    private void Start()
    {
        leftMouseUseDefault = leftMouseUse;
        rightMouseUseDefault = rightMouseUse;
    }

    private void OnEnable()
    {
        if (!doOnce)
        {
            leftMouseUse = leftMouseUseDefault;
            rightMouseUse = rightMouseUseDefault;
        }

        else
        {
            doOnce = false;
        }
    }
}
