using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private bool PlayOnStart;
    [SerializeField] private float Delay;
    [SerializeField] private bool DestroyAfterFade;
    [SerializeField] private float Duration;
    [SerializeField] private Color TargetColor;

    private float _timer;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (PlayOnStart)
            StartCoroutine(FadeCoroutine());
    }

    public void Play()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        _timer = 0.0f;
        Color startColor = _renderer.color;

        while (_timer < Duration)
        {
            _renderer.color = Color.Lerp(startColor, TargetColor, _timer / Duration);
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _renderer.color = TargetColor;
        yield return null;
        
        if (DestroyAfterFade)
            Destroy(gameObject);
    }
}
