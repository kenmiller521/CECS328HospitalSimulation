using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour, IComparable<EventScript>
{
    public int time;
    public string eventType;
    public string ambulanceName;
    public string description;
    public string victimName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public EventScript()
    {
    }
    public EventScript(int t, string e, string a, string d, string v)
    {
        time = t;
        eventType = e;
        ambulanceName = a;
        description = d;
        victimName = v;
    }
    public int getTime()
    {
        return time;
    }
    public string getEventType()
    {
        return eventType;
    }
    public string getAmbulanceName()
    {
        return ambulanceName;
    }
    public string getDescription()
    {
        return description;
    }
    public string getVictimName()
    {
        return victimName;
    }

    public int CompareTo(EventScript other)
    {
        return this.time.CompareTo(other.time);
    }
    public virtual string getEventData()
    {
        string str = time + " " + eventType + " " + ambulanceName + " " + description + " " + victimName;
        return str;
    }
}
