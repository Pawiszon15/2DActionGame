using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] int howManyEnemies;
    //private bool isFirstEnemy;
    //private TimeDisplay timeDisplay;
    
    //private Tutorial_GrapplingGun grapplingHook;
    //private ShootingMechanic shootingMechanic;

    private void Start()
    {
        howManyEnemies = FindObjectsOfType<BasicEnemy>().Length;
        //timeDisplay = FindObjectOfType<TimeDisplay>();
        //grapplingHook = FindObjectOfType<Tutorial_GrapplingGun>();
        //shootingMechanic = FindObjectOfType<ShootingMechanic>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void KilledEnemy()
    {
        howManyEnemies--;
        CheckForWin();
    }

    private void CheckForWin()
    {
        if(0 >= howManyEnemies)
        {
            Time.timeScale = 0f;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
