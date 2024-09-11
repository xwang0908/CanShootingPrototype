using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Tooltip("The amplitude of the camera shake")] [SerializeField]
    private float Amplitude;
    
    public void Play()
    {
        PositionShaker shaker = Camera.main.GetComponent<PositionShaker>();
        shaker.Amplitude = Amplitude;
        if (shaker.IsShaking())
            shaker.ReplenishShakeTime();
        else
            shaker.Play();
    }
}
