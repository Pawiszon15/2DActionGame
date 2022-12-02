using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAbility : MonoBehaviour
{
    
    [SerializeField] float deflectRange;
    [SerializeField] float defectTime;
    [SerializeField] float deflectCooldown;
    [HideInInspector] public bool isDeflecting = false;
    private int numberOfDeflects = 1;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && numberOfDeflects > 0 && !isDeflecting)
        {
            isDeflecting = true;
            StartCoroutine(DeflectTime());
        }
        
        if(isDeflecting)
        {
            DeflectEnemiesBullet();
        }    

    }

    private void DeflectEnemiesBullet()
    {
        var overlapedBullets = Physics2D.OverlapCircleAll(transform.position, deflectRange);

        foreach(var overlapedBullet in overlapedBullets)
        {

            if(overlapedBullet.gameObject.GetComponent<EnemyBullet>())
            {
                GameObject gameObject = overlapedBullet.gameObject;


            }
        }
    }

    IEnumerator DeflectTime()
    {
        isDeflecting = true;
        yield return new WaitForSeconds(defectTime);
        isDeflecting = false;

        yield return new WaitForSeconds(deflectCooldown);
    }
}
