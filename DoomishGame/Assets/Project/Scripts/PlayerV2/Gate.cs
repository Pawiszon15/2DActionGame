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
        if (collision.gameObject.GetComponent<SingleShot>())
        {
            SingleShot singleShot = collision.gameObject.GetComponent<SingleShot>();
            singleShot.BoostByGate();
            Debug.Log("Bullet go through");
        }

        //else if (collision.gameObject.GetComponent<Grenade>())
        //{
        //    Grenade grenade = collision.gameObject.GetComponent<Grenade>();
        //    grenade.BoostByGate();
        //    Debug.Log("Bullet go through");
        //}

        //else if (collision.gameObject.GetComponent<CharacterMovement>())
        //{   //improve katana slash or ground slam
        //    CharacterMovement player = GetComponent<CharacterMovement>();
        //    player.BoostByGate();
        //    Debug.Log("Player went through the gate");
        //}
    }
}
