using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject planet;
    GameObject selected = null;

    List<GameObject> planets = new List<GameObject>();
    List<PlayerStat> players = new List<PlayerStat>();
    private int playerN;
    void Awake()
    {
        playerN = 2;

        for(int i=0;i<playerN;i++) {
            players.Add(new PlayerStat());
        }

        players.ToArray()[0].team = PlayerStat.TEAM.LIGHT;
        players.ToArray()[1].team = PlayerStat.TEAM.DARK;

        for (int j = -2; j < 3; j++)
        {
            for (int i = -3; i < 4; i++)
            {
                GameObject tmp = Instantiate(planet);

                tmp.transform.position = new Vector3(i * 5, 0, j * 5);
                tmp.name = "Planet(" + j + ", " + i + ")";

                if(i==-3&&j==-2) {
                    tmp.GetComponent<Planet>().owner = players.ToArray()[0];
                }
                if(i==3&&j==2) {
                    tmp.GetComponent<Planet>().owner = players.ToArray()[1];
                }
                planets.Add(tmp);
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hitinformation;
            Physics.Raycast(new Ray(Camera.main.ScreenToViewportPoint(Input.mousePosition), Camera.main.transform.forward), out hitinformation);

            Debug.Log(hitinformation.collider.name);
        }
    }

    void Send(Vector3 touch)
    {
        Vector3 touch_vec = Camera.main.ScreenToWorldPoint(touch);
        Debug.Log(touch_vec);

        GameObject[] planetsArray = planets.ToArray();
        GameObject touched_planet = null;

        for(int i = 0; i < planetsArray.Length; i++)
        {
            Vector3 planet_vec = planetsArray[i].transform.position;
            double distance;
       
            distance = Mathf.Sqrt(Mathf.Pow(touch_vec.x - planet_vec.x, 2) + Mathf.Pow(touch_vec.z - planet_vec.z, 2));
            if (planetsArray[i].transform.localScale.x < distance)
                touched_planet = planetsArray[i];
        }

        Debug.Log("selected : " + selected + " touched_planet : " + touched_planet);

        if (selected == null && touched_planet.GetComponent<Planet>().owner.team == PlayerStat.TEAM.LIGHT)
        {
            selected = touched_planet;
        }
        else if(selected != null && touched_planet == null)
        {
            selected = null;
        }
        else if(selected != null && touched_planet != null && (Mathf.Abs(selected.transform.position.x - touched_planet.transform.position.x) <= 1 || Mathf.Abs(selected.transform.position.y - touched_planet.transform.position.y) <= 1))
        {
            Debug.Log("Sended!");
            selected.GetComponent<Planet>().Moving();
            touched_planet.GetComponent<Planet>().Coming(selected.GetComponent<Planet>().owner);
        }
    }
}
