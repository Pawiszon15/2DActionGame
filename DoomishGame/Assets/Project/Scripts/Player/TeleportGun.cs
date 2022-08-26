using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : MonoBehaviour
{
    [SerializeField] Transform gunPosition;

    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletLifeTime;

    private Vector2 bulletPos;
    private GameObject activeBullet;

    // Start is called before the first frame update
    void Start()
    {
        activeBullet = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && activeBullet == null)
        {
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
