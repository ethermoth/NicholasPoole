using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ExampleServer : MonoBehaviour
{
    public static ExampleServer instance;

    public GameObject database;

    public ServerNetwork serverNet;

    //public List<GameObject> SpawnPoints;

    // Name of the server
    public string serverName = "DaveDevelopmentServer";
    public int portNumber = 603;

    public int numberOfReadyPlayers = 0;
    public int numberOfReds = 0;
    public int numberOfBlues = 0;

    public int playerData;

    //State of the board
    int[][] board;

    // Use this for initialization
    void Awake()
    {
        instance = this;

        // Initialization of the server network
        ServerNetwork.port = portNumber;
        if (serverNet == null)
        {
            serverNet = GetComponent<ServerNetwork>();
        }
        if (serverNet == null)
        {
            serverNet = (ServerNetwork)gameObject.AddComponent(typeof(ServerNetwork));
            Debug.Log("ServerNetwork component added.");
        }

        //serverNet.EnableLogging("rpcLog.txt");
    }

    // Start it up
    void Start()
    {

    }

    // A client has just requested to connect to the server
    void ConnectionRequest(ServerNetwork.ConnectionRequestInfo data)
    {
        Debug.Log("Connection request from " + data.username);

        database.GetComponent<testThis>().X = numberOfReadyPlayers;
        database.GetComponent<testThis>().Y = data.username;
        database.GetComponent<testThis>().TestReturn();

        // We either need to approve a connection or deny it
        serverNet.ConnectionApproved(data.id);

        // Deny the request
        //serverNet.ConnectionDenied(data.id);
    }

    public void OnClientConnected(long aClientId)//was long
    {
        serverNet.CallRPC("RPCTest", aClientId, -1, 3, 4, 5, 6, 7);
        Debug.Log("The client's ID is " + aClientId);
       
        Debug.Log("OnClientConnection was called!");
        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);
        serverNet.CallRPC("NewClientConnected", UCNetwork.MessageReciever.AllClients, -1, aClientId, "bob");

        
        //Call or Create a new ID for a connecting player//
    }

    public void NameNewPlayer(string nn)
    {

    }

    public void PlayerIsReady()
    {
        Debug.Log("Player is ready");
        numberOfReadyPlayers += 1;

        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);

        serverNet.CallRPC("GetReady", UCNetwork.MessageReciever.AllClients, -1, numberOfReadyPlayers);

        if (numberOfReds <= numberOfBlues)
        {
            numberOfReds += 1;
        }
        else numberOfBlues += 1;
    
            

        //serverNet.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    public void PlayerNeedsTeam(int playerID)
    {
        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);
        if (numberOfReds <= numberOfBlues)
        {
            serverNet.CallRPC("SetTeam", UCNetwork.MessageReciever.AllClients, playerID, 1, playerID);
        }
        else serverNet.CallRPC("SetTeam", UCNetwork.MessageReciever.AllClients, playerID, 2, playerID);

    }

    public void PlayerGrabbedItem(int itemNum, int playerID)
    {
        Debug.Log("Player grabbed an item!");

        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);

        serverNet.CallRPC("GrabItem", UCNetwork.MessageReciever.AllClients, playerID, itemNum);
    }

    public void PlayerShootsRocket(int playerID)
    {
        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);

        serverNet.CallRPC("RocketsConfirmed", UCNetwork.MessageReciever.AllClients, playerID, playerID);
    }

    public void EndThis(int team, int playerID, string userName)
    {
        ServerNetwork.ClientData data = serverNet.GetClientData(serverNet.SendingClientId);

        serverNet.CallRPC("FinishGame", UCNetwork.MessageReciever.AllClients, playerID, team);

        database.GetComponent<ServerDatabase>().AddScore(userName);
    }

    // RPC from the client
    public void RequestMove(int x, int y)
    {
        // Validate the move
        bool moveIsValid = true;
        if (moveIsValid)
        {
            serverNet.CallRPC("UpdateState", UCNetwork.MessageReciever.AllClients, -1, board, board[x][y], "x");
        }
        else
        {
            serverNet.CallRPC("UpdateState", serverNet.SendingClientId, -1, 1, 1, " ");
        }
        serverNet.Kick(serverNet.SendingClientId);
    }

    // The server network wants to send any data needed to initialize a network object on a specific client
    // This happens when clients first connect or change areas
    void InitializeNetworkObject(ServerNetwork.InitializationInfo aInfo)
    {

    }

    void OnClientDisconnected(int aClientId)//was long
    {
        ServerNetwork.ClientData data = serverNet.GetClientData(aClientId);
        numberOfReadyPlayers -= 1;
    }

    // A new object was just instantiated over the network
    void OnInstantiateNetworkObject(ServerNetwork.IntantiateObjectData aObjectData)
    {

    }

    public void OnAddArea(ServerNetwork.AreaChangeInfo info)
    {

    }

    public void AddedPlayerToArea(int aNetObjId)
    {

    }

    // An object was just removed from the network
    void OnDestroyNetworkObject(int aNetworkId)
    {

    }
}
