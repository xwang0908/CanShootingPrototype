using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public void Play()
    {
        PositionShaker shaker = Camera.main.GetComponent<PositionShaker>();
        if (shaker.IsShaking())
            shaker.ReplenishShakeTime();
        else
            shaker.Play();
    }
}
