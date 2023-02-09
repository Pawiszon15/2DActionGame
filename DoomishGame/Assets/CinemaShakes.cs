using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaShakes : MonoBehaviour
{
    CinemachineVirtualCamera[] camerasOnMap;

    private void Awake()
    {
        camerasOnMap = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    public void CameraShakeStart(float intesity, float time)
    {
        StopCoroutine(CameraShakeTimer(time));

        foreach (CinemachineVirtualCamera cam in camerasOnMap)
        {
            cam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intesity;
            cam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = time;
        }
    }

    private void CameraShakeStop()
    {
        foreach (CinemachineVirtualCamera cam in camerasOnMap)
        {
            cam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            cam.GetComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }

    IEnumerator CameraShakeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        CameraShakeStop();
    }

}
