using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraActivator : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponentInParent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement player))
        {
            virtualCamera.Priority = 20;
        }
    }
}
