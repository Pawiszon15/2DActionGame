using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemiesWithShields;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in enemiesWithShields)
        {
            enemy.GetComponent<BasicEnemy>().TurnOnShield();
        }
    }
    
    private void ShieldGeneratorDestroyed()
    {
        foreach (GameObject enemy in enemiesWithShields)
        {
            enemy.GetComponent<BasicEnemy>().TurnOffShield();
        }
    }

}
