using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WantUDed : State {
    //This should be attached to the customer prefab

    public Transform target;
	// Use this for initialization
	void Awake () {
        target = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void EnterState()
    {

    }

    override public void UpdateState()
    {
        if(target == null)
        {

        }
    }

    override public void ExitState()
    {

    }
}
