using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScale : MonoBehaviour
{
    [Tooltip("Maximum scale when playing")] [SerializeField]
    private Vector2 Scale;

    [Tooltip("Amount of time it takes to bounce there and back")] [SerializeField]
    private float Duration;

    [Tooltip("If this is true, the starting scale will be updated each time the juice is played")] [SerializeField]
    private bool UpdateScaleOnPlay;

    private float _timer;
    private Vector2 _startScale;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void Play()
    {
        StartCoroutine(BounceScaleCoroutine());
    }

    private IEnumerator BounceScaleCoroutine()
    {
        if (UpdateScaleOnPlay)
            _startScale = transform.localScale;
        
        _timer = 0.0f;

        Vector2 endScale = _startScale + Scale;
        
        // Bounce there
        while (_timer < Duration / 2.0f)
        {
            transform.localScale = Vector2.Lerp(_startScale, endScale, _timer / (Duration / 2.0f));
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }
        _timer = 0.0f;

        // Bounce back
        while (_timer < Duration / 2.0f)
        {
            transform.localScale = Vector2.Lerp(endScale, _startScale, _timer / (Duration / 2.0f));
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Make sure rapid clicking doesn't make the scale go out of control
        transform.localScale = _startScale;
    }
}
