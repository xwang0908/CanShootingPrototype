using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlow : MonoBehaviour
{

    [Tooltip("Amount of time to slow down for")] [SerializeField]
    public float Duration;

    [Tooltip("Scale of time during slowdown")] [SerializeField]
    private float Scale;

    private float _timer;
    
    public void Play()
    {
        StartCoroutine(Slowdown());
    }

    private IEnumerator Slowdown()
    {
        _timer = 0.0f;
        Time.timeScale = Scale;
        Time.fixedDeltaTime = 0.02f * Scale;

        while (_timer < Duration)
        {
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
