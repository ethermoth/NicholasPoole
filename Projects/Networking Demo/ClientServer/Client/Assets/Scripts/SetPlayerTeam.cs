using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerTeam : MonoBehaviour {

    public int myID = -1;

    public GameObject teamIndicator;

    public bool onlyOnce = false;

    public Material redMat;
    public Material blueMat;

    public int teamDesignation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myID = GetComponent<NetworkSync>().GetId();

        if (onlyOnce == false)
        {
            if (teamIndicator.GetComponent<MeshRenderer>().material.name == "explode (Instance)")
            {
                GetComponent<ItemUse>().Client.GetComponent<ExampleClient>().SetTeamColor(myID);
               
            }
        }
    }

    public void SetTeam(int teamNum, int playerID)
    {
        if (myID == playerID && onlyOnce == false)
        {
            if (teamNum % 2 == 1)
            {
                teamIndicator.GetComponent<MeshRenderer>().material = redMat;
                teamDesignation = 1;
                onlyOnce = true;
            }
            else
            {
                teamIndicator.GetComponent<MeshRenderer>().material = blueMat;
                teamDesignation = 2;
                onlyOnce = true;
            }
        }
    }
}
