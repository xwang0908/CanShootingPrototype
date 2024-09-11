using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanManager : MonoBehaviour
{

    public static CanManager Instance;
    
    private List<CanHit> _activeCans;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
        _activeCans = new List<CanHit>();
    }

    public void AddCan(CanHit can)
    {
        _activeCans.Add(can);
    }

    public void RemoveCan(CanHit can)
    {
        if(_activeCans.Contains(can))
            _activeCans.Remove(can);
    }

    public int NumActiveCans()
    {
        return _activeCans.Count;
    }
}
