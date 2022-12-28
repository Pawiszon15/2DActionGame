using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkingAbility : MonoBehaviour
{
    [Header("Ability properties")]
    [SerializeField][Range(0, 1)] float slowmoEffect;
    [SerializeField] float multiplayerToPreviewSpeed;
    [SerializeField] float blinkingDistance;
    [SerializeField] float maxTimeOfSlowmo;
    [SerializeField] float hangTimeAfterBlink;

    [Header("Offensive ability")]
    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform bulletShotPos;

    [Header("Resources")]
    [SerializeField] int blinkCost;
    [SerializeField] int maxResourcesAmount;

    [Header("References")]
    [SerializeField] GameObject previewOfBlink;

    [HideInInspector] public bool ongoingBlink;
    [HideInInspector] public bool afterBlink = false;
    private GameObject instancesOfBlink;
    private int currentReousrceAmount;
    private Vector2 moveInput;
    private float ongoingBlinkDuration;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    { 
        ongoingBlinkDuration += multiplayerToPreviewSpeed * Time.deltaTime;

        HandleInputs();
        PrevieOfBlink();
    }

    private void PrevieOfBlink()
    {
        if(ongoingBlink)
        {
            instancesOfBlink.transform.position = new Vector2(transform.position.x, transform.position.y) +
            moveInput * blinkingDistance * ongoingBlinkDuration;
        }
    }

    private void HandleInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //idea diffrent ability for no resources?
        if (blinkCost <= currentReousrceAmount)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {            
                ongoingBlinkDuration = 0f;
                instancesOfBlink = Instantiate(previewOfBlink, transform.position, Quaternion.identity);
                instancesOfBlink.transform.parent = transform;
                ongoingBlink = true;
                StartCoroutine(StartAbility());
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                StopCoroutine(StartAbility());
                if (ongoingBlink)
                {
                    EndAbility();
                }
            }
        }
    }

    IEnumerator StartAbility()
    {
        Debug.Log("start sth");
        Time.timeScale = slowmoEffect;
        yield return new WaitForSecondsRealtime(maxTimeOfSlowmo);
        if (ongoingBlink)
        {
            EndAbility();
        }
    }

    private void EndAbility()
    {
        Instantiate(playerBullet, bulletShotPos.position, Quaternion.identity);
        Destroy(instancesOfBlink);
        ongoingBlink = false;
        afterBlink = true;

        transform.position = new Vector2(transform.position.x, transform.position.y) +
        (moveInput * blinkingDistance * ongoingBlinkDuration);

        ongoingBlinkDuration = 0f;
        StartCoroutine(WaitForSec());
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
        if (currentReousrceAmount == maxResourcesAmount)
            currentReousrceAmount++;
    }
}
