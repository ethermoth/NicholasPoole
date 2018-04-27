using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //abstract functions to bring into the child states:

    //For starting state behavior
    virtual public void EnterState()
    {

    }

    //For running state behavior
    abstract public void UpdateState();

    //For leaving a state
    virtual public void ExitState()
    {

    }
}
