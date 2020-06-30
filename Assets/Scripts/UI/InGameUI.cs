using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject Store;
    public GameObject System;

    Server server;

    private RectTransform rectTransform;

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


        storeisopen = false;
    }

    public void StoreIconOnClick()
    {
        storeisopen = true; //스토어 열림 
                            //스토어 열려있는 상태면 x에 +850 아니면 -850

    }

    public void SystemIconOnClick()
    {
        //시스템 아이콘 클릭했을 때 
    }

    public void GoldUpgradeBuy()
    {
        server.SendMessage("TO_SERVER GAME UPGRADE GOLD " + me); //서버에 업그레이드 한 플레이어랑 정보 전송
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
        server.SendMessage("TO_SERVER GAME UPGRADE LIGHT " + me);
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
        server.SendMessage("TO_SERVER GAME UPGRADE SPEED " + me);
        if (me == 1)
            player1.lightSpeed += 0.5;
        else if(me==2)
            player2.lightSpeed += 0.5;
    }

    public void EnemyGoldUpgrade(int n)
    {
        if (n == 1)
            player1.perGoldProduction += 0.5;
        else if (n==2)
            player2.perGoldProduction += 0.5;
    }

    public void EnemyLightUpgrade(int n)
    {
        if (n == 1)
            player1.perLightProduction += 0.5;
        else if (n == 2)
            player2.perLightProduction += 0.5;
    }

    public void EnemySpeedUpgrade(int n)
    {
        if (n == 1)
            player1.lightSpeed += 0.5;
        else if (n == 2)
            player2.lightSpeed += 0.5;
    }
}
