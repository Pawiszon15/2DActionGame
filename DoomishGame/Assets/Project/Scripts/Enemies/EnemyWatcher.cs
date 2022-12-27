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
    [SerializeField] Transform UpShootPoint, downShootPoint;

    [HideInInspector] public bool shouldUseLaser = false;
    private EnemyAnimator animator;
    private EnemyActivation enemyActivation;
    private Vector3 positionUpPoint, positionDownPoint;

    private void Awake()
    {
        enemyActivation = GetComponent<EnemyActivation>();
        animator = GetComponent<EnemyAnimator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        positionDownPoint = new Vector3(downShootPoint.position.x, downShootPoint.position.y, downShootPoint.position.z);
        positionUpPoint = new Vector3(UpShootPoint.position.x, UpShootPoint.position.y, UpShootPoint.position.z);
        SetBeginingRotation(positionUpPoint);
        StartCoroutine(AbilityCooldown());
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
        RotateGun(positionDownPoint);

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
        StartCoroutine(StopLaserAttack());
        shouldUseLaser = true;
    }

    void CheckWhetherItEndedAttack()
    {

    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        enemyActivation.isEnemyReadyToShoot = true;
    }

    IEnumerator StopLaserAttack()
    {
        SetBeginingRotation(positionUpPoint);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(durationOfLaserAttack);
        lineRenderer.enabled = false;
        shouldUseLaser = false;
        StartCoroutine(AbilityCooldown());
    }
       
}

