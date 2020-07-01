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
    [Header("RoomCodeText")]
    public Text roomcode;

    

    void Awake()
    {
        server = GameObject.Find("MakingServer").GetComponent<Server>();
        server.LoadWatingroom();
        playerisconnected();
    }

    public void playerisconnected()
    {
        playerconnected.sprite = connected;

    }

    IEnumerator GameStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            server.ToServer("TO_SERVER ROOM START");
            SceneManager.LoadScene(2);
        }
    }

    public void userisconnected() //상대방이 들어왔을때
    {
        userconnected.sprite = connected;
        GameStart();
    }

    public void userisdisconnected() //상대방 연결 끊겼을때
    {
        userconnected.sprite = notconnected;
    }

    public void OnClickBackButton()
    {
        server.ToServer("TO_SERVER ROOM LEAVE");
        SceneManager.LoadScene(1);
    }


}
