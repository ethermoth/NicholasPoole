using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMood : MonoBehaviour {
    //ATTACH TO THE CUSTOMER PREFAB
    float health;
    float temper;

    public State Attacking;
    public State Passive;
    public State Leaving;

    bool veryMad = false;
    bool isDead = false;
    bool isFed = false;

	// Use this for initialization
	void Awake () {
        Attacking = GetComponent<WantUDed>();
        Passive = GetComponent<CustomerPassiveState>();
        Leaving = GetComponent<CustomerSatisfiedState>();
        health = Random.Range(5, 19);
        temper = Random.Range(4, 12);
	}
	
	// Update is called once per frame
	void Update () {
		if(isDead == false && temper < 5)
        {
            AttackMode();
        }

        if(isDead == false && veryMad == false && isFed == true)
        {
            HaveANiceDay();
        }

        if(health < 1)
        {
            KillCustomer();
        }
	}

    public void Harm()
    {
        health -= 1;
        temper -= 3;
    }

    void KillCustomer()
    {
        isDead = true;
    }

    void AttackMode()
    {
        veryMad = true;
        GetComponent<FiniteStateMachine>().ChangeState(Attacking);
    }

    void HaveANiceDay()
    {
        isFed = true;
        GetComponent<FiniteStateMachine>().ChangeState(Leaving);
    }

    public float TemperCheck()
    {
        Debug.Log("temper is " + temper);
        return temper;
    }
}
