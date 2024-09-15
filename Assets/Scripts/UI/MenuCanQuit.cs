using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanQuit : CanHitEffect
{
    public override void Hit(Vector2 hitPos)
    {
        Application.Quit();
    }
}
