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


    private PlayerMovement player;

    [HideInInspector] public bool isEnemyReadyToShoot = false;
    [HideInInspector] public bool isThereLineOfSight = false;

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
        }

        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void TryTurnOnLogic()
    {
        if (!isEnemyActivated)
        {
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            Debug.Log("distance to player is - " + distanceToPlayer);

            if (distanceToActivate > distanceToPlayer)
            {
                Debug.Log("distance to player is - " + distanceToPlayer);
                isEnemyActivated = true;
            }

        }
    }

    private void CheckWhetherEnemyCanShot()
    {
        if (isEnemyActivated)
        {
            if (isEnemyReadyToShoot)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(shootPoint.position, shootPoint.right, 100f, layers);
                if (hit2D.collider.TryGetComponent(out PlayerMovement playerFound))
                {
                    isThereLineOfSight = true;
                }

                else
                {
                    isThereLineOfSight = false;
                    Debug.Log(hit2D.collider.gameObject.name);
                }

                //Debug.DrawRay(shootPoint.position, shootPoint.right * 100f, Color.red, 0.3f);
                Debug.Log("is there line of sight - " + isThereLineOfSight);
            }
        }
    }

    IEnumerator CheckDiffrentCondition()
    {
        TryTurnOnLogic();
        Turn();
        CheckWhetherEnemyCanShot();

        yield return new WaitForSeconds(timeToEvaulateBehaviour);

        Debug.Log("Evaluate");
        StartCoroutine(CheckDiffrentCondition());
    }
}
