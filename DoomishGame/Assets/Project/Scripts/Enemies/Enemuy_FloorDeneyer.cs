using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemuy_FloorDeneyer : MonoBehaviour
{
    [Header("Enemy properties")]
    [SerializeField] float movementSpeed;
    [SerializeField] float movementSpeedDuringChase;

    [Header("References")]
    [SerializeField] BoxCollider2D leftObstacleChecker;
    [SerializeField] BoxCollider2D rightObstacleChecker;
    [SerializeField] BoxCollider2D guardedGround;

    private Vector3 dirOfMovement;
    private Vector2 floorDeneyerPos;
    private float currentMovmenetSpeed;

    private void Start()
    {
        dirOfMovement = -transform.right;
        currentMovmenetSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dirOfMovement * currentMovmenetSpeed * Time.deltaTime;
    }

    public void ChangeDirectionToRight()
    {
        dirOfMovement = transform.right;
    }

    public void ChangeDirectionToLeft()
    {
        dirOfMovement = -transform.right;
    }

    public void StartChase(Vector2 playerPos)
    {
        floorDeneyerPos = gameObject.transform.position;
        float playerPosRelativeToEnemy = -floorDeneyerPos.x * playerPos.y + floorDeneyerPos.y * playerPos.x;
        currentMovmenetSpeed = movementSpeedDuringChase;

        if(playerPosRelativeToEnemy > 0)
        {
            ChangeDirectionToRight();
        }

        else if(playerPosRelativeToEnemy < 0)
        {
            ChangeDirectionToLeft();
        }

    }

    public void StopChase()
    {

    }


}
