using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private float time;
    private int numberOfEnemies;
    private int numberOfKilledEnemies;

    // Start is called before the first frame update
    void Start()
    {
        BasicEnemy[] enemies = FindObjectsOfType<BasicEnemy>();
        
        numberOfEnemies = enemies.Length;
        numberOfKilledEnemies = 0;

        Debug.Log("enmies on map" + numberOfEnemies);
        Debug.Log("How many enemies you've killed" + numberOfKilledEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
    }

    public void KilledEnemy()
    {
        numberOfKilledEnemies = numberOfKilledEnemies + 1;
        Debug.Log("How many enemies you've killed" + numberOfKilledEnemies);
        CheckForWin();    
    }

    private void CheckForWin()
    {
        if(numberOfKilledEnemies >= numberOfEnemies)
        {
            Debug.Log("Good job! Your time is" + time);
            Time.timeScale = 0;
        }
    }
}
