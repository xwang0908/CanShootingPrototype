using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Tooltip("Where to display the final score information")] [SerializeField]
    private TextMeshProUGUI FinalScoreDisplay;

    [Tooltip("The can button that restarts the game")] [SerializeField]
    private CanHit RestartGameCan;
    
    [Tooltip("The can button that returns to the main menu")] [SerializeField]
    private CanHit MenuGameCan;

    [Tooltip("The amount of time to wait before activating the restart can")] [SerializeField]
    private float RestartCanActivationDelay;
    
    [SerializeField] private VolleyManager Volley;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        int score = ScoreManager.Instance.GetScore();
        FinalScoreDisplay.text =
            score + " destroyed\n" + (Volley.NumThrownCans() - score) + " remain";
        FinalScoreDisplay.color = ScoreManager.Instance.GetScoreColor();
        
        Camera.main.GetComponent<MovePosition>().Play();

        yield return new WaitForSecondsRealtime(RestartCanActivationDelay);
        RestartGameCan.gameObject.SetActive(true);
        MenuGameCan.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    private IEnumerator RestartGameCoroutine()
    {
        GameObject[] all = GameObject.FindObjectsOfType<GameObject>();
        // Destroy old paint splatters
        foreach (GameObject go in all)
        {
            if (go.layer == 3)
                Destroy(go);
        }

        MovePosition move = Camera.main.GetComponent<MovePosition>(); 
        move.PlayReversed();
        yield return new WaitForSecondsRealtime(move.GetDelay() + move.GetDuration());
        SceneManager.LoadScene("GAME");
    }
}
