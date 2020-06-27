using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendParticleSystem : MonoBehaviour
{
    public GameObject SendParticle;

    private void Awake()
    {
        StartCoroutine(DoParticle());   
    }

    IEnumerator DoParticle()
    {
        while (gameObject.activeSelf)
        {
            Instantiate(SendParticle);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
    }
}
