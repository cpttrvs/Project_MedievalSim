using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {


    private float timer;
    private float dayTimer;
    private int day;
    private int week;
    private int month;

    public float speedMultiplicator = 1f;
    private float initialSpeed = 1f;
    private float speed = 1f;
    private float agentSpeed = 10f;

    private bool dayHasChanged = false;
    private bool weekHasChanged = false;
    private bool monthHasChanged = false;

    void Start() {
        timer = 0.0f;
    }
	
	void Update () {
        setGlobalSpeed(speedMultiplicator);

        timer += Time.deltaTime * speed;
        dayTimer += Time.deltaTime * speed;

        if(dayTimer >= 1) {
            day++;
            dayTimer = 0;
            dayHasChanged = true;
        } else {
            dayHasChanged = false;
        }

        if(day % 7 == 0 && dayHasChanged) {
            week++;
            weekHasChanged = true;
        } else {
            weekHasChanged = false;
        }

        if (day % 30 == 0 && dayHasChanged)
        {
            ++month;
            monthHasChanged = true;
        }
        else
            monthHasChanged = false;

        //Debug.Log(day + " " + week);
    }

    public float getTimer()
    {
        return timer;
    }

    public int getDay() 
    {
        return day;
    }

    public int getWeek()
    {
        return week;
    }

    public int getMonth()
    {
        return month;
    }

    public bool dayChanged() { return dayHasChanged; }
    public bool weekChanged() { return weekHasChanged; }
    public bool monthChanged() { return monthHasChanged; }

    public void setGlobalSpeed(float _value)
    {
        speed = initialSpeed * speedMultiplicator;
        foreach(ANPC npc in FindObjectsOfType<ANPC>())
        {
            npc.SetSpeed(speed * 10f);
        }
    }
}
