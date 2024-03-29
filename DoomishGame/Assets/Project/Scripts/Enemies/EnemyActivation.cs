 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform weaponPivot;
    [SerializeField] LayerMask layers;
    [SerializeField] float timeToEvaulateBehaviour;
    [SerializeField] float distanceToActivate;
    [SerializeField] float distanceToAttack;

    private PlayerMovement player;

     [HideInInspector] public float isFacingRight = 1f;
     public bool isEnemyReadyToShoot = true;
     public bool isThereLineOfSightAndInRange = false;
     public bool ongoingShoot = false;

    private bool isEnemyActivated = false;
    private float distanceToPlayer = 0f;


    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isEnemyReadyToShoot = true;
        StartCoroutine(CheckDiffrentCondition());
    }

    private void Turn()
    {
        
        if (player.transform.position.x > transform.position.x && !ongoingShoot)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = 1f;
        }

        else if(!ongoingShoot)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = -1f;
        }
    }

    private void TryTurnOnLogic()
    {
        if (!isEnemyActivated)
        {
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

            if (distanceToActivate > distanceToPlayer)
            {
                isEnemyActivated = true;
            }

        }
    }

    private void CheckWhetherEnemyCanAttack()
    {
        if (isEnemyActivated)
        {

            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            //Debug.Log(isEnemyReadyToShoot);
            //Debug.Log(distanceToPlayer);

            if (distanceToPlayer < distanceToAttack)
            {
                

                RaycastHit2D hit2D = Physics2D.Raycast(shootPoint.position, shootPoint.right, 100f, layers);
                if (hit2D.collider.TryGetComponent(out PlayerMovement playerFound) /*|| hit2D.collider.TryGetComponent(out SpecialTarget specialTarget)*/)
                {
                    isThereLineOfSightAndInRange = true;
                }

                else
                {
                    isThereLineOfSightAndInRange = false;
                }
            }
        }
    }

    IEnumerator CheckDiffrentCondition()
    {
        TryTurnOnLogic();
        Turn();
        CheckWhetherEnemyCanAttack();

        yield return new WaitForSeconds(timeToEvaulateBehaviour);

        StartCoroutine(CheckDiffrentCondition());
    }
}
