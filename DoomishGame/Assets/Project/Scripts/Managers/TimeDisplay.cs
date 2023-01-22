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

    void Start()
    {
        shouldTimerBeforeAlarmTick = true;
        Time.timeScale = 0f;
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

    private void Update()
    {
        if (Input.anyKeyDown && !firstInput)
        {
            Time.timeScale = 1;
            firstInput = true;
        }
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

