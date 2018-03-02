using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour {

    public float rSpeed = 80;

    public float timeActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 1 * rSpeed * Time.deltaTime);

        timeActive += Time.deltaTime;
        if(timeActive > 10)
        {
            Destroy(this.gameObject);
        }
    }

   
}
