using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    public GameObject spinePiece;
    public bool moving;
    public float magnitude;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float random = Random.Range(.1f, 2f);
        if (moving)
        {
            transform.Rotate(new Vector3(0,1,0) * magnitude * random);
        }
	}
}
