using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour, IComparable<EventScript>
{
    public int victimNumber;
    public Transform position;
    public int survivalTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int getVictmNumber()
    {
        return victimNumber;
    }
    public Transform getPosition()
    {
        return position;
    }
    public int getSurvivalTime()
    {
        return survivalTime;
    }

    public int CompareTo(EventScript other)
    {
        return this.victimNumber.CompareTo(other.victimNumber);
    }
}
