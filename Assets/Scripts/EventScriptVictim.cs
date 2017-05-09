using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScriptVictim : EventScript
{
    public string victimName;
    public Vector2 loc;
    public int survivalTime;
    public bool isSaved;
    int timeRescued;
    string ambulanceName;
    int timeUnloadedFromAmbulance;

    public EventScriptVictim(string v, int x, int y, int survTime, bool saved, int t, string aName, int timeUnloaded)
    {
        victimName = v;
        loc.x = x;
        loc.y = y;
        survivalTime = survTime;
        isSaved = saved;
        timeRescued = t;
        ambulanceName = aName;
        timeUnloadedFromAmbulance = timeUnloaded;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int CompareTo(EventScriptVictim other)
    {
        return this.victimName.CompareTo(other.victimName);
    }
    public string getEventData()
    {
        string str = null;
        if (isSaved)
        {
            str = victimName + " (" + loc.x + "," + loc.y + ") " + survivalTime + " " + " SAVED " + timeRescued + " " + ambulanceName + " " + timeUnloadedFromAmbulance;
        }
        else
            str = victimName + " (" + loc.x + "," + loc.y + ") " + survivalTime + " " + " NOT SAVED ";
        //string str = time + " " + eventType + " " + ambulanceName + " " + description + " " + victimName;
        return str;
    }

}
