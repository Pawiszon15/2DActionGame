using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaShakes : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] camerasOnMap;
    [SerializeField] float intesity_0, intesity_1, intesity_2;

    [SerializeField] CinemachineBasicMultiChannelPerlin[] shakeComponent;
    private int currnetActiveCamera;

    void Awake()
    {
        for (int j = 0; j < camerasOnMap.Length; ++j)
        {
            shakeComponent[j] = camerasOnMap[j].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Start()
    {
        currnetActiveCamera = 0;
    }

    void Update()
    {
        if (shakeComponent[currnetActiveCamera].m_AmplitudeGain > 0)
        {
            shakeComponent[currnetActiveCamera].m_AmplitudeGain -= 4 *Time.deltaTime;
            shakeComponent[currnetActiveCamera].m_FrequencyGain -= 4 *Time.deltaTime;
        }
    }

    public void CameraShakeStart(int switchInt)
    {

        switch(switchInt)
        {
            case 0:
                shakeComponent[currnetActiveCamera].m_AmplitudeGain = intesity_0;
                shakeComponent[currnetActiveCamera].m_FrequencyGain = intesity_0 / 2;
                break;

            case 1:
                shakeComponent[currnetActiveCamera].m_AmplitudeGain = intesity_1;
                shakeComponent[currnetActiveCamera].m_FrequencyGain = intesity_1 /2;
                break;

            case 2:
                shakeComponent[currnetActiveCamera].m_AmplitudeGain = intesity_2;
                shakeComponent[currnetActiveCamera].m_FrequencyGain = intesity_2 / 2;
                break;
        }
    }


    public void GetHighestPriorityVirtualCamera()
    {
        CinemachineVirtualCamera highestPriorityVirtualCamera = null;
        int highestPriority = int.MinValue;
        int highestPriorityIndex = -1;

        for (int i = 0; i < camerasOnMap.Length; i++)
        {
            CinemachineVirtualCamera virtualCamera = camerasOnMap[i];

            if (virtualCamera.Priority > highestPriority)
            {
                highestPriority = virtualCamera.Priority;
                highestPriorityVirtualCamera = virtualCamera;
                highestPriorityIndex = i;
            }
        }

        currnetActiveCamera = highestPriorityIndex;
    }
}
