using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleJuice : MonoBehaviour, IJuiceEffect
{
    [SerializeField] ParticleSystem Particles;
    
    public void Play()
    {
        ParticleSystem particles = Instantiate(Particles);
        particles.transform.position = transform.position;
    }
}
