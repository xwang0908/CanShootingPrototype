using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanIndicator : MonoBehaviour
{

    private Fade _fade;
    private GameObject _target;

    void Start()
    {
        _fade = GetComponent<Fade>();
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.x = _target.transform.position.x;
        transform.position = pos;
    }

    public void SetTarget(GameObject t)
    {
        _target = t;
    }
    
    public void Disappear()
    {
        _fade.Play();
    }
}
