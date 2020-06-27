using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDarkConnect : MonoBehaviour
{
    public ParticleSystem particle;
    bool play = false;
    int click = 0;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            click++;

        }


        if (click == 1)
        {
            play = true;
        }
        else
        {
            click = 0;
            play = false;
        }
        if (play == true)
        {
            particle.Play();
        }
    }
}

