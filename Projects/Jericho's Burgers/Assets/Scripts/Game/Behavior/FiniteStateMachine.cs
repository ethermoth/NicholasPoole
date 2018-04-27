using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FiniteStateMachine : MonoBehaviour
{
    //Attach this to the customers.
   
    public List<State> allStates;

    public State currentS;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        currentS.UpdateState();

    }



    public void ChangeState(State newState)
    {
        currentS.ExitState();
        currentS = newState;
        currentS.EnterState();
    }
}

