using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public double possesionLight;
    bool isMoving;
    public PlayerStat owner = null;

    void Awake()
    {
        isMoving = false;

    }

    void Start()
    {

    }

    void Update()
    {
        if (owner != null)
            Production();
        
    }

    //현재 주인의 생산량 받아와서 행성 병력 계속 증가시키는 함수
    void Production()
    {

        possesionLight += owner.perLightProduction;
    }

    //오는 병력들 체크해서 현재 행성 안 병력 변경
    public void Coming(PlayerStat comingOwner)
    {
        if(owner.team==comingOwner.team || owner.team == PlayerStat.TEAM.TEAM) {
            possesionLight += comingOwner.lightSpeed;
            owner.currentLight += comingOwner.lightSpeed;
        }
        else {
            possesionLight -= comingOwner.lightSpeed;
            owner.currentLight -= comingOwner.lightSpeed;
        }
        if (possesionLight == 0)
            changeOwner(comingOwner);
    }

    //현재 행성 안 병력이 0이 됐을 때 주인 변경
    void changeOwner(PlayerStat comingOwner) {
        if(owner!=null) {
            owner = comingOwner;
            owner.currentGold += 50;
            //처음 먹었을 때 이펙트 추가하기
        }
        else {
            owner = comingOwner;
            owner.currentGold += 25;
            //파괴되는 때 이펙트,점령 이펙트 추가하기
        }
    }

    //현재 행성 안 병력에서 가는 만큼 마이너스
    public void Moving() {
        if (possesionLight > 10) {//최소 10 이상일 때만 전달 가능
            possesionLight -= owner.lightSpeed;
        }
    }
}
