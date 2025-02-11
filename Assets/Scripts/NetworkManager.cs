using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#if UNITY_WEBGL
//Unity WebGL Code

#else

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
        
        client.OnConnected = () => 
        {
            Debug.Log("Client Connected");
            
            /*
            List<byte> _packet = new List<byte>();
            _packet.AddRange(BitConverter.GetBytes((ushort)0x0000));
            client.Send(new ArraySegment<byte>(_packet.ToArray()));
            */
            
            
            /*
            List<byte> _packet = new List<byte>();
            _packet.AddRange(BitConverter.GetBytes((ushort)0x0001));
            string str1 = "test.txt\0";
            _packet.AddRange(BitConverter.GetBytes((int)str1.Length));
            _packet.AddRange(Encoding.UTF8.GetBytes(str1));
            string str2 = "hello everyone. goodnight";
            _packet.AddRange(BitConverter.GetBytes((int)str2.Length));
            _packet.AddRange(Encoding.UTF8.GetBytes(str2));
            client.Send(new ArraySegment<byte>(_packet.ToArray()));
            */

            /*
            string str = @"
            {
                ""levelName"" : ""test7"",
                ""data"" : ""datass oh yeah""
            }";
            List<byte> _packet = new List<byte>();
            _packet.AddRange(BitConverter.GetBytes((ushort)0x0000));
            _packet.AddRange(BitConverter.GetBytes((int)str.Length));
            _packet.AddRange(Encoding.UTF8.GetBytes(str));
            client.Send(new ArraySegment<byte>(_packet.ToArray()));
            Debug.Log(str.Length);
            */

            
            List<byte> _packet = new List<byte>();
            _packet.AddRange(BitConverter.GetBytes((ushort)0x0001));
            client.Send(new ArraySegment<byte>(_packet.ToArray()));
            
            
            _packet = new List<byte>();
            _packet.AddRange(BitConverter.GetBytes((ushort)0x0002));
            _packet.AddRange(BitConverter.GetBytes((int)1));
            client.Send(new ArraySegment<byte>(_packet.ToArray()));
        };
        client.OnData = PacketHandler.HandleData;
        client.OnDisconnected = () => Debug.Log("Client Disconnected");

        //client.Connect("localhost", 13);
        //client.Connect("192.168.0.120", 12345);
        client.Connect("118.42.117.50", 12345);
    }

    void Update(){
        client.Tick(100);
    }

    void OnDestroy(){
        Debug.Log("disconnect");
        if(client.Connected)
            client.Disconnect();
    }
}
#endif