using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void IntoWatingRoom() //서버에서 방이 존재한다고 말했을 때
    {
        SceneManager.LoadScene(5); //대기방으로 씬 옮김
    }

    public void CannotFindRoom() //방이 존재하지 않을 때
    {
        //존재하지 않는다는 이미지 띄워주기
    }

    public void RoomIsFull() //방이 꽉 찼을 때 
    {
        //꽉 찼다는 이미지 띄워주기
    }

    public void SendRoomMakeInfo(bool isPrivate)
    {
        
        if (isPrivate)
            server.SendMessage("TO_SERVER JOIN CREATE TRUE"); //비공개 
        else
            server.SendMessage("TO_SERVER JOIN CREATE FALSE"); //공개

        SceneManager.LoadScene(5); //서버에 비공개여부 전송하고 대기방으로 씬 이동
    }
    
    
}
