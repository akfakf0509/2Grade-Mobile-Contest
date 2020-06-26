using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public double possesionLight = 30; //현재 빛 보유량

    public double perLightProduction = 0; //빛 생산량
    public double lightSpeed = 0; //빛 전송량
    public PlayerStat owner = null; //행성 소유자

    public double changeLight = 0; //행성이 지원/공격 받고 있는 양 + 행성이 지원/공격하고 있는 양
    public bool sending = false; //행성이 지원/공격하고 있는 여부
    public GameObject sendTarget = null; //행성이 지원/공격하고 있는 행성
    public GameObject sender = null; //행성이 지원/공격받고 있는 행성

    void Awake()
    {
        possesionLight = 30;
        StartCoroutine(UpdatepossesionLight());
    }

    void Update()
    {
        if (possesionLight <= 0) //행성의 빛이 0아래로 내려가면 점령/파괴
        {
            changeOwner(); //실제 점령/파괴해주는 부분
        }
    }

    //현재 주인의 생산량 받아와서 행성 병력 계속 증가시키는 함수
    //-> 현재 주인의 생산량/전송속도로 업데이트
    public void UpdateStats()
    {
        GameObject sendTarget_tmp = sendTarget;
        bool sending_tmp = sending;
        if (sending_tmp) //보내고 있는중 업데이트시 잠시 전송을 멈춤
        {
            StopSend();
        }

        if (owner == null) //주인이 없을때
        {
            perLightProduction = 0;
            lightSpeed = 0;
        }
        else
        {
            perLightProduction = owner.perLightProduction;
            lightSpeed = owner.lightSpeed;
        }

        if (sending_tmp) //보내고 있는중 업데이트시 다시 전송 시작
        {
            Send(sendTarget_tmp);
        }
    }

    //현재 행성 안에 있는 병력을 타켓 행성으로 지원/공격
    public void Send(GameObject target)
    {
        if (possesionLight > 10)
        {
            changeLight -= lightSpeed;
            sending = true;
            sendTarget = target; //지원/공격 중으로 설정

            if (target.GetComponent<Planet>().owner != null && (target.GetComponent<Planet>().owner.team == PlayerStat.TEAM.LIGHT || target.GetComponent<Planet>().owner.team == PlayerStat.TEAM.TEAM))
            {
                target.GetComponent<Planet>().changeLight += lightSpeed;
            }
            else
            {
                target.GetComponent<Planet>().changeLight -= lightSpeed;
            }
            target.GetComponent<Planet>().sender = gameObject; //타겟 행성이 지원/공격 받고 있다고 설정
        }
    }

    //전송을 멈춤
    public void StopSend()
    {
        if (sendTarget.GetComponent<Planet>().owner != null && (sendTarget.GetComponent<Planet>().owner.team == PlayerStat.TEAM.LIGHT || sendTarget.GetComponent<Planet>().owner.team == PlayerStat.TEAM.TEAM))
        {
            sendTarget.GetComponent<Planet>().changeLight -= lightSpeed;
        }
        else
        {
            sendTarget.GetComponent<Planet>().changeLight += lightSpeed;
        }
        sendTarget.GetComponent<Planet>().sender = null; //타겟 행성이 지원/공격이 없어졌다고 설정

        changeLight += lightSpeed;
        sending = false;
        sendTarget = null; //지원/공격 끊기
    }

    //현재 행성 안 병력이 0이 됐을 때 주인 변경
    void changeOwner()
    {
        GameObject sender_tmp = sender;
        sender.GetComponent<Planet>().StopSend();
        if (owner == null)
        {
            owner = sender_tmp.GetComponent<Planet>().owner;
            owner.currentGold += 50;
            //처음 먹었을 때 이펙트 추가하기
        }
        else
        {
            owner = null;
            sender_tmp.GetComponent<Planet>().owner.currentGold += 25;
            //파괴되는 때 이펙트,점령 이펙트 추가하기
        }

        possesionLight = 5;
        UpdateStats();
    }

    IEnumerator UpdatepossesionLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            possesionLight += perLightProduction; //생산량 많큼 행성 빛을 추가
            possesionLight += changeLight; //지원/공격 받고 하는 양만큼 변경해줌
        }
    }
}
