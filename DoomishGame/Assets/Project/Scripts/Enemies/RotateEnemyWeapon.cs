using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemyWeapon : MonoBehaviour
{
    [SerializeField] Transform gunPivot;
    [SerializeField] float rotationSpeed;

    private Player player;
    private Vector2 playerPos;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        RotateGun(playerPos);
    }

    void RotateGun(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }
}
