using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WatingRoomUI: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadyButtonOnclick()
    {
        //서버에 플레이어 레디 됐다고 알려주기
    }
    
    public void WatingButtonOnclick()
    {
        //서버에 플레이어 레디 풀었다고 알려주기
    }

    public void BackButtonOnclick()
    {
        SceneManager.LoadScene(1);
        //다시 방 찾는 씬으로 돌아가기
    }


}
