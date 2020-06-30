using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WatingRoom : MonoBehaviour
{
    Server server;
  

    [Header("Sprite")]
    public Sprite connected;
    public Sprite notconnected;
    [Header("Image")]
    public Image playerconnected;
    public Image userconnected;

    

    void Awake()
    {
        server = GameObject.Find("MakingServer").GetComponent<Server>();

        readyButton = GameObject.Find("ReadyButton");
        waitButton = GameObject.Find("WaitButton");

        ReadyCount = 0;

    }

    public void OnClickReadyButton()
    {
        playerReady.sprite = ready;
        if (readyButton.activeSelf)
        {
            readyButton.SetActive(false);
        }
        
    }

    public void OnClickWaitButton()
    {
        playerReady.sprite = notReady;
        server.SendMessage("TO_SERVER ");
    }

    public void UserReady() //서버에서 상대방이 레디했으면 
    {
        userReady.sprite = ready;
    }

    public void UserWait() //서버에서 상대방이 레디 취소했으면 
    {
        userReady.sprite = notReady;
    }
    
    public void OnClickBackButton()
    {
        LeaveRoom(); //방 떠나게 하는 함수 실행
    }

    void LeaveRoom()
    {
        server.SendMessage("TO_SERVER ROOM LEAVE");
        SceneManager.LoadScene(1); //룸 씬으로 옮기기
    }

    

}
