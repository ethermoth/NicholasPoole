using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomies : MonoBehaviour {

    public float speed = 3.0f;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
