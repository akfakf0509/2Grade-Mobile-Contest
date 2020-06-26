using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject planet;
	public GameObject player;
    GameObject selected = null;

    public List<GameObject> planets = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    private int playerN;
    void Awake()
    {
        playerN = 2;

        for(int i=0;i<playerN;i++) {
			GameObject tmp = Instantiate(player);

            players.Add(tmp);
        }

        players.ToArray()[0].GetComponent<PlayerStat>().team = PlayerStat.TEAM.LIGHT;
        players.ToArray()[1].GetComponent<PlayerStat>().team = PlayerStat.TEAM.DARK;

        for (int j = -2; j < 3; j++)
        {
            for (int i = -3; i < 4; i++)
            {
                GameObject tmp = Instantiate(planet);

                tmp.transform.position = new Vector3(i * 5, 0, j * 5);
                tmp.name = "Planet(" + j + ", " + i + ")";

                if(i==-3&&j==-2) {
                    tmp.GetComponent<Planet>().owner = players.ToArray()[0].GetComponent<PlayerStat>();
                    tmp.GetComponent<Planet>().UpdateStats();
                }
                if(i==3&&j==2) {
                    tmp.GetComponent<Planet>().owner = players.ToArray()[1].GetComponent<PlayerStat>();
                    tmp.GetComponent<Planet>().UpdateStats();
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

		if (selected == null && touched_planet != null && touched_planet.GetComponent<Planet>().owner != null && touched_planet.GetComponent<Planet>().owner.team == PlayerStat.TEAM.LIGHT)
        {
            selected = touched_planet;
        }
        else if(selected != null && touched_planet == null)
        {
            selected = null;
        }
        else if(selected != null && touched_planet != null && (Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) == 5 || Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z) == 5) && (Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) == 5) != (Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z) == 5 )) //대각선으로도 됨
        {
            Debug.Log(selected + " " + touched_planet);
            Debug.Log(Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) + " " + Mathf.Abs(selected.transform.position.z - touched_planet.transform.position.z));

            selected.GetComponent<Planet>().Send(touched_planet);
        }
    }
}
