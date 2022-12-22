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
    [HideInInspector] public bool isEnemyReadyToShoot = false;
    [HideInInspector] public bool isThereLineOfSightAndInRange = false;

    private bool isEnemyActivated = false;
    private float distanceToPlayer = 0f;


    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckDiffrentCondition());
    }

    private void Turn()
    {
        
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = 1f;
        }

        else
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
                if (hit2D.collider.TryGetComponent(out PlayerMovement playerFound))
                {
                    isThereLineOfSightAndInRange = true;
                }

                else
                {
                    isThereLineOfSightAndInRange = false;
                }

                //Debug.DrawRay(shootPoint.position, shootPoint.right * 100f, Color.red, 0.3f);
                
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
