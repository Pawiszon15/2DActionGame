using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerBlinkingAbility : MonoBehaviour
{
    [Header("Ability properties")]
    public float maxTimeOfSlowmo;
    [SerializeField][Range(0, 1)] float slowmoEffect;
    [SerializeField] float multiplayerToPreviewSpeed;
    [SerializeField] float blinkingDistance;
    [SerializeField] float hangTimeAfterBlink;
    [SerializeField] int howOftenCollisionCheck;
    [SerializeField] Vector2 sizeOfPlayersBoxCollision;
    [SerializeField] bool[] wherePlayerCanBeSpawned;
    [SerializeField] LayerMask layersToCheckWithAbility;
    private int temp;

    [Header("Offensive ability")]
    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform bulletShotPos;
    [SerializeField] LayerMask enemyLayers;

    [Header("Resources")]
    [SerializeField] int blinkCost;
    [SerializeField] int maxResourcesAmount;

    [Header("References")]
    [SerializeField] LayerMask layerToCheck;
    [SerializeField] GameObject previewOfBlink;
    [SerializeField] Transform placeOfCheck;
    [HideInInspector] public float ongoingBlinkDuration;
    [HideInInspector] public bool ongoingBlink;
    [HideInInspector] public bool afterBlink = false;
    [HideInInspector] public int currentReousrceAmount;
    public Vector2 whereToSpawnPlayer;
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

        if (ongoingBlink)
        {
            CheckWherePlayerCanBeSpawned();
            PrevieOfBlink();
        }

    }

    private void CheckWherePlayerCanBeSpawned()
    {
        float distanceIntervals = blinkingDistance / howOftenCollisionCheck;
        Debug.Log(distanceIntervals);
        //check for point avaialibity
        for (int i = 0; i < howOftenCollisionCheck; i++)
        {
            Debug.Log(1);
            Vector2 whereToCheckCollision = new Vector2(transform.position.x, transform.position.y) + (moveInput * (i * distanceIntervals));
            wherePlayerCanBeSpawned[i] = Physics2D.OverlapBox(whereToCheckCollision, sizeOfPlayersBoxCollision, 0f, layersToCheckWithAbility);
        }
        
        Debug.Log(wherePlayerCanBeSpawned);
        for(int i = wherePlayerCanBeSpawned.Length ; i > 0; i--)
        {
            Debug.Log(2);
            if (wherePlayerCanBeSpawned[i-1] == false)
            {            
                whereToSpawnPlayer = new Vector2(transform.position.x, transform.position.y) + (moveInput * (temp * distanceIntervals));
                Debug.Log(whereToSpawnPlayer);
                temp = i;
                break;
            }
            

        }


    }


    private void PrevieOfBlink()
    {
        instancesOfBlink.transform.position = whereToSpawnPlayer;   
    }

    private void HandleInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Z))
        {
            CheckWherePlayerCanBeSpawned();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (blinkCost <= currentReousrceAmount)
            {
                HandleResources();
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

    private void ShootLaser()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        RaycastHit2D hit = Physics2D.Raycast(placeOfCheck.position, dir, 100f, layerToCheck);   
    }

    private void HandleResources()
    {
        currentReousrceAmount -= blinkCost;
        ui.SetCurrentResources(currentReousrceAmount);
        ui.TurnBlinkSlider(true);
    }
    IEnumerator StartAbility()
    {
        ongoingBlinkDuration = maxTimeOfSlowmo;
        instancesOfBlink = Instantiate(previewOfBlink, transform.position, Quaternion.identity);
        instancesOfBlink.transform.parent = transform;
        ongoingBlink = true;

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

        transform.position = whereToSpawnPlayer;

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
