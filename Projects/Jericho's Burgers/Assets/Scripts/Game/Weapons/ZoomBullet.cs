using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBullet : MonoBehaviour {

    public Rigidbody rb;
    private float destroyDelay = 0.5f;
    public float lifespan = 7.0f;
    public bool removal = false;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce((Vector3.forward) * 750.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(removal == true)
        {
            destroyDelay -= Time.deltaTime;
            if (destroyDelay < 0)
                Destroy(this.gameObject);
        }

        if(lifespan > 0)
            lifespan -= Time.deltaTime;

        if (lifespan < 0)
            removal = true;
	}

    void OnTriggerEnter (Collider col)
    {
        if(col.tag == "Customer")
        {
            col.GetComponent<CustomerMood>().Harm();
        }
        removal = true;
    }
}
