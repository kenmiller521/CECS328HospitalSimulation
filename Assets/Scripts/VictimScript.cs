using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimScript : MonoBehaviour {
    public Vector3 position;
    public bool isAlive;
    public bool isPickedUp;
    public GameObject ambulanceToPickUpVictim;
    public int survivalTime;
    public int victimNumber;
    public SimulationManager simulationManager;
    public float simulationStep;
    private float timer;
    public float currentStep;
    public VictimScript(int victimNum, int x, int y, int survTime)
    {
        victimNumber = victimNum;
        position = new Vector3(x, y, 0);
        survivalTime = survTime;
        
    }
	// Use this for initialization
	void Start () {
        isAlive = true;
        simulationStep = simulationManager.simulationStep;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > simulationStep)
        {
            timer = 0;
            currentStep++;
        }
        if (currentStep >= survivalTime)
            isAlive = false;
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<AmbulanceScript>() == true) { }
            
    }
}
