using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{

    [Tooltip("The prefab to instantiate when spawning new cans")] [SerializeField]
    private GameObject CanPrefab;

    [Tooltip("The initial speed to launch the can at")] [SerializeField]
    private float LaunchSpeed;

    [Tooltip("Minimum spin speed of the can when launched")] [SerializeField]
    private float MinSpinSpeed;

    [Tooltip("Maximum spin speed of the can when launched")] [SerializeField]
    private float MaxSpinSpeed;

    [Tooltip("Possible variation in launch angle as the component of total launch speed along the spawner's x-axis")]
    [SerializeField]
    private float HorizontalLaunchVariation;
    
    public void SpawnCan()
    {
        // Create a new can
        GameObject can = Instantiate(CanPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = can.GetComponent<Rigidbody2D>();
        CanManager.Instance.AddCan(can.GetComponent<CanHit>());

        // Launch it into frame
        float variation = Random.Range(-HorizontalLaunchVariation, HorizontalLaunchVariation);
        rb.AddForce(LaunchSpeed * rb.mass * (transform.up + variation * transform.right), ForceMode2D.Impulse);
        // Spin it
        float dir = Random.Range(0, 2) == 0 ? 1.0f : -1.0f;
        rb.AddTorque(dir * Random.Range(MinSpinSpeed, MaxSpinSpeed) * rb.inertia);
    }
}
