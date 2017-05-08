using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AmbulanceScript : MonoBehaviour {
    private const int NUMB_OF_PATIENTS = 2;
    public GameObject hospital;
    public HospitalScript hospitalScript;
    public GameObject[] hospitalArray;
    public GameObject victim1, victim2;
    private List<GameObject> victimList;
    public SimulationManager simulationManager;
    private float simulationStep;
    private float timer;
    private const int MOVE_ONE_BLOCK_STEP = 1;
    private const int LOAD_VICTIM_STEP = 3;
    private const int UNLOAD_VICTIM_STEP = 1;
    private int loadStepCounter = LOAD_VICTIM_STEP;
    private int unloadStepcounter = UNLOAD_VICTIM_STEP;
    public bool headingToVictim, headingToHospital;
    private int currentStep;
    private bool seekOnce;
    private bool addedArriveHospitalEvent;
    private bool firstTimeLeavingHospital = true;
    // Use this for initialization
    void Start () {
        simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
       // hospitalArray = GameObject.FindGameObjectsWithTag("Hospital");
        simulationStep = simulationManager.simulationStep;
       // victim1 = null;
        //victim2 = null;
        headingToHospital = false;
        headingToVictim = false;
        victimList = new List<GameObject>();
        currentStep = 0;
        addedArriveHospitalEvent = false;
        firstTimeLeavingHospital = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(simulationManager.initialSeekAlgorithmDone)
        {
            timer += Time.deltaTime;
            if (timer > simulationStep)
            {
                timer = 0;
                currentStep++;
                if(hospital.transform.position == transform.position && headingToHospital == true)
                {
                    if(addedArriveHospitalEvent == false)
                    {
                        EventScript es = new EventScript(currentStep, " ArriveHospital ", gameObject.name, " arrived ", hospital.name);
                        simulationManager.addEvent(es);
                        addedArriveHospitalEvent = true;
                    }
                    //If the hospital locations are not full
                    if(!hospitalScript.allLocationsFull)
                    {
                        //Take up a spot by incrementing the counter
                        hospitalScript.numberOfSpotsTaken+=1;
                        
                        if (victim2 != null)
                        {
                            unloadStepcounter--;
                            if (unloadStepcounter == 0)
                            {
                                victim1.GetComponent<VictimScript>().isSaved = true;
                                victim1.transform.parent = hospital.transform;
                                EventScript es = new EventScript(currentStep, " UnloadedVictim ", gameObject.name, " unloaded ", victim1.name);
                                simulationManager.addEvent(es);
                                victim1 = null;
                            }
                            if (unloadStepcounter == -1)
                            {
                                victim2.GetComponent<VictimScript>().isSaved = true;
                                victim2.transform.parent = hospital.transform;
                                EventScript es = new EventScript(currentStep, " UnloadedVictim ", gameObject.name, " unloaded ", victim2.name);
                                simulationManager.addEvent(es);
                                victim2 = null;
                                //decrement the counter;
                                hospitalScript.numberOfSpotsTaken-=1;
                                addedArriveHospitalEvent = false;
                                headingToHospital = false;
                                seekOnce = false;
                                unloadStepcounter = UNLOAD_VICTIM_STEP;
                            }
                        }
                        else if (victim1 != null)
                        {
                            unloadStepcounter--;
                            if (unloadStepcounter == 0)
                            {
                                victim1.GetComponent<VictimScript>().isSaved = true;
                                victim1.transform.parent = hospital.transform;
                                EventScript es = new EventScript(currentStep, " UnloadedVictim ", gameObject.name, " unloaded ", victim1.name);
                                simulationManager.addEvent(es);
                                victim1 = null;
                                //decrement the counter;
                                hospitalScript.numberOfSpotsTaken-=1;
                                addedArriveHospitalEvent = false;
                                headingToHospital = false;
                                seekOnce = false;
                                unloadStepcounter = UNLOAD_VICTIM_STEP;

                            }
                        }
                    }
                    
                }
                else if (victim1 != null || victim2 != null)
                {
                    if (victim1.GetComponent<VictimScript>().isPickedUp == false)
                    {
                        headingToVictim = true;
                        headingToHospital = false;
                        //If the ambulance is on top of the victim, pick up the victim
                        if (victim1.transform.position == transform.position)
                        {
                            loadStepCounter--;
                            Debug.Log(loadStepCounter);
                            if (loadStepCounter == 0)
                            {
                                victim1.GetComponent<VictimScript>().isPickedUp = true;
                                victim1.transform.parent = gameObject.transform;
                                loadStepCounter = LOAD_VICTIM_STEP;
                                EventScript es = new EventScript(currentStep + LOAD_VICTIM_STEP, " RescuedVictim ", gameObject.name, " rescued ", victim1.name);
                                simulationManager.addEvent(es);
                                //Make an event and add it to the heap
                                es = new EventScript(currentStep, " LoadVictim ", gameObject.name, " loaded ", victim1.name);
                                simulationManager.addEvent(es);
                            }

                        }

                        else if (victim1.transform.position.x > transform.position.x)
                            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                        else if (victim1.transform.position.x < transform.position.x)
                            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                        else if (victim1.transform.position.y > transform.position.y)
                            transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                        else if (victim1.transform.position.y < transform.position.y)
                            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                    }
                    else if (victim2 != null)
                    {
                        if (victim2.GetComponent<VictimScript>().isPickedUp == false)
                        {
                            headingToVictim = true;
                            headingToHospital = false;
                            if (victim2.transform.position == transform.position)
                            {
                                loadStepCounter--;
                                Debug.Log(loadStepCounter);
                                if (loadStepCounter == 0)
                                {
                                    victim2.GetComponent<VictimScript>().isPickedUp = true;
                                    victim2.transform.parent = gameObject.transform;
                                    loadStepCounter = LOAD_VICTIM_STEP;
                                    EventScript es = new EventScript(currentStep + LOAD_VICTIM_STEP, " RescuedVictim ", gameObject.name, " rescued ", victim2.name);
                                    simulationManager.addEvent(es);
                                    es = new EventScript(currentStep, " LoadVictim ", gameObject.name, " loaded ", victim2.name);
                                    simulationManager.addEvent(es);
                                }
                            }
                            else if (victim2.transform.position.x > transform.position.x)
                                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                            else if (victim2.transform.position.x < transform.position.x)
                                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                            else if (victim2.transform.position.y > transform.position.y)
                                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                            else if (victim2.transform.position.y < transform.position.y)
                                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                        }
                        else
                        {
                            headingToHospital = true;
                            headingToVictim = false;
                            if (hospital.transform.position.x > transform.position.x)
                                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                            else if (hospital.transform.position.x < transform.position.x)
                                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                            else if (hospital.transform.position.y > transform.position.y)
                                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                            else if (hospital.transform.position.y < transform.position.y)
                                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                        }

                    }
                    //else if both victims are picked up, drive back to the hospital
                    else
                    {
                        headingToHospital = true;
                        headingToVictim = false;
                        if (hospital.transform.position.x > transform.position.x)
                            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                        else if (hospital.transform.position.x < transform.position.x)
                            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                        else if (hospital.transform.position.y > transform.position.y)
                            transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                        else if (hospital.transform.position.y < transform.position.y)
                            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                    }
                }
                //else seek for more victims
                else
                {
                    if(seekOnce == false)
                    {
                        seekOnce = true;
                        seekVictim();
                        Debug.Log("SEEKING");
                    }
                    

                }
            }


        }


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
        int victim1TotalNumberOfSteps = 0;
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
                //X distance for the ambulance to drive to the victim
                totalNumberOfSteps += (int)Mathf.Abs(victim.transform.position.x - transform.position.x);
                //Y distance for the ambulance to drive to the victim
                totalNumberOfSteps += (int)Mathf.Abs(victim.transform.position.y - transform.position.y);
                //find closest hospital from the victim
                hospital = victim.GetComponent<VictimScript>().closestHospital;
                //hospitalScript = hospital.GetComponent<HospitalScript>();
                /*foreach(GameObject hosp in hospitalArray)
                {
                    hospitalDistance = Vector3.Distance(hosp.transform.position, victim.transform.position);
                    if (hospitalDistance < hospitalShortestDistance)
                    {
                        hospitalShortestDistance = hospitalDistance;
                        hospital = hosp;
                        hospitalScript = hospital.GetComponent<HospitalScript>();
                    }
                }*/
                //X distance for the ambulance to drive to the closest hospital
                totalNumberOfSteps += (int)Mathf.Abs(hospital.transform.position.x - victim.transform.position.x);
                //Y distance for the ambulance to drive to the closest hospital
                totalNumberOfSteps += (int)Mathf.Abs(hospital.transform.position.y - victim.transform.position.y);
                //Add the number of steps to load the victim(3 steps)
                totalNumberOfSteps += LOAD_VICTIM_STEP;
                //Add the number of steps to unload the victim(1 step)
                totalNumberOfSteps += UNLOAD_VICTIM_STEP;
                //If the total number of steps is less than the victim's survival time then the victim will still be alive when ambulance gets to the hospital
                if (totalNumberOfSteps < (victim.GetComponent<VictimScript>().survivalTime-currentStep))
                {
                    //update the shortest distance
                    shortestDistance = distance;
                    //set victim1 of the ambulance
                    victim1 = victim;
                    victim1TotalNumberOfSteps = totalNumberOfSteps;
                   // Debug.Log(this.gameObject.name + " - "+ victim.name + " TOTALNUMBOFSTEPS: " + victim1TotalNumberOfSteps);
                    
                }
                //reset the total number of steps for the next victim
                totalNumberOfSteps = 0;
                
            }
        }
        //if victim1 is null that means that there are no more victims to be picked up and thus break out of the function;
        if (victim1 == null)
        {
            return;
        }

        //in Victim1 set the ambulance to pick it up to be this ambulance
        victim1.GetComponent<VictimScript>().ambulanceToPickUpVictim = this.gameObject;
        //hospital = victim1.GetComponent<VictimScript>().closestHospital;
        //hospitalScript = hospital.GetComponent<HospitalScript>();
        //reset shortestDistance
        shortestDistance = 100;
        //Start totalNumberOfSteps to begin at the time it would take for victim 1 to be saved for proper calculation
        totalNumberOfSteps = victim1TotalNumberOfSteps;
        //Go through the list again to find closest victim to the FIRST victim
        foreach (GameObject victim in victimList)
        {
            //find closest victim to the first victim
            distance = Vector3.Distance(victim.transform.position, victim1.transform.position);
            //If the distance is shorter than the victim before it AND it is not being picked up by another ambulance
            if (distance < shortestDistance && victim.GetComponent<VictimScript>().ambulanceToPickUpVictim == null)
            {
                
                //X distance for the ambulance to drive from the first victim to the second victim
                totalNumberOfSteps += (int)Mathf.Abs(victim.transform.position.x - victim1.transform.position.x);
                //Y distance for the ambulance to drive from the first victim to the second victim
                totalNumberOfSteps += (int)Mathf.Abs(victim.transform.position.y - victim1.transform.position.y);
                hospital = victim.GetComponent<VictimScript>().closestHospital;
                //hospitalScript = hospital.GetComponent<HospitalScript>();
                //find closest hospital from the victim
               /* foreach (GameObject hosp in hospitalArray)
                {
                    hospitalDistance = Vector3.Distance(hosp.transform.position, victim.transform.position);
                    if (hospitalDistance < hospitalShortestDistance)
                    {
                        hospitalShortestDistance = hospitalDistance;
                        hospital = hosp;
                        hospitalScript = hospital.GetComponent<HospitalScript>();
                    }
                }*/
                //X distance for the ambulance to drive from the second victim back to the hospital;
                totalNumberOfSteps += (int)Mathf.Abs(hospital.transform.position.x - victim.transform.position.x);
                //Y distance for the ambulance to drive from the second victim back to the hospital;
                totalNumberOfSteps += (int)Mathf.Abs(hospital.transform.position.y - victim.transform.position.y);
                //Double the distance for the time for the ambulance to drive back to the hospital
                //totalNumberOfSteps *= 2;
                //Add the number of steps to load the victim(3 steps)
                totalNumberOfSteps += LOAD_VICTIM_STEP;
                //Add the number of steps to unload the victim(1 step)
                totalNumberOfSteps += UNLOAD_VICTIM_STEP;
                //If the updated total number of steps is less than the FIRST victim's survival time. Ambulance won't pick up second victim if it meanas the first will die
                if (totalNumberOfSteps < (victim1.GetComponent<VictimScript>().survivalTime - currentStep))
                {
                    //If the total number of steps is less than the current victim's survival time then ambulane can pick up victim 2
                    if (totalNumberOfSteps < (victim.GetComponent<VictimScript>().survivalTime - currentStep))
                    {
                        //set the shortest distance to compare for any future closer victims
                        shortestDistance = distance;
                        //set the victim
                        victim2 = victim;
                        //hospital = victim.GetComponent<VictimScript>().closestHospital;
                        //hospitalScript = hospital.GetComponent<HospitalScript>();
                    }
                }
                //reset the total number of steps to be the victim1's totalNumberOfSteps for proper calculation for next victim
                totalNumberOfSteps = victim1TotalNumberOfSteps;

            }
        }
        if(victim2 != null)
        {
            //Set in victim2 that it is being picked up by an ambulance.
            victim2.GetComponent<VictimScript>().ambulanceToPickUpVictim = this.gameObject;
            hospital = victim2.GetComponent<VictimScript>().closestHospital;
            hospitalScript = hospital.GetComponent<HospitalScript>();
        }
        else
        {
            hospital = victim1.GetComponent<VictimScript>().closestHospital;
            hospitalScript = hospital.GetComponent<HospitalScript>();
        }
            
        headingToVictim = true;
        if (firstTimeLeavingHospital == true)
        {
            if (gameObject.name.Contains("Aust"))
            {                
                EventScript eventScr = new EventScript(currentStep, " LeaveHospital ", gameObject.name, " left ", "Austerlitz");
                simulationManager.addEvent(eventScr);
            }
            if (gameObject.name.Contains("Past"))
            {
                EventScript eventScr = new EventScript(currentStep, " LeaveHospital ", gameObject.name, " left ", "Pasteur");
                simulationManager.addEvent(eventScr);
            }
            if (gameObject.name.Contains("DeGa"))
            {
                EventScript eventScr = new EventScript(currentStep, " LeaveHospital ", gameObject.name, " left ", "DeGaulle");
                simulationManager.addEvent(eventScr);
            }
            firstTimeLeavingHospital = false;
        }
        else
        {
            EventScript es = new EventScript(currentStep, " LeaveHospital ", gameObject.name, " left ", hospital.name);
            simulationManager.addEvent(es);
        }
        
    }
}
