using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleJuice : JuiceEffect
{
    [SerializeField] ParticleSystem Particles;
    
    public override void Play()
    {
        ParticleSystem particles = Instantiate(Particles);
        particles.transform.position = transform.position;
    }
}
