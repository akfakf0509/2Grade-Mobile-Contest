using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomInputInfo : MonoBehaviour
{
    public InputField roomcode;
    //public InputField roomname; 나중에 방 이름 만들 때 사용

    public Image privateCheckBox;

    public GameObject roomisFullMessage;
    public GameObject noRoomMessage;

    public Sprite isChecked;
    public Sprite isnotChecked;

    RoomCodeUI roomCodeUi;
    RoomMakingUI roomMakingUi;

    Server server;

    void Awake()
    {
        server = GameObject.Find("MakingServer").GetComponent<Server>();
        roomCodeUi = GameObject.Find("RoomChoice").GetComponent<RoomCodeUI>();
        roomMakingUi = GameObject.Find("RoomChoice").GetComponent<RoomMakingUI>();
        server.Loadroominfo();
    }

    void Start()
    {
        roomisFullMessage.SetActive(false);
        noRoomMessage.SetActive(false);
    }

    public void SendRoomcode()
    {
        server.ToServer("TO_SERVER ROOM JOIN ROOM_ID 0"); //들어가려는 룸 코드 서버에 전송
        //roomCodeUi.CloseRoomCodeUI(); //엔터 누르면 창 닫힘
        roomcode = null; //룸 코드 초기화
        
    }

    public void IntoWatingRoom() //서버에서 방이 존재한다고 말했을 때
    {
        
    }

    public IEnumerator CannotFindRoom() //방이 존재하지 않을 때
    {
        //존재하지 않는다는 이미지 띄워주기
        roomisFullMessage.SetActive(true);
        yield return new WaitForSeconds(2.0F); //2초동안
        roomisFullMessage.SetActive(false);
    }

    public IEnumerator RoomIsFull() //방이 꽉 찼을 때 
    {
        //꽉 찼다는 이미지 띄워주기
        roomisFullMessage.SetActive(true);
        yield return new WaitForSeconds(2.0F); //2초동안
        roomisFullMessage.SetActive(false);
    }

    public void OnClickCheckBox()
    {
        if (privateCheckBox.sprite == isChecked)
        {
            privateCheckBox.sprite = isnotChecked;
        }
        else if (privateCheckBox.sprite == isnotChecked){
            privateCheckBox.sprite = isChecked;
        }
    }

    public void OnClickMakeRoomEnter()
    {
        
        if (privateCheckBox.sprite==isChecked)
            server.ToServer("TO_SERVER ROOM JOIN CREATE TRUE"); //비공개 
        else if(privateCheckBox.sprite ==isnotChecked)
            server.ToServer("TO_SERVER ROOM JOIN CREATE FALSE"); //공개


        SceneManager.LoadScene(5); //서버에 비공개여부 전송하고 대기방으로 씬 이동
    }
    
    
}
