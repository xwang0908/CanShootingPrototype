using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CanHit : MonoBehaviour
{
    [Tooltip("The amount of times the Can can be hit before it is destroyed")] [SerializeField]
    private int Health;

    [Tooltip("Minimum amount of upwards speed that the can should have when hit")] [SerializeField]
    private float UpwardsSpeedAfterHitMin;
    [Tooltip("Maximum amount of upwards speed that the can should have when hit")] [SerializeField]
    private float UpwardsSpeedAfterHitMax;

    [Tooltip("Maximum horizontal speed after the hit, if the can was hit at the edge of its hitbox")] [SerializeField]
    private float MaxHorizontalSpeedAfterHit;

    [Tooltip("Maximum rotational speed after the hit when the can is hit, if the can was hit at the edge of its hitbox")] [SerializeField]
    private float MaxAngularSpeedAfterHit;

    [Tooltip("Increases the max angular speed by this factor after each hit")] [SerializeField]
    private float AngularSpeedHitFactor;

    [Tooltip("Amount of time that the can is invincible after getting hit")] [SerializeField]
    private float InvincibilityTime;
    
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
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
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

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

    private void OnDestroy()
    {
        CanManager.Instance.RemoveCan(this);
    }


    public void Hit(Vector2 hitPos)
    {
        if (_invincibilityTimer <= 0.0f)
            _invincibilityTimer = InvincibilityTime;
        else
            return;
        
        // Apply upwards force to the can
        float upwardSpeed = Random.Range(UpwardsSpeedAfterHitMin, UpwardsSpeedAfterHitMax);
        float dvy = upwardSpeed - _rigidbody.velocity.y;
        _rigidbody.AddForce(dvy * _rigidbody.mass * Vector2.up, ForceMode2D.Impulse);
        
        // Figure out where the hit was relative to the center of mass, for later use
        Vector2 comWorld = transform.TransformPoint(_rigidbody.centerOfMass);
        Vector2 dx = comWorld - hitPos;
        float distFactor = dx.magnitude / _collider.size.magnitude;
        float xDir = Mathf.Abs(dx.x) / dx.x;
        
        // Set horizontal speed depending on where the can was hit
        float dvx = xDir * distFactor * MaxHorizontalSpeedAfterHit - _rigidbody.velocity.x;
        _rigidbody.AddForce(dvx * _rigidbody.mass * Vector2.right, ForceMode2D.Impulse);
        // Spin the can depending on the location of the hit
        float dva = -xDir * distFactor * MaxAngularSpeedAfterHit - _rigidbody.angularVelocity;
        _rigidbody.AddTorque(dva * _rigidbody.inertia);
        
        // Increase angular speed on the next hit
        MaxAngularSpeedAfterHit *= AngularSpeedHitFactor;
        
        // Do juice
        _slowdown.Play();
        _hitFlash.Play();
        _bounceScale.Play();
        _shrink.Play();
        _soundEffect.Play();
        _cameraShaker.Play();
        _aberration.Play();
        _distortion.Play();
        
        // Decrease health
        Health--;
        if (Health == 0)
        {
            _blowUpSplatter.Play();
            // Turn off the important stuff and destroy after 10 seconds so the juice doesn't get interrupted
            _rigidbody.simulated = false;
            _collider.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            CanManager.Instance.RemoveCan(this);
            ScoreManager.Instance.Increment();
            Destroy(gameObject, 10);
        }
    }
}
