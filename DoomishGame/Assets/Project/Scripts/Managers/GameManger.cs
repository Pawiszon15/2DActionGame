using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Cinemachine;

class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public Vector3 whereToSpawnPlayer;
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
        howManyEnemies = FindObjectsOfType<BasicEnemy>().Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r pressed");
            ResetScene();
            StartCoroutine(WaitForSecond());
        }
    }

    public void KilledEnemy()
    {
        howManyEnemies--;
        //CheckForWin();
    }

    //private void CheckForWin()
    //{
    //    if(0 >= howManyEnemies)
    //    {
    //        Time.timeScale = 0f;
    //    }
    //}

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SavePlayerPosition(Vector3 transform)
    {
        Instance.whereToSpawnPlayer = transform;
    }

    IEnumerator WaitForSecond()
    {       
        yield return new WaitForSeconds(0.05f);
        player = FindObjectOfType<PlayerMovement>().gameObject;
        player.transform.position = whereToSpawnPlayer;
    }
}
