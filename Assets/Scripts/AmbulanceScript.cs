using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceScript : MonoBehaviour {
    private const int NUMB_OF_PATIENTS = 2;
    public GameObject hospital;
    public GameObject patient1, patient2;
	// Use this for initialization
	void Start () {
        patient1 = null;
        patient2 = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pickUpPatient(GameObject p)
    {
        //If at least on patient slot is open then continue
        if(patient1 == null || patient2 == null)
        {
            //If patient 1 slot is open then set passed patient as patient 1 otherwise set patient as patient 2
            if (patient1 == null)
                patient1 = p;
            else
                patient2 = p;
        }
        
    }
}
