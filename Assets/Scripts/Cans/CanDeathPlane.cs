using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CanHit ch = other.GetComponent<CanHit>();
        if (ch && ch.enabled)
        {
            // Make the collider actually fit the can
            other.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
            other.isTrigger = false;
            CanManager.Instance.RemoveCan(ch);
        }
    }
}
