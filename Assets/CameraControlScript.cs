using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, -10);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, -10);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position = new Vector3(transform.position.x, transform.position.y-0.5f, -10);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, -10);
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
    }
}
