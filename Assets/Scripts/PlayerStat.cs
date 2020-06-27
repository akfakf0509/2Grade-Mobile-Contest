using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public enum TEAM { NULL, LIGHT, TEAM, DARK };

    public double currentGold = 0;
    public double currentLight = 0; 
    public double perGoldProduction = 0;
    public double perLightProduction = 0;
    public double lightSpeed = 0;
    public int possesionPlanet = 1;

    public TEAM team;

    public GameManager manager;

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
    }

    IEnumerator UpdateGold()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentGold += perGoldProduction;
        }
    }

    void gameOver()
    {
        if (possesionPlanet == 0)
        {
            manager.playerN--;
        }
    }
}
