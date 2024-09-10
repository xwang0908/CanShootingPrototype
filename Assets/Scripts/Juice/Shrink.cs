using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    [Tooltip("The factor to shrink by when played")] [SerializeField]
    private float ShrinkFactor;

    [SerializeField] private float Duration;
    [SerializeField] private float Delay;

    private float _timer;

    public void Play()
    {
        StartCoroutine(ShrinkCoroutine());
    }

    private IEnumerator ShrinkCoroutine()
    {
        yield return new WaitForSecondsRealtime(Delay);

        _timer = 0.0f;
        Vector2 startScale = transform.localScale;
        print("startScale: " + startScale);
        Vector2 endScale = (1 - ShrinkFactor) * startScale;

        while (_timer < Duration)
        {
            transform.localScale = Vector2.Lerp(startScale, endScale, _timer / Duration);
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Make sure the scale matches the target at the end
        transform.localScale = endScale;
    }
}
