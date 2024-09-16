using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanHitUnityEvent : CanHitEffect
{
    [Tooltip("The event to invoke when this juice is activated")] [SerializeField]
    private UnityEvent EffectEvent;

    public override void Hit(Vector2 hitPos)
    {
        EffectEvent.Invoke();
    }
}
