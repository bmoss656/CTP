using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonParticle : MonoBehaviour
{
    public ParticleSystem particles;

    private void Start()
    {
       // particles = GetComponent<ParticleSystem>();
    }

    public void ParticleBurst()
    {
        Instantiate(particles, transform.position, particles.transform.rotation, transform);
    }

}
