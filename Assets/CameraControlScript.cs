using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(transform.position.x, transform.position.y+1, -10);
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, -10);
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y-1, -10);
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, -10);
    }
}
