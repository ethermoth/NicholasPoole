using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGauge : MonoBehaviour {

    public float temperature = 0.0f;
    public bool cooked = false;
    public bool overcooked = false;

    public Renderer thisRen;

    public Material rawTex;
    public Material cookTex;
    public Material burnTex;

	// Use this for initialization
	void Awake () {
        thisRen.material = rawTex;
	}
	
	// Update is called once per frame
	void Update () {
        //when the temperature of an object reached 100 units, it will be considered "cooked"
		if(temperature >= 100 && overcooked == false)
        {
            cooked = true;
            thisRen.material = cookTex;
        }

        //however, if something is removed from a heat source before this point, the temperature will quickly roll to zero.
        if(cooked == false && temperature >= 0)
        {
            temperature -= Time.fixedDeltaTime;
        }

        //and if left on a heat source too long, the meat is overcooked and will be considered "burnt"
        if(temperature >= 180)
        {
            thisRen.material = burnTex;
        }
	}

    void OnTriggerStay (Collider other)
    {
        if(other.tag == "HeatSource")
        {
            temperature += 15 * Time.fixedDeltaTime;
        }
    }
}
