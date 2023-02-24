using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{    
    [SerializeField] int enemiesToOpenTheDoor;
    [SerializeField] bool isThisFinalDoor;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] CinemachineVirtualCamera cameraToActivate;
    [SerializeField] Transform whereToMovePlayers;

    private Vector2 VectorWhereToMovePlayer;
    private GameObject PlayerPointDoors;
    private GameManger gameManager;
    private bool doorOpen = false;
    private Animator animator;
    private GameObject playerController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManger>();
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Start()
    {
        VectorWhereToMovePlayer = new Vector2(whereToMovePlayers.transform.position.x, whereToMovePlayers.transform.position.y);

        if (enemiesToOpenTheDoor == 0)
        {
            OpenTheDoor();
        }
    }

    public void HowManyEnemiesAreThere()
    {
        enemiesToOpenTheDoor++;
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
            if (isThisFinalDoor)
            {
                gameManager.WinTheLevel();
            }

            else
            {
                cameraToActivate.Priority = currentCamera.Priority;
                currentCamera.Priority = 0;
                gameManager.SavePlayerPosition(VectorWhereToMovePlayer, cameraToActivate, FindObjectOfType<TimeDisplay>().timeAfterStartingLevel);
                Time.timeScale = 0.05f;
                FindObjectOfType<CinemaShakes>().GetHighestPriorityVirtualCamera();
                StartCoroutine(WaitForCameraChanged());
            }
        }
    }

    private void OpenTheDoor()
    {
        doorOpen = true;
        animator.SetBool("DoorOpen", true);
    }

    IEnumerator WaitForCameraChanged()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        playerController.transform.position = whereToMovePlayers.position;
        Time.timeScale = 1f;
    }
}
