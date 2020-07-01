using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    Socket socket;
    InGameUI ingame;
    RoomInputInfo roominfo;
    WatingRoom watingroom;

    string ipAdress = "3.34.179.89";
    int port = 4578;

    string recv_buf = "";

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

        Thread thread = new Thread(FromServer);
        thread.Start();
    }

    void Update()
    {
        if (recv_buf != "") 
        {
            string[] megs = recv_buf.Split(' ');

            if(megs.Length > 0 && megs[0] == "TO_CLIENT")
            {
                if(megs.Length > 1 && megs[1] == "GAME") //게임 관련 이벤트
                {
                    if(megs.Length > 2 && megs[2] == "NUMBER")  //자기 넘버
                    {
                        if(megs.Length > 3)
                        {
                            GameObject.Find("Managers").GetComponent<GameManager>().me = int.Parse(megs[3]);
                        }
                    }
                    else if(megs.Length > 2 && megs[2] == "UPGRADE") //상점부분
                    {
                        if(megs.Length > 3 && megs[3] == "GOLD")
                        {
                            if (megs.Length > 4 && megs[4] == "1")
                            {
                                ingame.EnemyGoldUpgrade(1);
                            }
                            else if (megs.Length > 4 && megs[4] == "2")
                            {
                                ingame.EnemyGoldUpgrade(2);
                            }
                        }
                        else if(megs.Length > 3 && megs[3] == "LIGHT"){
                            if (megs.Length > 4 && megs[4] == "1")
                            {
                                ingame.EnemyLightUpgrade(1);
                            }
                            else if (megs.Length > 4 && megs[4] == "2")
                            {
                                ingame.EnemyLightUpgrade(2);
                            }
                        }
                        else if(megs.Length > 3 && megs[3] == "SPEED")
                        {
                            if (megs.Length > 4 && megs[4] == "1")
                            {
                                ingame.EnemySpeedUpgrade(1);
                            }
                            else if (megs.Length > 4 && megs[4] == "2")
                            {
                                ingame.EnemySpeedUpgrade(2); //여기까지 상점
                            }
                        }
                    }
                }
                else if (megs.Length > 1 && megs[1] == "ROOM") //방 관련 이벤트
                {
                    if(megs.Length>2 && megs[2] == "UPDATE")
                    {
                        if(megs.Length > 3 && megs[3] == "1")
                        {
                            //player를 not connected로 바꾸기
                            watingroom.userisdisconnected();
                            
                        }
                        else if (megs.Length > 3 && megs[3] == "2"){
                            //Player2를 conected로 바꾸기
                            watingroom.userisconnected();
                        }
                    }
                    else if (megs.Length > 2 && megs[2] == "CONNECT")
                    {
                        if (megs.Length > 3)
                        {
                            SceneManager.LoadScene(5);
                            
                            if (megs.Length > 4 && megs[4] == "1")
                            {
                                //Player1을 connected로 바꾸고, 씬 옮겨주기
                                roominfo.IntoWatingRoom();
                                watingroom.playerisconnected();
                                GameObject.Find("UI").GetComponent<WatingRoom>().roomcode.text = megs[3];
                            }
                            else if (megs.Length > 4 && megs[4] == "2")
                            {
                                //player2를 connected로 바꾸고, 씬 옮겨주기
                                roominfo.IntoWatingRoom();
                                watingroom.userisconnected();
                                GameObject.Find("UI").GetComponent<WatingRoom>().roomcode.text = megs[3];
                            }
                        }
                    }
                    else if (megs.Length > 2 && megs[2] == "FULL")
                    {
                        //방 꽉찼다는 이미지 띄워주는 함수 실행
                        roominfo.RoomIsFull();
                    }
                    else if (megs.Length >2 &&megs[2]=="NOROOM")
                    {
                        //방 없다는 이미지 띄워주는 함수 실행 
                        roominfo.CannotFindRoom();
                    }
                }
            }

            recv_buf = "";
        }


    }

    public void ToServer(string st)
    {
        try
        { 
            int sended = socket.Send(Encoding.UTF8.GetBytes(st.Length.ToString() + "$"), 0);
            sended = socket.Send(Encoding.Default.GetBytes(st));

        }

        catch (System.Exception e)
        {
            Debug.Log("Server Error : " + e);
        }
    }

    void FromServer()
    {
        while (true)
        {
            byte[] size_buf = new byte[1];
            int size = 0;
            int recvsize = 0;

            while (true)
            {
                recvsize = socket.Receive(size_buf, 1, 0);
                if(Encoding.Default.GetString(size_buf)[0] == '$' || recvsize <= 0)
                {
                    break;
                }

                size = size * 10 + int.Parse(Encoding.Default.GetString(size_buf));
            }

            byte[] buf = new byte[256];
            recvsize = socket.Receive(buf, size, 0);

            if (recvsize <= 0)
                break;
            else
            {
                recv_buf = Encoding.Default.GetString(buf);
            }
        }

        Debug.Log("Server is Disconnected");

        socket.Close();
        socket = null;
    }

    public void LoadIngameUi()
    {
       
        ingame = GameObject.Find("InGameUI").GetComponent<InGameUI>();
    }

    public void Loadroominfo()
    {
        roominfo = GameObject.Find("RoomInputInfo").GetComponent<RoomInputInfo>();
    }

    public void LoadWatingroom()
    {
        watingroom = GameObject.Find("UI").GetComponent<WatingRoom>();
    }
}
