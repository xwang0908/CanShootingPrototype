using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Tooltip("The UI element to display the score int")] [SerializeField]
    private TextMeshProUGUI ScoreDisplay;

    [Tooltip("UI element that displays the total number of cans to throw")] [SerializeField]
    private TextMeshProUGUI TotalNumCansDisplay;

    [Tooltip("UI element that displays the slash between the score and the number of cans thrown")] [SerializeField]
    private TextMeshProUGUI SlashDisplay;
    
    [Tooltip("The volley manager, which determines the total number of cans thrown")] [SerializeField]
    private VolleyManager Volley;

    [Tooltip("The color that the text will be when the player hasn't scored at all")] [SerializeField]
    private Color NoScoreColor;

    [Tooltip("The color that the text will be when the player has gotten every can")] [SerializeField]
    private Color MaxScoreColor;
    
    private int _score;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);

        TotalNumCansDisplay.text = Volley.NumScheduledCans() + "";
    }

    private void OnDestroy()
    {
        if(Instance == this)
            Instance = null;
    }

    private void Update()
    {
        UpdateText();
    }

    public int GetScore()
    {
        return _score;
    }

    public Color GetScoreColor()
    {
        return Color.Lerp(NoScoreColor, MaxScoreColor, _score / (float)Volley.NumScheduledCans());
    }

    public void Increment()
    {
        _score++;
        ScoreDisplay.text = _score + "";
        TotalNumCansDisplay.text = Volley.NumScheduledCans() + "";

        Color c = Color.Lerp(NoScoreColor, MaxScoreColor, _score / (float)Volley.NumScheduledCans());
        ScoreDisplay.color = c;
        SlashDisplay.color = c;
        TotalNumCansDisplay.color = c;
    }

    private void UpdateText()
    {
        ScoreDisplay.text = _score + "";
        TotalNumCansDisplay.text = Volley.NumScheduledCans() + "";

        Color c = Color.Lerp(NoScoreColor, MaxScoreColor, _score / (float)Volley.NumScheduledCans());
        ScoreDisplay.color = c;
        SlashDisplay.color = c;
        TotalNumCansDisplay.color = c;
    }
}
