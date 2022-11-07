using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] private List<GameObject> allEnemies;

    [SerializeField] List<Enemuy_FloorDeneyer> allFloorDenayers;
    [SerializeField] List<Enemy_Pistol> allPistols;
    [SerializeField] List<Enemy_RocketLuncher> allRocketLunchers;
    [SerializeField] List<RailGunner> allRailGunners;

    private bool isFirstEnemy;
    private TimeDisplay timeDisplay;
    
    private Tutorial_GrapplingGun grapplingHook;
    private ShootingMechanic shootingMechanic;

    private void Awake()
    {
/*        foreach (Enemuy_FloorDeneyer denayer in allFloorDenayers)
        {
            denayer.enabled = false;
        }

        foreach (Enemy_Pistol pistol in allPistols)
        {
            pistol.enabled = false;
        }

        foreach (Enemy_RocketLuncher RocketLuncher in allRocketLunchers)
        {
            RocketLuncher.enabled = false;
        }

        foreach (RailGunner railGunner in allRailGunners)
        {
            railGunner.enabled = false;
        }*/

        //isFirstEnemy = true;
    }

    private void Start()
    {
        timeDisplay = FindObjectOfType<TimeDisplay>();
        grapplingHook = FindObjectOfType<Tutorial_GrapplingGun>();
        shootingMechanic = FindObjectOfType<ShootingMechanic>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void KilledEnemy(string enemyName)
    {
        if(isFirstEnemy)
        {
            ActivateEnemies();
            timeDisplay.TurnOffTimerBeforeAlarm();
            isFirstEnemy = false;
        }

        GameObject temp = allEnemies.Where(obj => obj.name == enemyName).SingleOrDefault();
        allEnemies.Remove(temp);

        CheckForWin();
    }

    private void CheckForWin()
    {
        if(0 >= allEnemies.Count)
        {
            Time.timeScale = 0f;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateEnemies()
    {
        foreach (Enemuy_FloorDeneyer denayer in allFloorDenayers)
        {
            denayer.enabled = true;
        }

        foreach (Enemy_Pistol pistol in allPistols)
        {
            pistol.enabled = true;
        }

        foreach (Enemy_RocketLuncher RocketLuncher in allRocketLunchers)
        {
            RocketLuncher.enabled = true;
        }

        foreach (RailGunner railGunner in allRailGunners)
        {
            railGunner.enabled = true;
        }
    }
}
