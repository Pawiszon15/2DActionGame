using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotLauncher : MonoBehaviour
{
    [SerializeField] GameObject singleBullet;
    [SerializeField] GameObject firePoint;

    private ItemSwaper itemSwaper;
    private ToolCooldown toolCooldown;

    // Start is called before the first frame update
    void Start()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();
        toolCooldown = GetComponent<ToolCooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && toolCooldown.leftMouseUse > 0)
        {
            Instantiate(singleBullet,  firePoint.transform.position, firePoint.transform.rotation);
            --toolCooldown.leftMouseUse;
            itemSwaper.TryToStartCooldown();
        }
    }
}
