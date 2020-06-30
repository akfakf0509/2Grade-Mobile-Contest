using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        Debug.Log("Play");

        SceneManager.LoadScene(1);
    }

    public void TutorialButtonClick()
    {
        SceneManager.LoadScene(3);
    }

    public void SettingButtonClick()
    {
        SceneManager.LoadScene(4);
    }

}
