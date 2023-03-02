using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyWatcher : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float rayDist;
    [SerializeField] Transform laserFirePoint;
    LineRenderer lineRenderer;

    [Header("Weapon properties")]
    [SerializeField] float durationOfLaserAttack;
    [SerializeField] float abilityCooldown;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform gunPivot;
    [SerializeField] GameObject positionUpPoint, positionDownPoint;
    private bool isShootingFromDownPoint;

    [HideInInspector] public bool shouldUseLaser = false;
    private EnemyAnimator animator;
    private EnemyActivation enemyActivation;
    private ColliderLineRenderer colliderLineRenderer;

    private void Awake()
    {
        enemyActivation = GetComponent<EnemyActivation>();
        animator = GetComponent<EnemyAnimator>();
        colliderLineRenderer = GetComponent<ColliderLineRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        isShootingFromDownPoint = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyActivation.isEnemyReadyToShoot && enemyActivation.isThereLineOfSightAndInRange)
        {
            enemyActivation.isEnemyReadyToShoot = false;
            enemyActivation.isThereLineOfSightAndInRange = false;
            animator.StartAttackAnimation();
        }

        if (shouldUseLaser)
        {            
            ShootLaser();
        }
    }


    private void ShootLaser()
    {
        if(isShootingFromDownPoint)
            RotateGun(positionUpPoint.transform.position);
        else
            RotateGun(positionDownPoint.transform.position);

        //Debug.DrawRay(laserFirePoint.position, laserFirePoint.right * 100f, Color.red,  1f);
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.right, 100f, layer);
            Draw2DRay(laserFirePoint.position, hit.point);

            if(hit.collider.gameObject.TryGetComponent(out Player player))
            {
                player.TryToKillPlayer();
            }
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
        colliderLineRenderer.SetEdgeCollider(startPos, endPos);
    }

    void SetBeginingRotation(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void RotateGun(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }

    void StartOfTheAttack()
    {
        if(isShootingFromDownPoint)
            SetBeginingRotation(positionDownPoint.transform.position);
        else
            SetBeginingRotation(positionUpPoint.transform.position);

        StartCoroutine(LaserAttack());
    }

    void ChangDestinationPoint()
    {
        if(isShootingFromDownPoint)
            isShootingFromDownPoint = false;
        else
            isShootingFromDownPoint = true;
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        enemyActivation.isEnemyReadyToShoot = true;
    }

    IEnumerator LaserAttack()
    {
        enemyActivation.ongoingShoot = true;
        shouldUseLaser = true;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(durationOfLaserAttack);
        enemyActivation.ongoingShoot = false;
        lineRenderer.enabled = false;
        shouldUseLaser = false;
        StartCoroutine(AbilityCooldown());
        ChangDestinationPoint();
    }
       
}

