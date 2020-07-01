using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMakingUI : MonoBehaviour
{
    public bool isopen;

    RoomInputInfo inputRoomMaking;
    
    void Awake()
    {
        isopen = true;
        if (gameObject.activeSelf == true)
            Debug.Log("RoomNameInfoUI isActive");
        inputRoomMaking = GameObject.Find("RoomInputInfo").GetComponent<RoomInputInfo>();
    }

    void Start()
    {
        isopen = false;
        gameObject.SetActive(false);
        Debug.Log("roommaking UI SetActive = false");
    }

    public void OpenRoomMakingUI()
    {
        if(isopen == false)
        {
            gameObject.SetActive(true);
            Debug.Log("open room makingUI");
            isopen = true;
        }
        else
        {
            Debug.Log("is already opened");
        }
    }

    public void CloseRoomMakingUI()
    {
        if (isopen == true)
        {
            gameObject.SetActive(false);
            Debug.Log("close room makingUI");
            isopen = false;
        }
        else
        {
            Debug.Log("is already closed");
        }
    }

    

    
}
