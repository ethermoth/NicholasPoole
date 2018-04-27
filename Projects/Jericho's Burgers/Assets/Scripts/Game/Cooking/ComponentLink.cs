using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is to make sandwiches stay as one
public class ComponentLink : MonoBehaviour {

    //sandwich materials have "link nodes" above and/or below them, that note whether they are to be attached to anything.
    //this script should be on those individual nodes.

    //freeLink simply checks that the node is empty or not.
    public bool freeLink = true;

    //Nodes must be attached to a parent object.
    public GameObject parent;

    public int ingredientType;
    //0: empty, ignore
    //1: bottom bun
    //2: top bun
    //3: patty (raw)
    //4: patty (cooked)
    //5: patty (burned)
    //6: cheese
    //7: veggie
    //etc.

    public List<int> burgerStack;
    //burgerStack must contain 1 and 2 for the burger to "count"

	// Use this for initialization
	void Awake () {
        burgerStack.Add(ingredientType);
        freeLink = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
    //If a component is placed over or under another, they lock together.
        if(other.tag == "Node")
        {
            if(other.GetComponent<ComponentLink>())
            {
                if(other.GetComponent<ComponentLink>().freeLink == true)
                {
                    //if (ingredientType <= other.GetComponent<ComponentLink>().ingredientType)
                    //burgers can't feature two of the same object in a row. The buns have highest priority in terms
                    //of which acts as a "base" object.
                    //{
                    /*For whatever reason this seeems to work with the patty as the base object. Not sure why as of yet.*/
                        Debug.Log("Objects linked!");
                        other.GetComponent<ComponentLink>().parent.transform.SetParent(parent.transform);
                    other.GetComponent<ComponentLink>().parent.GetComponent<Rigidbody>().freezeRotation = true;
                    other.GetComponent<ComponentLink>().parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    other.GetComponent<ComponentLink>().parent.GetComponent<Rigidbody>().useGravity = false;
                    //}

                    for (int ii = 0; ii < other.GetComponent<ComponentLink>().burgerStack.Count; ii++)
                    {
                        Debug.Log("Stack updated!");
                        burgerStack.Add(other.GetComponent<ComponentLink>().burgerStack[ii]);
                    }


                    freeLink = false;
                }
            }
        }
    }
}
