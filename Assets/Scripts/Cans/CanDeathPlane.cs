using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<CanHit>())
            Destroy(other.gameObject);
    }
}
