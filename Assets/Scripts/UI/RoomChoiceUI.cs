using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChoiceUI : MonoBehaviour
{

    Server server;
    RoomCodeUI roomCodeUi=null;
    RoomMakingUI roomMakingUi = null;


    void Awake()
    {
        server = GameObject.Find("MakingServer").GetComponent<Server>();
        roomCodeUi = GameObject.Find("RoomChoice").GetComponent<RoomCodeUI>();
        roomMakingUi = GameObject.Find("RoomChoice").GetComponent<RoomMakingUI>();


    }

    public void MatchingOnClick()
    {
        server.ToServer("TO_SERVER JOIN AUTO");
    }

    public void MakingRoomOnClick()
    {
        //방 만드는 UI 띄우기
        roomMakingUi.OpenRoomMakingUI();
        Debug.Log("open roommaking UI");
    }

    public void RoomSearchOnClick()
    {
        //방 코드 입력하는 UI 띄우기 
        roomCodeUi.OpenRoomCodeUI();
        Debug.Log("open roomcode UI");

    }

    public void closeRoomCodeButtonOnClick()
    {
        roomCodeUi.CloseRoomCodeUI();
    }

    public void closeRoomMakingButtonOnClick()
    {
        roomMakingUi.CloseRoomMakingUI(); 
    }

   


}
