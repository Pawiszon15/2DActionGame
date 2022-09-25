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
    [SerializeField] GameObject airCorridor;
    private ItemSwaper itemSwaper;
    private Camera camera;

    private void Awake()
    {
        itemSwaper = FindObjectOfType<ItemSwaper>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(UseOfCorridorLuncher(mousePos));
        }
    }

    IEnumerator UseOfCorridorLuncher(Vector2 mousePos)
    {
        yield return new WaitForSeconds(setupTime);
        Instantiate(airCorridor, mousePos, Quaternion.identity);
        itemSwaper.StartCooldown(cooldown);
    }
}
