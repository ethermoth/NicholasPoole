using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SetTeamColor : MonoBehaviour
{

    public GameObject teamIndicator;

    public Material redMat;
    public Material blueMat;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTeam(int MyNumber)
    {
        if (MyNumber % 2 == 1)
        {
            teamIndicator.GetComponent<MeshRenderer>().material = redMat;
        }
        else
        {
            teamIndicator.GetComponent<MeshRenderer>().material = blueMat;
        }
    }
}
