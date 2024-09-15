using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class MenuCanText : CanHitEffect
{
    [Tooltip("For making the text appear awesome")] [SerializeField]
    private TextMeshProUGUI[] TextToShow;

    private int _textIndex;

    public override void Hit(Vector2 hitPos)
    {
        if (_textIndex < TextToShow.Length)
        {
            TextToShow[_textIndex].enabled = true;
            _textIndex++;
        }
    }
}
