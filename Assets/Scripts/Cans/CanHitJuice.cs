using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanHitJuice : CanHitEffect
{
    [Tooltip("The game object containing all the juice effects")] [SerializeField]
    private GameObject JuiceContainer;
    
    // The list of juice effects that will be triggered when hit
    private JuiceEffect[] _juice;

    void Start()
    {
        _juice = JuiceContainer.GetComponents<JuiceEffect>();
    }
    
    public override void Hit(Vector2 hitPos)
    {
        foreach (JuiceEffect effect in _juice)
            effect.Play();
    }
    
}
