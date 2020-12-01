using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticle : MonoBehaviour
{
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        // Disable the particle system if its no longer doing anything
        if (!particle.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
