using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingThroughHalfPlatforms : MonoBehaviour
{
    //[SerializeField] KeyCode changePlatform;
    //[SerializeField] KeyCode changePlatform2;

    private PlatformEffector2D platformEffector2D;

    // Start is called before the first frame update
    void Start()
    {
        platformEffector2D = FindObjectOfType<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ChangeDirOfPlatforms(180f);
            }
        }

        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            ChangeDirOfPlatforms(0f);
        }

    }

    private void ChangeDirOfPlatforms(float offset)
    {
        platformEffector2D.rotationalOffset = offset;
    }

}
