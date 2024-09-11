using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CanHit ch = other.GetComponent<CanHit>();
        MenuCanDestroyed mcd = other.GetComponent<MenuCanDestroyed>();
        if (ch && !mcd && ch.enabled)
        {
            other.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
            other.isTrigger = false;
            ch.enabled = false;
        }
    }
}
