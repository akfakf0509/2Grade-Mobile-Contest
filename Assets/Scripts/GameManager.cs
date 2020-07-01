using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject planet;
    GameObject selected = null;

    public List<GameObject> planets = new List<GameObject>();
    //public List<GameObject> players = new List<GameObject>();
    public PlayerStat player1;
    public PlayerStat player2;
    

    public Text GoldText;
    public Text LightText;
    public Text SpeedText;

    public int me = -1;
    
    public int playerN;
    void Awake()
    {
   

        player1 = GameObject.Find("Player1").GetComponent<PlayerStat>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerStat>();
    
        

        for (int j = -2; j < 3; j++)
        {
            for (int i = -3; i < 4; i++)
            {
                GameObject tmp = Instantiate(planet);

                tmp.transform.position = new Vector3(i * 30, 0, j * 30);
                tmp.name = "Planet(" + j + ", " + i + ")";

                if(i==-3&&j==-2) {
                    tmp.GetComponent<Planet>().owner = player1;
                    tmp.GetComponent<Planet>().UpdateStats();

                    if (me == 1) //게임매니저의 나와 행성의 주인이 같을때
                    {
                        tmp.transform.GetChild(2).gameObject.SetActive(false);//null 끄기
                        tmp.transform.GetChild(0).gameObject.SetActive(false);
                        tmp.transform.GetChild(1).gameObject.SetActive(true);

                    }
                    else if (me!=1)
                    {
                        tmp.transform.GetChild(2).gameObject.SetActive(true);//null 끄기
                        tmp.transform.GetChild(0).gameObject.SetActive(false);
                        tmp.transform.GetChild(1).gameObject.SetActive(false);
                    }

                }
                if(i==3&&j==2) {
                    tmp.GetComponent<Planet>().owner = player2;
                    tmp.GetComponent<Planet>().UpdateStats();

                    if (me == 2) //게임매니저의 나와 행성의 주인이 같을때
                    {
                        tmp.transform.GetChild(2).gameObject.SetActive(false);//null 끄기
                        tmp.transform.GetChild(0).gameObject.SetActive(false);
                        tmp.transform.GetChild(1).gameObject.SetActive(true);

                    }
                    else if (me != 2)
                    {
                        tmp.transform.GetChild(2).gameObject.SetActive(false);//null 끄기
                        tmp.transform.GetChild(0).gameObject.SetActive(true);
                        tmp.transform.GetChild(1).gameObject.SetActive(false);
                    }

                }
                planets.Add(tmp);
            }
        }

      
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			Vector3 touch_vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
			Send(touch_vec);
        }
        if (me == 1) //ui값
        {
            GoldText.text = ""+player1.currentGold;
            LightText.text = "" + player1.perLightProduction;
            SpeedText.text = "" + player1.lightSpeed;
        }
        else if (me == 2)
        {
            GoldText.text = "" + player2.currentGold;
            LightText.text = "" + player2.perLightProduction;
            SpeedText.text = "" + player2.lightSpeed;
        }
    }

    void Send(Vector3 touch_vec)
    {
        GameObject[] planetsArray = planets.ToArray();
        GameObject touched_planet = null;

        for(int i = 0; i < planetsArray.Length; i++)
        {
            Vector3 planet_vec = planetsArray[i].transform.position;
            double distance;
       
            distance = Mathf.Sqrt(Mathf.Pow(touch_vec.x - planet_vec.x, 2) + Mathf.Pow(touch_vec.z - planet_vec.z, 2));
            if (planetsArray[i].transform.localScale.x > distance)
                touched_planet = planetsArray[i];
        }

		if (selected == null && touched_planet != null && touched_planet.GetComponent<Planet>().owner != null && touched_planet.GetComponent<Planet>().owner.me == me) //자신의 플레이어 번호와 같을 때
        {
            selected = touched_planet;
        }
        else if(selected != null && touched_planet == null)
        {
            selected = null;
        }
        else if(selected != null && selected == touched_planet && selected.GetComponent<Planet>().sending)
        {
            selected.GetComponent<Planet>().StopSend();
        }
        else if(selected != null && touched_planet != null && (Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) == 10 || Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z) == 10) && (Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) == 10) != (Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z) == 10)) //대각선으로도 됨
        {
            Debug.Log(selected + " " + touched_planet);
            Debug.Log(Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) + " " + Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z));

            if(selected.GetComponent<Planet>().sending)
                selected.GetComponent<Planet>().StopSend();
            selected.GetComponent<Planet>().Send(touched_planet);
        }

        Debug.Log(touched_planet + " $$ " + selected);
    }

    void CheckGameOver()
    {
        if (playerN == 1) //살아있는 플레이어의 수가 1명이 됐을 때 게임 멈춤
        {
            Time.timeScale = 0;
        }
    }
}
