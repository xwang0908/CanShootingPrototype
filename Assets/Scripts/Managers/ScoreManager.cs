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

        TotalNumCansDisplay.text = Volley.CountCans() + "";
    }

    public void Increment()
    {
        _score++;
        ScoreDisplay.text = _score + "";

        Color c = Color.Lerp(NoScoreColor, MaxScoreColor, _score / (float)Volley.CountCans());
        ScoreDisplay.color = c;
        SlashDisplay.color = c;
        TotalNumCansDisplay.color = c;
    }
}
