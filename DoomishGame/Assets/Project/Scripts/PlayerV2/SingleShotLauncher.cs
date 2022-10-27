using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotLauncher : MonoBehaviour
{
    [SerializeField] GameObject singleBullet;
    [SerializeField] GameObject firePoint;

    private AbilitiyCooldown abilityCooldown;

    // Start is called before the first frame update
    void Start()
    {
        abilityCooldown = GetComponent<AbilitiyCooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && abilityCooldown.numberOfUses > 0)
        {
            Instantiate(singleBullet,  firePoint.transform.position, firePoint.transform.rotation);
            abilityCooldown.UseAbility();
        }
    }
}
