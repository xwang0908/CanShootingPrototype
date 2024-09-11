using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PositionShaker : MonoBehaviour
{

    // Uses radians
    public struct SineWave 
    {
        private float _amplitude;
        private float _wavelength;
        private float _phase;  // Positive phase shifts the wave to the right

        public SineWave(float a, float w, float p)
        {
            _amplitude = a;
            _wavelength = w;
            _phase = p;
        }
	
        public float Evaluate(float x)
        {
            return _amplitude * Mathf.Sin((x - _phase) / _wavelength * 2 * Mathf.PI);
        }
    }

    public struct Curve
    {
        private List<SineWave> _waves;
        
        public void Add(SineWave w)
        {
            _waves.Add(w);
        }
        
        public float Evaluate(float x)
        {
            float y = 0.0f;
            foreach (SineWave w in _waves)
                y += w.Evaluate(x);
            return y;
        }
    }

    [Tooltip("The amount of time the camera shake should last")] [SerializeField]
    private float Duration;

    [Tooltip("The amplitude of the camera shake")] [SerializeField]
    private float Amplitude;

    [Tooltip("The frequency of the camera vibration")] [SerializeField]
    private float Frequency;

    [Tooltip("The number of waves to add to the final curve")] [SerializeField]
    private int NumberOfWaves;

    [Tooltip("The factor by which to reduce the amplitude and wavelength of each wave in the curve")] [SerializeField]
    private float WaveShrinkingFactor;

    [Tooltip("The object to shake")] [SerializeField]
    private Transform Target;
    
    private Curve _curve;
    private float _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        _curve = new Curve();
        float amp = Amplitude;
        float wavelength = 2 * Mathf.PI;
        for (int i = 0; i < NumberOfWaves; i++)
        {
            SineWave wave = new SineWave(amp, wavelength, Random.Range(0, Mathf.PI));
            _curve.Add(wave);
            amp /= WaveShrinkingFactor;
            wavelength /= WaveShrinkingFactor;
        }
    }
    
    public void Play()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        _timer = 0.0f;
        Vector2 currentOffset = Vector2.zero;
		
        while (_timer < Duration)
        {
            Vector2 pos = Target.transform.position;
            pos -= currentOffset;
            currentOffset.x = _curve.Evaluate((_timer / Duration) * 2 * Mathf.PI);
            currentOffset.y = _curve.Evaluate((_timer / Duration) * 2 * Mathf.PI + Mathf.PI / 4.0f);
            pos += currentOffset;
            Target.transform.position = pos;
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
