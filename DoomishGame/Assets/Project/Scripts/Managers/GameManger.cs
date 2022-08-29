using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    private float time;
    private int numbersOfEnemies;
    [SerializeField] private List<GameObject> allEnemies;

    private void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
    }

    public void KilledEnemy(string enemyName)
    {
        GameObject temp = allEnemies.Where(obj => obj.name == enemyName).SingleOrDefault();
        allEnemies.Remove(temp);
        CheckForWin();    
    }

    private void CheckForWin()
    {
        if(0 >= allEnemies.Count)
        {
            Debug.Log("Good job! Your time is " + time);
            Time.timeScale = 0;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
