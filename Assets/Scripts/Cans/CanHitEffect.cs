using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHitEffect : MonoBehaviour
{
    public enum HitType
    {
        Any,
        Destroy,
        NonDestroy
    }

    [Tooltip("What types of hits should we play this effect on")] [SerializeField]
    public HitType WhenToPlay;

    public virtual void Hit(Vector2 hitPos) { }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
