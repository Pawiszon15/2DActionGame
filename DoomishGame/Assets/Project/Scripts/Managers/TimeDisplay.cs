using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    [Header("Timer properties")]
    [SerializeField] float TimeLeftToStartAlarm;
    [SerializeField] bool shouldTimerBeforeAlarmTick = false;

    [Header("References")]
    [SerializeField] GameManger gameManger;

    private bool firstInput;
    public float timeAfterStartingLevel;
    private TextMeshProUGUI TimerTxt;

    private void Awake()
    {
        TimerTxt = GetComponent<TextMeshProUGUI>();
        timeAfterStartingLevel = 0f;
        firstInput = false;
    }

    private void FixedUpdate()
    {
        if (shouldTimerBeforeAlarmTick)
        {
            if (TimeLeftToStartAlarm > 0)
            {
                TimeLeftToStartAlarm -= Time.deltaTime;
                updateTimer(TimeLeftToStartAlarm);
            }

            else
            {
                TimeLeftToStartAlarm = 0;
                shouldTimerBeforeAlarmTick = false;
            }
        }

        else if (!shouldTimerBeforeAlarmTick)
        {
            timeAfterStartingLevel += Time.deltaTime;
            updateTimer(timeAfterStartingLevel);
        }
    }

    public void TurnOffTimerBeforeAlarm()
    {
        shouldTimerBeforeAlarmTick = false;
        TimeLeftToStartAlarm = 0;
    }


    void updateTimer(float currentTime)
    {

        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliseconds = Mathf.FloorToInt(currentTime * 1000);

        TimerTxt.text = TimeSpan.FromSeconds(currentTime).ToString("ss\\.fff");
    }

    public void setTimerAfter(float timeAfterLoad)
    {
        timeAfterStartingLevel = timeAfterLoad;
    }
}

