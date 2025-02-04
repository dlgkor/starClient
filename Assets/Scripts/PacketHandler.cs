using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PacketHandler
{
    public delegate void Handler(ArraySegment<byte> _packet);
    public static Dictionary<ushort, Handler> packetHandlers;

    static PacketHandler(){
        packetHandlers = new Dictionary<ushort, Handler>();

        packetHandlers.Add(0x0000, (ArraySegment<byte> _packet) => {Debug.Log(Encoding.UTF8.GetString(_packet.Array, 2+_packet.Offset, _packet.Count));});
    }

    public static void HandleData(ArraySegment<byte> data){
        Debug.Log("Handling Data");
        ushort _packetID = BitConverter.ToUInt16(data.Array, data.Offset);
        packetHandlers[_packetID](data);
    }
}
