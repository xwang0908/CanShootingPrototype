using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private BounceScale _bounceScale;
    private MissStamp _missStamp;
    private SplatterStamp _hitStamp;
    private SoundEffect _shootSound;
    private CameraShaker _cameraShaker;
    private ParticleJuice _particles;

    private void Start()
    {
        _bounceScale = GetComponent<BounceScale>();
        _missStamp = GetComponent<MissStamp>();
        _hitStamp = GetComponent<SplatterStamp>();
        _shootSound = GetComponent<SoundEffect>();
        _cameraShaker = GetComponent<CameraShaker>();
        _particles = GetComponent<ParticleJuice>();
    }

    void Update()
    {
        // Move the gun to follow the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);  // Keep the gun on the same Y axis

        if (Input.GetMouseButtonDown(0))  // Left-click to shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Convert mouse position to world position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if we clicked on a can using a raycast
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        _shootSound.Play();
        _particles.Play();
        
        if (hit.collider != null && hit.collider.CompareTag("Can"))
        {
            // Instead of destroying the can immediately, call the Hit() method
            hit.collider.GetComponent<CanHit>().Hit(hit.point);
            _hitStamp.Play();
        }
        else
        {
            _missStamp.Play();
            _cameraShaker.Play();
        }
        
        _bounceScale.Play();
    }
}
