using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHitJuggle : CanHitEffect
{
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

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }
    
    public override void Hit(Vector2 hitPos)
    {
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

    }
}
