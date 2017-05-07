using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalScript : MonoBehaviour {
    public int totalNumberOfSpots;
    public int numberOfSpotsTaken;
    public bool allLocationsFull;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (numberOfSpotsTaken == totalNumberOfSpots)
            allLocationsFull = true;
        else
            allLocationsFull = false;
	}
}
