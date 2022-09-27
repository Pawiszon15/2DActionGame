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
    [SerializeField] GameObject firePoint1;
    [SerializeField] GameObject firePoint2;
    [SerializeField] GameObject airCorridor;
    [SerializeField] GameObject player;

    private ItemSwaper itemSwaper;
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
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        float corridorRotation;
        

        RaycastHit2D hit = Physics2D.Linecast(firePoint1.transform.position, firePoint1.transform.right * distanceOfRayCast);
        RaycastHit2D hit2 = Physics2D.Linecast(firePoint2.transform.position, firePoint2.transform.right * distanceOfRayCast);

        placeOfSpawn = hit2.point;

        var hitX_1 =Mathf.Round(hit.point.x * 10f) * 0.1f; var hitY_1 = Mathf.Round(hit.point.y * 10f) *0.1f; 
        var hitX_2 = Mathf.Round(hit2.point.x * 10f) * 0.1f; var hitY_2 = Mathf.Round(hit2.point.y * 10f) * 0.1f;
       
        if (hitY_1 == hitY_2 && playerPos.y < hitY_2) {corridorRotation = 180f; StartCoroutine(UseOfCorridorLuncher(placeOfSpawn, corridorRotation));}
        if (hitX_1 == hitX_2 && playerPos.x <= hitX_2) { corridorRotation = 90f; StartCoroutine(UseOfCorridorLuncher(placeOfSpawn, corridorRotation));}
        if (hitX_1 == hitX_2 && playerPos.x > hitX_2) { corridorRotation = 270f; StartCoroutine(UseOfCorridorLuncher(placeOfSpawn, corridorRotation));}
        else { corridorRotation = 0f; StartCoroutine(UseOfCorridorLuncher(placeOfSpawn, corridorRotation)); }
    }

    IEnumerator UseOfCorridorLuncher(Vector2 corridorSpawnPos, float corridorRotation)
    {
        yield return new WaitForSeconds(setupTime);
        Instantiate(airCorridor, corridorSpawnPos, Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, corridorRotation));
        itemSwaper.StartCooldown(cooldown);
    }
}
