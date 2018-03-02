using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCams : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;
	// Use this for initialization
	void Awake () {
        cam1.enabled = true;
        cam2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            if (cam1.enabled == true)
            {
                cam2.enabled = true;
                cam1.enabled = false;
            }

            else
            {
                cam1.enabled = true;
                cam2.enabled = false;
            }
        }


	}
}
