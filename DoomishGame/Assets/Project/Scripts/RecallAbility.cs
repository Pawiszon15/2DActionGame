using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RecallAbility : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float timeBeforePoints;
    [SerializeField] Vector2[] leavedPoints;
    [SerializeField] Vector2[] velocity;
    private Rigidbody2D rb;
    bool leavingPointsIsActive = true;
    int i = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(leavePoints());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            int toWhichPointTeleportPlayer = 0;
            if (i == leavedPoints.Length)
            {
                toWhichPointTeleportPlayer = 0;
            }

            else
            {
                toWhichPointTeleportPlayer = i + 1;
            }

            gameObject.transform.position = leavedPoints[toWhichPointTeleportPlayer];
            rb.velocity = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0f, 30f);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(30f, 0f);
            }
            //else if(Input.GetKeyDown(KeyCode.S))
            //{
            //    rb.velocity = new Vector2(0f, 20f);
            //}
            else if(Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-30f, 0f);
            }


            //rb.velocity = velocity[toWhichPointTeleportPlayer];
        }
    }

    IEnumerator leavePoints()
    {
        GameObject spawnedObject;

        while (leavingPointsIsActive)
        {   
            leavedPoints[i] = gameObject.transform.position;
           // velocity[i] = rb.velocity;
            spawnedObject = Instantiate(spawnObject, gameObject.transform.position, transform.rotation);
            Destroy(spawnedObject, leavedPoints.Length * timeBeforePoints);

            i++;

            if(i == leavedPoints.Length)
            {
                Debug.Log(i);
                i = 0;
            }

            yield return new WaitForSeconds(timeBeforePoints);
        }
    }
}
