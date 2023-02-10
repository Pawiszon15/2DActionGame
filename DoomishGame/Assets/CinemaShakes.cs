using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaShakes : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] camerasOnMap;
    [SerializeField] CinemachineBasicMultiChannelPerlin[] shakeComponent;

    private void Awake()
    {
        for (int j = 0; j < shakeComponent.Length; ++j)
        {
            shakeComponent[j] = camerasOnMap[j].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void CameraShakeStart(float intesity, float time)
    {
        StartCoroutine(CameraShakeTimer());

        for(int j = 0; j < shakeComponent.Length; ++j)
        {
            shakeComponent[j].m_AmplitudeGain = intesity;
            shakeComponent[j].m_FrequencyGain = time;
        }
    }

    private void CameraShakeStop()
    {
        for (int j = 0; j < shakeComponent.Length; ++j)
        {
            shakeComponent[j].m_AmplitudeGain = 0f;
            shakeComponent[j].m_FrequencyGain = 0f;
        }
    }

    IEnumerator CameraShakeTimer()
    {
        yield return new WaitForSeconds(0.1f);
        CameraShakeStop();
    }

}
