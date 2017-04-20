﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimScript : MonoBehaviour {
    public Vector3 position;
    public bool isAlive;
    public GameObject ambulanceToPickUpVictim;
    public int survivalTime;
    public int victimNumber;

    public VictimScript(int victimNum, int x, int y, int survTime)
    {
        victimNumber = victimNum;
        position = new Vector3(x, y, 0);
        survivalTime = survTime;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setVictimStats(int victimNum, int x, int y, int survTime)
    {
        victimNumber = victimNum;
        position = new Vector3(x, y, 0);
        survivalTime = survTime;
    }
    public void setAmbulanceToPickUpVictim(GameObject amb)
    {
        ambulanceToPickUpVictim = amb;
    }
}