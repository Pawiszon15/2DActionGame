using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : MonoBehaviour
{
    [SerializeField] Transform gunPosition;

    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletLifeTime;

    private Vector2 bulletPos;
    private bool isBulletActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && !isBulletActive)
        {
            GameObject thisBullet = Instantiate(Bullet, gunPosition.position, gunPosition.rotation);
            Destroy(thisBullet, bulletLifeTime);

        }

        if (Input.GetKeyDown(KeyCode.F) && isBulletActive)
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
/*        gameObject.transform.position;
*/    }
}
