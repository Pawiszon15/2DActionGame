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
    private float timeAfterStartingLevel;
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
                Debug.Log("Time is UP!");
                TimeLeftToStartAlarm = 0;
                shouldTimerBeforeAlarmTick = false;
                gameManger.ActivateEnemies();
            }
        }

        else if(!shouldTimerBeforeAlarmTick)
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
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    

}
