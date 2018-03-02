using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
            transform.Translate(Vector3.down * Time.deltaTime);

	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
