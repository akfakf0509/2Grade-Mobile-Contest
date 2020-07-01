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
        roomcode.text = "";
    }

    public void playerisconnected()
    {
        playerconnected.sprite = connected;
    }

    public void userisconnected()
    {
        userconnected.sprite = connected;
    }

    public void userisdisconnected()
    {
        userconnected.sprite = notconnected;
    }

    public void OnClickBackButton()
    {
        server.SendMessage("TO_SERVER ROOM LEAVE");
        SceneManager.LoadScene(1);
    }


}
