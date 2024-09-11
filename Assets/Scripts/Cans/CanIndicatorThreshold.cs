using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanIndicatorThreshold : MonoBehaviour
{
    [Tooltip("The prefab to instantiate as the indicator of where off-screen cans are")] [SerializeField]
    private CanIndicator IndicatorPrefab;

    [Tooltip("The amount to offset the y location of the indicators from this object")] [SerializeField]
    private float IndicatorVerticalPositionOffset;
    
    private Dictionary<Collider2D, CanIndicator> _indicators;

    private void Start()
    {
        _indicators = new Dictionary<Collider2D, CanIndicator>();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        CanIndicator ind;
        if (other.transform.position.y > transform.position.y)
        {
            ind = Instantiate(IndicatorPrefab);
            ind.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + IndicatorVerticalPositionOffset);
            ind.SetTarget(other.gameObject);
            _indicators[other] = ind;
            return;
        }

        if (_indicators.TryGetValue(other, out ind))
        {
            ind.Disappear();
        }
    }
}
