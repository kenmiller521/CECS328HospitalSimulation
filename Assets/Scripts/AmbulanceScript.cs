using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceScript : MonoBehaviour {
    private const int NUMB_OF_PATIENTS = 2;
    public GameObject hospital;
    public GameObject victim1, victim2;
    private List<GameObject> victimList;
    public SimulationManager simulationManager;
    private float simulationStep;
    private float timer;
    private const int MOVE_ONE_BLOCK_STEP = 1;
    private const int LOAD_VICTION_STEP = 3;
    private const int UNLOAD_VICTION_STEP = 1;
    public bool headingToVictim, headingToHospital;
    // Use this for initialization
    void Start () {
        simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        simulationStep = simulationManager.simulationStep;
       // victim1 = null;
        //victim2 = null;
        headingToHospital = false;
        headingToVictim = true;
        victimList = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pickUpVictim(GameObject v)
    {
        //If at least on patient slot is open then continue
        if(victim1 == null || victim2 == null)
        {
            //If patient 1 slot is open then set passed patient as patient 1 otherwise set patient as patient 2
            if (victim1 == null)
                victim1 = v;
            else
                victim2 = v;
        }
        
    }
    public void seekVictim()
    {
        //temporary variable to hold the total number of steps
        int totalNumberOfSteps = 0;
        //get victim list
        victimList = simulationManager.getVictimList();
        //temporary variable to get the shortest distance between two objects
        float shortestDistance = 100;
        //temporary variable to calculate the distance and to compare the shortest distance
        float distance = 0;
        //foreach victim in the list
        foreach (GameObject victim in victimList)
        {
            //Get the distance between the victim and the ambulance
            distance = Vector3.Distance(victim.transform.position, transform.position);
            //if the distance is shorter than the victim before it AND if it isn't set to be picked up by another ambulance
            if (distance < shortestDistance && victim.GetComponent<VictimScript>().ambulanceToPickUpVictim == null)
            {
                
                //update the shortest distance
                shortestDistance = distance;
                //set victim1 of the ambulance
                victim1 = victim;
            }
        }
        //if victim1 is null that means that there are no more victims to be picked up and thus break out of the function;
        if (victim1 == null) return;

        //in Victim1 set the ambulance to pick it up to be this ambulance
        victim1.GetComponent<VictimScript>().ambulanceToPickUpVictim = this.gameObject;
        //reset shortestDistance
        shortestDistance = 100;
        //Go through the list again to find closest victim to the FIRST victim
        foreach (GameObject victim in victimList)
        {
            //find closest victim to the first victim
            distance = Vector3.Distance(victim.transform.position, victim1.transform.position);
            //If the distance is shorter than the victim before it AND it is not being picked up by another ambulance
            if (distance < shortestDistance && victim.GetComponent<VictimScript>().ambulanceToPickUpVictim == null)
            {
                //set the shortest distance to compare for any future closer victims
                shortestDistance = distance;
                //set the victim
                victim2 = victim;
            }
        }
        //Set in victim2 that it is being picked up by an ambulance.
        victim2.GetComponent<VictimScript>().ambulanceToPickUpVictim = this.gameObject;
    }
}
