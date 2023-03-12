using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    [SerializeField] GameObject[] EnemiesInRoom;
    [HideInInspector] [SerializeField] int enemiesToOpenTheDoor;
    [SerializeField] Doors nextDoors;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] Transform whereToMovePlayers;
    [SerializeField] bool isThisFinalDoor;

    private Vector2 playerPos;
    private int playerRes;

    private Transform[] poitionOfEnemies;
    private GameObject[] enemiesSaver;
    private Vector2 VectorWhereToMovePlayer;
    private GameObject PlayerPointDoors;
    private GameManger gameManager;
    private bool doorOpen = false;
    private Animator animator;
    private GameObject playerController;
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = FindObjectOfType<BlackScreen>().GetComponent<Image>();

        poitionOfEnemies = new Transform[EnemiesInRoom.Length];
        enemiesSaver = new GameObject[EnemiesInRoom.Length];

        gameManager = FindObjectOfType<GameManger>();
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerMovement>().gameObject;

        Debug.Log(poitionOfEnemies.Length);
        SaveEnemiesPostion();
    }

    private void Start()
    {
        VectorWhereToMovePlayer = new Vector2(whereToMovePlayers.transform.position.x, whereToMovePlayers.transform.position.y);

        if (enemiesToOpenTheDoor == 0)
        {
            OpenTheDoor();
        }
    }

    public void enemyKilled()
    {
        Debug.Log(EnemiesInRoom);
        enemiesToOpenTheDoor--;
        if (enemiesToOpenTheDoor == 0)
        {
            OpenTheDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && doorOpen)
        {
            if (isThisFinalDoor)
            {
                gameManager.WinTheLevel();
            }

            else
            {
                //this need a change
                nextDoors.currentCamera.Priority = currentCamera.Priority;
                currentCamera.Priority = 0;
                gameManager.SavePlayerPosition(new Vector2(whereToMovePlayers.position.x, whereToMovePlayers.position.y), nextDoors);
                //Time.timeScale = 0.05f;
                FindObjectOfType<CinemaShakes>().GetHighestPriorityVirtualCamera();
                StartCoroutine(WaitForCameraChanged());
                StartCoroutine(CameraFadeInTime(255f, 0f));

            }
        }
    }

    private void OpenTheDoor()
    {
        doorOpen = true;
        animator.SetBool("DoorOpen", true);
    }

    public void SaveEnemiesPostion()
    {
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            enemiesSaver[i] = EnemiesInRoom[i];
            poitionOfEnemies[i] = enemiesSaver[i].transform;

            GameObject temp = Instantiate(EnemiesInRoom[i], poitionOfEnemies[i].position, Quaternion.identity);
            enemiesSaver[i] = temp;
            temp.SetActive(false);
        }

        enemiesToOpenTheDoor = EnemiesInRoom.Length;
    }

    public void RestartEnemiesOnLevel()
    {
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            enemiesSaver[i].SetActive(true);        
            Destroy(EnemiesInRoom[i]);
            EnemiesInRoom[i] = enemiesSaver[i];
            poitionOfEnemies[i] = enemiesSaver[i].transform;
            
        }

        SaveEnemiesPostion();
    }

    IEnumerator CameraFadeInTime(float startAlpha, float endAlpha)
    {
        float startTime = 0f;
        float endTime = 0.35f;
        Debug.Log("startsth");


        while (startTime < endTime)
        {
            Debug.Log("duringSth");
            startTime += Time.deltaTime;

            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, endAlpha, 0.000001f));
            yield return null;  
        }
    }
    IEnumerator WaitForCameraChanged()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        yield return new WaitForSecondsRealtime(0.35f);
                StartCoroutine(CameraFadeInTime(0, 255f));

        playerController.transform.position = whereToMovePlayers.position;
        //Time.timeScale = 1f;
    }
}
