using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyWatcher : MonoBehaviour
{
    [SerializeField] private float rayDist;
    [SerializeField] Transform laserFirePoint;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ShootLaser()
    {
        if( Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            Draw2DRay(laserFirePoint.position, hit.point);
        }
        
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.right * rayDist);
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
