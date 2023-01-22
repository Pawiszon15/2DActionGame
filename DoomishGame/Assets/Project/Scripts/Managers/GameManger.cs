using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public Vector3 whereToSpawnPlayer;
    public string whichCameraToChose;
    public float savedLevelDuration;
    [SerializeField] int howManyEnemies;
    

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
        StartCoroutine(WaitForSecond());
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

    IEnumerator WaitForSecond()
    {       
        yield return new WaitForSeconds(0.3f);

        FindObjectOfType<TimeDisplay>().setTimerAfter(savedLevelDuration);

        player = FindObjectOfType<PlayerMovement>().gameObject;
        player.transform.position = whereToSpawnPlayer;

        GameObject cameraVirtualAfterRestart =  GameObject.Find(whichCameraToChose);
        cameraVirtualAfterRestart.GetComponent<CinemachineVirtualCamera>().Priority = 20;
    }
}
