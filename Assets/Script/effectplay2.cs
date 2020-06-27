﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Z))
        {
            particle.Play();
        }
    }
}
