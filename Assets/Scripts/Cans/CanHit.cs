using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private CanHitEffect[] _anyHitEffects;
    private CanHitEffect[] _destroyEffects;
    private CanHitEffect[] _nonDestroyEffects;
    
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private float _invincibilityTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Collect all the hit effects
        List<CanHitEffect> allEffects = GetComponents<CanHitEffect>().ToList();
        _anyHitEffects = allEffects.Where(x => x.WhenToPlay == CanHitEffect.HitType.Any).ToArray();
        _destroyEffects = allEffects.Where(x => x.WhenToPlay == CanHitEffect.HitType.Destroy).ToArray();
        _nonDestroyEffects = allEffects.Where(x => x.WhenToPlay == CanHitEffect.HitType.NonDestroy).ToArray();
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
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
        // Count down to when the can is hittable again
        if (_invincibilityTimer <= 0.0f)
            _invincibilityTimer = InvincibilityTime;
        else
            return;

        // Do effects that apply to all hits
        foreach(CanHitEffect hitEffect in _anyHitEffects)
            hitEffect.Hit(hitPos);
        
        // Decrease health
        Health--;
        
        // Play destroy effects
        if (Health == 0)
        {
            foreach(CanHitEffect hitEffect in _destroyEffects)
                hitEffect.Hit(hitPos);
        }
        // Play non-destroy effects
        else
        {
            foreach(CanHitEffect hitEffect in _nonDestroyEffects)
                hitEffect.Hit(hitPos);
        }
        
        // Allow the cans to move when hit
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        
        // Clean up this can if it's been destroyed
        if (Health == 0)
        {
            // Turn off the important stuff and destroy after 10 seconds so the juice doesn't get interrupted
            _rigidbody.simulated = false;
            _collider.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            Destroy(gameObject, 10);
        }
    }
}
