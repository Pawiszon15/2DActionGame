using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLineRenderer : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    LineRenderer lineRenderer;
    EnemyActivation enemyActivation;

    
    void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        lineRenderer = this.GetComponent<LineRenderer>();
        enemyActivation = this.GetComponent<EnemyActivation>();
    }   

    public void SetEdgeCollider(Vector2 point1,Vector2 point2)
    {
        List<Vector2> edges = new List<Vector2>();
        Vector2 secondPoint = Vector2.zero;

        secondPoint =(point2 - new Vector2(transform.position.x, transform.position.y));
        edges.Add((point1 - new Vector2(transform.position.x, transform.position.y)));
        edges.Add(new Vector2(enemyActivation.isFacingRight * secondPoint.x, secondPoint.y));

        edgeCollider.SetPoints(edges);
    }

}
