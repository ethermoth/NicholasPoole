using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyThing : MonoBehaviour {

    public float anglething = 0.4f;
    //public GameObject core;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.RotateAround(core.transform.position, anglething);
        transform.Rotate(0, anglething * Time.deltaTime, 0);
	}
}
