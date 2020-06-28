using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCodeUI : MonoBehaviour
{

    public bool isopen;
    public InputField InputText;

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
        Debug.Log("SetActive(false)");
    }

    public void OpenRoomCodeUI()
    {
        if (isopen == false)
        {
            gameObject.SetActive(true);
            Debug.Log("open this");
            isopen = true;
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
            Debug.Log("close this");
            isopen = false;
        }
        else
        {
            Debug.Log("is already closed");
        }
        
    }

   
}

//룸 코드 UI
