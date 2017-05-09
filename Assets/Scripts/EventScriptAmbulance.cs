using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScriptAmbulance : EventScript {

    public string departOrArrive;
    public string hospitalOrVictim;
    public EventScriptAmbulance(int t, string ambName, string depOrArr, string hospOrVic)
    {
        time = t;
        ambulanceName = ambName;
        departOrArrive = depOrArr;
        hospitalOrVictim = hospOrVic;
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
    public override string getEventData()
    {
        string str = time + " " + ambulanceName + " " + departOrArrive + " " + hospitalOrVictim;
        return str;
    }
}
