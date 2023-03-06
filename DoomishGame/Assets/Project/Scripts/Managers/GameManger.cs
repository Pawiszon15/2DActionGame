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

    public Doors currentDoors;

    private GameObject player;
    private Vector2 playerPos;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        Time.timeScale = 0f;
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r pressed");
            ResetScene();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Time.timeScale = 1;
        }
    }

    public void ResetScene()
    {
        PlayerDie();
    }

    public void SavePlayerPosition(Vector2 newPlayerSpawn, Doors nextDoors)
    {
        playerPos = newPlayerSpawn;
        currentDoors = nextDoors;
        
    }

    public void WinTheLevel()
    {
        Time.timeScale = 0f;
    }

    //public void OnLevelWasLoaded()
    //{
    //    StartCoroutine(WaitOnLevelLoaded());
    //}

    //IEnumerator WaitOnLevelLoaded()
    //{
    //    yield return new WaitForSecondsRealtime(0.2f);

    //    if (whichCameraToChose != null)
    //    {
    //        FindObjectOfType<TimeDisplay>().setTimerAfter(savedLevelDuration);

    //        player = FindObjectOfType<PlayerMovement>().gameObject;
    //        player.transform.position = whereToSpawnPlayer;

    //        GameObject cameraVirtualAfterRestart = GameObject.Find(whichCameraToChose);
    //        cameraVirtualAfterRestart.GetComponent<CinemachineVirtualCamera>().Priority = 20;
    //        FindObjectOfType<CinemaShakes>().GetHighestPriorityVirtualCamera();
    //    }
    //}

    public void PlayerDie()
    {
        RespawnEnemies();
        SpawnPlayerInRightSpot();
        DestroyAllBullets();
        StopTime();
    }

    private void SpawnPlayerInRightSpot()
    {
        player.transform.position = playerPos;
    }

    private void CloseAllDoors()
    {
        throw new NotImplementedException();
    }

    private void RespawnEnemies()
    {
        if(currentDoors != null)
            currentDoors.RestartEnemiesOnLevel();
    }

    public void DestroyAllBullets()
    {
        TempElements[] tempArray = FindObjectsOfType<TempElements>();

        for(int i = 0; i < tempArray.Length; i++)
        {
            Destroy(tempArray[i].gameObject);
        }
        StopTime();
    }    
    public void StopTime()
    {
        Time.timeScale = 0f;
    }

}
