using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotWeapon : MonoBehaviour
{
    [SerializeField] Transform gunPivot;
    [SerializeField] float rotationSpeed;
    [SerializeField] Camera m_camera;

    private void Update()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        RotateGun(mousePos);
    }

    void RotateGun(Vector3 lookPoint/*, bool allowRotationOverTime*/)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        
        //else
        //{
        //    gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }
}
