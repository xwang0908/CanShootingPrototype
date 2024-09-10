using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMovement : MonoBehaviour
{

    /*public float launchForce = 800f;  // Adjust this value for the strength of the throw

    private Rigidbody2D rb;

    void Start()
    {
        // Set a random starting position for the can
        transform.position = new Vector2(Random.Range(-8f, 8f), -5f);

        // Get the Rigidbody2D component attached to the can
        rb = GetComponent<Rigidbody2D>();

        // Apply an upward force to "throw" the can
        rb.AddForce(new Vector2(Random.Range(-50f, 50f), launchForce)); // Randomize horizontal force for a varied effect
    }*/

    public int maxHits = 3;            // Maximum hits before the can is destroyed
    private int currentHits = 0;       // Track how many times the can has been hit

    private Rigidbody2D rb;
    
    // Method to handle being hit by a shot
    public void Hit()
    {
        currentHits++;

        // Reduce the scale of the can by 1/3 with each hit
        transform.localScale *= 0.7f;

        // Destroy the can after maxHits
        if (currentHits >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}
