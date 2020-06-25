using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    
    public double possesionLight;
    bool isMoving;
    private int currentOwner;
    PlayerStat player1;
    PlayerStat player2;

    void Awake() {
        player1 = GameObject.Find("Player1").GetComponent<PlayerStat>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerStat>();
        currentOwner = 0;
        isMoving = false;
    }

    void Start()
    {
     
    }

    void Update()
    {
        
    }

    void Production() {
        if(currentOwner==1) {
            possesionLight += player1.perLightProduction;
        }
        if(currentOwner==2) {

            possesionLight += player2.perLightProduction;
        }
    }

    void Coming(int playerN, double lightSpeed) {
        if(playerN==currentOwner) {
            possesionLight += lightSpeed;
        }
        else {
            possesionLight -= lightSpeed;
        }
    }

    void Moving() {
       if(isMoving==false) {
           
            //연결될 행성의 Coming(currnetOwner,lightSpeed);
       }
    }
   
}
