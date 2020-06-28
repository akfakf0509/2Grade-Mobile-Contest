using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server : MonoBehaviour
{
    Socket socket;

    string ipAdress = "13.125.249.44";
    int port = 4578;

    byte[] sendByte;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        //create socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        //connect
        try
        {
            IPAddress ipAddr = IPAddress.Parse(ipAdress);
            IPEndPoint ipendPoint = new IPEndPoint(ipAddr, port);
            socket.Connect(ipendPoint);

        }
        catch (SocketException se)
        {
            Debug.Log("Socket connect error ! : " + se.ToString());

            return;
        }
    }



    public void ToServer(string st)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(st);
            int l = Encoding.Default.GetByteCount(sb.ToString());
            byte[] d = Encoding.Default.GetBytes(sb.ToString());
            socket.Send(Encoding.Default.GetBytes(l.ToString()), sizeof(byte), 0);
            socket.Send(d, l, 0);
        }

        catch (System.Exception e)
        {
            Debug.Log("Server Error : " + e);
        }


    }
}
