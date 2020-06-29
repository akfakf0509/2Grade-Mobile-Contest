using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInputInfo : MonoBehaviour
{
    public InputField roomcode;
    //public InputField roomname; 나중에 방 이름 만들 때 사용

    Server server;

    void Awake()
    {
        server = GameObject.Find("MakingServer").GetComponent<Server>();
    }

    
    public void SendRoomcode()
    {
        server.SendMessage("TO_SERVER JOIN ROOM_ID " + roomcode.text); //들어가려는 룸 코드 서버에 전송
        roomcode = null; //룸 코드 초기화
    }

    public void SendRoomMakeInfo(bool isPrivate)
    {
        
        if (isPrivate)
            server.SendMessage("TO_SERVER JOIN CREATE TRUE"); //비공개 
        else
            server.SendMessage("TO_SERVER JOIN CREATE FALSE"); //공개
    }
    
    
}
