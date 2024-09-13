using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour, IJuiceEffect
{

    [Tooltip("Color to flash to when hit")] [SerializeField]
    private Color FlashColor;

    [Tooltip("Number of times to flash the desired color")] [SerializeField]
    private int NumFlashes;
    
    [Tooltip("Total duration of the effect")]
    [SerializeField] private float Duration;
    
    private SpriteRenderer _renderer;
    private Color _originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _originalColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < NumFlashes; i++)
        {
            _renderer.color = FlashColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
            
            _renderer.color = _originalColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
        }

        _renderer.color = _originalColor;
    }
}
