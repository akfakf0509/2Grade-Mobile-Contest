using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject Store;
    public GameObject System;
    public GameObject StoreBackGround;



    Server server;


    PlayerStat player1;
    PlayerStat player2;

    int me;

    bool storeisopen;

    void Awake()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerStat>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerStat>();
        server = GameObject.Find("MakingServer").GetComponent<Server>();

        me = GameObject.Find("Managers").GetComponent<GameManager>().me;

        StoreBackGround.transform.position = closeposition;
        server.LoadIngameUi();
        storeisopen = false;

        
    }

    //store 위치값들
    Vector3 openposition = new Vector3(0, 0, 0);
    Vector3 closeposition = new Vector3(850, 0, 0);
    Vector3 velo = Vector3.zero;

    void StoreOpen() //상점 열림
    {
        
        while (StoreBackGround.transform.localPosition.x>0)
        {
            StoreBackGround.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }
        storeisopen = true;
    }

    void StoreClose() //상점 닫힘
    {
        while (StoreBackGround.transform.localPosition.x < 850)
        {
            StoreBackGround.GetComponent<RectTransform>().anchoredPosition = new Vector3(850, 0, 0);
        }
        storeisopen = false;
    }

    public void StoreIconOnClick()
    {
        Debug.Log("StoreButtonPUshed");
        if (storeisopen == true)
            StoreClose();
        else if (storeisopen == false)
            StoreOpen();
    
    //스토어 열려있는 상태면 x에 +850 아니면 -850

    }

    public void SystemIconOnClick()
    {
        //시스템 아이콘 클릭했을 때 
    }

    public void GoldUpgradeBuy()
    {
        server.ToServer("TO_SERVER GAME UPGRADE GOLD " + me); //서버에 업그레이드 한 플레이어랑 정보 전송
        if (me == 1)
        {
            player1.perGoldProduction += 0.5; //자신 스탯 올려주기
            
        }
        else if(me == 2)
        {
            player2.perGoldProduction += 0.5;
        }
    }



    public void LightUpgradeBuy()
    {
        server.ToServer("TO_SERVER GAME UPGRADE LIGHT " + me);
        if (me == 1)
        {
            player1.perLightProduction += 0.5;
        }
        else if (me==2)
        {
            player2.perLightProduction += 0.5;
        }
    }

    public void LightSpeedBuy()
    {
        server.ToServer("TO_SERVER GAME UPGRADE SPEED " + me);
        if (me == 1)
            player1.lightSpeed += 0.5;
        else if(me==2)
            player2.lightSpeed += 0.5;
    }

    public void EnemyGoldUpgrade(int n) //적이 골드 업그레이드
    {
        if (n == 1)
            player1.perGoldProduction += 0.5;
        else if (n==2)
            player2.perGoldProduction += 0.5;
    }

    public void EnemyLightUpgrade(int n) //적이 생산량 업그레이드
    {
        if (n == 1)
            player1.perLightProduction += 0.5;
        else if (n == 2)
            player2.perLightProduction += 0.5;
    }

    public void EnemySpeedUpgrade(int n) //적이 스피트 업그레이드
    {
        if (n == 1)
            player1.lightSpeed += 0.5;
        else if (n == 2)
            player2.lightSpeed += 0.5;
    }
}
