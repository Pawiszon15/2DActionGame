using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirCorridorLuncher : MonoBehaviour
{
    [Header("Gun Properties")]
    [SerializeField] float cooldown;
    [SerializeField] float useRange;
    [SerializeField] float setupTime;

    [Header("References")]
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject airCorridor;
    
    private ItemSwaper itemSwaper;
    private Camera camera;
    private float distanceOfRayCast = 1000f;

    private void Awake()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChosePlaceOfSpawn();
        }
    }

    private void ChosePlaceOfSpawn()
    {
        Vector2 placeOfSpawn;
        RaycastHit2D hit = Physics2D.Linecast(weapon.transform.position, weapon.transform.right * distanceOfRayCast);
        placeOfSpawn = hit.point;
        StartCoroutine(UseOfCorridorLuncher(placeOfSpawn));
    }

    IEnumerator UseOfCorridorLuncher(Vector2 corridorSpawnPos)
    {
        yield return new WaitForSeconds(setupTime);
        Instantiate(airCorridor, corridorSpawnPos, Quaternion.identity);
        itemSwaper.StartCooldown(cooldown);
    }
}
