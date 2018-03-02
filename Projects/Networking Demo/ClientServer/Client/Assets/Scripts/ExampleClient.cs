using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExampleClient : MonoBehaviour
{
    public ClientNetwork clientNet;

    // Get the instance of the FlagshipClient
    static ExampleClient instance = null;
    
    // Are we in the process of logging into a server
    private bool loginInProcess = false;

    public GameObject loginScreen;

    public List<GameObject> SpawnPoints;

    //These are GameObjects required for the Item usage to function.
    public GameObject itemNode;//where the active item is being displayed.
    
   
    public float timeToSend = 10;

    //the player using this client.
    public GameObject myPlayer;

    //list of all players in game.
    public List<GameObject> allPlayers;

    int counter = 1;

    public string userName;

    // Get the FlagshipClient
    public static ExampleClient GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("ExampleClient is uninitialized");
            return null;
        }
        return instance;
    }

    // Use this for initialization
    void Awake()
    {
        // Make sure we have a ClientNetwork to use
        if (clientNet == null)
        {
            clientNet = GetComponent<ClientNetwork>();
        }
        if (clientNet == null)
        {
            clientNet = (ClientNetwork)gameObject.AddComponent(typeof(ClientNetwork));
        }
    }

    void Start()
    {
        
    }
    
    // Start the process to login to a server
    public void ConnectToServer(string aServerAddress, int aPort, string username)
    {
        if (loginInProcess)
        {
            return;
        }
        loginInProcess = true;

        userName = username;

        ClientNetwork.port = aPort;
        clientNet.Connect(aServerAddress, ClientNetwork.port, username, "", "", 0);
    }

    public void InputName(string nn)
    {
        clientNet.CallRPC("NameNewPlayer", UCNetwork.MessageReciever.ServerOnly, -1, nn);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timeToSend -= Time.deltaTime;
        if (timeToSend <= 0)
        {
            clientNet.CallRPC("RequestMove", UCNetwork.MessageReciever.ServerOnly, -1, 1, 1, "x");
            clientNet.CallRPC("Blah", UCNetwork.MessageReciever.ServerOnly, -1, 1, 1, "x");
            timeToSend = 10;
        }
        */

    }

    public void SetPlayerReady()
    {//tells server that a player is ready.
        myPlayer.AddComponent<CartMotion>();
        clientNet.CallRPC("PlayerIsReady", UCNetwork.MessageReciever.ServerOnly, -1);
    }

    public void GetReady(int PlayerNum)//sets the Player to be ready. Points them to a spawn point for PlayerNum
    {//WORKS
        Debug.Log("Hey this is being reached");
        if (myPlayer.GetComponent<Player>().MyNumber <= 0)
        {
            myPlayer.GetComponent<Player>().MyNumber = PlayerNum;
        }

        if (myPlayer.GetComponent<Player>().MyNumber == PlayerNum)
        {
            myPlayer.transform.position = SpawnPoints[PlayerNum].transform.position;
            //myPlayer.GetComponent<SetPlayerTeam>().SetTeam(PlayerNum);
            myPlayer.GetComponent<CartMotion>().isReady = true;
        }
    }

    public void SetTeamColor(int cliID)
    {
        clientNet.CallRPC("PlayerNeedsTeam", UCNetwork.MessageReciever.ServerOnly, -1, cliID);
    }

    public void SetItem(int itemNum, int cliID)
    {
        //calls Server to confirm a player grabbed an item.
        Debug.Log("Somebody grabbed an item!");
        clientNet.CallRPC("PlayerGrabbedItem", UCNetwork.MessageReciever.ServerOnly, -1, itemNum, cliID);
    }

    public void MakeRocket(int cliID)
    {
        clientNet.CallRPC("PlayerShootsRocket", UCNetwork.MessageReciever.ServerOnly, -1, cliID);
    }

    public void EndGame(int team, int cliID)
    {
        Debug.Log("Reached Endgame");
        clientNet.CallRPC("EndThis", UCNetwork.MessageReciever.ServerOnly, -1, team, cliID, userName);
    }

    

    public void UpdateState(int x, int y, string player)
    {
        // Update the visuals for the game
    }

    public void RPCTest(int a, int b, int c, int d, int e)
    {
        Debug.Log("RPC Test has been called!");
    }


    void NewClientConnected(string Val)
    {
        Debug.Log("Client " + Val + " has connected!");
    }

    // Networking callbacks
    // These are all the callbacks from the ClientNetwork
    void OnNetStatusNone()
    {
        Debug.Log("OnNetStatusNone called");
    }
    void OnNetStatusInitiatedConnect()
    {
        Debug.Log("OnNetStatusInitiatedConnect called");
    }
    void OnNetStatusReceivedInitiation()
    {
        Debug.Log("OnNetStatusReceivedInitiation called");
    }
    void OnNetStatusRespondedAwaitingApproval()
    {
        Debug.Log("OnNetStatusRespondedAwaitingApproval called");
    }
    void OnNetStatusRespondedConnect()
    {
        Debug.Log("OnNetStatusRespondedConnect called");
    }
    void OnNetStatusConnected()
    {
        loginScreen.GetComponent<TitleScreenLogic>().ToGameUI();
        Debug.Log("OnNetStatusConnected called");

        clientNet.AddToArea(1);
    }

    void OnNetStatusDisconnecting()
    {
        Debug.Log("OnNetStatusDisconnecting called");

        if (myPlayer)
        {
            clientNet.Destroy(myPlayer.GetComponent<NetworkSync>().GetId());
        }
    }
    void OnNetStatusDisconnected()
    {
        Debug.Log("OnNetStatusDisconnected called");
        SceneManager.LoadScene("Prototype");
        
        loginInProcess = false;

        if (myPlayer)
        {
            clientNet.Destroy(myPlayer.GetComponent<NetworkSync>().GetId());
        }
    }
    public void OnChangeArea()
    {
        Debug.Log("OnChangeArea called");

        // Intantiate a player

        myPlayer = clientNet.Instantiate("Player", Vector3.zero, Quaternion.identity);
        myPlayer.GetComponent<NetworkSync>().AddToArea(1);
        myPlayer.AddComponent<Player>();
        myPlayer.GetComponent<Rigidbody>().isKinematic = false;
        myPlayer.GetComponent<Rigidbody>().useGravity = true;
        myPlayer.GetComponent<Player>().PlayerCount += counter;
        counter += 1;
    }

    // RPC Called by the server once it has finished sending all area initization data for a new area
    public void AreaInitialized()
    {
        Debug.Log("AreaInitialized called");
    }
    
    void OnDestroy()
    {
        if (myPlayer)
        {
            clientNet.Destroy(myPlayer.GetComponent<NetworkSync>().GetId());
        }
        if (clientNet.IsConnected())
        {
            clientNet.Disconnect("Peace out");
        }
    }
}


