using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeaponCollider : MonoBehaviour
{

    public void RotateWeapon(Vector2 dir)
    {
        transform.right = dir;
    }
}
