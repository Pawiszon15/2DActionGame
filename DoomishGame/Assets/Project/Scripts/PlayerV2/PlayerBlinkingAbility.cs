using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkingAbility : MonoBehaviour
{
    [Header("Ability properties")]
    public float maxTimeOfSlowmo;
    [SerializeField][Range(0, 1)] float slowmoEffect;
    [SerializeField] float multiplayerToPreviewSpeed;
    [SerializeField] float blinkingDistance;
    [SerializeField] float hangTimeAfterBlink;

    [Header("Offensive ability")]
    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform bulletShotPos;
    [SerializeField] LayerMask enemyLayers;

    [Header("Resources")]
    [SerializeField] int blinkCost;
    [SerializeField] int maxResourcesAmount;

    [Header("References")]
    [SerializeField] GameObject previewOfBlink;

    [HideInInspector] public float ongoingBlinkDuration;
    [HideInInspector] public bool ongoingBlink;
    [HideInInspector] public bool afterBlink = false;
    [HideInInspector] public int currentReousrceAmount;
    private GameObject instancesOfBlink;
    private Vector2 moveInput;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerBlinkingAbilityUI ui;

    private Vector2 beforeBlinkTransfrom;
    private Vector2 afterBlinkTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        ui = FindObjectOfType<PlayerBlinkingAbilityUI>();
    }

    private void Start()
    {
        ui.TurnOffAllResources();
    }

    // Update is called once per frame
    void Update()
    { 
        ongoingBlinkDuration -= Time.deltaTime;

        HandleInputs();
        PrevieOfBlink();
    }

    private void PrevieOfBlink()
    {
        if(ongoingBlink)
        {
            instancesOfBlink.transform.position = new Vector2(transform.position.x, transform.position.y) +
            moveInput * blinkingDistance;
        }
    }

    private void HandleInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");



        if (Input.GetKeyDown(KeyCode.X))
        {
            if (blinkCost <= currentReousrceAmount)
            {
                currentReousrceAmount -= blinkCost;
                ui.SetCurrentResources(currentReousrceAmount);
                ui.TurnBlinkSlider(true);

                ongoingBlinkDuration = maxTimeOfSlowmo;
                instancesOfBlink = Instantiate(previewOfBlink, transform.position, Quaternion.identity);
                instancesOfBlink.transform.parent = transform;
                ongoingBlink = true;
                StartCoroutine(StartAbility());
            }

        }
            if (Input.GetKeyUp(KeyCode.X) && ongoingBlink)
            {
                StopCoroutine(StartAbility());
                if (ongoingBlink)
                {
                    EndAbility();
                }
            }
        
    }

    IEnumerator StartAbility()
    {
        Debug.Log("start sth");
        Time.timeScale = slowmoEffect;
        yield return new WaitForSeconds(maxTimeOfSlowmo);
        if (ongoingBlink)
        {
            EndAbility();
        }
    }

    private void EndAbility()
    {
        beforeBlinkTransfrom = transform.position;
        //Instantiate(playerBullet, bulletShotPos.position, Quaternion.identity);
        Destroy(instancesOfBlink);
        ongoingBlink = false;
        afterBlink = true;

        transform.position = new Vector2(transform.position.x, transform.position.y) +
        (moveInput * blinkingDistance);

        afterBlinkTransform = transform.position;
        ongoingBlinkDuration = 0f;

        ui.TurnBlinkSlider(false);
        KillEnemiesAlongTheWay();
        StartCoroutine(WaitForSec());
    }

    private void KillEnemiesAlongTheWay()
    {
        RaycastHit2D[] hits2D = Physics2D.LinecastAll(beforeBlinkTransfrom, afterBlinkTransform, enemyLayers);
        
        Debug.Log(hits2D.Length);
        Debug.DrawLine(beforeBlinkTransfrom, afterBlinkTransform);

        foreach(RaycastHit2D hit in hits2D)
        {
            if(hit.collider.TryGetComponent(out BasicEnemy enemy))
            {
                enemy.StartDeathAnimation();
            }
        }
    }

    IEnumerator WaitForSec()
    {
        rb.velocity = Vector2.zero;
        afterBlink = true;
        yield return new WaitForSeconds(hangTimeAfterBlink);
        Time.timeScale = 1f;
        afterBlink = false;
    }

    public void AddResources()
    {
        if (currentReousrceAmount != maxResourcesAmount)
        {
            currentReousrceAmount++;

        }
        ui.SetCurrentResources(currentReousrceAmount);

    }

    
}
