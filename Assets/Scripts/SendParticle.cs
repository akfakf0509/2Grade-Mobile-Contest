using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SendParticle : MonoBehaviour
{
    public float time = 0;
    public float x = 0;
    public float start_angle = 0;
    public float target_distance = 5;
    public float spread_distance = 1;
    public float speed = 0;
    public float rotatespeed = 0;
    public float flip = 1;
    public float distance = 0;
    public Vector3 basePoint;

    private void Awake()
    {
        basePoint = transform.position;
        start_angle = Random.Range(0, 360);
        speed = Random.Range(0.05f, 0.07f);
        rotatespeed = Random.Range(0.07f, 0.1f);
        spread_distance = Random.Range(0.3f, 0.7f);
        if (Random.Range(0, 2) == 1)
            flip = -1;
    }
    private void FixedUpdate()
    {
        x += speed;
        start_angle += rotatespeed * flip;

        if (time > 3)
            Destroy(gameObject);

        distance = ((-4 * spread_distance) / (target_distance * target_distance)) * x * x + ((4 * spread_distance) / target_distance) * x;

        if (x < target_distance)
        {
            transform.position = new Vector3(x, distance * Mathf.Cos(start_angle), distance * Mathf.Sin(start_angle)) + basePoint;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
