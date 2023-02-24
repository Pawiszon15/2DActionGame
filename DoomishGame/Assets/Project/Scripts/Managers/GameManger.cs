using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;

class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public Vector3 whereToSpawnPlayer;
    public string whichCameraToChose = null;
    public float savedLevelDuration;
    
    [SerializeField] int howManyEnemies;
    [SerializeField] Image image;
    

    private GameObject player;


    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MovePlayer();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r pressed");
            ResetScene();
        }


    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SavePlayerPosition(Vector3 transform, CinemachineVirtualCamera virtualCamera, float currentTime)
    {
        Instance.whereToSpawnPlayer = transform;
        whichCameraToChose = virtualCamera.name;
        savedLevelDuration = currentTime;
    }

    public void WinTheLevel()
    {
        Time.timeScale = 0f;
    }

    public void OnLevelWasLoaded(int level)
    {
        new WaitForSeconds(1f);

        if (whichCameraToChose != null)
        {
            FindObjectOfType<TimeDisplay>().setTimerAfter(savedLevelDuration);

            player = FindObjectOfType<PlayerMovement>().gameObject;
            player.transform.position = whereToSpawnPlayer;

            GameObject cameraVirtualAfterRestart = GameObject.Find(whichCameraToChose);
            cameraVirtualAfterRestart.GetComponent<CinemachineVirtualCamera>().Priority = 20;
            FindObjectOfType<CinemaShakes>().GetHighestPriorityVirtualCamera();
        }
    }
    public void MovePlayer()
    {

    }
}
