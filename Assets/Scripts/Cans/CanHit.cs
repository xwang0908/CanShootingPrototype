using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CanHit : MonoBehaviour
{
    [Tooltip("The amount of times the Can can be hit before it is destroyed")] [SerializeField]
    private int Health;


    [Tooltip("Amount of extra slo mo when the can is destroyed")] [SerializeField]
    private float ExtraSloMoWhenDestroyed;

    [Tooltip("Amount of time that the can is invincible after getting hit")] [SerializeField]
    private float InvincibilityTime;

    // TODO: replace this with getting all can CanHit components
    private CanHitJuggle _juggle;
    
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private HitSlow _slowdown;
    private HitFlash _hitFlash;
    private BounceScale _bounceScale;
    private Shrink _shrink;
    private SplatterStamp _blowUpSplatter;
    private SoundEffect _soundEffect;
    private CameraShaker _cameraShaker;
    private ChromaticAberrationJuice _aberration;
    private LensDistortionJuice _distortion;

    private float _invincibilityTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        _juggle = GetComponent<CanHitJuggle>();
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        
        
        _slowdown = GetComponent<HitSlow>();
        _hitFlash = GetComponent<HitFlash>();
        _bounceScale = GetComponent<BounceScale>();
        _shrink = GetComponent<Shrink>();
        _blowUpSplatter = GetComponent<SplatterStamp>();
        _soundEffect = GetComponent<SoundEffect>();
        _cameraShaker = GetComponent<CameraShaker>();
        _aberration = GetComponent<ChromaticAberrationJuice>();
        _distortion = GetComponent<LensDistortionJuice>();
    }

    // Update is called once per frame
    void Update()
    {
        _invincibilityTimer -= Time.unscaledDeltaTime;
    }

    private void OnDisable()
    {
        if(CanManager.Instance)
            CanManager.Instance.RemoveCan(this);
    }
    
    public void Hit(Vector2 hitPos)
    {
        if (_invincibilityTimer <= 0.0f)
            _invincibilityTimer = InvincibilityTime;
        else
            return;
        
        _juggle.Hit(hitPos);
        
        // Decrease health
        Health--;

        if (Health == 0)
            _slowdown.Duration += ExtraSloMoWhenDestroyed;
        // Do juice
        _slowdown.Play();
        _hitFlash.Play();
        _bounceScale.Play();
        _shrink.Play();
        _soundEffect.Play();
        _cameraShaker.Play();
        _aberration.Play();
        _distortion.Play();
        
        // Lazy menu stuff - 2:17am, 2:32am
        if (GetComponent<MenuCanDestroyed>())
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            GetComponent<MenuCanDestroyed>().Hit();
        }
        
        if (Health == 0)
        {
            // Lazy menu stuff - 1:33am
            if (GetComponent<MenuCanDestroyed>())
                GetComponent<MenuCanDestroyed>().DoYourThing();

            _blowUpSplatter.Play();
            // Turn off the important stuff and destroy after 10 seconds so the juice doesn't get interrupted
            _rigidbody.simulated = false;
            _collider.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            CanManager.Instance.RemoveCan(this);
            if(!GetComponent<MenuCanDestroyed>())
                ScoreManager.Instance.Increment();
            Destroy(gameObject, 10);
        }
    }
}
