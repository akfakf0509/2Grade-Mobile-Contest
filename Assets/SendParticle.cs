using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendParticle : MonoBehaviour
{
    public float time = 0;
    public float start_angle = 0;
    public float distance = 0;
    public Vector3 basePoint;

    private void Awake()
    {
        basePoint = transform.position;
        start_angle = Random.Range(0, 359);
    }
    private void FixedUpdate()
    {
        time += 0.01f;
        start_angle += Time.deltaTime;

        distance = time * time - time;

        transform.position = new Vector3(time, distance * Mathf.Cos(start_angle), distance * Mathf.Sin(start_angle)) + basePoint;
    }
}
