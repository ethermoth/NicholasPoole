using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMaker : MonoBehaviour {

    public bool redGoal = false;
    public bool blueGoal = false;

    public GameObject Client;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Client == null)
        {
            Client = GameObject.Find("ExampleClient");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (redGoal == true && other.GetComponent<ItemUse>().heldItem == 4)
            {
                Client.GetComponent<ExampleClient>().EndGame(1, other.GetComponent<ItemUse>().myID);

            }

            if (blueGoal == true && other.GetComponent<ItemUse>().heldItem == 3)
            {
                Client.GetComponent<ExampleClient>().EndGame(2, other.GetComponent<ItemUse>().myID);
            }
        }
    }
}
