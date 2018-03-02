using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour {

    public GameObject Client;

    private Vector3 RocketPos;
   
    public int myID = -1;

    public int heldItem = 0;
    //0: no item.   1: rocket   2: mushroom     3: red flag     4: blue flag

    //models for the items, displayed when ready
    public GameObject rocketMod;
    public GameObject boostMod;
    public GameObject redFlagMod;
    public GameObject blueFlagMod;

    //records the spawn point of a flag, so that it can be reset when lost.
    public GameObject flagHome;
    public Transform flagPosition;

    // Use this for initialization
    void Awake () {
        Client = GameObject.Find("ExampleClient");
	}
	
	// Update is called once per frame
	void Update () {
        RocketPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1.5f);

		if(Client == null)
        {
            Client = GameObject.Find("ExampleClient");
        }

        myID = GetComponent<NetworkSync>().GetId();

        if (heldItem != 0)
        {
            GrabItem(heldItem);
        }

        if (heldItem == 1 && Input.GetButtonDown("Fire2"))
            FireRocket();

       

        if(GetComponent<Rigidbody>())
        {
            if(Mathf.Abs(GetComponent<Rigidbody>().angularVelocity.y) > 5)
            {
                LoseItem();
            }
        }

    }


    public void FireRocket()
    {
        Client.GetComponent<ExampleClient>().MakeRocket(myID);
    }

    public void RocketsConfirmed(int playerID)
    {
        if (myID == playerID)
        {
            Client.GetComponent<ExampleClient>().clientNet.Instantiate("Rocket", RocketPos, transform.localRotation);

            LoseItem();
        }
       
    }


    public void GrabItem(int itemToGet)
    {
        //when run, it will make whatever item was selected visible.
        switch(itemToGet)
        {
            case 0:
                LoseItem();
                break;
            case 1:
                
                rocketMod.transform.localScale = new Vector3(.2f, .2f, .2f);
                heldItem = 1;
                break;

            case 2:
                
                boostMod.transform.localScale = new Vector3(.2f, .2f, .2f);
                heldItem = 2;
                break;

            case 3:

                redFlagMod.transform.localScale = new Vector3(.05f, .05f, .05f);
               
                heldItem = 3;
                break;

            case 4:

                blueFlagMod.transform.localScale = new Vector3(.05f, .05f, .05f);
                
                heldItem = 4;
                break;
        }
    }

    public void LoseItem()
    {
        heldItem = 0;
        rocketMod.transform.localScale = new Vector3(0, 0, 0);
        boostMod.transform.localScale = new Vector3(0, 0, 0);
        redFlagMod.transform.localScale = new Vector3(0, 0, 0);
        blueFlagMod.transform.localScale = new Vector3(0, 0, 0);
        //Client.GetComponent<ExampleClient>().SetItem(0, myID);
    }

    public void FinishGame(int team)
    {
        Debug.Log("Reached Finish Game with team " + team);
        if (team == 1)
        {
            Client.GetComponent<ExampleClient>().myPlayer.GetComponent<CartMotion>().isReady = false;
            Client.GetComponent<ExampleClient>().loginScreen.GetComponent<TitleScreenLogic>().RedWins();
        }

        if (team == 2)
        {
            Client.GetComponent<ExampleClient>().myPlayer.GetComponent<CartMotion>().isReady = false;
            Client.GetComponent<ExampleClient>().loginScreen.GetComponent<TitleScreenLogic>().BlueWins();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ItemGrab")
        {
            //this ensures that only the client's player can grab items on their build.
            if (GetComponent<Player>())
            {
                //check if the map item is active, and that the player grabbing has no item.
                if ((other.GetComponent<ItemIdentifier>().itemType == 1 || other.GetComponent<ItemIdentifier>().itemType == 2) 
                    && heldItem == 0)
                {
                    //tells client to run the script and grab the item of that type.
                    Client.GetComponent<ExampleClient>().SetItem(other.GetComponent<ItemIdentifier>().itemType, myID);
                    float timer = 0;
                    other.transform.position -= new Vector3(0, 15, 0);
                    timer += Time.deltaTime;
                    if(timer > 5)
                    {
                        other.transform.position += new Vector3(0, 15, 0);
                    }
                }

                if ((other.GetComponent<ItemIdentifier>().itemType == 3 && GetComponent<SetPlayerTeam>().teamDesignation == 2)
                    && heldItem == 0)
                {
                   Client.GetComponent<ExampleClient>().SetItem(other.GetComponent<ItemIdentifier>().itemType, myID);
                    other.transform.position -= new Vector3(0, 15, 0);
                }

                if ((other.GetComponent<ItemIdentifier>().itemType == 4 && GetComponent<SetPlayerTeam>().teamDesignation == 1)
                    && heldItem == 0)
                {
                    Client.GetComponent<ExampleClient>().SetItem(other.GetComponent<ItemIdentifier>().itemType, myID);
                    other.transform.position -= new Vector3(0, 15, 0);
                }
            }
        }

        if (other.tag == "Rocket")
        {
            if (GetComponent<Player>())
            {
                //tells client to remove its item.
                Client.GetComponent<ExampleClient>().SetItem(0, myID);
                //tells client to make the client spin out.
                if(GetComponent<CartMotion>())
                {
                    GetComponent<CartMotion>().SpinOut();
                }
                Destroy(other.gameObject);
            }
        }

    }
}
