using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkingAbility : MonoBehaviour
{
    [Header("Ability properties")]
    [SerializeField][Range(0, 1)] float slowmoEffect;
    [SerializeField] float blinkingDistance;
    [SerializeField] float maxTimeOfSlowmo;
    [SerializeField] float hangTimeAfterBlink;

    [Header("Offensive ability")]
    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform bulletShotPos;

    [Header("Resources")]
    [SerializeField] int blinkCost;
    [SerializeField] int maxResourcesAmount;

    [HideInInspector] public bool ongoingBlink;
    [HideInInspector] public bool afterBlink = false;
    private int currentReousrceAmount;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private float ongoingBlinkDuration;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        ongoingBlinkDuration += Time.deltaTime;
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
                ongoingBlink = true;
                ongoingBlinkDuration = maxTimeOfSlowmo;
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
        ongoingBlink = false;
        afterBlink = true;
       
        transform.position = new Vector2(transform.position.x, transform.position.y) + 
        (moveInput * blinkingDistance *  ongoingBlinkDuration);

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
