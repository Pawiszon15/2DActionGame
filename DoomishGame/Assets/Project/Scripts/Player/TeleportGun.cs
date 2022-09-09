using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : MonoBehaviour
{
    [SerializeField] Transform gunPosition;
    [SerializeField] ResourceDisplayer resourceDisplayer;
    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletLifeTime;
    [SerializeField] int teleporterEnergyCost;

    private Vector2 bulletPos;
    private GameObject activeBullet;
    int currentEnergy = ShootingMechanic.currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        activeBullet = null;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && activeBullet == null && currentEnergy >= teleporterEnergyCost)
        {
            Debug.Log("hello");
            currentEnergy -= teleporterEnergyCost;
            resourceDisplayer.ChangeResourceAmount(currentEnergy);

            activeBullet = Instantiate(Bullet, gunPosition.position, gunPosition.rotation);
            Destroy(activeBullet, bulletLifeTime);
        }

        else if (Input.GetKeyDown(KeyCode.F) && activeBullet != null)
        {
            TeleportPlayer();
        }

    }

    void TeleportPlayer()
    {
        gameObject.transform.position = activeBullet.transform.position;
        Destroy(activeBullet);
        activeBullet = null;
    }
}
