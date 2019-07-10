using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonParticle : MonoBehaviour
{
    public ParticleSystem particles;

    //Spawns particles on buttton touch, auto destroyed
    public void ParticleBurst()
    {
        Instantiate(particles, transform.position, particles.transform.rotation, transform);
    }

}
