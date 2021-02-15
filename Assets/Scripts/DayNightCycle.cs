using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private float targetDayLength;

    [SerializeField]
    private float timeOfDay;

    public bool pause = false;

    private int dayNumber;

    private void SetTimeScale()
    {
        timeScale = 24 / (targetDayLength / 60) ;
    }

    private void UpdateTime()
    {
        timeOfDay += timeScale / 86400; // seconds in a day
        if(timeOfDay > 1)
        {
            dayNumber++;
            timeOfDay -= 1;
            pause = false;
        }
    }

    private void Update()
    {
        if(!pause)
        {
            SetTimeScale();
            UpdateTime();
        }
    }
}
