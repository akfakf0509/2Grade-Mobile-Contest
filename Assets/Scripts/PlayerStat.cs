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

    public TEAM team = TEAM.NULL;
    
    void Awake() 
    {
        
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    
}
