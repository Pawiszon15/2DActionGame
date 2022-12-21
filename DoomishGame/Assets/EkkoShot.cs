using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoShot : MonoBehaviour
{
    LayerMask mask;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform posOfShot;
    [SerializeField] Transform dirOfShot;

    private bool isEkkoShotAviable;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(Bullet, posOfShot.position, Quaternion.identity);
        }
    }
}
