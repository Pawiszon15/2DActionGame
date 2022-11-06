using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] LayerMask maskToCheck;
    [SerializeField] Transform raycastFirePoint;
    private bool canCheckAgain = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            HomingRocket homingRocket = collision.gameObject.GetComponent<HomingRocket>();
            if(homingRocket)
            {
                homingRocket.CreateExplosion();
                Destroy(collision.gameObject);
            }

            else
            {
                Destroy(collision.gameObject);
            }
        }

        if(collision.gameObject.GetComponent<Grenade>())
        {
            Debug.Log("Grenade was hit by katana slash");
            Grenade grenade = collision.gameObject.GetComponent<Grenade>();
            grenade.ThrowInDirection(gameObject.transform.right);
        }

        //if(collision.gameObject.tag == "EnemyShield")
        //{
        //    //RaycastHit2D raycast = Physics2D.Raycast(raycastFirePoint.position, transform.right, 20f, maskToCheck);
        //    //Debug.Log(raycast.collider.gameObject.name);

        //    Collider2D whatItIs = Physics2D.OverlapCircle(new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y), 0.3f, maskToCheck);

        //    if (whatItIs.gameObject.GetComponent<BasicEnemy>())
        //    {
        //        BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();
        //        enemy.KillEnemy();
        //    }
            
        //    else if(whatItIs.gameObject.GetComponent<ProtectorShield>())
        //    {
        //        Debug.Log("ShieldHit");
        //    }
                 
        //}
    }

    IEnumerator DoItOnce()
    {
        canCheckAgain = false;
        yield return new WaitForSeconds(0.1f);
        canCheckAgain = true;
    }

}
