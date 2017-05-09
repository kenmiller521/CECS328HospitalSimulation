using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimScript : MonoBehaviour {
    public Vector3 position;
    public bool isAlive;
    public bool isPickedUp;
    public bool isSaved;
    public GameObject ambulanceToPickUpVictim;
    public int survivalTime;
    public int victimNumber;
    public SimulationManager simulationManager;
    public float simulationStep;
    private float timer;
    public int currentStep;
    public GameObject victimDeadX;
    public GameObject closestHospital;
    public GameObject[] hospitalsArray;
    private float hospitalDistance;
    private float hospitalShortestDistance;
    private bool madeSavedEvent;
    public int timeRescued = 0;
    public VictimScript(int victimNum, int x, int y, int survTime)
    {
        victimNumber = victimNum;
        position = new Vector3(x, y, 0);
        survivalTime = survTime;
        
    }
    private void Awake()
    {
        hospitalsArray = GameObject.FindGameObjectsWithTag("Hospital");
        hospitalShortestDistance = 100;
        hospitalDistance = 101;
        foreach (GameObject hosp in hospitalsArray)
        {
            hospitalDistance = Vector3.Distance(hosp.transform.position, transform.position);
            if (hospitalDistance < hospitalShortestDistance)
            {
                hospitalShortestDistance = hospitalDistance;
                closestHospital = hosp;
            }
        }
        madeSavedEvent = false;
    }
    // Use this for initialization
    void Start () {
        isAlive = true;
        isSaved = false;
        simulationStep = simulationManager.simulationStep;
        //hospitalsArray = GameObject.FindGameObjectsWithTag("Hospital");
        
    }
	
	// Update is called once per frame
	void Update () {
        if(simulationManager.initialSeekAlgorithmDone)
        {
            //If the victim is NOT saved
            if (!isSaved)
            {
                timer += Time.deltaTime;
                if (timer > simulationStep)
                {
                    timer = 0;
                    currentStep++;
                }
                if (currentStep >= survivalTime && isAlive)
                {
                    isAlive = false;
                    //TODO: ADD TO HEAP FOR VICTIM DEATH
                    EventScript es = new EventScript(currentStep, " VictimDeath ", this.name, " - ", " - ");
                    simulationManager.addEvent(es);
                    //Destroy(this.gameObject);
                    GameObject X = Instantiate(victimDeadX, this.transform.position, Quaternion.identity);
                    X.transform.parent = this.transform;
                }
            }
            else if (madeSavedEvent == false)
            {
                madeSavedEvent = true;
                EventScriptVictim es = new EventScriptVictim(this.name, (int)position.x, (int)position.y, survivalTime, true, timeRescued, ambulanceToPickUpVictim.name, currentStep);
                //EventScript es = new EventScriptVictim(name, LocationInfo, survivalTime, ifSaved, timeRescued, ambulanceThatUnloaded, "unloaded at", time);

            }
            
        }
        
            
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
