using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCutdown());
    }

    IEnumerator StartCutdown()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sth triggered gate");
        if(collision.gameObject.tag == "Bullet")
        {
            SingleShot singleShot = collision.gameObject.GetComponent<SingleShot>();
            singleShot.BoostByGate();
            Debug.Log("Bullet go through");
        }
    }
}
