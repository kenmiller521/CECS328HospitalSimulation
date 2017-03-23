using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(new Vector3(-0.5f, -0.5f, 0), new Vector3(100.5f, -0.5f, 0));
        //Gizmos.DrawLine(new Vector3(100.5f, -0.5f, 0), new Vector3(100.5f, 100.5f, 0));
        //Gizmos.DrawLine(new Vector3(100.5f, 100.5f, 0), new Vector3(-0.5f, 100.5f, 0));
        //Gizmos.DrawLine(new Vector3(-.5f, 100.5f, 0), new Vector3(-.5f, -0.5f, 0));
        for(int i = 0; i <= 101; i++)
        {
            Gizmos.DrawLine(new Vector3(i-0.5f, -0.5f, 0), new Vector3(i-0.5f, 100.5f, 0));
        }
        for(int i = 0; i <= 101; i++)
        {
            Gizmos.DrawLine(new Vector3(-0.5f, i-0.5f, 0), new Vector3(100.5f, i-0.5f, 0));
        }
    }
}
