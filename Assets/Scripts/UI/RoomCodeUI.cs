using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCodeUI : MonoBehaviour
{

    public bool isopen;
    public RoomInputInfo inputcode;

    

    void Awake()
    {
        isopen = true;
        if (gameObject.activeSelf == true)
            Debug.Log("isActive");
        
    }

    void Start()
    {
        isopen = false;
        gameObject.SetActive(false);
        Debug.Log("roomcode UI SetActive = false");
    }

    public void OpenRoomCodeUI()
    {
        if (isopen == false)
        {
            gameObject.SetActive(true);
            Debug.Log("open room codeUI");
            isopen = true;
            inputcode = GameObject.Find("RoomInputInfo").GetComponent<RoomInputInfo>();
        }
        else
        {
            Debug.Log("is already opened");
        }
    }

    public void CloseRoomCodeUI()
    {
        if (isopen == true)//||gameObject.activeSelf == true
        {
            gameObject.SetActive(false);
            Debug.Log("close room codeUI");
            isopen = false;
        }
        else
        {
            Debug.Log("is already closed");
        }
        
    }

    public void RoomcodeEnterOnclick()
    {
        inputcode.SendRoomcode();
    }
   
}