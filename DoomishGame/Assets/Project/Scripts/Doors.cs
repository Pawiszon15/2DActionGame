using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] int enemiesToOpenTheDoor;
    [SerializeField] GameObject whereToMovePlayers;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] CinemachineVirtualCamera cameraToActivate;

    private GameManger gameManager;
    private bool doorOpen = false;
    private Animator animator;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManger>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (enemiesToOpenTheDoor == 0)
        {
            OpenTheDoor();
        }
    }

    public void enemyKilled()
    {
        enemiesToOpenTheDoor--;
        if (enemiesToOpenTheDoor == 0)
        {
            OpenTheDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement player) && doorOpen)
        {
            player.gameObject.transform.position = whereToMovePlayers.transform.position;
            cameraToActivate.Priority = currentCamera.Priority;
            currentCamera.Priority = 0;
            gameManager.SavePlayerPosition(whereToMovePlayers.transform.position);
        }
    }

    private void OpenTheDoor()
    {
        doorOpen = true;
        animator.SetBool("DoorOpen", true);
    }
}
