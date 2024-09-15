using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanRestart : CanHitEffect
{
    public override void Hit(Vector2 hitPos)
    {
        GameManager.Instance.RestartGame();
    }
}
