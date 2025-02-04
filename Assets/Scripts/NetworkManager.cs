using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    private Telepathy.Client client;

    private void Awake(){
        Application.runInBackground = true;

        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }
    
    void Start(){
        client = new Telepathy.Client(1024);
        
        client.OnConnected = () => Debug.Log("Client Connected");
        client.OnData = PacketHandler.HandleData;
        client.OnDisconnected = () => Debug.Log("Client Disconnected");

        client.Connect("localhost", 13);
    }

    void Update(){
        client.Tick(100);
    }

    void OnApplicationQuit(){
        client.Disconnect();
    }
}
