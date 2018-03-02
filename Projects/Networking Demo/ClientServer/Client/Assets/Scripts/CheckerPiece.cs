using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour {

    public bool isRed = false;
    public bool CheckUp = false;


    public GameObject LightUp;
    public GameObject LLight;
    public GameObject RLight;


    public GameObject Plyr;

    float HiddenZPos;
    float Zpos;

	// Use this for initialization
	void Start () {
       
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        Plyr = GameObject.FindGameObjectWithTag("Player");//temporary definition for testing purposes

        if (Plyr)
        {//highlights the checker piece the user clicks on
            if (Vector3.Distance(Plyr.transform.position, transform.position) < 3 && Plyr.GetComponent<Player>().Highlight == true)
            {
                LightUp.GetComponent<Light>().intensity = 20;
                LLight.GetComponent<Light>().intensity = 15;
                RLight.GetComponent<Light>().intensity = 15;
                CheckUp = true;
            }
           

            if(CheckUp == true && Vector3.Distance(Plyr.transform.position, LLight.transform.position) < 2 && Plyr.GetComponent<Player>().Highlight == true)
            {
                transform.position = LLight.transform.position;
            }

            if (CheckUp == true && Vector3.Distance(Plyr.transform.position, RLight.transform.position) < 2 && Plyr.GetComponent<Player>().Highlight == true)
            {
                transform.position = RLight.transform.position;
            }

            if (Vector3.Distance(Plyr.transform.position, transform.position) >= 3 && Plyr.GetComponent<Player>().Highlight == true)
            {//highlight is removed if the player clicks anywhere else
                LightUp.GetComponent<Light>().intensity = 0;
                LLight.GetComponent<Light>().intensity = 0;
                RLight.GetComponent<Light>().intensity = 0;
                CheckUp = false;
            }
        }
	}



  
}
