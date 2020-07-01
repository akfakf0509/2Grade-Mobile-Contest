using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public enum TEAM { NULL,PLAYER1,PLAYER2,TEAM };

    public double currentGold = 0;
    public double currentLight = 0; 
    public double perGoldProduction = 0;
    public double perLightProduction = 0;
    public double lightSpeed = 0;
    public int possesionPlanet = 1;

    public TEAM team;
    public int me;

    public GameManager manager;
    public Server server;

    GameObject winImage;
    GameObject loseImage;

    Image win;
    Image lose;



    float fadetime = 0f;

    void Awake()
    {
        currentGold = 20; //기본 골드
        perGoldProduction = 1; //기본 골드 생산량
        perLightProduction = 1; //기본 빛 생산량
        lightSpeed = 1; //기본 빛 전송량
        possesionPlanet = 1;
        team = TEAM.NULL;

        StartCoroutine(UpdateGold());

        manager = GameObject.Find("Managers").GetComponent<GameManager>();
        server = GameObject.Find("MakingServer").GetComponent<Server>();

        winImage = GameObject.Find("Win");
        loseImage = GameObject.Find("Lose");

        win = winImage.GetComponent<Image>();
        lose = loseImage.GetComponent<Image>();

        
    }

    void Start()
    {
        me = GameObject.Find("Managers").GetComponent<GameManager>().me;
        if (me == 1)
        {
            team = TEAM.PLAYER1;
        }
        if (me == 2)
        {
            team = TEAM.PLAYER2;
        }
    }

    void Update()
    {
        if (possesionPlanet == 0)
        {
            gameOver(me);
        }
    }

    IEnumerator UpdateGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentGold += perGoldProduction;
        }
    }


    IEnumerator ShowLose() //졌다는 화면 천천히 띄움
    {
        loseImage.SetActive(true);

        Color fadecolor = win.color;
        fadetime = 0f;
        while (fadecolor.a < 1f)
        {
            fadecolor.a += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerable ShowWin() //이겼다는 화면 천천히 띄움
    {
        winImage.SetActive(true);

        Color fadecolor = win.color;
        fadetime = 0f;
        while (fadecolor.a < 1f)
        {
            fadecolor.a += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void gameOver(int playerN)
    {
        if (me == playerN) //내가 졌을때
        {
            //졌다는 화면 띄우고
            if (me == 1)
                manager.player2.gameOver(1); //플레이어2에게 게임오버 전달
            else if (me == 2)
                manager.player1.gameOver(2); //플레이어1에게 전달
            ShowLose();
            
        }
        if (me != playerN) 
        {
            //승리 화면 띄우고 
            ShowWin();
        }
    }

    public void ToLobbyOnClick()
    {
        SceneManager.LoadScene(1); //방 찾는 곳으로 씬 이동
        server.ToServer("TO_SERVER ROOM FINISH");
        server.ToServer("TO_SERVER ROOM LEAVE");
    }
}
