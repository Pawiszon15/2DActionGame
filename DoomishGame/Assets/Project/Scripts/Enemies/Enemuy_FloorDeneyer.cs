using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemuy_FloorDeneyer : MonoBehaviour
{
    [Header("Enemy properties")]
    [SerializeField] float movementSpeedDuringPatrol;
    [SerializeField] float movementSpeedDuringChase;

    private Vector3 dirOfMovement;
    private Vector2 floorDeneyerPos;
    private float currentMovmenetSpeed;
    private GameManger gameManger;

    private void Start()
    {
        dirOfMovement = -transform.right;
        currentMovmenetSpeed = movementSpeedDuringPatrol;
        gameManger = FindObjectOfType<GameManger>();
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

    public void ChangeDirectionToOpposite()
    {
        if(dirOfMovement == transform.right)
        {
            dirOfMovement = -transform.right;
        }

        else if(dirOfMovement == -transform.right)
        {
            dirOfMovement = transform.right;
        }
    }

    public void StartChase(Vector2 playerPos)
    {
        floorDeneyerPos = gameObject.transform.position;
        float playerPosRelativeToEnemy = -floorDeneyerPos.x * playerPos.y + floorDeneyerPos.y * playerPos.x;
        currentMovmenetSpeed = movementSpeedDuringChase;

        if(playerPosRelativeToEnemy < 0)
        {
            ChangeDirectionToRight();
        }

        else if(playerPosRelativeToEnemy > 0)
        {
            ChangeDirectionToLeft();
        }

    }

    public void StopChase()
    {
        currentMovmenetSpeed = movementSpeedDuringPatrol;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(collision.gameObject == player)
        {
            gameManger.ResetScene();
        }
    }

}
