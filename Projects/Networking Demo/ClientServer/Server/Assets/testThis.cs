using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testThis : MonoBehaviour {

    public GameObject sDB;
    public long X = 2;
    public string Y = "test";

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //this will add a dummy entry to the players table.
    public void TestTable()
    {
        Debug.Log("Test Started");
        sDB.GetComponent<ServerDatabase>().AddNewPlayer(X, Y);
        Debug.Log("Item added.");
    }

    public void TestReturn()
    {
        Debug.Log("Searching for player " + X +":");
        sDB.GetComponent<ServerDatabase>().GetPlayer(X, Y);
    }

    public void TestScoreAdd()
    {
        Debug.Log("Adding to score:");
        sDB.GetComponent<ServerDatabase>().AddScore(Y);
    }
}
