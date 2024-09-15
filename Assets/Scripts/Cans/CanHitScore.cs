using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHitScore : CanHitEffect
{
    public override void Hit(Vector2 hitPos)
    {
        ScoreManager.Instance.Increment();
    }
}
