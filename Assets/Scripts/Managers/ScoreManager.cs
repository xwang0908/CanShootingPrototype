using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Tooltip("The UI element to display the score int")] [SerializeField]
    private TextMeshProUGUI ScoreDisplay;
    
    private int _score;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Increment()
    {
        _score++;
        ScoreDisplay.text = _score + "";
    }
}
