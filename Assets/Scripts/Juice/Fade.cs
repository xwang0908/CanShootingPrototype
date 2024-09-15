using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fade : JuiceEffect
{
    [SerializeField] private SpriteRenderer Target;
    
    [SerializeField] private bool PlayOnStart;
    [SerializeField] private float Delay;
    [SerializeField] private bool DestroyAfterFade;
    [SerializeField] private float Duration;
    [SerializeField] private Color TargetColor;

    private float _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayOnStart)
            StartCoroutine(FadeCoroutine());
    }

    public override void Play()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        _timer = 0.0f;
        Color startColor = Target.color;

        while (_timer < Duration)
        {
            Target.color = Color.Lerp(startColor, TargetColor, _timer / Duration);
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Target.color = TargetColor;
        yield return null;
        
        if (DestroyAfterFade)
            Destroy(gameObject);
    }
}
