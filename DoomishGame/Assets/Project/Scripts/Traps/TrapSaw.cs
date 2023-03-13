using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TrapSaw : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToMove;
    [SerializeField] float sawSpeed;
    [SerializeField] bool HorizontalSaw;
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;

    private Rigidbody2D rb;
    Transform pointToMove;
    Transform secondPoint;
    Transform tempPoint;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        pointToMove = Point1;
        secondPoint = Point2;
    }

    private void FixedUpdate()
    {
        MoveSaw();
        AchivedPoint();
    }

    private void MoveSaw()
    {
        gameObjectToMove.transform.position = Vector2.MoveTowards(gameObjectToMove.transform.position, pointToMove.position, sawSpeed);
    }

    private void AchivedPoint()
    {
        if (Mathf.Round(gameObjectToMove.transform.position.x) == Mathf.Round(pointToMove.position.x) && HorizontalSaw)
        //|| Mathf.Round(gameObjectToMove.transform.position.y) == Mathf.Round(pointToMove.position.y))
        {
            tempPoint = pointToMove;
            pointToMove = secondPoint;
            secondPoint = tempPoint;
        }

        else if (Mathf.Round(gameObjectToMove.transform.position.y) == Mathf.Round(pointToMove.position.y) && !HorizontalSaw)
        {
            tempPoint = pointToMove;
            pointToMove = secondPoint;
            secondPoint = tempPoint;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.TryToKillPlayer();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(Point1.position, Point2.position);
    }
}
