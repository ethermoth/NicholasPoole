using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoIPManager : MonoBehaviour
{

    ServerNetwork serverNet;
    int nextVoIPId = 1;
    Dictionary<long, int> customListeners = new Dictionary<long, int>();

    // Use this for initialization
    void Start()
    {
        serverNet = GetComponent(typeof(ServerNetwork)) as ServerNetwork;
    }

    public void OnClientConnected(long aClientId)
    {
        // When a new client connects, send them a message to start their VoIP service
        serverNet.CallRPC("StartVoIP", aClientId, -1, nextVoIPId);
        nextVoIPId++;
    }

    public void OnClientDisconnected(long aClientId)
    {
        serverNet.CallRPC("VoiceClientDisconnected", aClientId, -1);
    }

    // Called from ServerNetwork to get the list of clients a particular message should be sent to
    // Returns a dictionary mapping a list of clients who should recieve on which channel: {channelId: [clientId, clientId, ...], ... }   
    public List<long> GetClientsForVoiceChat(int aNetObjId, VoiceChatChannel aChannel)
    {
        List<long> clientList = new List<long>();

        // When talking over voip, who should get the data?

        // Loop through clients, check if their player is on the team of the object broadcasting
        List<ServerNetwork.ClientData> allClients = serverNet.GetAllClients();
        foreach(ServerNetwork.ClientData data in allClients)
        {
            clientList.Add((long)data.uniqueId);
        }

        return clientList;
    }
    
    // Maps clientId to a netObjId that they want to listen to
    public void SetCustomListener(long clientId, int targetNetObjectId)
    {
            customListeners[clientId] = targetNetObjectId;
    }

    // Removes a netObject from the dictionary
    public void RemoveCustomListener(long clientId)
    {
        if (customListeners.ContainsKey(clientId))
        {
            if (customListeners[clientId] != -1)
            {
                customListeners[clientId] = -1;
            }
        }
    }

    // See if the NetObject is currently being listened to
    public long GetListeningId(int targetNetObjectId)
    {
        foreach (KeyValuePair<long,int> pair in customListeners)
        {
            if(pair.Value == targetNetObjectId)
            {
                return pair.Key;
            }
        }
        return -1;
    }
}
