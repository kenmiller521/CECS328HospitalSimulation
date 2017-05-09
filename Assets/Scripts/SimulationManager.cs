﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {
    private EventMinHeap EMH;
    private EventMinHeap EMHVictim;
    public GameObject victim;
    public List<GameObject> victimList;
    public List<GameObject> ambulanceList;
    public float simulationStep;
    public int stepToFinish;
    private int victimCount;
    public int currentStep;
    private float timer;
    public bool initialSeekAlgorithmDone;
    private bool simulationDone;
    private bool algorithmChosen;
	// Use this for initialization
	void Start () {
        algorithmChosen = false;
        victimCount = 1;
        EMH = new EventMinHeap();
        EMHVictim = new EventMinHeap();
        victimList = new List<GameObject>();
        createVictim(0, 50, 55, 35);
        createVictim(1, 48, 64, 42);
        createVictim(2, 49, 53, 32);
        createVictim(3, 53, 56, 39);
        createVictim(4, 53, 48, 31);
        createVictim(5, 51, 47, 28);
        createVictim(6, 52, 51, 33);
        createVictim(7, 52, 50, 32);
        createVictim(8, 52, 60, 42);
        createVictim(9, 47, 65, 42);
        createVictim(10, 57, 54, 31);
        createVictim(11, 69, 50, 39);
        createVictim(12, 57, 57, 34);
        createVictim(13, 56, 58, 34);
        createVictim(14, 64, 50, 34);
        createVictim(15, 62, 51, 33);
        createVictim(16, 56, 56, 32);
        createVictim(17, 63, 61, 44);
        createVictim(18, 60, 51, 31);
        createVictim(19, 58, 53, 31);
        createVictim(20, 57, 72, 39);
        createVictim(21, 66, 60, 36);
        createVictim(22, 77, 56, 43);
        createVictim(23, 57, 62, 29);
        createVictim(24, 65, 65, 40);
        createVictim(25, 58, 69, 37);
        createVictim(26, 61, 56, 27);
        createVictim(27, 65, 57, 32);
        createVictim(28, 63, 70, 43);
        createVictim(29, 65, 56, 31);
        //initializeAmbulanceSeek();
        StartCoroutine(ChooseAlgorithm());
        simulationDone = false;
    }

    // Update is called once per frame
    void Update () {
	    if(initialSeekAlgorithmDone)
        {
            timer += Time.deltaTime;
            if(timer > simulationStep)
            {
                timer = 0;
                currentStep++;
                if (currentStep == stepToFinish)
                {
                    if(simulationDone == false)
                    {
                        simulationDone = true;
                        //TODO: Write to file
                        EMH.writeToFile();
                        EMHVictim.writeToFileVictim();
                        // StartCoroutine(WriteToFile());
                        //StartCoroutine(WriteToFileVictim());
                    }
                }
            }
        }	
	}

    public void createVictim(int numb, int x, int y, int survTime)
    {
        //victimScript = new VictimScript(numb, x, y, survTime);
        victim.GetComponent<VictimScript>().setVictimStats(numb, x, y, survTime);
        victim = (GameObject)Instantiate(victim, new Vector3(x,y,0), Quaternion.identity);
        victimList.Add(victim);
        victim.GetComponent<VictimScript>().simulationManager = this;
        victim.name = "Victim" + victimCount;
        victimCount++;
    }
    //This function, called in start, initializes the greedy algorithm in each ambulance to find the best victims
    private void initializeAmbulanceSeek()
    {
        StartCoroutine(AmbulanceSeek());   
    }
    private void initializeAmbulanceSeekMyAlgorithm()
    {
        StartCoroutine(AmbulanceSeekMyAlgorithm());
    }
    IEnumerator AmbulanceSeek()
    {
        foreach(GameObject obj in ambulanceList)
        {
            AmbulanceScript ambScript = obj.GetComponent<AmbulanceScript>();
            ambScript.usingMyAlgorithm = false;
            ambScript.seekVictim();
            Debug.Log(ambScript.name + " seeking");
            while (ambScript.headingToVictim == false)
            {
                Debug.Log(ambScript.name + " seeking");
                yield return null;
            }
            
        }
        initialSeekAlgorithmDone = true;
        yield return null;
    }
    IEnumerator AmbulanceSeekMyAlgorithm()
    {
        foreach (GameObject obj in ambulanceList)
        {
            AmbulanceScript ambScript = obj.GetComponent<AmbulanceScript>();
            ambScript.usingMyAlgorithm = true;
            ambScript.seekVictimMyAlgorithm();
            Debug.Log(ambScript.name + " seeking");
            while (ambScript.seekFinished == false)
            {
                Debug.Log(ambScript.name + " seeking");
                yield return null;
            }

        }
        initialSeekAlgorithmDone = true;
        yield return null;
    }
    public List<GameObject> getVictimList()
    {
        return victimList;
    }
    public void addEvent(EventScript es)
    {
        EMH.insert(es);
    }
    public void addEventVictim(EventScript es)
    {
        EMHVictim.insert(es);
    }
    IEnumerator WriteToFile()
    {
        EMH.writeToFile();
        yield return null;
    }
    IEnumerator WriteToFileVictim()
    {
        EMHVictim.writeToFile();
        yield return null;
    }

    IEnumerator ChooseAlgorithm()
    {
        while(algorithmChosen == false)
        {
            //regular algorithm
            if(Input.GetKeyDown(KeyCode.Z))
            {
                initializeAmbulanceSeek();
                algorithmChosen = true;
            }
            //my algorithm
            else if(Input.GetKeyDown(KeyCode.X))
            {
                initializeAmbulanceSeekMyAlgorithm();
                algorithmChosen = true;
            }
            yield return null;
        }
        yield return null;
    }
}
