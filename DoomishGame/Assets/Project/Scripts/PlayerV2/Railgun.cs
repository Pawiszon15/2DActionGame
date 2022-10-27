using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Railgun : MonoBehaviour
{
    [Header("Gun properties")]
    [SerializeField] float maxTimeOfCharging;
    [SerializeField] float minRangeOfShot;
    [SerializeField] float lineLifeTime;
    private float time;
    private bool playerReachedMaxTime;

    [Header("References")]
    [SerializeField] GameObject railGunLine;
    [SerializeField] Transform firePoint;
    private ItemSwaper itemSwaper;
    private AbilitiyCooldown toolCooldown;



    // Start is called before the first frame update
    void Start()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();
        toolCooldown = GetComponent<AbilitiyCooldown>();
        time = 0f;
        playerReachedMaxTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.Mouse0) && toolCooldown.leftMouseUse > 0)
        //{
        //    time = time + Time.deltaTime;
        //    if(time >= maxTimeOfCharging)
        //    {
        //        playerReachedMaxTime = true;
        //        CalculateShotRange();
        //    }
        //}
        
        if(Input.GetKeyUp(KeyCode.Mouse0) && !playerReachedMaxTime)
        {
            CalculateShotRange();
        }
    }

    private void CalculateShotRange()
    {
        float lineRange = minRangeOfShot * time;
        ShotRailgun(lineRange);
        playerReachedMaxTime = false;
        time = 0f;
    }

    private void ShotRailgun(float rangeOfShot)
    {
        GameObject bullet;
        bullet = Instantiate(railGunLine, firePoint.transform.position + (firePoint.transform.right * rangeOfShot / 2), firePoint.transform.rotation);
        bullet.transform.localScale = new Vector3(rangeOfShot, bullet.transform.localScale.x, bullet.transform.localScale.y);
        bullet.transform.parent = firePoint.transform;
        Destroy(bullet, lineLifeTime);
        //--toolCooldown.leftMouseUse;
        //itemSwaper.TryToStartCooldown();
    }
}
